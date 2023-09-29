Imports System.Collections.ObjectModel
Imports DevComponents.DotNetBar
Imports Ingr.SP3D.Common.Client.Services
Imports Ingr.SP3D.Common.Middle
Imports Ingr.SP3D.Common.Middle.Services
Imports Ingr.SP3D.Equipment.Middle
Imports Ingr.SP3D.Grids.Middle
Imports Ingr.SP3D.ReferenceData.Middle.Services
Imports Ingr.SP3D.Space.Middle

Public Class Form1
    Private oSelect As SelectSet = ClientServiceProvider.SelectSet
    Private m_oEqp As Equipment ' The Selected Equipment we are dealing with
    Private m_oRoot As HierarchiesRoot
    Private m_UOM As UOMManager

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxX1.Text = My.Settings.Folder1
        TextBoxX2.Text = My.Settings.Folder2
    End Sub

    'This function creates Space for a given Object given its IRange Interface
    Private Function CreateSpace(ByVal objRange As IRange, ByVal oParent As FolderSpace, ByVal strSpaceType As String, ByVal oCType As SpaceConstructionType) As SpaceBase

        'Create the Space Range as 10% extra on each edge
        ' Arrive at diagonal ends of such expanded range
        Dim oPtLow, oPtHigh, oPtCtr As Position, oVec As Vector
        oPtLow = New Position(objRange.Range.Low)
        oPtHigh = New Position(objRange.Range.High)
        oPtCtr = New Position(0.5 * (oPtLow.X + oPtHigh.X),
                         0.5 * (oPtLow.Y + oPtHigh.Y),
                          0.5 * (oPtLow.Z + oPtHigh.Z))

        oVec = oPtCtr.Subtract(oPtLow) ' Get vector from center to Low Point
        oVec.Length = 1.1 * oVec.Length ' elongate the vector by 10% in magnitude
        oPtLow = oPtCtr.Offset(oVec) ' arrive at the expanded range end point
        oVec = oPtCtr.Subtract(oPtHigh) ' Get vector from center to High Point
        oVec.Length = 1.1 * oVec.Length ' elongate the vector by 10% in magnitude
        oPtHigh = oPtCtr.Offset(oVec) ' arrive at the expanded range end point

        ' Create Space by 2 points
        Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase
        Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)

        ' Prepare SpaceInputs for creation
        oSpaceInputs(0) = New SpaceInputs
        oSpaceInputs(1) = New SpaceInputs
        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
        oInputsColl.Add(oSpaceInputs(0))
        oInputsColl.Add(oSpaceInputs(1))

        ' Create appropriate Space object : Area / Interference / Zone
        Select Case strSpaceType
            Case "AREATYPE"
                oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                            oCType, oParent, oInputsColl)

            Case "INTERFERENCETYPE"
                oSpaceBase = New InterferenceSpace(oCatBaseHelper.GetPart("SPACE_DEF_IV01"),
                            oCType, oParent, oInputsColl)

            Case "ZONETYPE"
                oSpaceBase = New ZoneSpace(oCatBaseHelper.GetPart("SPACE_DEF_HZ01"),
                            oCType, oParent, oInputsColl)

            Case Else
                Return Nothing ' Error
        End Select

        ' associate the object with this space
        oSpaceBase.AssociatedObject = objRange
        oSpaceBase.SetUserDefinedName(oCType.ToString & "-" & strSpaceType)
        ClientServiceProvider.TransactionMgr.Compute()
        Return oSpaceBase

    End Function

    'This function creates a Space Folder with given name
    Private Function CreateSpaceFolder(ByVal strName As String) As FolderSpace
        Dim oSpaceRoot As BusinessObject, oSpaceFolder As FolderSpace
        ' Get Space Root of ActivePlant
        oSpaceRoot = (MiddleServiceProvider.SiteMgr.ActiveSite.ActivePlant.PlantModel.RootSystem)
        ' Create a Space Folder with given name
        oSpaceFolder = New FolderSpace(oSpaceRoot)
        oSpaceFolder.SetUserDefinedName(strName)
        Return oSpaceFolder
    End Function

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Dim oFilter As New Filter(), i As Integer, oSelObjs As New Collection(Of BusinessObject)
        For i = 0 To ClientServiceProvider.SelectSet.SelectedObjects.Count - 1
            oSelObjs.Add(ClientServiceProvider.SelectSet.SelectedObjects.Item(i))
        Next
        If oSelObjs.Count = 0 Then
            Dim answer As MsgBoxResult
            answer = MsgBox（"没有选择范围，范围为整个项目", 4）
            If answer = vbNo Then
                Exit Sub
            End If
        End If
        Dim oROBOColl = New ReadOnlyCollection(Of BusinessObject)(oSelObjs)
        oFilter.Definition.AddHierarchy(HierarchyTypes.System, oROBOColl, True)
        oFilter.Definition.AddObjectType("Equipment&Furnishing\EquipmentTypes\SmartEquipment")
        Dim oFeats = oFilter.Apply()
        For Each m_oEqp As BusinessObject In oFeats
            m_UOM = MiddleServiceProvider.UOMMgr
            Dim oEqp As Equipment
            oEqp = CType(m_oEqp, Equipment)
            Dim zLow As Vector = New Vector(0, 0, 1)
            Dim xLow As Vector = New Vector(1, 0, 0)
            Dim yLow As Vector = New Vector(0, 1, 0)
            Dim zHigh As Vector = New Vector(0, 0, 1)
            Dim xHigh As Vector = New Vector(1, 0, 0)
            Dim yHigh As Vector = New Vector(0, 1, 0)
            Dim EqpComponent As ReadOnlyCollection(Of BusinessObject)
            Dim oPtLow As Position = Nothing
            Dim oPtHigh As Position = Nothing
            Dim Plantformheight As String
            Dim Plantformwidth As String
            Dim ClearancefromVessel As String
            Dim VesselDiameter As String = Nothing
            Dim NoofSections As String
            Dim Plantformwidth1 As String
            Dim Plantformwidth2 As String
            Dim Plantformheight1 As String
            Dim Plantformheight2 As String
            Dim d1 As Double
            Dim d2 As Double
            Dim d3 As Double = 0
            Dim d4 As Double
            Dim F As FolderSpace = Nothing
            Dim EE As FolderSpace = Nothing
            EqpComponent = oEqp.GetRelationship("HasEqpComponents", "EqpComponent").TargetObjects
            For Each Platform As EquipmentComponent In EqpComponent
                If Platform.SupportsInterface("IJUACircularPlatform") Then
                    Plantformheight = Platform.GetPropertyValue("IJUACircularPlatform", "PlatformHeight").ToString
                    Plantformwidth = Platform.GetPropertyValue("IJUACircularPlatform", "PlatformWidth").ToString
                    ClearancefromVessel = Platform.GetPropertyValue("IJUACircularPlatformDim", "ClearancefromVessel").ToString
                    VesselDiameter = Platform.GetPropertyValue("IJUAVesselDiameter", "VesselDiameter").ToString
                    d1 = m_UOM.ParseUnit(UnitType.Distance, Plantformheight)
                    d2 = m_UOM.ParseUnit(UnitType.Distance, Plantformwidth)
                    d3 = m_UOM.ParseUnit(UnitType.Distance, ClearancefromVessel)
                    d4 = m_UOM.ParseUnit(UnitType.Distance, VesselDiameter)
                    VesselDiameter = Platform.GetPropertyValue("IJUAVesselDiameter", "VesselDiameter").ToString
                    d4 = m_UOM.ParseUnit(UnitType.Distance, VesselDiameter)
                    zLow.Length = d1
                    zHigh.Length = DoubleInput5.Value
                    xLow.Length = (d2 + d3 + d4 / 2) * DoubleInput1.Value
                    xHigh.Length = (d2 + d3 + d4 / 2) * DoubleInput1.Value
                    yLow.Length = (d2 + d3 + d4 / 2) * DoubleInput1.Value
                    yHigh.Length = (d2 + d3 + d4 / 2) * DoubleInput1.Value
                    oPtLow = New Position(Platform.Matrix.Origin.X - xLow.X - yLow.X - zLow.X, Platform.Matrix.Origin.Y - xLow.Y - yLow.Y - zLow.Y, Platform.Matrix.Origin.Z - xLow.Z - yLow.Z - zLow.Z)
                    oPtHigh = New Position(Platform.Matrix.Origin.X + xHigh.X + yHigh.X + zHigh.X, Platform.Matrix.Origin.Y + xHigh.Y + yHigh.Y + zHigh.Y, Platform.Matrix.Origin.Z + xHigh.Z + yHigh.Z + zHigh.Z)

                ElseIf Platform.SupportsInterface("IJUAVesselPlatform") Then
                    NoofSections = Platform.GetPropertyValue("IJUAVesselPlatform", "NoofSections").ToString
                    Plantformheight1 = Platform.GetPropertyValue("IJUAVesselPlatform", "PlatformHeight1").ToString
                    Plantformwidth1 = Platform.GetPropertyValue("IJUAVesselPlatform", "PlatformWidth1").ToString
                    Plantformheight2 = Platform.GetPropertyValue("IJUAVesselPlatform", "PlatformHeight2").ToString
                    Plantformwidth2 = Platform.GetPropertyValue("IJUAVesselPlatform", "PlatformWidth2").ToString
                    If NoofSections = "1" Then
                        d1 = m_UOM.ParseUnit(UnitType.Distance, Plantformheight1)
                        d2 = m_UOM.ParseUnit(UnitType.Distance, Plantformwidth1)
                    ElseIf NoofSections = "2" Then
                        d1 = Math.Max(m_UOM.ParseUnit(UnitType.Distance, Plantformheight1), m_UOM.ParseUnit(UnitType.Distance, Plantformheight2))
                        d2 = Math.Max(m_UOM.ParseUnit(UnitType.Distance, Plantformwidth1), m_UOM.ParseUnit(UnitType.Distance, Plantformwidth2))
                    End If
                    VesselDiameter = Platform.GetPropertyValue("IJUAVesselDiameter", "VesselDiameter").ToString
                    d4 = m_UOM.ParseUnit(UnitType.Distance, VesselDiameter)
                    zLow.Length = d1
                    zHigh.Length = DoubleInput5.Value
                    xLow.Length = (d2 + d3 + d4 / 2) * DoubleInput1.Value
                    xHigh.Length = (d2 + d3 + d4 / 2) * DoubleInput1.Value
                    yLow.Length = (d2 + d3 + d4 / 2) * DoubleInput1.Value
                    yHigh.Length = (d2 + d3 + d4 / 2) * DoubleInput1.Value
                    oPtLow = New Position(Platform.Matrix.Origin.X - xLow.X - yLow.X - zLow.X, Platform.Matrix.Origin.Y - xLow.Y - yLow.Y - zLow.Y, Platform.Matrix.Origin.Z - xLow.Z - yLow.Z - zLow.Z)
                    oPtHigh = New Position(Platform.Matrix.Origin.X + xHigh.X + yHigh.X + zHigh.X, Platform.Matrix.Origin.Y + xHigh.Y + yHigh.Y + zHigh.Y, Platform.Matrix.Origin.Z + xHigh.Z + yHigh.Z + zHigh.Z)
                Else
                    GoTo 30
                End If

                Dim oSpaceRoot As HierarchiesRoot, oSpaceFolder As FolderSpace
                oSpaceRoot = (MiddleServiceProvider.SiteMgr.ActiveSite.ActivePlant.PlantModel.RootSystem)
                'oSpaceFolder = New FolderSpace(oSpaceRoot)
                For Each oSpaceFolder In oSpaceRoot.SpaceChildren.OfType(Of FolderSpace)
                    If oSpaceFolder.Name = My.Settings.Folder1 Then
                        F = oSpaceFolder
                        GoTo 10
                    End If
                Next
                If F = Nothing Then
                    F = CreateSpaceFolder(My.Settings.Folder1)
                End If
