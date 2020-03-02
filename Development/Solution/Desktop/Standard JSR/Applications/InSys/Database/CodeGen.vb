Option Explicit On
Option Strict On


Friend Class CodeGen
#Region "MenuEnumeration"
    Private Function MenuEnumeration(ByVal pConnection As SqlClient.SqlConnection) As String
        Dim sb As New Text.StringBuilder
        Dim s As String = ""
        Dim dt As DataTable
        dt = GSCOM.SQL.TableQuery("SELECT [ID],[Name],[ID_Menu] FROM tMenu WHERE ID_MenuType=1 ORDER BY [Name]", pConnection)
        PopulateMenuString(s, dt, DBNull.Value)
        sb.Append("Public Enum Menu")
        sb.Append(vbCrLf)
        sb.Append(s)
        sb.Append("End Enum")
        Return sb.ToString
    End Function

#End Region

#Region "GetMenuStrings"
    Private Sub PopulateMenuString(ByRef pString As String, ByVal dt As DataTable, ByVal vID As Object, Optional ByVal vPrefix As String = "")
        Dim dr As DataRow
        Dim dra As DataRow()
        Dim s As String
        Try
            If vID Is DBNull.Value Then
                pString = ""
                s = "ID_Menu IS " & GSCOM.SQL.SQLFormat(vID)
            Else
                s = "ID_Menu=" & GSCOM.SQL.SQLFormat(vID)
            End If
            dra = dt.Select(s)
            For Each dr In dra
                s = dr.Item("Name").ToString
                s = s.Replace(" ", "")
                s = s.Replace("(", "")
                s = s.Replace(")", "")
                s = s.Replace("'", "")
                s = s.Replace("-", "")
                s = s.Replace(".", "")
                s = s.Replace(":", "")
                s = s.Replace(";", "")
                s = s.Replace("?", "")
                s = s.Replace("/", "")
                s = s.Replace("&", "")
                pString &= "[" & vPrefix & s & "] = " & dr.Item("ID").ToString & vbCrLf
                PopulateMenuString(pString, dt, dr.Item("ID"), vPrefix & s & "_")
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "GetTableGenCode"
    Public Function GetTableGenCode(ByVal pConnection As SqlClient.SqlConnection) As String
        Dim dt As DataTable
        'Dim ts As GSCOM.SQL.SchemaTable
        'Dim tName As String
        Dim s As String
        Dim sa() As String
        Dim ret As String = ""
        Try
            ret = String.Format(My.Resources.resGenCodeHeader, Now)
            'dt = GSCOM.SQL.TableQuery("SELECT TOP 100 PERCENT [name] FROM sysobjects WHERE (xtype = 'u') AND name <> 'dtproperties' ORDER BY [Name]", pConnection)
            Dim q As String
            q = "SELECT DISTINCT"
            q &= " so.Name [TableName]"
            q &= " ,sc.[name] [ColumnName]"
            q &= " ,sc.ColOrder"
            q &= " FROM	syscolumns sc "
            'q &= " INNER JOIN systypes st ON st.xtype = sc.xtype "
            q &= " INNER JOIN sysobjects so ON so.id = sc.id "
            q &= " inner JOIN tTable t on t.Name = so.Name"
            q &= " WHERE(sc.number = 0)"
            q &= " and (t.IsForCodeGen = 1)"
            q &= " ORDER BY so.name, sc.colorder"
            dt = GSCOM.SQL.TableQuery(q, pConnection)


            Dim sc As New Collections.Specialized.StringCollection

            Dim tn As String
            For Each dr As DataRow In dt.Select
                tn = dr("TableName").ToString
                If Not sc.Contains(tn) Then
                    sc.Add(tn)
                End If
            Next
            For Each tn In sc
                Debug.WriteLine(tn)
                q = "TableName=" & GSCOM.SQL.SQLFormat(tn)
                sa = GSCOM.SQL.ColumnStringValues(dt.Select(q), "ColumnName", "[", "]")
                s = Strings.Join(sa, vbCrLf)
                s = String.Format(My.Resources.resTableTemplate, tn, s)
                ret &= s & vbCrLf & vbCrLf
            Next

            ret &= vbCrLf & "End Namespace"
            ret &= vbCrLf & vbCrLf & MenuEnumeration(pConnection) & vbCrLf
            ret &= vbCrLf & vbCrLf & GetEnumeration(pConnection, "tBank") & vbCrLf
            ret &= vbCrLf & vbCrLf & GetEnumeration(pConnection, "tSetting") & vbCrLf
            ret &= vbCrLf & vbCrLf & GetEnumeration(pConnection, "tSystemControlType") & vbCrLf
            ret &= vbCrLf & vbCrLf & GetEnumeration(pConnection, "tMenuDetailTabType") & vbCrLf
            ret &= vbCrLf & vbCrLf & GetEnumeration(pConnection, "tAuditTrailType") & vbCrLf
            'ret &= vbCrLf & vbCrLf & GetEnumeration("(select ID, Name from sysobjects where xtype = 'P' AND status <> -536870912) A", "StoredProcedure") & vbCrLf
            Return ret
        Catch ex As Exception

            Throw ex
        End Try
    End Function

#End Region

#Region "GetEnumeration"
    Private Function GetEnumeration(ByVal pConnection As SqlClient.SqlConnection, ByVal pTableName As String, Optional ByVal pName As String = "") As String
        Dim sb As New Text.StringBuilder
        Dim s As String = ""
        Dim a As String = ""
        Dim dt As DataTable
        dt = GSCOM.SQL.TableQuery("SELECT [ID],[Name] FROM " & pTableName & " ORDER BY [Name]", pConnection)
        For Each dr As DataRow In dt.Rows
            a = dr.Item("Name").ToString
            a = a.Replace(" ", "")
            a = a.Replace("(", "")
            a = a.Replace(")", "")
            a = a.Replace("'", "")
            a = a.Replace("-", "")
            s &= "[" & a & "] = " & dr.Item("ID").ToString & vbCrLf
        Next
        'PopulateMenuString(s, dt, DBNull.Value)
        If pName = "" Then
            pName = Strings.Right(pTableName, pTableName.Length - 1)
        End If
        sb.Append("Public Enum " & pName & "Enum")
        sb.Append(vbCrLf)
        sb.Append(s)
        sb.Append("End Enum")
        Return sb.ToString
    End Function

#End Region

End Class