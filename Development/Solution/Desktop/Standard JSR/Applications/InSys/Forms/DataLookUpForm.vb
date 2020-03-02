


Friend Class DataLookUpForm

    Private mMenu As Database.Menu

    Public ReadOnly Property LookUp() As GSCOM.UI.DataLookUp.DataLookUp
        Get
            Return dl
        End Get
    End Property

    Public Sub New(ByVal pMenu As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.StartPosition = FormStartPosition.CenterScreen
        mMenu = CType(pMenu, GSCOM.Applications.InSys.Database.Menu)
        MainModule.InitLookUp(dl, mMenu)
        Me.Text = dl.Text
        Me.Controls.Add(dl)
    End Sub

    'Private mRow As DataRow

    Public ReadOnly Property Row() As DataRow
        Get
            Return dl.Worker.Row
        End Get
    End Property

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        If dl.SelectedValue IsNot DBNull.Value Then

            'Dim dtx As DataTable
            'Dim s As String
            's = nDB.GetMenuValue(mMenu, Database.Tables.tMenu.Field.TableName).ToString
            's = "v" & Strings.Right(s, s.Length - 1)
            's = "SELECT * FROM " & s & " WHERE ID=" & GSCOM.SQL.SQLFormat(dl.SelectedValue)
            'dtx = GSCOM.SQL.TableQuery(s, gConnection, False)
            'If dtx.Rows.Count > 0 Then
            '    mRow = dtx.Rows(0)
            'Else
            '    mRow = Nothing
            'End If


            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            MsgBox("Please select then click OK", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub DataLookUpForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        dl.Focus()
    End Sub
End Class