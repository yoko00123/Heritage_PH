Public Class ParameterDataTable
    Inherits DataTable
    Public Sub New()
        MyBase.New()
        MyBase.Columns.Add("Parameter", GetType(System.String))
        MyBase.Columns.Add("Value", GetType(System.String))
    End Sub
End Class