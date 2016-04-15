
Imports System.IO
Public Class frmMain

    Dim timer As Timer

    Private Const WAIT_TIME As Integer = 2000

    Private Const LOG_TAG As String = "BAR"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()

        btnDemo1.Select()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub tbxLogBox_TextChanged(sender As Object, e As EventArgs) Handles tbxLogBox.TextChanged
        tbxLogBox.SelectionStart = tbxLogBox.Text.Length
        tbxLogBox.ScrollToCaret()
    End Sub

    Private Sub btnDemo1_Click(sender As Object, e As EventArgs) Handles btnDemo1.Click
        LogBox("==== Demo STR ====")

        ProgressBarDemo1()

        LogBox("==== Demo END ====")
    End Sub

    Private Sub btnDemo2_Click(sender As Object, e As EventArgs) Handles btnDemo2.Click
        LogBox("==== Demo2 STR ====")

        ProgressBarDemo2()

        LogBox("==== Demo2 END ====")
    End Sub

    Private Sub btnDemo3_Click(sender As Object, e As EventArgs) Handles btnDemo3.Click
        LogBox("==== Demo3 STR ====")

        ProgressBarDemo3()

        LogBox("==== Demo3 END ====")
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        tbxLogBox.Text = String.Empty
    End Sub

    Private Sub LogBox(ByVal message As String)
        tbxLogBox.Text = tbxLogBox.Text + message + vbCrLf
    End Sub

    Private Sub LogBox(ByVal tag As String, ByVal message As String)
        Dim tmpMessage As String = String.Format("{0} : {1}", tag, message)
        tbxLogBox.Text = tbxLogBox.Text + tmpMessage + vbNewLine
    End Sub

    Private Sub ProgressBarDemo1()

        ProgressBarInitial()

        While (True)
            pgbBar.PerformStep()

            LogBox(LOG_TAG, "Wait 2 sec...")
            Threading.Thread.Sleep(WAIT_TIME)

            If pgbBar.Value = pgbBar.Maximum Then
                Exit While
            End If
        End While

    End Sub

    Private Sub ProgressBarDemo2()

        ProgressBarInitial()

        timer = New Timer()
        timer.Interval = WAIT_TIME
        AddHandler timer.Tick, New EventHandler(AddressOf ProgressBarPerformStep)

        timer.Start()

    End Sub

    Private Sub ProgressBarDemo3()

        ProgressBarInitial2()

        Dim LoopThread = New System.Threading.Thread(AddressOf RunLoop)
        LoopThread.Start()

    End Sub

    Private Sub ProgressBarInitial()
        pgbBar.Maximum = 100
        pgbBar.Minimum = 0
        pgbBar.Value = 1
        pgbBar.Step = 20
    End Sub

    Private Sub ProgressBarInitial2()
        pgbBar.Maximum = 1000
        pgbBar.Minimum = 0
        pgbBar.Value = 1
        pgbBar.Step = 1
    End Sub

    Private Sub ProgressBarPerformStep()
        LogBox(LOG_TAG, String.Format("Wait 2 sec... {0,-3} / {1,3}", pgbBar.Value, pgbBar.Maximum))
        pgbBar.PerformStep()

        If pgbBar.Value = pgbBar.Maximum Then
            timer.Stop()
        End If
    End Sub

    Private Sub RunLoop()

        For idx = 1 To 1000
            LogBoxThread(LOG_TAG, String.Format("{0,-4} / {1,4}", pgbBar.Value, pgbBar.Maximum))
            PerformStepThread()
        Next

    End Sub

    Private Delegate Sub LogBoxThreadDelegate(ByVal tag As String, ByVal message As String)
    Private Sub LogBoxThread(ByVal tag As String, ByVal message As String)

        If Me.InvokeRequired() Then
            Dim delegateFunc As New LogBoxThreadDelegate(AddressOf LogBox)
            Me.Invoke(delegateFunc, tag, message)
        Else
            Dim tmpMessage As String = String.Format("{0} : {1}", tag, message)
            tbxLogBox.Text = tbxLogBox.Text + tmpMessage + vbNewLine
        End If
    End Sub

    Private Delegate Sub PerformStepThreadDelegate()
    Private Sub PerformStepThread()

        If Me.InvokeRequired() Then
            Dim delegateFunc As New PerformStepThreadDelegate(AddressOf PerformStepThread)
            Me.Invoke(delegateFunc)
        Else
            pgbBar.PerformStep()
        End If
    End Sub

End Class
