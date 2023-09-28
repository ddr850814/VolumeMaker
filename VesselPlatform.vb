Imports Ingr.SP3D.Common.Client


Public Class VesselPlatform
    Inherits BaseModalCommand
    Private WithEvents m_frm1 As Form1
    Private m_bExiting As Boolean

    Public Overrides ReadOnly Property EnableUIFlags() As Integer
        'Our command must be enabled only when there is an ActiveView
        Get
            Return EnableUIFlagSettings.ActiveView
        End Get
    End Property

    Public Overrides Sub OnStart(ByVal commandID As Integer, ByVal argument As Object)
        Dim Form1 As New Form1
        Form1.Show()
        m_bExiting = False
    End Sub
    Public Overrides Sub OnStop()
        Dim Form1 As New Form1
        If m_bExiting = False Then
            m_bExiting = True
            Form1.Close()
        End If
        m_frm1 = Nothing
    End Sub
    Private Sub m_frm1_Closed(sender As Object, e As EventArgs) Handles m_frm1.Closed
        If m_bExiting = False Then
            m_bExiting = True
            StopCommand()
        End If
    End Sub
End Class
