
Imports System.IO
Public Class frmMain

    Private Const LOG_TAG As String = "LOG"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()

        LogBox("Click [Exit] or [X] Button")

        btnExit.Select()
    End Sub

    Private Sub frmMain_Close(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim result As MsgBoxResult = CloseApp()

        If result = MsgBoxResult.Cancel Then
            LogBox(String.Format("User Cancel >> {0}", sender.ToString))
            e.Cancel = True
        End If

        If result = MsgBoxResult.Ok Then
            ' Nothing...
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim result As MsgBoxResult = CloseApp()

        If result = MsgBoxResult.Cancel Then
            LogBox(String.Format("User Cancel >> {0}", sender.ToString))
            Exit Sub
        End If

        If result = MsgBoxResult.Ok Then
            Application.Exit()
        End If
    End Sub

    Private Sub LogBox(ByVal message As String)
        tbxLogBox.Text = tbxLogBox.Text + message + vbCrLf
    End Sub

    Private Sub LogBox(ByVal tag As String, ByVal message As String)
        Dim tmpMessage As String = String.Format("{0} : {1}", tag, message)
        tbxLogBox.Text = tbxLogBox.Text + tmpMessage + vbNewLine
    End Sub

    Private Function CloseApp() As MsgBoxResult
        Return MsgBox("Close this App?", vbExclamation + vbOKCancel)
    End Function

End Class
