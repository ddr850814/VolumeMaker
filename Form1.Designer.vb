Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Metro

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits Office2007Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.StyleManager1 = New DevComponents.DotNetBar.StyleManager(Me.components)
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.TextBoxX1 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.ButtonX2 = New DevComponents.DotNetBar.ButtonX()
        Me.DoubleInput1 = New DevComponents.Editors.DoubleInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.Line1 = New DevComponents.DotNetBar.Controls.Line()
        Me.DoubleInput2 = New DevComponents.Editors.DoubleInput()
        Me.TextBoxX2 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.DoubleInput3 = New DevComponents.Editors.DoubleInput()
        Me.DoubleInput4 = New DevComponents.Editors.DoubleInput()
        Me.ButtonX3 = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.ButtonX4 = New DevComponents.DotNetBar.ButtonX()
        Me.DoubleInput5 = New DevComponents.Editors.DoubleInput()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        CType(Me.DoubleInput1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DoubleInput2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DoubleInput3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DoubleInput4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DoubleInput5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StyleManager1
        '
        Me.StyleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue
        Me.StyleManager1.MetroColorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(CType(CType(183, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(42, Byte), Integer)))
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX1.Location = New System.Drawing.Point(7, 53)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(76, 41)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.TabIndex = 0
        Me.ButtonX1.Text = "扇形平台"
        '
        'TextBoxX1
        '
        '
        '
        '
        Me.TextBoxX1.Border.Class = "TextBoxBorder"
        Me.TextBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TextBoxX1.Location = New System.Drawing.Point(7, 26)
        Me.TextBoxX1.Name = "TextBoxX1"
        Me.TextBoxX1.PreventEnterBeep = True
        Me.TextBoxX1.Size = New System.Drawing.Size(169, 21)
        Me.TextBoxX1.TabIndex = 1
        '
        'ButtonX2
        '
        Me.ButtonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX2.Location = New System.Drawing.Point(100, 53)
        Me.ButtonX2.Name = "ButtonX2"
        Me.ButtonX2.Size = New System.Drawing.Size(76, 41)
        Me.ButtonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX2.TabIndex = 0
        Me.ButtonX2.Text = "其他平台"
        '
        'DoubleInput1
        '
        '
        '
        '
        Me.DoubleInput1.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.DoubleInput1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.DoubleInput1.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.DoubleInput1.Increment = 0.1R
        Me.DoubleInput1.Location = New System.Drawing.Point(199, 71)
        Me.DoubleInput1.MaxValue = 2.0R
        Me.DoubleInput1.MinValue = 1.0R
        Me.DoubleInput1.Name = "DoubleInput1"
        Me.DoubleInput1.ShowUpDown = True
        Me.DoubleInput1.Size = New System.Drawing.Size(52, 21)
        Me.DoubleInput1.TabIndex = 2
        Me.DoubleInput1.Value = 1.1R
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(13, 7)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(75, 18)
        Me.LabelX1.TabIndex = 3
        Me.LabelX1.Text = "文件夹名字"
        '
        'LabelX2
        '
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(200, 52)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(51, 18)
        Me.LabelX2.TabIndex = 3
        Me.LabelX2.Text = "留白%"
        '
        'Line1
        '
        Me.Line1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Line1.Location = New System.Drawing.Point(-5, 98)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(265, 10)
        Me.Line1.TabIndex = 4
        Me.Line1.Text = "Line1"
        Me.Line1.Thickness = 2
        '
        'DoubleInput2
        '
        '
        '
        '
        Me.DoubleInput2.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.DoubleInput2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.DoubleInput2.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.DoubleInput2.Increment = 0.1R
        Me.DoubleInput2.Location = New System.Drawing.Point(200, 175)
        Me.DoubleInput2.MinValue = 0R
        Me.DoubleInput2.Name = "DoubleInput2"
        Me.DoubleInput2.ShowUpDown = True
        Me.DoubleInput2.Size = New System.Drawing.Size(52, 21)
        Me.DoubleInput2.TabIndex = 5
        Me.DoubleInput2.Value = 1.0R
        '
        'TextBoxX2
        '
        '
        '
        '
        Me.TextBoxX2.Border.Class = "TextBoxBorder"
        Me.TextBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TextBoxX2.Location = New System.Drawing.Point(13, 128)
        Me.TextBoxX2.Name = "TextBoxX2"
        Me.TextBoxX2.PreventEnterBeep = True
        Me.TextBoxX2.Size = New System.Drawing.Size(122, 21)
        Me.TextBoxX2.TabIndex = 6
        '
        'DoubleInput3
        '
        '
        '
        '
        Me.DoubleInput3.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.DoubleInput3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.DoubleInput3.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.DoubleInput3.Increment = 0.1R
        Me.DoubleInput3.Location = New System.Drawing.Point(142, 128)
        Me.DoubleInput3.MinValue = 0R
        Me.DoubleInput3.Name = "DoubleInput3"
        Me.DoubleInput3.ShowUpDown = True
        Me.DoubleInput3.Size = New System.Drawing.Size(52, 21)
        Me.DoubleInput3.TabIndex = 7
        Me.DoubleInput3.Value = 10.0R
        '
        'DoubleInput4
        '
        '
        '
        '
        Me.DoubleInput4.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.DoubleInput4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.DoubleInput4.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.DoubleInput4.Increment = 0.1R
        Me.DoubleInput4.Location = New System.Drawing.Point(200, 128)
        Me.DoubleInput4.MinValue = 0R
        Me.DoubleInput4.Name = "DoubleInput4"
        Me.DoubleInput4.ShowUpDown = True
        Me.DoubleInput4.Size = New System.Drawing.Size(52, 21)
        Me.DoubleInput4.TabIndex = 8
        Me.DoubleInput4.Value = 0.5R
        '
        'ButtonX3
        '
        Me.ButtonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX3.Location = New System.Drawing.Point(7, 155)
        Me.ButtonX3.Name = "ButtonX3"
        Me.ButtonX3.Size = New System.Drawing.Size(76, 41)
        Me.ButtonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX3.TabIndex = 9
        Me.ButtonX3.Text = "结构平台"
        '
        'LabelX3
        '
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(200, 157)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(51, 18)
        Me.LabelX3.TabIndex = 10
        Me.LabelX3.Text = "外扩M"
        '
        'LabelX4
        '
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(19, 110)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(75, 18)
        Me.LabelX4.TabIndex = 11
        Me.LabelX4.Text = "文件夹名字"
        '
        'LabelX5
        '
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(200, 109)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(52, 18)
        Me.LabelX5.TabIndex = 10
        Me.LabelX5.Text = "平台下M"
        '
        'LabelX6
        '
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(144, 109)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(51, 18)
        Me.LabelX6.TabIndex = 10
        Me.LabelX6.Text = "平台上M"
        '
        'ButtonX4
        '
        Me.ButtonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX4.Location = New System.Drawing.Point(99, 155)
        Me.ButtonX4.Name = "ButtonX4"
        Me.ButtonX4.Size = New System.Drawing.Size(76, 41)
        Me.ButtonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX4.TabIndex = 12
        Me.ButtonX4.Text = "切"
        '
        'DoubleInput5
        '
        '
        '
        '
        Me.DoubleInput5.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.DoubleInput5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.DoubleInput5.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.DoubleInput5.Increment = 0.1R
        Me.DoubleInput5.Location = New System.Drawing.Point(198, 25)
        Me.DoubleInput5.MinValue = 0R
        Me.DoubleInput5.Name = "DoubleInput5"
        Me.DoubleInput5.ShowUpDown = True
        Me.DoubleInput5.Size = New System.Drawing.Size(52, 21)
        Me.DoubleInput5.TabIndex = 8
        Me.DoubleInput5.Value = 0.1R
        '
        'LabelX7
        '
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Location = New System.Drawing.Point(198, 6)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(52, 18)
        Me.LabelX7.TabIndex = 10
        Me.LabelX7.Text = "平台上M"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(255, 210)
        Me.Controls.Add(Me.ButtonX4)
        Me.Controls.Add(Me.LabelX6)
        Me.Controls.Add(Me.LabelX7)
        Me.Controls.Add(Me.LabelX5)
        Me.Controls.Add(Me.LabelX3)
        Me.Controls.Add(Me.LabelX4)
        Me.Controls.Add(Me.ButtonX3)
        Me.Controls.Add(Me.DoubleInput5)
        Me.Controls.Add(Me.DoubleInput4)
        Me.Controls.Add(Me.DoubleInput3)
        Me.Controls.Add(Me.TextBoxX2)
        Me.Controls.Add(Me.DoubleInput2)
        Me.Controls.Add(Me.Line1)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.DoubleInput1)
        Me.Controls.Add(Me.TextBoxX1)
        Me.Controls.Add(Me.ButtonX2)
        Me.Controls.Add(Me.ButtonX1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Volume"
        Me.TopMost = True
        CType(Me.DoubleInput1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DoubleInput2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DoubleInput3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DoubleInput4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DoubleInput5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StyleManager1 As DevComponents.DotNetBar.StyleManager
    Friend WithEvents ButtonX1 As ButtonX
    Friend WithEvents TextBoxX1 As Controls.TextBoxX
    Friend WithEvents ButtonX2 As ButtonX
    Friend WithEvents DoubleInput1 As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX1 As LabelX
    Friend WithEvents LabelX2 As LabelX
    Friend WithEvents Line1 As Controls.Line
    Friend WithEvents DoubleInput2 As DevComponents.Editors.DoubleInput
    Friend WithEvents TextBoxX2 As Controls.TextBoxX
    Friend WithEvents DoubleInput3 As DevComponents.Editors.DoubleInput
    Friend WithEvents DoubleInput4 As DevComponents.Editors.DoubleInput
    Friend WithEvents ButtonX3 As ButtonX
    Friend WithEvents LabelX3 As LabelX
    Friend WithEvents LabelX4 As LabelX
    Friend WithEvents LabelX5 As LabelX
    Friend WithEvents LabelX6 As LabelX
    Friend WithEvents ButtonX4 As ButtonX
    Friend WithEvents DoubleInput5 As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX7 As LabelX
End Class
