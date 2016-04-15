
::=================================================================
@ECHO OFF
ECHO Clean project temp files...
SET /P PERMISSION=Do you want to continue? (Y/N)
IF "%PERMISSION%"=="y" GOTO START
IF "%PERMISSION%"=="Y" GOTO START
IF "%PERMISSION%"=="n" GOTO END
IF "%PERMISSION%"=="N" GOTO END

:START
@ECHO ON
SET PROJECTNAME_LIST=(DemoLog4Net DemoLog4Net_NoConfigFile DemoOverrideCloseEvent DemoXMLReader, DemoExcelEditor, DemoProgressBar)
SET PROJECTROOT=DemoApplication

::==== Visual Studio 2015
FOR %%i IN %PROJECTNAME_LIST% DO ATTRIB -R -H ..\%PROJECTROOT%\%%i\.vs /S /D
FOR %%i IN %PROJECTNAME_LIST% DO RD /S /Q ..\%PROJECTROOT%\.vs

::==== Visual Studio
FOR %%i IN %PROJECTNAME_LIST% DO DEL /Q /A:H ..\%PROJECTROOT%\%%i\*.suo
FOR %%i IN %PROJECTNAME_LIST% DO DEL /Q ..\%PROJECTROOT%\%%i\*.suo
FOR %%i IN %PROJECTNAME_LIST% DO DEL /Q ..\%PROJECTROOT%\%%i\*.user

FOR %%i IN %PROJECTNAME_LIST% DO RD /S /Q ..\%PROJECTROOT%\%%i\bin\Release
FOR %%i IN %PROJECTNAME_LIST% DO RD /S /Q ..\%PROJECTROOT%\%%i\bin\Debug
FOR %%i IN %PROJECTNAME_LIST% DO RD /S /Q ..\%PROJECTROOT%\%%i\obj

FOR %%i IN %PROJECTNAME_LIST% DO MD ..\%PROJECTROOT%\%%i\bin\Release\
FOR %%i IN %PROJECTNAME_LIST% DO MD ..\%PROJECTROOT%\%%i\bin\Debug\
FOR %%i IN %PROJECTNAME_LIST% DO XCOPY /S ".\_BackupFiles" "..\%PROJECTROOT%\%%i\bin\Release"
FOR %%i IN %PROJECTNAME_LIST% DO XCOPY /S ".\_BackupFiles" "..\%PROJECTROOT%\%%i\bin\Debug"

:END
@ECHO OFF
ECHO =================================================================
ECHO Finish Delete Files...
PAUSE