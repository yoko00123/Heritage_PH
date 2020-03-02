Option Explicit On
Option Strict On


Public Class HtmlTab

    'Inherits HtmlContent

    Private mParent As HtmlContent
    Private mEditMode As Boolean
    Private mHelpMode As Boolean
    Public Sub New(ByVal pParent As HtmlContent, ByVal pEditMode As Boolean, ByVal pHelpMode As Boolean)
        ' MyBase.New(pMenuSet, pDataSet)
        mParent = pParent
        mEditMode = pEditMode
        mHelpMode = pHelpMode
    End Sub

    Public ReadOnly Property TabHeaderColor() As String
        Get
            Return mParent.mMenuRow.DarkColorRGB
        End Get
    End Property

    Public ReadOnly Property TableHeaderColor() As String
        Get
            Return mParent.mMenuRow.ColorRGB
        End Get
    End Property

    Public Function GetHtml(ByVal mtr As Database.MenuTabRow, ByVal pFull As Boolean) As String
        Dim sb As New System.Text.StringBuilder()
        sb.Append("<div class=""zgroup"" style=""background-color:#" & TabHeaderColor & """>")
        sb.AppendLine(mtr.Name & "</div>")
        '   sb.Append("<caption>" & dr("Name").ToString & "</caption>")
        sb.AppendLine("<table border=""0px"" cellpadding=""0px"" cellspacing=""4px"" width=""100%"">")
        sb.Append("<tr>")
        If pFull Then
            sb.Append("<td style=""width:100%"">")
        Else
            sb.Append("<td style=""width:50%"">")
        End If
        sb.AppendLine(PrintTabCore(mtr, 1))
        sb.Append("</td>")
        If Not pFull Then
            sb.Append("<td style=""width:50%"">")
            sb.AppendLine(PrintTabCore(mtr, 2))
            sb.Append("</td>")
        End If
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")
        Return sb.ToString
    End Function



    Private Function PrintFieldCore(ByVal mr As Database.MenuTabFieldRow) As String
        Dim a As String = mr.Name
        Dim dr As DataRow = mParent.mHeaderRow
        Dim sb As New System.Text.StringBuilder()
        Dim mSchemaRow As DataRow
        Dim s As String
        Dim o As Object
        Dim vRightAligned As Boolean
        Dim vIsBoolean As Boolean
        Dim vIsTrue As Boolean
        'Dim vIsForeignKey As Boolean = Strings.Left(a, 3) = "ID_"
        'Dim vIsComboBox As Boolean = vIsForeignKey And IsDBNull(mr.ID_Menu)
        If mr.IsForeignKey Then
            a = Strings.Right(a, a.Length - 3)
        End If
        Try
            mSchemaRow = DirectCast(dr.Table, GSCOM.SQL.ZDataTable).SchemaRow(a)
            Dim vDataType As String
            vDataType = mSchemaRow.Item("DataType").ToString.ToUpper
            Select Case vDataType
                Case "INT", "DECIMAL", "DATETIME", "NUMERIC"
                    vRightAligned = True
                Case "BIT"
                    vIsBoolean = True
            End Select
            Dim vIsDate As Boolean = (vDataType = "DATETIME")

            If dr.Table Is mParent.mHeaderRow.Table Then
                If mEditMode OrElse (Not vRightAligned) Then
                    'If Not vRightAligned Then
                    sb.Append("<td>")
                    'sb.Append("<td style=""text-align:left;"">")
                Else
                    sb.Append("<td style=""text-align:right;"">")
                End If
            Else
                sb.Append("<td>")
            End If
            o = dr.Item(a)
            If o IsNot DBNull.Value Then
                If vIsBoolean Then
                    vIsTrue = CBool(o)
                End If
                s = mSchemaRow("StringFormat").ToString
                If s <> "" Then
                    o = Strings.Format(o, s)
                End If
                If dr.Table.Columns(a).DataType Is GetType(Boolean) Then
                    o = o.ToString.ToUpper
                End If

            End If
            If mEditMode Then
                If vIsBoolean Then
                    sb.Append("<input type=""checkbox""")
                    If vIsTrue Then
                        sb.Append(" checked=""checked""")
                    End If
                    sb.Append(" style=""width:auto;""") 'FOR ALIGNMENT
                    'If vRightAligned Then
                    'sb.Append(" style=""text-align:left;""")
                    'End If
                    sb.Append("/>")
                Else
                    If False And vIsDate Then
                        sb.Append("<input type=""date""")
                        sb.Append(" value=""" & o.ToString & """")
                        sb.Append(" style=""text-align:right;""")
                        sb.Append("/>")
                    Else
                        If mr.IsComboBox Then
                            sb.Append("<select>")
                            'sb.Append("<option>" & o.ToString & "</option>")
                            sb.Append(Html.GetLookUp(mr.Name, dr(mr.Name)))
                            sb.Append("</select>")
                        ElseIf mr.ID_SystemControlType = Database.SystemControlTypeEnum.DockedTextBox Then
                            sb.Append("<textarea")
                            If vRightAligned Then
                                sb.Append(" style=""text-align:right;""")
                            End If
                            sb.Append(">")
                            sb.Append(System.Web.HttpUtility.HtmlEncode(o))
                            sb.Append("</textarea>")

                        Else
                            sb.Append("<input type=""text""")
                            sb.Append(" value=""" & System.Web.HttpUtility.HtmlEncode(o) & """")
                            If vRightAligned Then
                                sb.Append(" style=""text-align:right;""")
                            End If
                            sb.Append("/>")
                        End If
                    End If
                End If
            Else
                sb.Append(o.ToString)
            End If




        Catch ex As Exception
            'MsgBox(ex.Message)
            sb.Append(ex.Message)
        End Try
        sb.Append("</td>")
        Return sb.ToString
    End Function

    Private Function PrintTabCore(ByVal mtr As Database.MenuTabRow, ByVal pPanel As Integer) As String
        Dim sb As New System.Text.StringBuilder()
        Dim b As Boolean
        sb.AppendLine("<table border=""0px"" cellpadding=""2px"" cellspacing=""0px"" width=""100%"">")
        b = CBool(GSCOM.SQL.ExecuteScalar("SELECT CanViewEmployeeSalary FROM tUserGroup ug INNER JOIN tUser u on ug.id=u.ID_UserGroup WHERE u.ID=" & nDB.GetUserID.ToString, nDB.Connection))
        'STYLES WORK IN IE BUT NOT IN OPERA
        'sb.AppendLine("<col class=""zProperty"" align=""right"" />")
        'sb.AppendLine("<col/>")
        Dim mtfr As New Database.MenuTabFieldRow
        Dim dv As New DataView(mParent.mMenuSet.tMenuTabField, "ID_Menutab=" & mtr.ID.ToString & " AND Panel='" & pPanel & "'" & If(b, "", " AND IsSalaryAuthenticatedField = 0"), "", DataViewRowState.CurrentRows)
        For Each dr2 As DataRowView In dv
            'For Each dr2 As DataRow In mParent.mMenuSet.tMenuTabField.Select("ID_Menutab=" & mtr.ID.ToString & " AND Panel='" & pPanel & "'")
            mtfr.InnerRow = dr2.Row
            sb.Append(PrintField(mtr, mtfr))

        Next
        sb.AppendLine("</table>")
        Return sb.ToString
    End Function

    Private Function PrintField(ByVal mtr As Database.MenuTabRow, ByVal mtfr As Database.MenuTabFieldRow) As String
        Dim sb As New System.Text.StringBuilder()
        Dim a As String
        a = mtfr.Name
        If mtfr.ShowInBrowser And a <> "ID" Then
            If Strings.Left(a, 3) = "ID_" Then
                a = Strings.Right(a, a.Length - 3)
            End If
            sb.Append("<tr>")
            'STYLES WORK IN IE BUT NOT IN OPERA
            'sb.Append("<td>")
            sb.Append("<td class=""zProperty"">")
            If mtr.HasTable Then
                sb.Append(mtfr.EffectiveLabel & ":")
            End If
            sb.Append("</td>")
            If mHelpMode Then
                sb.Append("<td>" & mtfr.Description & "</td>")
            Else
                sb.Append(PrintFieldCore(mtfr))
            End If
            sb.AppendLine("</tr>")
        End If
        Return sb.ToString
    End Function



    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
