Imports System.IO
Imports System.Text
Imports System.Security.AccessControl

<CLSCompliant(True)> _
Public Class ErrorLoggerClass 'Emil 07132012

#Region "Initialization"
    Dim currentDomain As AppDomain = AppDomain.CurrentDomain
#End Region

#Region "Constructors"
    Public Sub New()

        'USE UNHANDLED ERROR HANDLER
        AddHandler currentDomain.UnhandledException, AddressOf UnhandledExnHandler
        AddHandler Application.ThreadException, AddressOf UnhandledThreadHandler

    End Sub
#End Region

#Region "Events"

#Region "UnhandledErrorHandlers"
    Private Sub UnhandledExnHandler(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        Dim ex As Exception
        ex = CType(e.ExceptionObject, Exception)
        MsgBox("An error occured while processing request." + vbNewLine + "Error Message: " + ex.Message, CType(MsgBoxStyle.Critical + vbOKOnly, MsgBoxStyle), "GIRAFFE - ERROR")
        WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source.ToString)
    End Sub

    Private Sub UnhandledThreadHandler(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        MsgBox("An error occured while processing request." + vbNewLine + "Error Message: " + e.Exception.Message, CType(MsgBoxStyle.Critical + vbOKOnly, MsgBoxStyle), "GIRAFFE - ERROR")
        WriteToErrorLog(e.Exception.Message, e.Exception.StackTrace, e.Exception.Source.ToString)
    End Sub

#End Region

#Region "Write Log To File"
    Public Sub WriteToErrorLog(ByVal msg As String, ByVal stkTrace As String, ByVal title As String)
        Try
            If Not System.IO.Directory.Exists(Application.StartupPath & "\Errors\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\Errors\")
                SetPermission(Application.StartupPath & "\Errors\")
            End If

            'Check if File Exists then Open or Create
            Dim fs As IO.FileStream = New IO.FileStream(Application.StartupPath & "\Errors\ErrorLog.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite)
            Dim s As IO.StreamWriter = New IO.StreamWriter(fs)
            s.Close()
            fs.Close()

            'Write Error to File
            Dim fs1 As IO.FileStream = New IO.FileStream(Application.StartupPath & "\Errors\ErrorLog.txt", FileMode.Append, FileAccess.Write)
            Dim s1 As IO.StreamWriter = New IO.StreamWriter(fs1)
            s1.Write("Title: " & title & vbCrLf)
            s1.Write("Message: " & msg & vbCrLf)
            s1.Write("StackTrace: " & stkTrace & vbCrLf)
            s1.Write("Date/Time: " & DateTime.Now.ToString() & vbCrLf)
            s1.Write("================================================" & vbCrLf)
            s1.Close()
            fs1.Close()
        Catch 'ex As Exception
            'MsgBox(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub SetPermission(ByVal FolderPath As String)
        Dim UserAccount As String = My.User.Name
        Dim FolderInfo As IO.DirectoryInfo = New IO.DirectoryInfo(FolderPath)
        Dim FolderAcl As New DirectorySecurity
        Try
            FolderAcl.AddAccessRule(New FileSystemAccessRule(UserAccount, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit Or InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
            'FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
            FolderInfo.SetAccessControl(FolderAcl)
        Catch
        End Try
    End Sub
#End Region

#End Region

End Class
