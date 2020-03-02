Public NotInheritable Class AboutForm

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim loginBG As String = IO.Path.Combine(GSCOM.SQL.ExecuteScalar("Select dbo.fGetSetting('ResourcePath')", gConnection).ToString + "BG\", GSCOM.SQL.ExecuteScalar("Select dbo.fGetSetting('LoginBG')", gConnection).ToString)
        If IO.File.Exists(loginBG) Then
            Dim b As Bitmap
            b = New Bitmap(loginBG)
            Me.BackgroundImage = b
        Else
            Me.BackgroundImage = Nothing
        End If
        'GSCOM.SQL.ExecuteScalar("Select Name From tSystemVersion Where IsActive = 1", gConnection).ToString

        ' Set the title of the form.
        Dim ApplicationTitle As String = GSCOM.SQL.ExecuteScalar("Select Name From tSystemVersion Where IsActive = 1", gConnection).ToString
        Me.Icon = gIcon
        'If My.Application.Info.Title <> "" Then
        '    ApplicationTitle = GSCOM.SQL.ExecuteScalar("Select Name From tSystemVersion Where IsActive = 1", gConnection).ToString
        'Else
        '    ApplicationTitle = GSCOM.SQL.ExecuteScalar("Select Name From tSystemVersion Where IsActive = 1", gConnection).ToString
        'End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.ProductName + vbCrLf
        Me.TextBoxDescription.Text &= String.Format("Version {0}", My.Application.Info.Version.ToString) + vbCrLf
        Me.TextBoxDescription.Text &= My.Application.Info.Copyright + vbCrLf
        Me.TextBoxDescription.Text &= My.Application.Info.CompanyName + vbCrLf + vbCrLf
        Me.TextBoxDescription.Text &= My.Application.Info.Description + vbCrLf
        Me.TextBoxDescription.Text &= vbCrLf & vbCrLf
        Me.TextBoxDescription.Text &= "Installed Applications: "
        Me.TextBoxDescription.Text &= vbCrLf & vbCrLf

        Dim dt As DataTable
        Dim a As String = ""
        Dim al As Specialized.StringCollection
        dt = GSCOM.SQL.TableQuery("SELECT Name FROM tSystemApplication WHERE IsActive=1", gConnection)
        al = GSCOM.Common.GetDistinctStrings(dt.Select, "Name")
        For Each s As String In al
            a &= "  - " & s & vbCrLf
        Next
        Me.TextBoxDescription.Text &= a

    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
