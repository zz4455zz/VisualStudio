
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
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

        Dim sqlTran As SqlClient.SqlTransaction = Nothing
        Dim tmpTable As DataTable = Nothing

        Try
            Using sqlConn = New SqlClient.SqlConnection(SQL_CON)
                sqlConn.Open()
                sqlTran = sqlConn.BeginTransaction()

                LogBox(LOG_TAG, "Lock table...")
                SQLLockTable(sqlTran, {SQL_TABLE_KEY_1}, {SQL_TABLE_1, SQL_TABLE_2})

                LogBox(LOG_TAG, "Read table 1 ...")
                tmpTable = SQLReadTable(sqlTran, {SQL_TABLE_KEY_1}, SQL_TABLE_1)
                LogTable(tmpTable)

                LogBox(LOG_TAG, "Read table 2 ...")
                tmpTable = SQLReadTable(sqlTran, {}, SQL_TABLE_2)
                LogTable(tmpTable)

                LogBox(LOG_TAG, "Write table...")
                Dim tmpValue = tmpTable.Rows(0)(SQL_TABLE_KEY_2)
                tmpValue = tmpValue + 1
                SQLWriteTable(sqlTran,
                              SQL_TABLE_KEY_2, tmpValue.ToString,
                              SQL_TABLE_KEY_1, tmpTable.Rows(0)(SQL_TABLE_KEY_1).ToString,
                              SQL_TABLE_2)

                LogBox(LOG_TAG, "Read table 2 ...")
                tmpTable = SQLReadTable(sqlTran, {}, SQL_TABLE_2)
                LogTable(tmpTable)

                sqlTran.Commit()
                sqlTran.Dispose()
            End Using

        Catch ex As Exception
            LogBox("See Error MessageBox...")
            MsgBox(ex.Message, MsgBoxStyle.OkOnly)

            Try
                If sqlTran IsNot Nothing Then
                    sqlTran.Rollback()
                    sqlTran.Dispose()
                End If

            Catch ex2 As Exception
                LogBox("See Error MessageBox...")
                MsgBox(ex.Message, MsgBoxStyle.OkOnly)
            End Try

        End Try

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

    Private Sub SQLLockTable(ByVal sqlTran As SqlClient.SqlTransaction,
                             ByVal keyList As String(),
                             ByVal tableList As String())
        Dim sqlCmd As String = String.Empty
        Dim sqlKey As String = String.Empty

        'Single-Table
        'SELECT * FROM TABLE_NAME WITH (TABLOCKX) 

        'Multi-Table
        'SELECT TABLE_KEY_1, TABLE_KEY_2 FROM TABLE_NAME_1 WITH (TABLOCKX) 
        'UNION
        'SELECT TABLE_KEY_1, TABLE_KEY_2 FROM TABLE_NAME_2 WITH (TABLOCKX)

        If keyList.Count = 0 Then
            sqlKey = "*"
        Else
            For idx = 0 To keyList.Count - 1
                sqlKey += String.Format("{0}{1}", keyList(idx), IIf(idx = (keyList.Count - 1), "", ", "))
            Next
        End If

        For idx = 0 To tableList.Count - 1
            If idx = 0 Then
                'Nothing...
            Else
                sqlCmd += " UNION "
            End If
            sqlCmd += String.Format("SELECT {0} FROM {1} WITH (TABLOCKX)", sqlKey, tableList(idx))
        Next

        Try
            SqlHelper.ExecuteDataset(sqlTran, CommandType.Text, sqlCmd)
        Catch ex As Exception
            Throw ex
        End Try

        'False  : Will Timeout
        'True   : Nothing...
        'Try
        '    SqlHelper.ExecuteDataset(sqlTran, CommandType.Text, sqlCmd)
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.OkOnly)
        '    Return False
        'End Try
        'Return True

    End Sub

    Private Function SQLReadTable(ByVal sqlTran As SqlClient.SqlTransaction,
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
            sqlTable = SqlHelper.ExecuteDataset(sqlTran, CommandType.Text, sqlCmd).Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

        Return sqlTable
    End Function

    Private Sub SQLWriteTable(ByVal sqlTran As SqlClient.SqlTransaction,
                              ByVal key As String,
                              ByVal value As String,
                              ByVal condKey As String,
                              ByVal condValue As String,
                              ByVal tableName As String)

        Dim sqlCmd As String = String.Empty

        sqlCmd = String.Format("UPDATE {0} SET ", tableName)
        sqlCmd += String.Format("{0} = @{1} ", key, key)
        sqlCmd += String.Format("WHERE {0} = @{1}", condKey, condKey)
        Dim sqlParam() As SqlParameter = {
            New SqlParameter(key, value),
            New SqlParameter(condKey, condValue)
        }

        Try
            SqlHelper.ExecuteNonQuery(sqlTran, CommandType.Text, sqlCmd, sqlParam)
        Catch ex As Exception
            Throw ex
        End Try
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

End Class
