Option Explicit On
Option Strict On


Public Class HtmlCalendarGrid
    Inherits HtmlTreeGrid

    Public Property DateColumnName As String = "Date"
    Public Property DisplayColumnName As String = "Name"
    Public Property AllowNew As Boolean = True

    Public Overrides Function GetHtml() As String
        Dim sb As New System.Text.StringBuilder
        Dim p As Integer
        Dim i As Integer
        Dim tdcolor As String
         Dim colorCtr As Integer = 0
        Dim RowSpan As Integer
        Dim myDay() As String
        myDay = {"SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT"}
        Dim myColor() As String
        myColor = {"GRAY"}
        Dim myMonth() As String = {"", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}

        Try

            sb.AppendLine(GetHtmlHeader)
            'sb.AppendLine("<center><img src=""" & nDB.GetSetting(Database.SettingEnum.ResourcePath) & "/_month.png"" height=""32"" width=""32"" align=""absbottom""><b><font face=""Verdana, Geneva, sans-serif"" size=""+1"">Calendar View</center><br />")
            'sb.AppendLine("</font></b>")
            sb.AppendLine("<table border=""1"" id=""myCalendar"" width=""100%""  bordercolor=""" & Me.CaptionBackColor & """>")
            sb.AppendLine("<thead>")
            sb.AppendLine("</thead>")

            sb.AppendLine("<tbody id=""maintablebody"">")
            Dim vYearMonth As New Collections.Specialized.StringCollection

            For Each drv As DataRowView In Me.DataSource.DefaultView
                If drv(DateColumnName) IsNot DBNull.Value Then
                    Dim s As String = Strings.Format(drv(DateColumnName), "yyyyMM")
                    If Not vYearMonth.Contains(s) Then
                        vYearMonth.Add(s)
                    End If
                End If
            Next

            For Each s As String In vYearMonth

                Dim vMonth As Integer = CInt(Strings.Right(s, 2))
                Dim vYear As Integer = CInt(Strings.Left(s, 4))

                Dim FirstDayOfMonth As Date = CDate(vMonth & "/1/" & vYear)

                Dim FirstDayOFMonthDW As Integer = FirstDayOfMonth.DayOfWeek
                Dim CellStart As Date = DateAdd(DateInterval.Day, -FirstDayOFMonthDW, FirstDayOfMonth)
                Dim DayStart As Date = DateAdd(DateInterval.Day, -FirstDayOFMonthDW, FirstDayOfMonth)
                Dim LastDayOfMonth As Date = DateAdd(DateInterval.Month, 1, FirstDayOfMonth)
                LastDayOfMonth = DateAdd(DateInterval.Day, -1, LastDayOfMonth)

                RowSpan = CInt(Math.Ceiling(((LastDayOfMonth.Day + FirstDayOFMonthDW) / 7))) + 1

                sb.AppendLine("<tr id=""columnheader"" style=""background-color:" & Me.CaptionBackColor & ";color:#ffffff"">")
                Dim o As Integer
                sb.AppendLine("<th rowspan=" & RowSpan & "><span class=""hgc_left"">" & MonthName(FirstDayOfMonth.Month) & " <sup>" & FirstDayOfMonth.Year & "</sup></span></th>")
                For o = 0 To 6
                    sb.AppendLine(vbTab & "<th class=""calendarheightday"" width=""150px"">" & myDay(o) & "</th>")
                Next
                sb.AppendLine("</tr>")

                colorCtr += 1
                colorCtr = colorCtr Mod 2
                Do While CellStart <= LastDayOfMonth
                    RowSpan += 1
                    
                    If DayStart.DayOfWeek = 0 Then
                        sb.AppendLine("<tr class=""alt_color" & colorCtr & """>")
                    End If
                    For p = 0 To 6
                        If CellStart.Date < FirstDayOfMonth.Date Or CellStart.Date > LastDayOfMonth.Date Then
                            tdcolor = " bgcolor=""white"""
                        Else
                            tdcolor = ""
                        End If
                        sb.Append("<td" & tdcolor & " class=""hcg_cell""")
                        sb.Append(" id=""_" & i & p & """")
                        'sb.Append(" onmouseover=""colorChange('_" & i & p & "')""")
                        'sb.Append(" onmouseout=""colorChange2('_" & i & p & "')""")
                        sb.Append(">")

                        If CellStart.Date >= FirstDayOfMonth.Date And CellStart.Date <= LastDayOfMonth.Date Then
                            If Me.AllowNew Then
                                sb.Append("<div class=""hcg_date"" data-date=""" & Format(CellStart.Date, "MM/dd/yyyy") & """>")
                            Else
                                sb.Append("<div class=""hcg_date"">")
                            End If
                            'sb.AppendLine(" style=""" & displayNone & """>")
                            sb.Append(CStr(CellStart.Day))
                            sb.Append("</div>")

                        End If

                        If Me.DataSource.Columns.Contains(DateColumnName) Then


                            For Each drv As DataRowView In Me.DataSource.DefaultView
                                If drv(DateColumnName) IsNot DBNull.Value Then
                                    Dim vCurrentDate As Date = CDate(drv(DateColumnName))
                                    If vCurrentDate.Date = CellStart.Date And vCurrentDate.Month = vMonth And vCurrentDate.Year = vYear Then
                                        sb.AppendLine()
                                        sb.Append(vbTab & "<a href=""""")
                                        sb.Append(" onclick=""fselectrow2(" & CStr(drv("ID")) & "); return false;"" id=""" & CStr(drv("ID")) & """ data-id=""" & CStr(drv("ID")) & """>")
                                        ' onmouseover=""this.style.color='RED'"" onmouseout=""this.style.color=''""
                                        If DisplayColumnName = "ImageFile" Then
                                            sb.Append("<img src=""")
                                            Dim tmp As String
                                            tmp = drv(DisplayColumnName).ToString
                                            If tmp = "" Then
                                                tmp = Me.ImagePath(Me.ImageFile)
                                            Else
                                                tmp = Me.ImagePath(tmp)
                                            End If
                                            sb.Append(tmp)
                                            sb.Append(""" alt=""" & "" & """/>")
                                        Else
                                            Dim dra() As DataRow
                                            Dim mSchemaRow As DataRow = Nothing
                                            dra = Me.SchemaTable.Select("ColumnName=" & GSCOM.SQL.SQLFormat(DisplayColumnName))
                                            If dra.Length > 0 Then
                                                mSchemaRow = dra(0)
                                            Else
                                            End If
                                            sb.Append(GSCOM.Html.Common.GetFormattedString(drv(DisplayColumnName), Me.DataSource.Columns(DisplayColumnName).DataType, mSchemaRow))
                                            If DisplayColumnName = "HotelRoom" Then
                                                sb.Append(" - ")
                                                sb.Append(GSCOM.Html.Common.GetFormattedString(drv("HotelClient"), Me.DataSource.Columns("HotelClient").DataType, mSchemaRow))
                                            End If
                                        End If
                                        sb.Append("</a>")
                                        sb.Append("<br/>")
                                    End If
                                End If

                                'End If
                            Next
                        End If
                        sb.AppendLine("</td>")
                        i += 1
                        CellStart = DateAdd(DateInterval.Day, 1, CellStart)
                    Next
                    sb.AppendLine("</tr>")
                Loop

            Next

            sb.AppendLine("</tbody>")
            sb.AppendLine("</table>")

            sb.AppendLine(GetHtmlCalendarFooter)


        Catch ex As Exception
            Return ex.Message
        End Try
        Return sb.ToString
    End Function


    Public Function GetHtmlCalendarFooter() As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<script language=""javascript"" type=""text/javascript"">")
        sb.AppendLine("maltbackgroundcolor='" & Me.mAltBackColor & "';")
        sb.AppendLine("mdarkbackgroundcolor='" & Me.CaptionBackColor & "';")

        'sb.AppendLine("mexpandedimage='" & ExpandedImage.Replace("\", "\\").Replace("/", "//") & "';")
        'sb.AppendLine("mcollapsedimage='" & CollapsedImage.Replace("\", "\\").Replace("/", "//") & "';")

        sb.AppendLine("var minitrows = new finitcalendar();")
        sb.AppendLine("</script>")
        sb.AppendLine("</body>")
        sb.Append("</html>")
        Return sb.ToString
    End Function



    Protected Overrides Function GetHtmlHeader() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("<head>")
        sb.AppendLine(MyBase.GetLinks)
        sb.AppendLine("<style>")
        sb.AppendLine("tr.alt_color1 {")
        sb.AppendLine("background-color:" & Me.mAltBackColor & ";")
        sb.AppendLine("}")
        sb.AppendLine("</style>")
        sb.AppendLine("</head>")
        sb.AppendLine("<body onload=""heightControl();"" onresize=""heightControl();"">")
        Return sb.ToString
    End Function
  
End Class
