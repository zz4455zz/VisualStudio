<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProgressBar
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
        Me.pgbBar = New System.Windows.Forms.ProgressBar()
        Me.lblTitile = New System.Windows.Forms.Label()
        Me.lblPercentage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pgbBar
        '
        Me.pgbBar.Location = New System.Drawing.Point(12, 28)
        Me.pgbBar.Name = "pgbBar"
        Me.pgbBar.Size = New System.Drawing.Size(454, 30)
        Me.pgbBar.TabIndex = 102
        '
        'lblTitile
        '
        Me.lblTitile.AutoSize = True
        Me.lblTitile.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblTitile.Location = New System.Drawing.Point(12, 9)
        Me.lblTitile.Name = "lblTitile"
        Me.lblTitile.Size = New System.Drawing.Size(89, 16)
        Me.lblTitile.TabIndex = 103
        Me.lblTitile.Text = "Please wait..."
        '
        'lblPercentage
        '
        Me.lblPercentage.AutoSize = True
        Me.lblPercentage.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblPercentage.Location = New System.Drawing.Point(421, 9)
        Me.lblPercentage.Name = "lblPercentage"
        Me.lblPercentage.Size = New System.Drawing.Size(45, 16)
        Me.lblPercentage.TabIndex = 104
        Me.lblPercentage.Text = "100%"
        '
        'frmProgressBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(479, 71)
        Me.Controls.Add(Me.lblPercentage)
        Me.Controls.Add(Me.lblTitile)
        Me.Controls.Add(Me.pgbBar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProgressBar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmProgressBar"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pgbBar As ProgressBar
    Friend WithEvents lblTitile As Label
    Friend WithEvents lblPercentage As Label
End Class
