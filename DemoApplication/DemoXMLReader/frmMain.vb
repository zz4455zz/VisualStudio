Public Class frmMain

    Private Const LOG_TAG As String = "XML"
    Private Const XML_FILE As String = "\XMLTemplate\Sample.xml"
    Private Const XML_FILE_FAIL_TAG As String = "\XMLTemplate\SampleFailTag.xml"
    Private Const XML_FILE_FAIL_MEMBER As String = "\XMLTemplate\SampleFailMember.xml"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()

        btnDemo.Select()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnDemo_Click(sender As Object, e As EventArgs) Handles btnDemo.Click
        LogBox("==== Demo STR ====")

        ReadXMLFile(XML_FILE)

        LogBox("==== Demo END ====")
    End Sub

    Private Sub btnFailTag_Click(sender As Object, e As EventArgs) Handles btnFailTag.Click
        LogBox("==== Demo STR ====")

        ReadXMLFile(XML_FILE_FAIL_TAG)

        LogBox("==== Demo END ====")
    End Sub

    Private Sub btnFailMem_Click(sender As Object, e As EventArgs) Handles btnFailMem.Click
        LogBox("==== Demo STR ====")

        ReadXMLFile(XML_FILE_FAIL_MEMBER)

        LogBox("==== Demo END ====")
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        tbxLogBox.Text = String.Empty
    End Sub

    Private Sub LogBox(ByVal message As String)
        tbxLogBox.Text = tbxLogBox.Text + message + vbCrLf
        tbxLogBox.SelectionStart = tbxLogBox.Text.Length
        tbxLogBox.ScrollToCaret()
    End Sub

    Private Sub LogBox(ByVal tag As String, ByVal message As String)
        LogBox(String.Format("{0} : {1}", tag, message))
    End Sub

    Private Sub ReadXMLFile(ByVal cmlFale As String)
        Dim xmlObj As String = IO.Path.GetFullPath(Application.StartupPath & cmlFale)
        Dim objSerializerl As New Xml.Serialization.XmlSerializer(GetType(XMLTemplate))
        Dim objStreamReader As IO.StreamReader
        Try
            objStreamReader = New IO.StreamReader(xmlObj)
            LogBox("==================")
            LogBox(objStreamReader.ReadToEnd)
            LogBox("==================")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Dim objXML As New XMLTemplate()
        Try
            objStreamReader = New IO.StreamReader(xmlObj)
            objXML = objSerializerl.Deserialize(objStreamReader)
            objStreamReader.Close()

            LogBox(String.Format("*** : Provider <{0}>", objXML.Provider))
            For Each product In objXML.Products
                LogBox("------------------")
                LogBox(String.Format("*** : Name <{0}>", product.Name))
                LogBox(String.Format("*** : Price [{0} : {1}]", product.Price.Currency, product.Price.Value))
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

End Class