10：             For Each oSpaceFolder In F.SpaceChildren.OfType(Of FolderSpace)
                    If oSpaceFolder.Name = oEqp.Name Then
                        EE = oSpaceFolder
                        GoTo 20
                    End If
                Next
                If EE = Nothing Then
                    EE = New FolderSpace(F)
                    EE.SetUserDefinedName(oEqp.Name)
                End If
20:                    ' Create Space by 2 points
                Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase
                Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)

                ' Prepare SpaceInputs for creation
                oSpaceInputs(0) = New SpaceInputs
                oSpaceInputs(1) = New SpaceInputs
                oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                oInputsColl.Add(oSpaceInputs(0))
                oInputsColl.Add(oSpaceInputs(1))

                oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)

                ' associate the object with this space
                oSpaceBase.AssociatedObject = Platform
                oSpaceBase.SetUserDefinedName(Format((Platform.Origin.Z * 1000), "0").ToString)
                Dim j As Integer = 0
                Dim arry() As Integer = New Integer() {}
                For Each oSpace As SpaceBase In EE.SpaceChildren
                    If oSpace.Name.Contains(oSpaceBase.Name) And oSpace.Name.Contains("*") Then
                        Dim input As String = oSpace.Name
                        Dim phrase As String = "*"
                        Dim Occurrences As Integer = (input.Length - input.Replace(phrase, String.Empty).Length) / phrase.Length
                        ReDim Preserve arry(j)
                        arry(j) = Occurrences
                        j += 1
                    ElseIf oSpace.Name = oSpaceBase.Name Then
                        ReDim Preserve arry(j)
                        arry(j) = 0
                        j += 1
                    End If
                Next
                If j > 1 Then
                    For k = 0 To arry.Max
                        oSpaceBase.SetUserDefinedName("*" & oSpaceBase.Name)
                    Next
                End If
                ClientServiceProvider.TransactionMgr.Commit("Place volume")
