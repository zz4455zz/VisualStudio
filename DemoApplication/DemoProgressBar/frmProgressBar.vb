Public Class frmProgressBar

    Dim timer As Timer

    Public Sub New(ByVal barMax As Integer, ByVal barMin As Integer, ByVal barStep As Integer, ByVal title As String)

        InitializeComponent()

        ProgressBarInitial(barMax, barMin, barStep)
        Me.Text = title
    End Sub

    Public Sub ProgressBarInitial(ByVal barMax As Integer, ByVal barMin As Integer, ByVal barStep As Integer)
        pgbBar.Maximum = barMax
        pgbBar.Minimum = barMin
        pgbBar.Value = 0
        pgbBar.Step = barStep

        UpdateProgressLabel()
    End Sub

    Public Sub NextStep()
        pgbBar.PerformStep()

        UpdateProgressLabel()
    End Sub

    Private Sub UpdateProgressLabel()

        lblPercentage.Text = String.Format("{0:#}%", (((pgbBar.Value) / pgbBar.Maximum) * 100))
        Application.DoEvents()
        System.Threading.Thread.Sleep(1)

    End Sub

End Class