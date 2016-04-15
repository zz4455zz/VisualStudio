Public Class LogHelper

    Private Const LOGGRE_ID As String = "AppLogger"
    Private Const APPENDER_ID_ROLLING As String = "LogRollingAppender"
    Private Const APPENDER_ID_CONSOLE As String = "ConsoleAppender"

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
        Const layout As String = "%-5level %date{yyyy/MM/dd_HH:mm:ss,fff} [%thread] %logger - %message%newline"

        _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Dim rollingAppender = New log4net.Appender.RollingFileAppender() With {
            .Name = APPENDER_ID_ROLLING,
            .File = "Log\LogFile_",
            .DatePattern = "yyyyMMdd'.log'",
            .AppendToFile = True,
            .RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date,
            .StaticLogFileName = False,
            .Layout = New log4net.Layout.PatternLayout(layout)
        }

        Dim consoleAppender = New log4net.Appender.ConsoleAppender() With {
            .Name = APPENDER_ID_CONSOLE,
            .Layout = New log4net.Layout.PatternLayout(layout)
        }

        Dim lvfilter = New log4net.Filter.LevelRangeFilter()
        lvfilter.LevelMin = log4net.Core.Level.Debug
        lvfilter.LevelMax = log4net.Core.Level.Fatal

        rollingAppender.AddFilter(lvfilter)

        _logger = log4net.LogManager.GetLogger(LOGGRE_ID)
        Dim tmpLogger As log4net.Repository.Hierarchy.Logger = _logger.Logger
        With tmpLogger
            .AddAppender(rollingAppender)
            .AddAppender(consoleAppender)
        End With

        For Each tmpAppender In tmpLogger.Repository.GetAppenders()
            If tmpAppender.Name.Equals(APPENDER_ID_ROLLING) Then
                Dim tmp As log4net.Appender.FileAppender = tmpAppender
                tmp.ActivateOptions()
            End If

            If tmpAppender.Name.Equals(APPENDER_ID_CONSOLE) Then
                Dim tmp As log4net.Appender.ConsoleAppender = tmpAppender
                tmp.ActivateOptions()
            End If
        Next

        _logger.Logger.Repository.Configured = True
    End Sub

End Class