30:
            Next
        Next
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        Dim lowX() As Double = New Double() {}
        Dim lowY() As Double = New Double() {}
        Dim lowZ() As Double = New Double() {}
        Dim HighX() As Double = New Double() {}
        Dim HighY() As Double = New Double() {}
        Dim HighZ() As Double = New Double() {}
        Dim i As Integer = 0
        Dim oPtLow As Position = Nothing
        Dim oPtHigh As Position = Nothing
        Dim F As FolderSpace = Nothing
        Dim EE As FolderSpace = Nothing
        Dim oEqp As Equipment
        oEqp = CType(m_oEqp, Equipment)
        If oSelect.SelectedObjects.Count <> 0 Then
            For Each obj As BusinessObject In oSelect.SelectedObjects
                If obj.SupportsInterface("IJShape") = False Then
                    MsgBox("不是基本图形")
                    Exit Sub
                End If
            Next
        Else
            MsgBox("选择为空")
            Exit Sub
        End If

        Dim SystemChild As ISystemChild = oSelect.SelectedObjects.Item(0)
30:     Dim SystemParent As ISystem = SystemChild.SystemParent
        Try
            oEqp = SystemParent
        Catch ex As Exception
            SystemChild = SystemParent
            GoTo 30
        End Try

        For Each shape As GenericShape In oSelect.SelectedObjects
            ReDim Preserve lowX(i)
            ReDim Preserve lowY(i)
            ReDim Preserve lowZ(i)
            ReDim Preserve HighX(i)
            ReDim Preserve HighY(i)
            ReDim Preserve HighZ(i)

            lowX(i) = shape.Range.Low.X
            lowY(i) = shape.Range.Low.Y
            lowZ(i) = shape.Range.Low.Z
            HighX(i) = shape.Range.High.X
            HighY(i) = shape.Range.High.Y
            HighZ(i) = shape.Range.High.Z
            i += 1
        Next
        Dim midX As Double = (lowX.Min + HighX.Max) / 2
        Dim midY As Double = (lowY.Min + HighY.Max) / 2
        Dim vectorHigh As Vector = New Vector(HighX.Max - midX, HighY.Max - midY, 0)
        Dim vectorLow As Vector = New Vector(lowX.Min - midX, lowY.Min - midY, 0)
        vectorHigh.Length = vectorHigh.Length * DoubleInput1.Value
        vectorLow.Length = vectorLow.Length * DoubleInput1.Value
        Dim highpointX As Double = midX + vectorHigh.X
        Dim highpointY As Double = midY + vectorHigh.Y
        Dim lowpointX As Double = midX + vectorLow.X
        Dim lowpointY As Double = midY + vectorLow.Y
        oPtLow = New Position(lowpointX, lowpointY, lowZ.Min)
        oPtHigh = New Position(highpointX, highpointY, HighZ.Max + 0.1)
        Dim oSpaceRoot As HierarchiesRoot, oSpaceFolder As FolderSpace
        oSpaceRoot = (MiddleServiceProvider.SiteMgr.ActiveSite.ActivePlant.PlantModel.RootSystem)
        'oSpaceFolder = New FolderSpace(oSpaceRoot)
        For Each oSpaceFolder In oSpaceRoot.SpaceChildren.OfType(Of FolderSpace)
            If oSpaceFolder.Name = My.Settings.Folder1 Then
                F = oSpaceFolder
                GoTo 10
            End If
        Next
        If F = Nothing Then
            F = CreateSpaceFolder(My.Settings.Folder1)
        End If
