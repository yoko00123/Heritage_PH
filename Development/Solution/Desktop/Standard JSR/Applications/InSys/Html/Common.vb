Option Explicit On
Option Strict On




Public Module Common

    Public nDB As Database.UserSession


    Public Function GetLookUp(ByVal pColumnName As String, ByVal pSelected As Object) As String
        Dim sb As New System.Text.StringBuilder
        Dim o As Object
        Dim s As String
        s = "v"
        s &= Strings.Right(pColumnName, pColumnName.Length - ("ID_").Length)
        Dim dt As DataTable = nDB.GetLookUp(s)
        For Each dr As DataRow In dt.Select
            o = dr("Name")
            If dr("ID").ToString = pSelected.ToString Then
                sb.AppendLine("<option selected>" & o.ToString & "</option>")
            Else
                sb.AppendLine("<option>" & o.ToString & "</option>")
            End If
        Next
        Return sb.ToString
    End Function

    Friend Function dq(ByVal s As Object) As String
        dq = """" & s.ToString & """"
    End Function

End Module
