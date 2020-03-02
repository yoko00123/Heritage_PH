Option Explicit On
Option Strict On

Namespace Templates


    Public Class AttendanceTable
        Inherits DataTable

        Public Sub New()
            With Me.Columns
                .Add("Department", GetType(String))
                .Add("EnrollNumber", GetType(System.Int32))
                .Add("EmployeeCode", GetType(String))
                .Add("EmployeeName", GetType(String))
                .Add("Date", GetType(Date))
                .Add("TimeIn", GetType(Date))
                .Add("TimeOut", GetType(Date))
                .Add("ActualHours", GetType(Decimal))
                .Add("Tardy", GetType(Decimal))
                .Add("OT", GetType(Decimal))
                .Add("TotalHours", GetType(Decimal))
                .Add("ND", GetType(Decimal))
                .Add("Remarks", GetType(String))
            End With
        End Sub

    End Class

End Namespace