10：     For Each oSpaceFolder In F.SpaceChildren.OfType(Of FolderSpace)
            If oSpaceFolder.Name = oEqp.Name Then
                EE = oSpaceFolder
                GoTo 20
            End If
        Next
        If EE = Nothing Then
            EE = New FolderSpace(F)
            EE.SetUserDefinedName(oEqp.Name)
        End If
20:                    ' Create Space by 2 points
        Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase
        Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)

        ' Prepare SpaceInputs for creation
        oSpaceInputs(0) = New SpaceInputs
        oSpaceInputs(1) = New SpaceInputs
        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
        oInputsColl.Add(oSpaceInputs(0))
        oInputsColl.Add(oSpaceInputs(1))

        oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)

        ' associate the object with this space
        oSpaceBase.SetUserDefinedName(Format((HighZ.Max * 1000), "0").ToString)
        Dim j As Integer = 0
        Dim arry() As Integer = New Integer() {}
        For Each oSpace As SpaceBase In EE.SpaceChildren
            If oSpace.Name.Contains(oSpaceBase.Name) And oSpace.Name.Contains("*") Then
                Dim input As String = oSpace.Name
                Dim phrase As String = "*"
                Dim Occurrences As Integer = (input.Length - input.Replace(phrase, String.Empty).Length) / phrase.Length
                ReDim Preserve arry(j)
                arry(j) = Occurrences
                j += 1
            ElseIf oSpace.Name = oSpaceBase.Name Then
                ReDim Preserve arry(j)
                arry(j) = 0
                j += 1
            End If
        Next
        If j > 1 Then
            For k = 0 To arry.Max
                oSpaceBase.SetUserDefinedName("*" & oSpaceBase.Name)
            Next
        End If
        ClientServiceProvider.TransactionMgr.Commit("Place volume")
    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs) Handles ButtonX3.Click
        Dim oFilter As New Filter(), i As Integer, oSelObjs As New Collection(Of BusinessObject)
        For i = 0 To ClientServiceProvider.SelectSet.SelectedObjects.Count - 1
            oSelObjs.Add(ClientServiceProvider.SelectSet.SelectedObjects.Item(i))
        Next
        If oSelObjs.Count = 0 Then
            Dim answer As MsgBoxResult
            answer = MsgBox（"没有选择范围，范围为整个项目", 4）
            If answer = vbNo Then
                Exit Sub
            End If
        End If
        Dim oROBOColl = New ReadOnlyCollection(Of BusinessObject)(oSelObjs)
        oFilter.Definition.AddHierarchy(HierarchyTypes.System, oROBOColl, True)
        oFilter.Definition.AddObjectType("CommonObjects\CoordinateSystems")
        Dim oFeats = oFilter.Apply()
        For Each m_oCS As BusinessObject In oFeats
            m_UOM = MiddleServiceProvider.UOMMgr
            Dim oCS As CoordinateSystem
            oCS = CType(m_oCS, CoordinateSystem)
            Dim zLow As Vector = New Vector(0, 0, 1)
            Dim xLow As Vector = New Vector(1, 0, 0)
            Dim yLow As Vector = New Vector(0, 1, 0)
            Dim zHigh As Vector = New Vector(0, 0, 1)
            Dim xHigh As Vector = New Vector(1, 0, 0)
            Dim yHigh As Vector = New Vector(0, 1, 0)
            Dim oPtLow As Position = Nothing
            Dim oPtHigh As Position = Nothing
            Dim F As FolderSpace = Nothing
            Dim EE As FolderSpace = Nothing
            Dim X() As Double = New Double() {}
            Dim Y() As Double = New Double() {}
            Dim Z() As Double = New Double() {}
            Dim n As Integer = 0, k As Integer = 0, l As Integer = 0
            Dim ocsparent As ISystem = oCS
            If oCS.GetRelationship("XAxisCS", "XAxisCS_ORIG").TargetObjects.Count > 0 And oCS.GetRelationship("YAxisCS", "YAxisCS_ORIG").TargetObjects.Count > 0 And oCS.GetRelationship("ZAxisCS", "ZAxisCS_ORIG").TargetObjects.Count > 0 Then
                For Each XPlane As GridPlane In oCS.GetRelationship("XAxisCS", "XAxisCS_ORIG").TargetObjects
                    ReDim Preserve X(n)
                    X(n) = XPlane.Origin.X
                    n += 1
                Next
                For Each YPlane As GridPlane In oCS.GetRelationship("YAxisCS", "YAxisCS_ORIG").TargetObjects
                    ReDim Preserve Y(k)
                    Y(k) = YPlane.Origin.Y
                    k += 1
                Next
                For Each ZPlane As GridElevationPlane In oCS.GetRelationship("ZAxisCS", "ZAxisCS_ORIG").TargetObjects
                    ReDim Preserve Z(l)
                    Z(l) = ZPlane.Origin.Z
                    l += 1
                Next
                Dim xmax As Double = X.Max + DoubleInput2.Value
                Dim ymax As Double = Y.Max + DoubleInput2.Value
                Dim xmin As Double = X.Min - DoubleInput2.Value
                Dim ymin As Double = Y.Min - DoubleInput2.Value
                Dim zmax As Double = Z.Max + DoubleInput3.Value
                Dim zmin As Double = Z.Min - DoubleInput4.Value
                For m As Integer = 0 To l - 1
                    oPtLow = New Position(xmin, ymin, Z(m) - DoubleInput4.Value)
                    If m = l - 1 Then
                        oPtHigh = New Position(xmax, ymax, Z(m) + DoubleInput3.Value)
                    Else
                        If Z(m) + DoubleInput3.Value > Z(m + 1) - DoubleInput4.Value Then
                            oPtHigh = New Position(xmax, ymax, Z(m + 1) - DoubleInput4.Value)
                        Else
                            oPtHigh = New Position(xmax, ymax, Z(m) + DoubleInput3.Value)
                        End If
                    End If
                    Dim oSpaceRoot As HierarchiesRoot, oSpaceFolder As FolderSpace
                    oSpaceRoot = (MiddleServiceProvider.SiteMgr.ActiveSite.ActivePlant.PlantModel.RootSystem)
                    'oSpaceFolder = New FolderSpace(oSpaceRoot)
                    For Each oSpaceFolder In oSpaceRoot.SpaceChildren.OfType(Of FolderSpace)
                        If oSpaceFolder.Name = My.Settings.Folder2 Then
                            F = oSpaceFolder
                            GoTo 10
                        End If
                    Next
                    If F = Nothing Then
                        F = CreateSpaceFolder(My.Settings.Folder2)
                    End If
