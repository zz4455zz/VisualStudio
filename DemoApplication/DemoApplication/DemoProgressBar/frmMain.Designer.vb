<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnDemo1 = New System.Windows.Forms.Button()
        Me.tbxLogBox = New System.Windows.Forms.TextBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.pgbBar = New System.Windows.Forms.ProgressBar()
        Me.btnDemo2 = New System.Windows.Forms.Button()
        Me.btnDemo3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.White
        Me.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExit.FlatAppearance.BorderSize = 2
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExit.Location = New System.Drawing.Point(472, 219)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 30)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnDemo1
        '
        Me.btnDemo1.BackColor = System.Drawing.Color.White
        Me.btnDemo1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDemo1.FlatAppearance.BorderSize = 2
        Me.btnDemo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDemo1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDemo1.Location = New System.Drawing.Point(472, 12)
        Me.btnDemo1.Name = "btnDemo1"
        Me.btnDemo1.Size = New System.Drawing.Size(100, 30)
        Me.btnDemo1.TabIndex = 2
        Me.btnDemo1.Text = "Demo"
        Me.btnDemo1.UseVisualStyleBackColor = False
        '
        'tbxLogBox
        '
        Me.tbxLogBox.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.tbxLogBox.Location = New System.Drawing.Point(12, 12)
        Me.tbxLogBox.Multiline = True
        Me.tbxLogBox.Name = "tbxLogBox"
        Me.tbxLogBox.ReadOnly = True
        Me.tbxLogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbxLogBox.Size = New System.Drawing.Size(454, 201)
        Me.tbxLogBox.TabIndex = 100
        Me.tbxLogBox.TabStop = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.FlatAppearance.BorderSize = 2
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(472, 183)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 30)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'pgbBar
        '
        Me.pgbBar.Location = New System.Drawing.Point(12, 219)
        Me.pgbBar.Name = "pgbBar"
        Me.pgbBar.Size = New System.Drawing.Size(454, 30)
        Me.pgbBar.TabIndex = 101
        '
        'btnDemo2
        '
        Me.btnDemo2.BackColor = System.Drawing.Color.White
        Me.btnDemo2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDemo2.FlatAppearance.BorderSize = 2
        Me.btnDemo2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDemo2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDemo2.Location = New System.Drawing.Point(472, 48)
        Me.btnDemo2.Name = "btnDemo2"
        Me.btnDemo2.Size = New System.Drawing.Size(100, 30)
        Me.btnDemo2.TabIndex = 102
        Me.btnDemo2.Text = "Demo"
        Me.btnDemo2.UseVisualStyleBackColor = False
        '
        'btnDemo3
        '
        Me.btnDemo3.BackColor = System.Drawing.Color.White
        Me.btnDemo3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDemo3.FlatAppearance.BorderSize = 2
        Me.btnDemo3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDemo3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDemo3.Location = New System.Drawing.Point(472, 84)
        Me.btnDemo3.Name = "btnDemo3"
        Me.btnDemo3.Size = New System.Drawing.Size(100, 30)
        Me.btnDemo3.TabIndex = 103
        Me.btnDemo3.Text = "Demo"
        Me.btnDemo3.UseVisualStyleBackColor = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 261)
        Me.Controls.Add(Me.btnDemo3)
        Me.Controls.Add(Me.btnDemo2)
        Me.Controls.Add(Me.pgbBar)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.tbxLogBox)
        Me.Controls.Add(Me.btnDemo1)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MainForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents btnDemo1 As Button
    Friend WithEvents tbxLogBox As TextBox
    Friend WithEvents btnClear As Button
    Friend WithEvents pgbBar As ProgressBar
    Friend WithEvents btnDemo2 As Button
    Friend WithEvents btnDemo3 As Button
End Class
