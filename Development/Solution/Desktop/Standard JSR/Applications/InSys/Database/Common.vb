Public Module Common

#Region "gPassParameters"
    Public Function gPassParameters(ByVal pt As ParameterDataTable, ByVal s As String, Optional ByRef NullFields As String = "") As String
        Dim f As String
        Dim r As String
        If s <> "" Then
            For Each dr As DataRow In pt.Select("", "Parameter DESC") 'IMPORTANT, e.g. $ID, $ID_Menu 
                f = dr("Parameter").ToString
                r = dr("Value").ToString
                If r Is DBNull.Value Then
                    NullFields &= r.ToString & ","
                End If
                s = Replace(s, f, r)
            Next
        End If
        'reconsider
        s = s.Replace("#Date", "<b>" & Strings.Format(Date.Now.Date, GSCOM.Common.DefaultDateFormat) & "<b/>")
        Return s
    End Function
#End Region

#Region "GetTableGenCode"
    Public Function GetTableGenCode(ByVal pConnection As SqlClient.SqlConnection) As String
        Dim a As New CodeGen
        Return a.GetTableGenCode(pConnection)
    End Function

#End Region

    Public Function fUser(ByVal pName As Object, ByVal pPassword As Object, ByVal pConnection As SqlClient.SqlConnection) As DataTable
        Dim s As String
        s = "SELECT * FROM dbo.fUser (" & GSCOM.SQL.SQLFormat(pName) & ", " & GSCOM.SQL.SQLFormat(pPassword) & ")"
        Return GSCOM.SQL.TableQuery(s, pConnection)
    End Function


    Public Function LogInCompanyTable(ByVal pUserID As Integer, ByVal pConnection As SqlClient.SqlConnection) As DataTable
        Dim s As String
        s = "SELECT c.ID,c.Name FROM tCompany c"
        s &= " CROSS JOIN tUser u"
        s &= " INNER JOIN tUserGroup ug ON ug.ID=u.ID_UserGroup"
        s &= " INNER JOIN tUserGroupCompany ugc ON c.ID=ugc.ID_Company AND ug.ID=ugc.ID_UserGroup"
        s &= " WHERE (u.ID=" & pUserID.ToString & ")"
        's &= " UNION"
        's &= " SELECT c.ID,c.Name FROM tCompany c"
        's &= " INNER JOIN tEmployee e ON c.ID_Company=e.ID_Employee"
        's &= " WHERE (u.ID=" & pUserID.ToString & ")"

        Return GSCOM.SQL.TableQuery(s, pConnection)
    End Function


End Module
