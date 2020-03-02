Option Explicit On
Option Strict Off

Imports System.Text



Friend Class UserInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tUser(Connection)
    'Private mControl As New nDB.UserControl 
    Private mControl As New InSys.DataControl
    Private mPassword As TextBox
    Private mResetPassword As ToolStripButton
    Private mUnblockButton As ToolStripButton
    Private mChangePassword As ToolStripButton
    Private mShowPassword As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
        End With
        InitControl(pMenu)
        Dim s As String = "SELECT IsNull(dbo.fGetSetting('IsSimplePassword'),0)"
        If GSCOM.SQL.ExecuteScalar(s, gConnection) = 0 Then
            mResetPassword = MyBase.AddButton("Reset Password", gMainForm.imgList.Images("SystemSetting.png"), AddressOf ResetPassword)
            mUnblockButton = MyBase.AddButton("Unlock User", gMainForm.imgList.Images("SystemSetting.png"), AddressOf UnblockUser)
        End If

        mChangePassword = MyBase.AddButton("Change Password", gMainForm.imgList.Images("SystemSetting.png"), AddressOf ChangePassword)
        AfterNew()
        mPassword = CType(Me.GetControl("_Password"), TextBox)
        mPassword.UseSystemPasswordChar = True

        If nDB.GetUserID = 1 Then mShowPassword = MyBase.AddButton("Show Password", gMainForm.imgList.Images("SystemSetting.png"), AddressOf ShowPassword)
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        Me.GetControl("_Password").Enabled = (pID <> 1) Or (nDB.GetUserID = 1)
        'mControl._Password.Enabled = (pID <> 1) Or (nDB.GetUserID = 1)
        If Not mResetPassword Is Nothing Then mResetPassword.Enabled = (pID <> 0)
        If Not mUnblockButton Is Nothing Then mUnblockButton.Enabled = (pID <> 0)
        mChangePassword.Enabled = (pID <> 0)
    End Sub

    Protected Overrides Function CanSave() As Boolean
        Dim o As Object
        Dim b As Boolean
        o = myDT.Get(Database.Tables.tUser.Field.ID_UserGroup)
        b = MyBase.CanSave()
        Dim s As String
        If Not mPassword.Text.EndsWith("_BJTGLR") Then
            s = GSCOM.Common.EncryptA(mPassword.Text, 41).ToString & "_BJTGLR"
            If s = "_BJTGLR" Then
                myDT.Rows(0)("Password") = DBNull.Value
            Else
                myDT.Rows(0)("Password") = s
            End If
        End If

        If b Then
            If (Not IsDBNull(o)) AndAlso (CInt(o) = 1) AndAlso CInt(myDT.Get(Database.Tables.tUser.Field.ID)) <> 1 Then
                MsgBox("System Group is for System account only", MsgBoxStyle.Exclamation)
                b = False
            End If
        End If
        Return b
    End Function
    Private Sub ResetPassword(ByVal sender As Object, ByVal e As EventArgs)
        If CInt(myDT.Get(Database.Tables.tUser.Field.ID)) <> 1 Then
            If MsgBox("Do you want to reset user's password?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                myDT.Set(Database.Tables.tUser.Field.Password, GeneratePassword)
                myDT.Set(Database.Tables.tUser.Field.IsFirstLog, 1)
                Me.SaveButton.PerformClick()
                MsgBox("Password Reset.", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Cannot reset System Password .", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub UnblockUser(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Do you want to unblock user?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pResetInvalidLogCount " & myDT.Get(Database.Tables.tUser.Field.ID).ToString, Connection)
            'Me.mApplyButton.Enabled = False
            'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
            MsgBox("User is Unblocked.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ChangePassword(ByVal sender As Object, ByVal e As EventArgs)

        Dim f As New ChangePasswordForm
        f.uID = myDT.Get(Database.Tables.tUser.Field.ID).ToString
        f.changepasswordbool = True
        f.ShowDialog()

        Me.Refresh()
    End Sub

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tUser)
        End Set
    End Property



#End Region

    Private Sub UserInfo_Commited(ByVal sender As Object, ByVal e As InfoSet.CommitedEventArgs) Handles Me.Commited
        'MsgBox(Me.RowID.ToString)
    End Sub

    Private Sub ShowPassword(ByVal sender As Object, ByVal e As EventArgs)
        If CType(Me.GetControl("_Password"), TextBox).Text <> "" Then
            Dim message, title, defaultValue As String
            Dim myValue As Object
            message = "Current Password"   ' Set prompt.
            title = "Show Password"   ' Set title.
            Dim s As String = GSCOM.Common.EncryptA(CType(Me.GetControl("_Password"), TextBox).Text, 41)
            defaultValue = s.Substring(0, s.Length - 7)
            'DecryptPassword(CType(Me.GetControl("_Password"), TextBox).Text) ' Set default value.

            ' Display message, title, and default value.
            myValue = InputBox(message, title, defaultValue)
        End If
    End Sub

    Private Function GeneratePassword() As String

        Dim KeyGen As RandomKeyGenerator = New RandomKeyGenerator
        KeyGen.KeyLetters = "abcdefghijklmnopqrstuvwxyz"
        KeyGen.KeyNumbers = "0123456789"
        KeyGen.KeyChars = CInt(GSCOM.SQL.ExecuteScalar("SELECT ISNULL(dbo.fGetSetting('PasswordLength'), 0)", gConnection))

        Return KeyGen.Generate()
    End Function

End Class

Public Class RandomKeyGenerator
    Dim Key_Letters As String
    Dim Key_Numbers As String
    Dim Key_Chars As Integer
    Dim LettersArray As Char()
    Dim NumbersArray As Char()

    Protected Friend WriteOnly Property KeyLetters() As String
        Set(ByVal Value As String)
            Key_Letters = Value
        End Set
    End Property

    Protected Friend WriteOnly Property KeyNumbers() As String
        Set(ByVal Value As String)
            Key_Numbers = Value
        End Set
    End Property

    Protected Friend WriteOnly Property KeyChars() As Integer
        Set(ByVal Value As Integer)
            Key_Chars = Value
        End Set
    End Property

    Function Generate() As String
        Dim i_key As Integer
        Dim Random1 As Single
        Dim arrIndex As Int16
        Dim sb As New StringBuilder
        Dim RandomLetter As String

        LettersArray = Key_Letters.ToCharArray
        NumbersArray = Key_Numbers.ToCharArray

        For i_key = 1 To Key_Chars
            Randomize()
            Random1 = Rnd()
            arrIndex = -1
            If (CType(Random1 * 111, Integer)) Mod 2 = 0 Then
                Do While arrIndex < 0
                    arrIndex = _
                     Convert.ToInt16(LettersArray.GetUpperBound(0) _
                     * Random1)
                Loop
                RandomLetter = LettersArray(arrIndex)
                If (CType(arrIndex * Random1 * 99, Integer)) Mod 2 <> 0 Then
                    RandomLetter = LettersArray(arrIndex).ToString
                    RandomLetter = RandomLetter.ToUpper
                End If
                sb.Append(RandomLetter)
            Else
                Do While arrIndex < 0
                    arrIndex = _
                      Convert.ToInt16(NumbersArray.GetUpperBound(0) _
                      * Random1)
                Loop
                sb.Append(NumbersArray(arrIndex))
            End If
        Next
        Return sb.ToString
    End Function

End Class