Imports System
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Security.Permissions
Imports Microsoft.VisualBasic

Public Class Impersonator

    Private impersonatedUser As WindowsImpersonationContext
    Private tokenHandle As New IntPtr(0)

#Region "Extern"

    Private Declare Auto Function LogonUser Lib "advapi32.dll" (ByVal lpszUsername As [String], _
        ByVal lpszDomain As [String], ByVal lpszPassword As [String], _
        ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, _
        ByRef phToken As IntPtr) As Boolean

    <DllImport("kernel32.dll")> _
    Private Shared Function FormatMessage(ByVal dwFlags As Integer, ByRef lpSource As IntPtr, _
        ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, ByRef lpBuffer As [String], _
        ByVal nSize As Integer, ByRef Arguments As IntPtr) As Integer

    End Function

    Private Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Boolean

    Private Declare Auto Function DuplicateToken Lib "advapi32.dll" (ByVal ExistingTokenHandle As IntPtr, _
            ByVal SECURITY_IMPERSONATION_LEVEL As Integer, _
            ByRef DuplicateTokenHandle As IntPtr) As Boolean
#End Region

#Region "Impersonate"
    Public Function Impersonate(ByVal UserName As String, ByVal Password As String, ByVal DomainName As String) As Boolean
        Dim dupeTokenHandle As New IntPtr(0)
        Const LOGON32_PROVIDER_DEFAULT As Integer = 0
        Const LOGON32_LOGON_INTERACTIVE As Integer = 2
        tokenHandle = IntPtr.Zero
        If Not LogonUser(UserName, DomainName, Password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, tokenHandle) Then
            Return False
        End If
        Dim newId As New WindowsIdentity(tokenHandle)
        impersonatedUser = newId.Impersonate()
        Return True
    End Function
#End Region

#Region "Undo"
    Public Sub Undo()
        impersonatedUser.Undo()
        If Not System.IntPtr.op_Equality(tokenHandle, IntPtr.Zero) Then
            CloseHandle(tokenHandle)
        End If
    End Sub
#End Region

End Class
'    ' Test harness.
'    ' If you incorporate this code into a DLL, be sure to demand FullTrust.
'    <PermissionSetAttribute(SecurityAction.Demand, Name:="FullTrust")> _
'    Public Overloads Shared Sub Main_Ex(ByVal args() As String)

'        Dim tokenHandle As New IntPtr(0)
'        Dim dupeTokenHandle As New IntPtr(0)
'L1:
'        Try


'            Dim userName, domainName As String

'            ' Get the user token for the specified user, domain, and password using the 
'            ' unmanaged LogonUser method.  
'            ' The local machine name can be used for the domain name to impersonate a user on this machine.
'            Console.Write("Enter the name of a domain on which to log on: ")
'            domainName = Console.ReadLine()

'            Console.Write("Enter the login of a user on {0} that you wish to impersonate: ", domainName)
'            userName = Console.ReadLine()

'            Console.Write("Enter the password for {0}: ", userName)

'            Const LOGON32_PROVIDER_DEFAULT As Integer = 0
'            'This parameter causes LogonUser to create a primary token.
'            Const LOGON32_LOGON_INTERACTIVE As Integer = 2

'            tokenHandle = IntPtr.Zero

'            ' Call LogonUser to obtain a handle to an access token.
'            Dim returnValue As Boolean = LogonUser(userName, domainName, Console.ReadLine(), LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, tokenHandle)

'            Console.WriteLine("LogonUser called.")

'            If False = returnValue Then
'                Dim ret As Integer = Marshal.GetLastWin32Error()
'                Console.WriteLine("LogonUser failed with error code : {0}", ret)
'                Throw New System.ComponentModel.Win32Exception(ret)

'                Return
'            End If

'            Dim success As String
'            If returnValue Then success = "Yes" Else success = "No"
'            Console.WriteLine(("Did LogonUser succeed? " + success))
'            Console.WriteLine(("Value of Windows NT token: " + tokenHandle.ToString()))

'            ' Check the identity.
'            Console.WriteLine(("Before impersonation: " + WindowsIdentity.GetCurrent().Name))

'            ' Use the token handle returned by LogonUser.
'            Dim newId As New WindowsIdentity(tokenHandle)
'            Dim impersonatedUser As WindowsImpersonationContext = newId.Impersonate()

'            ' Check the identity.
'            Console.WriteLine(("After impersonation: " + WindowsIdentity.GetCurrent().Name))

'            ' Stop impersonating the user.
'            impersonatedUser.Undo()

'            ' Check the identity.
'            Console.WriteLine(("After Undo: " + WindowsIdentity.GetCurrent().Name))

'            ' Free the tokens.
'            If Not System.IntPtr.op_Equality(tokenHandle, IntPtr.Zero) Then
'                CloseHandle(tokenHandle)
'            End If

'        Catch ex As Exception
'            Console.WriteLine(("Exception occurred. " + ex.Message))
'        End Try
'        If Console.ReadLine() = "y" Then
'            GoTo L1


'        End If

'    End Sub 'Main