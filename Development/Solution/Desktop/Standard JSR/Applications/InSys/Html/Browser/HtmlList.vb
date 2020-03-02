Option Explicit On
Option Strict On

Public Class HtmlList

#Region "Declarations"
    Private mHeaderBackColor As String = "cccccc"
    Private mOddBackColor As String = "ffffff"
    Private mRowHeadersWidth As Integer = 24
    Private mRowHeight As Integer = 18
    Private mSettingsKey As String = String.Empty
    Public Property ImageFile As String

    Public mMenuSet As DataTable
    Public TabHeaderColor As String = "808080"
    Public CaptionBackColor As String = "c0c0c0"
    Public CaptionForeColor As String = "000000"
    Public SchemaTable As GSCOM.SQL.SchemaTable
    Public EvenBackColor As String = "ffffff"
    Public MenuID As Integer
    Public Property ShowID As Boolean = True
    Public Property UseHilight As Boolean = False
    Public Property LoadOnClick As Boolean = True

    Public Property ResourcePath As String
    Public Property Name As String
    Public Property StyleSheet As String
    Public Property ScriptFile As String
    Public Event RefreshedColors(ByVal sender As Object, ByVal e As EventArgs) 'Implements Interfaces.IDataListGrid.RefreshedColors
#End Region

    Private Function GetHtmlHeader() As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("<html xmlns=""http://www.w3.org/1999/xhtml"">")
        sb.AppendLine("<head>")
        sb.AppendLine("<link rel=""stylesheet"" type=""text/css"" href=""" & StyleSheet & """/>")
        sb.AppendLine("<script language=""javascript"" type=""text/javascript"">")
        If Me.UseHilight Then
            sb.AppendLine("function fselect(sExpID,sRow,scolor){")
            sb.AppendLine("var divname;")
            sb.AppendLine("divname = document.getElementById('id' + sExpID);")
            sb.AppendLine("divname.style.backgroundColor = scolor;")
            sb.AppendLine("}")
            sb.AppendLine("function fhilight(sExpID){")
            sb.AppendLine("var divname;")
            sb.AppendLine("divname = document.getElementById('id' + sExpID);")
            sb.AppendLine("divname.style.backgroundColor = 'Orange';")
            sb.AppendLine("}")
        End If
        If Me.LoadOnClick Then
            sb.AppendLine("function loadInfo(pID) {")
            'sb.AppendLine("var a;")
            'sb.AppendLine("a = 'infoframe.aspx?guid=76d26561-3619-4262-8244-0025a9d8dafa&menu=9&id=' + pID;")
            sb.AppendLine("location.href = pID")
            sb.AppendLine("}")
        End If
        sb.AppendLine("</script>")
        sb.AppendLine("</head>")
        sb.AppendLine("<body>")
        'sb.Append("<br/>")
        Return sb.ToString
    End Function

    Public Property StandAlone() As Boolean = True
    Public Property ShowCaption() As Boolean = True


    Private Function GetTableFooter(ByVal vDataRows As DataRow(), ByVal pColsPan As Integer) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<tfoot>")
        sb.AppendLine("<tr style=""background-color:#" & CaptionBackColor & ";color:#" & CaptionForeColor & """>")
        sb.Append("<th class=""zfooter"" colspan=""" & pColsPan & """>")
        If vDataRows.Length = 0 Then
            sb.Append("No record found")
        ElseIf vDataRows.Length = 1 Then
            sb.Append("1 record found")
        Else
            sb.Append(vDataRows.Length.ToString & " records found.")
        End If
        sb.Append("</th>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</tfoot>")
        Return sb.ToString
    End Function

    Private Function GetHilightScript(ByVal dr As DataRow, ByVal i As Integer) As String
        Dim sb As New System.Text.StringBuilder
        Dim a, b As String
        Dim c As String = ""
        Dim vID As String
        vID = dr("ID").ToString
        a = "onMouseOver=""javascript:fhilight('" & vID & "')"""
        If Me.LoadOnClick Then
            c = " onClick=""javascript:loadInfo(" & vID & ")"""
        End If
        If i Mod 2 = 0 Then
            b = "onMouseOut=""javascript:fselect('" & vID & "','" & i & "','#" & mOddBackColor & "')"""
            sb.AppendLine("<tr id=""id" & vID & """ style=""background-color:#" & mOddBackColor & " ;"" " & a & " " & b & c & " >")
        Else
            b = "onMouseOut=""javascript:fselect('" & vID & "','" & i & "','#" & EvenBackColor & "')"""
            sb.AppendLine("<tr id=""id" & vID & """ style=""background-color:#" & EvenBackColor & ";"" " & a & " " & b & c & " >")
        End If
        Return sb.ToString
    End Function


    Public Function GetHtml(ByVal dt As DataTable) As String
        Dim sb As New System.Text.StringBuilder
        'Dim dt As GSCOM.SQL.ZDataTable
        Dim s As String
        ' Dim vFieldRows As DataRow()
        Dim vDataRows As DataRow()
        If Me.StandAlone Then
            sb.AppendLine(Me.GetHtmlHeader)
        End If
        'If pChildMenuDetailTab.Length > 0 Then
        '    MsgBox(pChildMenuDetailTab.Length)
        'End If
        vDataRows = dt.Select(dt.DefaultView.RowFilter, dt.DefaultView.Sort)
        If vDataRows.Length > 0 Then
            s = "100%"
            sb.AppendLine("<table id=""" & TableID & """ border=""1px"" cellpadding=""3px"" cellspacing=""0px"" width=""" & s & """ bordercolor=""#" & TabHeaderColor & """>")
            'sb.AppendLine("<div class=""zgroup"">" & pMenuDetailTab.Item("Name").ToString() & "</div>")
            If Me.ShowCaption Then
                sb.AppendLine("<caption class=""zgroup""  style=""background-color:#" & TabHeaderColor & """>" & Me.Name & "</caption>")
            End If
            For Each dc As DataColumn In dt.Columns
                If dc.ColumnName <> "ID" Or ShowID Then
                    s = dc.ColumnName
                    If Strings.Left(s, 3) = "ID_" Then
                        s = Strings.Right(s, s.Length - 3)
                    End If
                    'If Not dt.Columns.Contains(s) Then
                    '    MsgBox(s & " is not in " & SchemaTable.SchemaRow(s), MsgBoxStyle.Exclamation)
                    '    'GoTo L1
                    'End If
                    Select Case SchemaTable.SchemaRow(s).Item("DataType").ToString.ToUpper   '    dr.Item("DataType").ToString.ToUpper
                        Case "INT", "DECIMAL", "DATETIME", "NUMERIC"
                            If s = "ID" Then
                                sb.AppendLine("<col style=""white-space:nowrap;text-align:right""/>")
                            Else
                                sb.AppendLine("<col style=""white-space:nowrap;text-align:right""/>")
                            End If
                        Case "BIT"
                            sb.AppendLine("<col style=""white-space:nowrap;text-align:center;""/>")
                        Case Else
                            If s = "ImageFile" Then
                                sb.AppendLine("<col style=""white-space:nowrap;text-align:center;""/>")
                            Else
                                sb.AppendLine("<col style=""white-space:nowrap""/>")
                            End If
                    End Select
                End If
            Next
            ''''''''''''''''''''''''''''''------------------------------------/
            sb.AppendLine("<thead>")
            sb.Append("<tr class=""zColumnHeader""  style=""background-color:#" & CaptionBackColor & ";color:#" & CaptionForeColor & """>")
            Dim mtfr As New Database.MenuTabFieldRow
            'Dim dtx As DataTable
            Dim col As String
            For Each dc As DataColumn In dt.Columns
                If dc.ColumnName <> "ID" Or ShowID Then
                    sb.Append("<th>")

                    col = dc.Caption
                    'If Strings.Left(col, 3) = "ID_" Then
                    '    col = 
                    'End If

                    's = "MenuTabMenuID=" & MenuID.ToString
                    's &= " AND Name=" & GSCOM.SQL.SQLFormat(dc.ColumnName)
                    'dtx = Database.MenuSet.tMenuTabField
                    'mtfr.InnerRow = dtx.Select(s)(0)
                    sb.Append(col)


                    sb.Append("</th>")
                End If
            Next
            sb.AppendLine("</tr>")
            sb.AppendLine("</thead>")
            Dim i As Integer
            For Each dr2 As DataRow In vDataRows
                'sb.Append("<tr style=""height:18"">")
                If UseHilight Then
                    sb.AppendLine(Me.GetHilightScript(dr2, i))
                Else
                    If (i Mod 2 = 0) OrElse EvenBackColor = "" Then
                        sb.Append("<tr>")
                    Else
                        sb.Append("<tr style=""background-color:#" & EvenBackColor & """>")
                    End If
                End If

                For Each dc As DataColumn In dt.Columns
                    If dc.ColumnName <> "ID" Or ShowID Then
                        sb.Append(PrintFieldCore(dr2, dc.ColumnName))
                    End If
                Next
                sb.AppendLine("</tr>")
                i += 1
            Next
            sb.AppendLine(Me.GetTableFooter(vDataRows, dt.Columns.Count - CInt(IIf(dt.Columns.Contains("ID") And Not ShowID, 1, 0))))
            sb.AppendLine("</table>")
        End If
        If Me.StandAlone Then
            sb.Append(Me.GetHtmlFooter)
        End If
        Return sb.ToString
    End Function

    Public Property TableID As String

    Private Function GetHtmlFooter() As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("</body>")
        sb.Append("</html>")
        Return sb.ToString
    End Function

    Private Function PrintFieldCore(ByVal dr As DataRow, ByVal a As String) As String
        Dim sb As New System.Text.StringBuilder()
        Dim mSchemaRow As DataRow
        Dim s As String
        Dim o As Object

        If Strings.Left(a, 3) = "ID_" Then
            a = Strings.Right(a, a.Length - 3)
        End If
        Try
            mSchemaRow = SchemaTable.SchemaRow(a)
            sb.Append("<td>")
            If a = "ImageFile" Then
                sb.Append("<img src=""")
                Dim tmp As String
                tmp = dr("ImageFile").ToString
                If tmp = "" Then
                    tmp = nDB.ImagePath(Me.ImageFile)
                Else
                    tmp = nDB.ImagePath(tmp)
                End If
                sb.Append(tmp)
                sb.AppendLine(""" width=""32px"" height=""32px"" alt=""" & tmp & """/>")
            Else
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
            End If




        Catch ex As Exception
            'MsgBox(ex.Message)
            sb.Append("***ERROR***" & a)
        End Try
        sb.Append("</td>")

        Return sb.ToString
    End Function



End Class





#Region "Comments"
'        Public Sub SortColumn()
'            Dim i As Integer
'            Dim p As String
'            Dim s As String
'            Dim b As Boolean
'            'Dim h As Boolean
'            Dim lvi As ListViewItem
'            Try
'                b = (Me.Columns.Count  0) And (mSortColumnIndex = 0) And (mSortColumnIndex  Me.Columns.Count)
'                If b Then
'                    s = GetColumn(mSortColumnIndex).DataColumn.DataType.ToString.ToUpper
'                    Select Case s
'                        Case SYSTEM.DATETIME
'                            Me.ListViewItemSorter = New ListViewItemDateTimeComparer(mSortColumnIndex)
'                        Case SYSTEM.INT32, SYSTEM.DECIMAL
'                            Me.ListViewItemSorter = New ListViewItemNumericComparer(mSortColumnIndex)
'                        Case Else
'                            Me.ListViewItemSorter = New ListViewItemStringComparer(mSortColumnIndex)
'                    End Select
'                    mEraseBackground = False
'                    'Try
'                    Me.Sort()
'                    'Catch ex As Exception
'                    'End Try
'                    Me.ListViewItemSorter = Nothing
'                    RefreshColors()
'                End If
'            Catch ex As Exception
'            Finally
'                Application.DoEvents()
'                mEraseBackground = True
'            End Try
'        End Sub
#End Region

