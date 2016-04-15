
Imports System.IO
Public Class frmMain

    Private Const LOG_TAG As String = "LOG"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()

        btnDemo.Select()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnDemo_Click(sender As Object, e As EventArgs) Handles btnDemo.Click
        LogBox("==== Demo STR ====")

        Log4NetDemo()

        LogBox("==== Demo END ====")
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

    Private Sub Log4NetDemo()
        LogHelper.ShareLogger.Info("Info")
        LogHelper.ShareLogger.Debug("Debug")
        LogHelper.ShareLogger.Error("Error")

        LogHelper.StopLogger()

        Log4NetShow()
    End Sub

    Private Sub Log4NetShow()
        LogBox(LOG_TAG, String.Format("FP : {0}", LogHelper.FilePath()))
        If Directory.Exists(LogHelper.FilePath()) Then
            Dim fileEntries As String() = Directory.GetFiles(LogHelper.FilePath())
            Dim fileName As String
            For Each fileName In fileEntries
                LogBox(LOG_TAG, String.Format("FN : {0}", Path.GetFileName(fileName)))

                LogBox(LOG_TAG, "--------------------------------")
                Dim objReader As New System.IO.StreamReader(fileName)
                Do While objReader.Peek() <> -1
                    LogBox(objReader.ReadLine())
                Loop
                objReader.Close()
                LogBox(LOG_TAG, "--------------------------------")
            Next fileName
        Else
            LogBox(LOG_TAG, "No log file")
        End If
    End Sub

End Class
