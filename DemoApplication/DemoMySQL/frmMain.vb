
Imports System.Configuration
Imports MySql.Data.MySqlClient

Public Class frmMain

    Private SQL_CON As String = ConfigurationManager.ConnectionStrings("SQLConnection").ConnectionString

    Private Const SQL_TABLE_1 As String = "DemoTable1"
    Private Const SQL_TABLE_2 As String = "DemoTable2"

    Private Const SQL_TABLE_KEY_1 As String = "Name"
    Private Const SQL_TABLE_KEY_2 As String = "PhoneNumber"

    Private Const LOG_TAG As String = "SQL"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()

        btnDemo.Select()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnDemo_Click(sender As Object, e As EventArgs) Handles btnDemo.Click
        LogBox("==== Demo STR ====")

        Dim sqlConn As New MySqlConnection
        Dim tmpTable As DataTable = Nothing

        Try
            'sqlConn.ConnectionString = SQLConnect()
            sqlConn.ConnectionString = SQL_CON
            sqlConn.Open()

            LogBox(LOG_TAG, "Read table 1 ...")
            tmpTable = SQLReadTable(sqlConn, {SQL_TABLE_KEY_1}, SQL_TABLE_1)
            LogTable(tmpTable)

            LogBox(LOG_TAG, "Read table 2 ...")
            tmpTable = SQLReadTable(sqlConn, {}, SQL_TABLE_2)
            LogTable(tmpTable)

            LogBox(LOG_TAG, "Write table...")
            Dim tmpValue = tmpTable.Rows(0)(SQL_TABLE_KEY_2)
            tmpValue = tmpValue + 1
            SQLWriteTable(sqlConn,
                          SQL_TABLE_KEY_2, tmpValue.ToString,
                          SQL_TABLE_KEY_1, tmpTable.Rows(0)(SQL_TABLE_KEY_1).ToString,
                          SQL_TABLE_2)

            LogBox(LOG_TAG, "Read table 2 ...")
            tmpTable = SQLReadTable(sqlConn, {}, SQL_TABLE_2)
            LogTable(tmpTable)

        Catch ex As Exception
            LogBox("See Error MessageBox...")
            MsgBox(ex.Message, MsgBoxStyle.OkOnly)
        End Try

        sqlConn.Clone()

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

    Private Sub LogBoxN(ByVal message As String)
        tbxLogBox.Text = tbxLogBox.Text + message
    End Sub

    Private Sub LogTable(ByVal refTable As DataTable)
        Dim column As DataColumn
        Dim row As DataRow

        Dim currentRows() As DataRow = refTable.Select(Nothing, Nothing, DataViewRowState.CurrentRows)

        If (currentRows.Length < 1) Then
            Debug.WriteLine("No Current Rows Found")
            LogBox("No Current Rows Found")
        Else
            For Each column In refTable.Columns
                Debug.Write(vbTab & column.ColumnName)
                LogBoxN(vbTab & column.ColumnName)
            Next

            Debug.WriteLine(vbTab & "RowState")
            LogBox(vbTab & "RowState")

            For Each row In currentRows
                For Each column In refTable.Columns
                    Debug.Write(vbTab & IIf(row(column).ToString() Is String.Empty, "N/A", row(column).ToString()))
                    LogBoxN(vbTab & IIf(row(column).ToString() Is String.Empty, "N/A", (row(column).ToString())))
                Next

                Dim rowState As String = System.Enum.GetName(row.RowState.GetType(), row.RowState)
                Debug.WriteLine(vbTab & rowState)
                LogBox(vbTab & rowState)
            Next
        End If
    End Sub

    Private Function SQLConnect() As String
        Dim connBuilder = New MySqlConnectionStringBuilder()
        connBuilder.Server = "127.0.0.1"
        'connBuilder.Server = "localhost"
        'connBuilder.Port = 3306
        connBuilder.Database = "DemoDatabase"
        connBuilder.UserID = "DemoUser"
        connBuilder.Password = "0000"

        Return connBuilder.ToString
    End Function

    Private Function SQLReadTable(ByVal sqlConn As MySqlConnection,
                                  ByVal keyList As String(),
                                  ByVal tableName As String) As DataTable

        Dim sqlCmd As String = String.Empty
        Dim sqlKey As String = String.Empty
        Dim sqlTable As DataTable = Nothing

        If keyList.Count = 0 Then
            sqlKey = "*"
        Else
            For idx = 0 To keyList.Count - 1
                sqlKey += String.Format("{0}{1}", keyList(idx), IIf(idx = (keyList.Count - 1), "", ", "))
            Next
        End If

        sqlCmd += String.Format("SELECT {0} FROM {1}", sqlKey, tableName)

        Try
            sqlTable = MySqlHelper.ExecuteDataset(sqlConn, sqlCmd).Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

        Return sqlTable
    End Function

    Private Sub SQLWriteTable(ByVal sqlConn As MySqlConnection,
                              ByVal key As String,
                              ByVal value As String,
                              ByVal condKey As String,
                              ByVal condValue As String,
                              ByVal tableName As String)

        Dim sqlCmd As String = String.Empty

        sqlCmd = String.Format("UPDATE {0} SET ", tableName)
        sqlCmd += String.Format("{0} = @{1} ", key, key)
        sqlCmd += String.Format("WHERE {0} = @{1}", condKey, condKey)
        Dim sqlParam() As MySqlParameter = {
            New MySqlParameter(key, value),
            New MySqlParameter(condKey, condValue)
        }

        Try
            MySqlHelper.ExecuteNonQuery(sqlConn, sqlCmd, sqlParam)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
