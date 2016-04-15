
Imports System.IO
Public Class frmMain

    Private Const FILE_NAME As String = "Demo"
    Private Const EXT_XLS As String = ".xls"
    Private Const EXT_XLSX As String = ".xlsx"
    Private Const SHEET_NAME As String = "DemoSheet"

    Private Const LOG_TAG As String = "XLS"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()

        btnDemo.Select()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnDemo_Click(sender As Object, e As EventArgs) Handles btnDemo.Click
        LogBox("==== Demo STR ====")

        Dim excelFiles As List(Of String) = New List(Of String)
        Dim fileName As String = ""

        fileName = ExcelWriteDemo1()
        excelFiles.Add(fileName)
        fileName = ExcelWriteDemo2(CreateSampleTable2())
        excelFiles.Add(fileName)

        ExcelReadDemo(excelFiles)

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

    Private Function CreateSampleTable2() As DataTable
        CreateSampleTable2 = New DataTable()

        ' Create colume
        For Idx1 As Integer = 2 To 9
            CreateSampleTable2.Columns.Add(String.Format("{0}", Idx1), GetType(String))
        Next

        ' Create row
        For Idx1 As Integer = 2 To 9
            Dim newRow As DataRow = CreateSampleTable2.NewRow()
            For Idx2 As Integer = 2 To 9
                newRow(Idx2 - 2) = String.Format("{0}x{1}={2}", Idx2, Idx1, (Idx1 * Idx2))
            Next
            CreateSampleTable2.Rows.Add(newRow)
        Next

    End Function

    Private Function ExcelWriteDemo1() As String
        '建立Excel 2003檔案
        Dim workbook As NPOI.HSSF.UserModel.HSSFWorkbook = New NPOI.HSSF.UserModel.HSSFWorkbook()
        Dim sheet As NPOI.HSSF.UserModel.HSSFSheet = workbook.CreateSheet(SHEET_NAME)

        Dim row As NPOI.HSSF.UserModel.HSSFRow
        Dim cell As NPOI.HSSF.UserModel.HSSFCell
        Dim rowIndex As Integer = 0

        While rowIndex < 9
            row = sheet.CreateRow(rowIndex)
            Dim colIndex As Integer = 0
            While colIndex <= rowIndex
                cell = row.CreateCell(colIndex)
                cell.SetCellValue([String].Format("{0}*{1}={2}", rowIndex + 1, colIndex + 1, (rowIndex + 1) * (colIndex + 1)))
                System.Math.Max(System.Threading.Interlocked.Increment(colIndex), colIndex - 1)
            End While
            System.Math.Max(System.Threading.Interlocked.Increment(rowIndex), rowIndex - 1)
        End While

        Dim name As String = String.Format("{0}_{1}{2}", FILE_NAME, DateTime.Now.ToString("yyyyMMdd-HHmmss"), EXT_XLS)
        Dim file As New FileStream(Path.Combine(Application.StartupPath, name), FileMode.Create)
        workbook.Write(file)
        file.Close()

        LogBox(LOG_TAG, name)
        ExcelWriteDemo1 = name
    End Function

    Private Function ExcelWriteDemo2(ByVal table As DataTable) As String
        '建立Excel 2007檔案
        Dim workbook As NPOI.XSSF.UserModel.XSSFWorkbook = New NPOI.XSSF.UserModel.XSSFWorkbook()
        Dim sheet As NPOI.XSSF.UserModel.XSSFSheet = workbook.CreateSheet(SHEET_NAME)

        '第一行為欄位名稱
        sheet.CreateRow(0)
        For idx As Integer = 0 To (table.Columns.Count - 1)
            sheet.GetRow(0).CreateCell(idx).SetCellValue(table.Columns(idx).ColumnName)
        Next

        '第二行開始為欄位數值
        For rowIdx As Integer = 0 To (table.Rows.Count - 1)
            sheet.CreateRow(rowIdx + 1)

            For colIdx As Integer = 0 To (table.Columns.Count - 1)
                sheet.GetRow(rowIdx + 1).CreateCell(colIdx).SetCellValue(table.Rows(rowIdx)(colIdx).ToString())
            Next
        Next

        Dim name As String = String.Format("{0}_{1}{2}", FILE_NAME, DateTime.Now.ToString("yyyyMMdd-HHmmss"), EXT_XLSX)
        Dim file As New FileStream(Path.Combine(Application.StartupPath, name), FileMode.Create)
        workbook.Write(file)
        file.Close()

        LogBox(LOG_TAG, name)
        ExcelWriteDemo2 = name
    End Function


    Private Sub ExcelReadDemo(ByVal fileList As List(Of String))

        For Each file In fileList
            Try
                Using dataStream As New IO.FileStream(file, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                    Dim workBook As NPOI.SS.UserModel.IWorkbook = Nothing
                    Dim sheet As NPOI.SS.UserModel.ISheet = Nothing
                    Dim fileExtension As String = IO.Path.GetExtension(file)

                    If String.Compare(fileExtension, EXT_XLS, True) = 0 Then
                        workBook = New NPOI.HSSF.UserModel.HSSFWorkbook(dataStream)
                    ElseIf String.Compare(fileExtension, EXT_XLSX, True) = 0 Then
                        workBook = New NPOI.XSSF.UserModel.XSSFWorkbook(dataStream)
                    End If

                    sheet = workBook.GetSheet(SHEET_NAME)

                    Dim table As DataTable = New DataTable
                    If ExcelSheetToDataTable(sheet, table) = False Then
                        LogBox(LOG_TAG, String.Format("Ooops! Import excel file ERROR. ({0})", file))
                    Else
                        LogBox(LOG_TAG, String.Format("Col({0,2}) Row({1:000}) {2}", table.Columns.Count, table.Rows.Count, file))
                    End If

                    LogTable(table)

                End Using
            Catch ex As Exception
                LogBox(LOG_TAG, ex.Message)
            End Try
        Next

    End Sub

    Private Function ExcelSheetToDataTable(ByVal refSheet As NPOI.SS.UserModel.ISheet,
                                           ByRef refTable As DataTable) As Boolean

        ExcelSheetToDataTable = True
        Try
            refTable = New DataTable()

            Dim headerRow As NPOI.SS.UserModel.IRow = refSheet.GetRow(0)
            Dim cellCount As Integer = headerRow.LastCellNum
            For i = headerRow.FirstCellNum To (cellCount - 1) Step 1
                Dim column As DataColumn = New DataColumn(headerRow.GetCell(i).StringCellValue)
                refTable.Columns.Add(column)
            Next

            Dim rowCount As Integer = refSheet.LastRowNum
            For i = (refSheet.FirstRowNum + 1) To (refSheet.LastRowNum) Step 1
                Dim row As NPOI.SS.UserModel.IRow = refSheet.GetRow(i)
                Dim dataRow As DataRow = refTable.NewRow()

                For j = row.FirstCellNum To (cellCount - 1) Step 1
                    If Not Convert.IsDBNull(row.GetCell(j)) Then
                        dataRow(j) = row.GetCell(j).ToString()
                    End If
                Next

                refTable.Rows.Add(dataRow)
            Next
        Catch ex As Exception
            ExcelSheetToDataTable = False
        End Try

    End Function

    Private Sub LogTable(ByVal refTable As DataTable)

        Dim column As DataColumn
        Dim row As DataRow

        Dim currentRows() As DataRow = refTable.Select(Nothing, Nothing, DataViewRowState.CurrentRows)

        If (currentRows.Length < 1) Then
            Console.WriteLine("No Current Rows Found")
            LogBox("No Current Rows Found")
        Else
            For Each column In refTable.Columns
                Console.Write(vbTab & column.ColumnName)
                LogBoxN(vbTab & column.ColumnName)
            Next

            Console.WriteLine(vbTab & "RowState")
            LogBox(vbTab & "RowState")

            For Each row In currentRows
                For Each column In refTable.Columns
                    Console.Write(vbTab & row(column).ToString())
                    LogBoxN(vbTab & row(column).ToString())
                Next

                Dim rowState As String = System.Enum.GetName(row.RowState.GetType(), row.RowState)
                Console.WriteLine(vbTab & rowState)
                LogBox(vbTab & rowState)
            Next
        End If

    End Sub


End Class
