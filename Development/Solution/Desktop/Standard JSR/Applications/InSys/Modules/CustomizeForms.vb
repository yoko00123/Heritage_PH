

Module CustomizeForms

    Public cusfrm(0) As CustomizeContainer
    Public Sub getCustomizeForm(ByVal title As String, ByVal tab As TabPage)

        


        cusfrm(0).Dock = DockStyle.Fill
        cusfrm(0).Label1.Text = title


    End Sub


End Module
