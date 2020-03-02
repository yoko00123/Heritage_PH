Public Class EmployeeSelectorForm

    Private Sub EmployeeSelectorForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim ds As String = nDB.GetMenuValue(Database.Menu.HumanResource_Employee, Database.Tables.tMenu.Field.DataSource).ToString
        Dim ds As String = nDB.GetMenuValue(Database.Menu.INSYSPEOPLE_EmployeeRecords201File, Database.Tables.tMenu.Field.DataSource).ToString


    End Sub
End Class