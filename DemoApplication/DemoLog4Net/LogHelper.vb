Public Class LogHelper

    Private Const LOG_FILE_XML_KEY As String = "Log4NetConfigFile"
    Private Const LOGGRE_ID As String = "AppLogger"
    'Private Const LOGGRE_ID As String = "AppLoggerDebug"

    Private Shared _logger As log4net.ILog

    Shared Sub New()
        InitialLogger()
    End Sub

    Public Shared Function ShareLogger() As log4net.ILog
        StopLogger()
        InitialLogger()
        Return _logger
    End Function

    Public Shared Function FilePath() As String
        Return Application.StartupPath() & "\Log"
    End Function

    Public Shared Sub StopLogger()
        If _logger IsNot Nothing Then
            _logger.Logger.Repository.Shutdown()
        End If
    End Sub

    Private Shared Sub InitialLogger()
        Dim configFile = System.Configuration.ConfigurationManager.AppSettings(LOG_FILE_XML_KEY)
        log4net.Config.XmlConfigurator.Configure(New System.IO.FileInfo(configFile))

        _logger = log4net.LogManager.GetLogger(LOGGRE_ID)
    End Sub

End Class