10：                 For Each oSpaceFolder In F.SpaceChildren.OfType(Of FolderSpace)
                        If oSpaceFolder.Name = oCS.Name Then
                            EE = oSpaceFolder
                            GoTo 20
                        End If
                    Next
                    If EE = Nothing Then
                        EE = New FolderSpace(F)
                        EE.SetUserDefinedName(oCS.Name)
                    End If

20:               ' Create Space by 2 points
                    Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase
                    Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)

                    ' Prepare SpaceInputs for creation
                    oSpaceInputs(0) = New SpaceInputs
                    oSpaceInputs(1) = New SpaceInputs
                    oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                    oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                    oInputsColl.Add(oSpaceInputs(0))
                    oInputsColl.Add(oSpaceInputs(1))

                    oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                    SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)

                    ' associate the object with this space
                    'oSpaceBase.AssociatedObject = Platform
                    oSpaceBase.SetUserDefinedName(Format((Z(m) * 1000), "0").ToString)
                    Dim j As Integer = 0
                    Dim arry() As Integer = New Integer() {}
                    For Each oSpace As SpaceBase In EE.SpaceChildren
                        If oSpace.Name.Contains(oSpaceBase.Name) And oSpace.Name.Contains("*") Then
                            Dim input As String = oSpace.Name
                            Dim phrase As String = "*"
                            Dim Occurrences As Integer = (input.Length - input.Replace(phrase, String.Empty).Length) / phrase.Length
                            ReDim Preserve arry(j)
                            arry(j) = Occurrences
                            j += 1
                        ElseIf oSpace.Name = oSpaceBase.Name Then
                            ReDim Preserve arry(j)
                            arry(j) = 0
                            j += 1
                        End If
                    Next
                    If j > 1 Then
                        For k = 0 To arry.Max
                            oSpaceBase.SetUserDefinedName("*" & oSpaceBase.Name)
                        Next
                    End If
                    ClientServiceProvider.TransactionMgr.Commit("Place volume")
                Next
            End If
        Next
    End Sub

    Private Sub TextBoxX1_TextChanged(sender As Object, e As EventArgs) Handles TextBoxX1.TextChanged
        My.Settings.Folder1 = TextBoxX1.Text
    End Sub

    Private Sub TextBoxX2_TextChanged(sender As Object, e As EventArgs) Handles TextBoxX2.TextChanged
        My.Settings.Folder2 = TextBoxX2.Text
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ClientServiceProvider.TransactionMgr.Abort()
    End Sub

    Private Sub ButtonX4_Click(sender As Object, e As EventArgs) Handles ButtonX4.Click
        Dim oPtLow As Position = Nothing
        Dim oPtHigh As Position = Nothing
        Dim oFilter As New Filter(), oFilter1 As New Filter(), i As Integer, oSelObjs As New Collection(Of BusinessObject)
        For i = 0 To ClientServiceProvider.SelectSet.SelectedObjects.Count - 1
            oSelObjs.Add(ClientServiceProvider.SelectSet.SelectedObjects.Item(i))
        Next
        Dim oROBOColl = New ReadOnlyCollection(Of BusinessObject)(oSelObjs)
        oFilter.Definition.AddHierarchy(HierarchyTypes.System, oROBOColl, True)
        oFilter.Definition.AddObjectType("GridSystems\GridLine")
        oFilter1.Definition.AddHierarchy(HierarchyTypes.System, oROBOColl, True)
        oFilter1.Definition.AddObjectType("SpaceItem\SpaceEntities\Areas")
        If oFilter.Apply().Count = 1 And oFilter1.Apply().Count > 0 And oFilter1.Apply().Count + 1 = oSelect.SelectedObjects.Count Then
            Dim gridline As GridLine = oFilter.Apply().Item(0)
            If gridline.GetPropertyValue("ISPGGridData", "Axis").ToString = "X" Then
                For Each oAreaSpace As AreaSpace In oFilter1.Apply
                    Dim EE As ISpaceParent = oAreaSpace.SpaceParent
                   If oGridLine.Origin.X > oAreaSpace.Range.Low.X And oGridLine.Origin.X < oAreaSpace.Range.High.X Then
                        oPtLow = oAreaSpace.Range.Low
                        oPtHigh = New Position(oGridLine.Origin.X, oAreaSpace.Range.High.Y, oAreaSpace.Range.High.Z)
                        ' Create Space by 2 points
                        Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase, oSpaceBase1 As SpaceBase
                        Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)
                        ' Prepare SpaceInputs for creation
                        oSpaceInputs(0) = New SpaceInputs
                        oSpaceInputs(1) = New SpaceInputs
                        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                        oInputsColl.Add(oSpaceInputs(0))
                        oInputsColl.Add(oSpaceInputs(1))
                        oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                            SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)
                        ' associate the object with this space
                        'oSpaceBase.AssociatedObject = Platform
                        oSpaceBase.SetUserDefinedName(oAreaSpace.Name)
                        oPtLow = New Position(oGridLine.Origin.X, oAreaSpace.Range.Low.Y, oAreaSpace.Range.Low.Z)
                        oPtHigh = oAreaSpace.Range.High
                        ' Prepare SpaceInputs for creation
                        oSpaceInputs(0) = New SpaceInputs
                        oSpaceInputs(1) = New SpaceInputs
                        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                        oInputsColl.Clear()
                        oInputsColl.Add(oSpaceInputs(0))
                        oInputsColl.Add(oSpaceInputs(1))
                        oSpaceBase1 = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                            SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)
                        ' associate the object with this space
                        'oSpaceBase.AssociatedObject = Platform
                        oSpaceBase1.SetUserDefinedName(oAreaSpace.Name)
                    ElseIf oGridLine.Origin.X < oAreaSpace.Range.Low.X Then
                        oPtLow = New Position(oGridLine.Origin.X, oAreaSpace.Range.Low.Y, oAreaSpace.Range.Low.Z)
                        oPtHigh = oAreaSpace.Range.High
                        ' Create Space by 2 points
                        Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase
                        Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)
                        ' Prepare SpaceInputs for creation
                        oSpaceInputs(0) = New SpaceInputs
                        oSpaceInputs(1) = New SpaceInputs
                        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                        oInputsColl.Add(oSpaceInputs(0))
                        oInputsColl.Add(oSpaceInputs(1))
                        oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                            SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)
                        ' associate the object with this space
                        'oSpaceBase.AssociatedObject = Platform
                        oSpaceBase.SetUserDefinedName(oAreaSpace.Name)
                    ElseIf oGridLine.Origin.X > oAreaSpace.Range.High.X Then
                        oPtLow = oAreaSpace.Range.Low
                        oPtHigh = New Position(oGridLine.Origin.X, oAreaSpace.Range.High.Y, oAreaSpace.Range.High.Z)
                        ' Create Space by 2 points
                        Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase
                        Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)
                        ' Prepare SpaceInputs for creation
                        oSpaceInputs(0) = New SpaceInputs
                        oSpaceInputs(1) = New SpaceInputs
                        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                        oInputsColl.Add(oSpaceInputs(0))
                        oInputsColl.Add(oSpaceInputs(1))
                        oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                            SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)
                        ' associate the object with this space
                        'oSpaceBase.AssociatedObject = Platform
                        oSpaceBase.SetUserDefinedName(oAreaSpace.Name)
                    End If
                    oAreaSpace.Delete()
                    ClientServiceProvider.TransactionMgr.Commit("Place volume")
                Next
            ElseIf gridline.GetPropertyValue("ISPGGridData", "Axis").ToString = "Y" Then
                For Each oAreaSpace As AreaSpace In oFilter1.Apply
                    Dim EE As ISpaceParent = oAreaSpace.SpaceParent
                    If oGridLine.Origin.Y > oAreaSpace.Range.Low.Y And oGridLine.Origin.Y < oAreaSpace.Range.High.Y Then
                        oPtLow = oAreaSpace.Range.Low
                        oPtHigh = New Position(oAreaSpace.Range.High.X, oGridLine.Origin.Y, oAreaSpace.Range.High.Z)
                        ' Create Space by 2 points
                        Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase, oSpaceBase1 As SpaceBase
                        Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)
                        ' Prepare SpaceInputs for creation
                        oSpaceInputs(0) = New SpaceInputs
                        oSpaceInputs(1) = New SpaceInputs
                        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                        oInputsColl.Add(oSpaceInputs(0))
                        oInputsColl.Add(oSpaceInputs(1))
                        oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                                SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)
                        ' associate the object with this space
                        'oSpaceBase.AssociatedObject = Platform
                        oSpaceBase.SetUserDefinedName(oAreaSpace.Name)
                        oPtLow = New Position(oAreaSpace.Range.Low.X, oGridLine.Origin.Y, oAreaSpace.Range.Low.Z)
                        oPtHigh = oAreaSpace.Range.High
                        ' Prepare SpaceInputs for creation
                        oSpaceInputs(0) = New SpaceInputs
                        oSpaceInputs(1) = New SpaceInputs
                        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                        oInputsColl.Clear()
                        oInputsColl.Add(oSpaceInputs(0))
                        oInputsColl.Add(oSpaceInputs(1))
                        oSpaceBase1 = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                                SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)
                        ' associate the object with this space
                        'oSpaceBase.AssociatedObject = Platform
                        oSpaceBase1.SetUserDefinedName(oAreaSpace.Name)
                    ElseIf oGridLine.Origin.Y < oAreaSpace.Range.Low.Y Then
                        oPtLow = New Position(oAreaSpace.Range.Low.X, oGridLine.Origin.Y, oAreaSpace.Range.Low.Z)
                        oPtHigh = oAreaSpace.Range.High
                        ' Create Space by 2 points
                        Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase
                        Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)
                        ' Prepare SpaceInputs for creation
                        oSpaceInputs(0) = New SpaceInputs
                        oSpaceInputs(1) = New SpaceInputs
                        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                        oInputsColl.Add(oSpaceInputs(0))
                        oInputsColl.Add(oSpaceInputs(1))
                        oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                            SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)
                        ' associate the object with this space
                        'oSpaceBase.AssociatedObject = Platform
                        oSpaceBase.SetUserDefinedName(oAreaSpace.Name)
                    ElseIf oGridLine.Origin.Y > oAreaSpace.Range.High.Y Then
                        oPtLow = oAreaSpace.Range.Low
                        oPtHigh = New Position(oAreaSpace.Range.High.X, oGridLine.Origin.Y, oAreaSpace.Range.High.Z)
                        ' Create Space by 2 points
                        Dim oCatBaseHelper As New CatalogBaseHelper, oSpaceBase As SpaceBase
                        Dim oSpaceInputs(1) As SpaceInputs, oInputsColl As New Collection(Of SpaceInputs)
                        ' Prepare SpaceInputs for creation
                        oSpaceInputs(0) = New SpaceInputs
                        oSpaceInputs(1) = New SpaceInputs
                        oSpaceInputs(0).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtLow)
                        oSpaceInputs(1).InputObject = New Point3d(ClientServiceProvider.WorkingSet.ActiveConnection, oPtHigh)
                        oInputsColl.Add(oSpaceInputs(0))
                        oInputsColl.Add(oSpaceInputs(1))
                        oSpaceBase = New AreaSpace(oCatBaseHelper.GetPart("SPACE_DEF_SA01"),
                            SpaceConstructionType.SpaceBy2Points, EE, oInputsColl)
                        ' associate the object with this space
                        'oSpaceBase.AssociatedObject = Platform
                        oSpaceBase.SetUserDefinedName(oAreaSpace.Name)
                    End If
                    oAreaSpace.Delete()
                    ClientServiceProvider.TransactionMgr.Commit("Place volume")
                Next
            End If
        End If
    End Sub
End Class
