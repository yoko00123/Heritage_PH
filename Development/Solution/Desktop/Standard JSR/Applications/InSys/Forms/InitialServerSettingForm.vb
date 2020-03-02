Imports System.Threading.Tasks
Imports System.Data.SqlClient

Public Class InitialServerSettingForm

    Private serversettingfile As String

    Public Sub New(serverfile As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        serversettingfile = serverfile
        Me.DialogResult = Windows.Forms.DialogResult.Cancel 'set default

    End Sub

    Private Sub InitialServerSettingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ServerSaveButton_Click(sender As Object, e As EventArgs) Handles ServerSaveButton.Click
        Try
            If Me.ServerBox.Text.Trim = "" Then Throw New Exception("Please Specify Server")
            If Me.DBUserNameBox.Text.Trim = "" Then Throw New Exception("Please Specify User")
            If Me.DBPasswordBox.Text.Trim = "" Then Throw New Exception("Please Specify Password")
            If Me.DBBox.Text.Trim = "" Then Throw New Exception("Please Specify Database")

            Me.pbar.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Me.ServerSaveButton.Enabled = False

            Dim args As New ServerArgs With {.server = Me.ServerBox.Text, .user = Me.DBUserNameBox.Text, .password = DBPasswordBox.Text, .database = DBBox.Text}

            Task.Factory.StartNew(Sub() CheckConnectionSetting(args, Sub(a) EnableUI(a)))

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub CheckConnectionSetting(e As ServerArgs, action As Action(Of Boolean))
        Try
            Using sqlcon As New SqlConnection(e.GetArguments)
                sqlcon.Open()
                sqlcon.Close()
                MessageBox.Show("Connected")
                action(True)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            action(False)
        End Try
    End Sub

    Delegate Sub dgEnableUI(ByVal a As Boolean)
    Sub EnableUI(a As Boolean)
        If Me.InvokeRequired Then
            Me.Invoke(New dgEnableUI(AddressOf EnableUI), a)
        Else
            Me.pbar.Visible = False
            Me.Cursor = Cursors.Default
            Me.ServerSaveButton.Enabled = True

            'Save current Settings
            If a Then
                CreateServerSettingFile(Me.ServerBox.Text, Me.DBBox.Text, Me.DBUserNameBox.Text, Me.DBPasswordBox.Text)
                Windows.Forms.Application.UserAppDataRegistry.SetValue("companyid", 1)
                Windows.Forms.Application.UserAppDataRegistry.SetValue("company", "Giraffe")
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub

    Public Class ServerArgs
        Inherits EventArgs

        Public Property server As String
        Public Property user As String
        Public Property password As String
        Public Property database As String

        Public ReadOnly Property GetArguments As String
            Get
                Dim s As String
                s = "Password=" & password & ""
                s &= ";Persist Security Info=True"
                s &= ";User ID=" & user & ";"
                s &= "Initial Catalog=" & database & ";"
                s &= "Data Source=" & server
                Return s
            End Get
        End Property

    End Class

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.pbar.Value += 1
        If pbar.Value >= Me.pbar.Maximum Then pbar.Value = 0
    End Sub
End Class