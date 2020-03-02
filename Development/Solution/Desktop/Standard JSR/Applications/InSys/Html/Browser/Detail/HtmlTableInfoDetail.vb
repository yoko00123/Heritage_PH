Option Explicit On
Option Strict On


Public Class HtmlTableInfoDetail
    Inherits HtmlInfoDetail

    Public Property HelpMode As Boolean
    Public Sub New(ByVal pParent As HtmlContent, pHelpMode As Boolean)
        MyBase.New(pParent)
        Me.HelpMode = pHelpMode
    End Sub

    'Public ReadOnly Property TableHeaderColor() As String
    '    Get
    '        Return CStr(mMenuSet.tMenu.Rows(0).Item("ColorRGB"))
    '    End Get
    '    'Set(ByVal value As String)
    '    '    mHeaderColor = value
    '    'End Set
    'End Property


    Private Function PrintFieldCore(ByVal dr As DataRow, ByVal a As String) As String
        Dim sb As New System.Text.StringBuilder()
        Dim mSchemaRow As DataRow
        Dim s As String
        Dim o As Object

        If Strings.Left(a, 3) = "ID_" Then
            a = Strings.Right(a, a.Length - 3)
        End If
        Try
            mSchemaRow = DirectCast(dr.Table, GSCOM.SQL.ZDataTable).SchemaRow(a)
            sb.Append("<td>")
            o = dr.Item(a)
            If o IsNot DBNull.Value Then
                s = mSchemaRow("StringFormat").ToString
                If s <> "" Then
                    o = Strings.Format(o, s)
                End If
                If dr.Table.Columns(a).DataType Is GetType(Boolean) Then
                    o = o.ToString.ToUpper
                ElseIf dr.Table.Columns(a).DataType Is GetType(Decimal) Or dr.Table.Columns(a).DataType Is GetType(Int32) Then
                    If CDec(o) = 0 Then
                        o = "-"
                    End If
                End If
            End If
            sb.Append(o.ToString)
        Catch ex As Exception
            'MsgBox(ex.Message)
            sb.Append("***ERROR***" & a)
        End Try
        sb.Append("</td>")

        Return sb.ToString
    End Function

    Friend Function GetHtml(ByVal mdtr As Database.MenuDetailTabRow, Optional ByVal pFilter As String = "", Optional ByVal pWidth As String = "") As String
        Dim sb As New System.Text.StringBuilder
        Dim dt As GSCOM.SQL.ZDataTable
        Dim s As String
        Dim vFieldRows As DataRow()
        Dim vDataRows As DataRow()
        Dim pChildMenuDetailTab As DataRow()
        s = mdtr.TableName
        dt = CType(mParent.mDataSet.Tables(s), SQL.ZDataTable)
        pChildMenuDetailTab = mParent.mMenuSet.tMenuDetailTab.Select("ParentTableName=" & GSCOM.SQL.SQLFormat(mdtr.TableName))
        vDataRows = dt.Select(pFilter, dt.DefaultView.Sort)
        If vDataRows.Length > 0 Or Me.HelpMode Then
            sb.AppendLine(Me.GetPreLineFeeds)
            s = "(ID_MenuDetailTab=" & mdtr.ID.ToString & ") AND (Name NOT IN ('ID','Comment')) AND (ShowInBrowser=1)"
            vFieldRows = mParent.mMenuSet.tMenuDetailTabField.Select(s)
            s = CStr(IIf(pWidth <> "", pWidth, CStr(IIf(vFieldRows.Length >= 6, "100%", "50%"))))

            If Me.HelpMode Then
                sb.AppendLine("<table border=""0px"" cellpadding=""2px"" cellspacing=""0px"" width=""50%"">")
            Else
                sb.AppendLine("<table border=""1px"" cellpadding=""3px"" cellspacing=""0px"" width=""" & s & """ bordercolor=""#" & TabHeaderColor & """>")
            End If



            sb.AppendLine("<caption class=""zgroup""  style=""background-color:#" & TabHeaderColor & """>" & mdtr.Name & "</caption>")
            If Me.HelpMode Then
                PrintField(sb, vFieldRows)
            Else
                WriteFields(sb, dt, vFieldRows)
                WriteData(sb, vFieldRows, vDataRows, pChildMenuDetailTab)
            End If
            sb.AppendLine("</table>")
        End If
        Return sb.ToString
    End Function

    Private Sub WriteFields(sb As System.Text.StringBuilder, dt As GSCOM.SQL.ZDataTable, vFieldRows As DataRow())
        Dim s As String
        For Each dr As DataRow In vFieldRows
            s = CStr(dr("Name"))
            If Strings.Left(s, 3) = "ID_" Then
                s = Strings.Right(s, s.Length - 3)
            End If
            If Not dt.Columns.Contains(s) Then
                MsgBox(s & " is not in " & dt.TableName, MsgBoxStyle.Exclamation)
            End If
            Select Case dt.SchemaRow(s).Item("DataType").ToString.ToUpper   '    dr.Item("DataType").ToString.ToUpper
                Case "INT", "DECIMAL", "DATETIME", "NUMERIC"
                    sb.AppendLine("<col style=""white-space:nowrap;text-align:right""/>")
                Case "BIT"
                    sb.AppendLine("<col style=""white-space:nowrap;text-align:center;""/>")
                Case Else
                    sb.AppendLine("<col style=""white-space:nowrap""/>")
            End Select
        Next
        sb.Append("<tr class=""zColumnHeader""  style=""background-color:#" & mParent.mMenuRow.ColorRGB & """>")
        For Each dr As DataRow In vFieldRows
            s = dr("EffectiveLabel").ToString
            If s <> "ID" Then
                sb.Append("<th>")
                sb.Append(s)
                sb.Append("</th>")
            End If
        Next
        sb.AppendLine("</tr>")
    End Sub

    Private Sub WriteData(sb As System.Text.StringBuilder, vFieldRows As DataRow(), vDataRows As DataRow(), pChildMenuDetailTab As DataRow())
        Dim s As String
        For Each dr2 As DataRow In vDataRows
            sb.Append("<tr>")
            For Each dr As DataRow In vFieldRows
                s = dr("EffectiveLabel").ToString
                If s <> "ID" Then
                    sb.Append(PrintFieldCore(dr2, dr("Name").ToString).ToString)
                End If
            Next
            sb.AppendLine("</tr>")
            Dim wdt As HtmlTableInfoDetail
            For Each dr As DataRow In pChildMenuDetailTab
                wdt = New HtmlTableInfoDetail(mParent, Me.HelpMode)
                s = dr("ChildColumn").ToString & "=" & dr2(dr("ParentColumn").ToString).ToString
                sb.AppendLine("<tr>")
                sb.AppendLine("<td/>")
                sb.AppendLine("<td colspan=""" & vFieldRows.Length - 1 & """>")
                sb.AppendLine(wdt.GetHtml(New Database.MenuDetailTabRow(dr), s, "100%"))
                sb.AppendLine("</td>")
                sb.AppendLine("</tr>")
            Next
        Next
    End Sub

    Private Function PrintField(sb As System.Text.StringBuilder, vFieldRows As DataRow()) As String
        Dim mdtfr As Database.MenuDetailTabFieldRow
        For Each dr As DataRow In vFieldRows
            mdtfr = New Database.MenuDetailTabFieldRow(dr)
            Dim a As String
            a = mdtfr.Name
            If mdtfr.ShowInBrowser And a <> "ID" Then
                If Strings.Left(a, 3) = "ID_" Then
                    a = Strings.Right(a, a.Length - 3)
                End If
                sb.Append("<tr>")
                sb.Append("<td class=""zProperty"">") 'STYLES WORK IN IE BUT NOT IN OPERA 'sb.Append("<td>")
                sb.Append(mdtfr.EffectiveLabel & ":")
                sb.Append("</td>")
                sb.Append("<td>" & mdtfr.Description & "</td>")
                sb.AppendLine("</tr>")
            End If
        Next
        Return sb.ToString
    End Function

End Class

'Else
'    MsgBox("Not in schematable")
'End If
'If mMenuRow.Table.Columns(a).DataType Is GetType(System.Boolean) Then
'    If o IsNot DBNull.Value Then
'        sb.Append("<img width=""16px"" height=""16px"" src=""")
'        If CBool(o) Then
'            sb.Append(mResourcePath & "CheckedTrue2.bmp")
'        Else
'            sb.Append(mResourcePath & "CheckedFalse2.bmp")
'        End If
'        sb.Append("""/>")
'    End If
'Else
'    sb.Append(o.ToString)
'End If
'If TypeOf o Is Boolean Then
'    Select Case CBool(o)
'        Case True
'            o = "Yes"
'        Case False
'            o = "No"
'    End Select
'End If