Option Explicit On
Option Strict On

Namespace Templates

    Public Class EmployeeLeaveCreditFileTable
        Inherits DataTable

        Public Sub New()
            With Me.Columns
                .Add("EmployeeCode", GetType(String))
                .Add("Employee", GetType(String))
            End With
        End Sub
    End Class

End Namespace
