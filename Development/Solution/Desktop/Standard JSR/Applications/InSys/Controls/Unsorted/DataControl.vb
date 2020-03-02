Friend Class DataControl
    Inherits TabControl

    Dim mTab As New TabControl

    Public Sub New()
        Me.Multiline = True
        Me.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(2)
    End Sub

    Public Sub SetTabPage(ByVal pTabPage As TabPage)
        pTabPage.Padding = New System.Windows.Forms.Padding(3)
        pTabPage.UseVisualStyleBackColor = True
        Me.Controls.Add(pTabPage)
    End Sub

    Public Function AddTable(ByVal pName As String, ByVal pHasTable As Boolean, ByVal pPanel As Integer) As GSCOM.UI.DataTabPage.DataTabPage
        Dim tp As New GSCOM.UI.DataTabPage.DataTabPage(pHasTable, pName, pPanel)
        Me.SetTabPage(tp)
        Return tp
    End Function

    Public Function AddTabPageWithTextBox(ByVal pName As String) As GSCOM.UI.DataTabPage.DataTabPage
        Dim c As New GSCOM.UI.DataTabPage.DataTabPage(False, pName, 1)
        c.AddTextBox(pName)
        Me.SetTabPage(c)
        Return c
    End Function

    Public Function ReturnControl(ByVal pName As String) As System.Windows.Forms.Control
        Dim a As Control = Nothing
        Dim cc() As Control = Nothing
        GSCOM.UI.AllControls(cc, Me)
        If cc IsNot Nothing Then
            For Each c As Control In cc
                If c.Name = pName Then
                    a = c
                End If
            Next
        End If
        Return a
    End Function

End Class
