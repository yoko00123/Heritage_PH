Option Explicit On
Option Strict On

Imports nDB = GSCOM.Applications.InSys.Database

Public Class ChangePasswordForm
    Public uID As String
    Public p As String
    Public c As Boolean
    Public vPassword As String
    Public pl As String
    Public plc As String
    Public ph As Boolean
    Public initlogbool As Boolean
    Public passexpiredbool As Boolean
    Public changepasswordbool As Boolean

#Region "Events"


    'Private Sub ChangePasswordForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    OK.Enabled = False
    '    Cancel.Enabled = False
    '    gLogInForm = Me
    'End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Change()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    'Private Sub LoginForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    Static b As Boolean = True
    '    If b Then
    '        Application.DoEvents()
    '        Init()
    '        If txtUserName.Text = "" Then
    '            txtUserName.Focus()
    '        Else
    '            txtPassword.Focus()
    '        End If

    '        OK.Enabled = True
    '        Cancel.Enabled = True


    '        b = False
    '    End If
    'End Sub

#End Region

#Region "Init"
    'Private Sub Init()
    '    Dim s As String
    '    Try
    '        Me.Text = Application.ProductName
    '        gInit()
    '        Me.Icon = gIcon
    '        s = "Password=" & gDBPassword & ""
    '        s &= ";Persist Security Info=True"
    '        s &= ";User ID=" & gDBUser & ";"
    '        s &= "Initial Catalog=" & gDBInitialCatalog & ";"
    '        s &= "Data Source=" & gDBDataSource
    '        'gConnection = New SqlClient.SqlConnection(s)
    '        nDB.Connection = New SqlClient.SqlConnection(s)
    '        'nDB.InitLookUp(_ID_Company)
    '        _ID_Company.Text = "Company"
    '        Dim dr As DataRow
    '        Dim dra() As DataRow
    '        dra = GSCOM.SQL.TableQuery("SELECT * FROM vMenu WHERE ID=" & nDB.Menu.MasterFile_Company, gConnection).Select
    '        If dra.Length > 0 Then
    '            dr = dra(0)
    '            s = dr.Item(nDB.Tables.tMenu.Field.DataSource.ToString).ToString
    '            If InStr(s, "@ID_Company") > 0 Then
    '                s = s.Replace("@ID_Company", GSCOM.SQL.SQLFormat(DBNull.Value))
    '                dr.Item(nDB.Tables.tMenu.Field.DataSource.ToString) = s
    '            End If
    '            nDB.InitLookUp(_ID_Company, dr)
    '        Else
    '            Throw New Exception("You must have the rights to view " & _ID_Company.Text & " List.")
    '        End If
    '        gRefreshSettings()
    '        txtUserName.Text = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("LastUser", "").ToString
    '        Dim o As String
    '        o = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("LastCompany", "").ToString
    '        If o = "" Then
    '            _ID_Company.SelectedValue = DBNull.Value
    '        Else
    '            _ID_Company.SelectedValue = CInt(o)
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

#End Region

#Region "LogOn"
    Private Sub Change()
        'Dim p As String
        'Dim dt As DataTable
        'Dim bstr As String
        'Dim bbool As Boolean

        Try
            If (CInt(uID) <> 1 AndAlso gUser <> 1) Or (gUser = 1) Then
                If txtPassword.Text <> txtConfirmPassword.Text Then
                    MsgBox("Must input the same password.", MsgBoxStyle.Information)
                Else
                    vPassword = GSCOM.SQL.SQLFormat(txtPassword.Text)
                    p = "SELECT dbo.fPasswordValidation(" & vPassword & ")"
                    c = CBool(GSCOM.SQL.ExecuteScalar(p, gConnection))
                    pl = "SELECT dbo.fGetSetting('PasswordLength')"
                    plc = CStr(GSCOM.SQL.ExecuteScalar(pl, gConnection))
                    If vPassword = "" Then
                        ph = False

                    Else
                        ph = CBool(GSCOM.SQL.ExecuteScalar("EXEC pPasswordHistoryValidation " & uID.ToString & "," & vPassword, gConnection))

                    End If
                    If Not c Then
                        MsgBox("Must be Alphanumeric and character length is <= " & plc.ToString, MsgBoxStyle.Exclamation)
                        Exit Sub
                    Else
                        If Not ph Then
                            MsgBox("This Password is already used and still in the password history parameter, Please use different password", MsgBoxStyle.Exclamation)
                            Exit Sub
                        Else
                            Dim vpencrypted As String
                            vpencrypted = "'" & GSCOM.Common.EncryptA(vPassword.ToString, 41).ToString & "_BJTGLR" & "'"
                            GSCOM.SQL.ExecuteNonQuery("EXEC pSavePassword " & uID.ToString & "," & vpencrypted.ToString, gConnection)
                            GSCOM.SQL.ExecuteNonQuery("EXEC pPasswordHistory " & uID.ToString & "," & vPassword, gConnection)
                            If initlogbool Then
                                GSCOM.SQL.ExecuteNonQuery("EXEC pUpdateIsFirstLog " & uID.ToString, gConnection)
                                LoginForm.txtPassword.Text = ""
                                Me.Close()
                            End If
                            If passexpiredbool Then
                                LoginForm.txtPassword.Text = ""
                                Me.Close()
                            End If
                            If changepasswordbool Then

                                Me.Close()
                            End If
                            MsgBox("Password Saved", MsgBoxStyle.Exclamation)
                        End If
                    End If
                End If
            Else
                MsgBox("Cannot Changed System Password ", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

    'Friend Sub ShowProgress(Optional ByVal pText As String = "")
    '    If MainProgressBar.Value < MainProgressBar.Maximum Then
    '        MainProgressBar.Value += 1
    '        If pText <> "" Then
    '            MainStatus.Text = pText
    '        End If
    '        Application.DoEvents()
    '    End If
    'End Sub

    'Public Sub New()

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub



    Private Sub ChangePasswordForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim loginBG As String = IO.Path.Combine(GSCOM.SQL.ExecuteScalar("Select dbo.fGetSetting('ResourcePath')", gConnection).ToString + "BG\", GSCOM.SQL.ExecuteScalar("Select dbo.fGetSetting('LoginBG')", gConnection).ToString)
        If IO.File.Exists(loginBG) Then
            Dim b As Bitmap
            b = New Bitmap(loginBG)
            Me.BackgroundImage = b
        Else
            Me.BackgroundImage = Nothing
        End If
        Me.OK.BackColor = getColor("LoginStatusBGColor")
        Me.Cancel.BackColor = getColor("LoginStatusBGColor")
    End Sub
End Class

#Region "Comments"

' TODO: Insert code to perform custom authentication using the provided username and password 
' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
' The custom principal can then be attached to the current thread's principal as follows: 
'     My.User.CurrentPrincipal = CustomPrincipal
' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
' such as the username, display name, etc.

#End Region
