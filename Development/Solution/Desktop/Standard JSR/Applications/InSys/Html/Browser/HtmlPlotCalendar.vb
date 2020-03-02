Option Explicit On
Option Strict On

Public Class HtmlPlotCalendar
    Inherits HtmlTreeGrid
    Public Property RowField As String = "Name"
    Public Property RowCategory As String = "RoomType"
    Public Property StartDate As String = "Date"
    Public Property EndDate As String = "Date"
    Public Property ListSource As String
    Private Property ListTable As DataTable
    Private Property ColorTable As DataTable

    Public Overrides Function GetHtml() As String
        Dim sb As New System.Text.StringBuilder
         Dim StartDate As Date
        Dim EndDate As Date
        Dim myDay() As String
        Dim myColor As String = "white"
        Dim isReserved As Boolean = False
        Dim ClientName As String = ""
        Dim HotelRoomType As String
        Dim HotelRoom As String
        Dim rColor As String = ""
        Dim ReservationID As String = ""
        Dim myCol As Integer

        Me.ListTable = GSCOM.SQL.TableQuery("SELECT ID,Name,HotelRoomType FROM " & Me.ListSource & " WHERE IsActive = 1", nDB.Connection, True)
        Me.ColorTable = GSCOM.SQL.TableQuery("SELECT Name,HotelStatusColor FROM vHotelRoomStatus", nDB.Connection, True)

        myDay = {"SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT"}

        StartDate = Now
        EndDate = DateAdd(DateInterval.Month, 1, StartDate)
        EndDate = DateAdd(DateInterval.Day, 1, EndDate)

        sb.AppendLine(GetHtmlHeader)
        Try
            sb.AppendLine("<table id=""plotcalendar"" width=""100%"">")
            sb.AppendLine("<tr>")
            For Each crv As DataRowView In Me.ColorTable.DefaultView
                myCol += 1
            Next
            sb.AppendLine("<td align=""center"" colspan=""" & myCol.ToString & """>")
            sb.AppendLine("<b>LEGEND</b>")
            sb.AppendLine("</td>")
            sb.AppendLine("</tr>")
            sb.AppendLine("<tr>")
            For Each crv As DataRowView In Me.ColorTable.DefaultView

                sb.AppendLine("<td nowrap=""nowrap"" bgcolor=""" & crv("HotelStatusColor").ToString & """>&nbsp;")
                sb.AppendLine(crv("Name").ToString)
                sb.AppendLine("</td>")

            Next
            sb.AppendLine("</tr>")
            sb.AppendLine("<tr border=""0"">")
            sb.AppendLine("<td border=""0"" align=""center"" colspan=""" & myCol.ToString & """>&nbsp;")
            sb.AppendLine("</td>")
            sb.AppendLine("</tr>")
            sb.AppendLine("<tr>")
            sb.AppendLine("<td bgcolor=""#FFCCFF"" rowspan=2 align=""center"" valign=""middle"">" & RowCategory & "</td>")
            sb.AppendLine("<td bgcolor=""#FFCCFF"" rowspan=2 align=""center"" valign=""middle"">" & RowField & "</td>")
            Do While StartDate < EndDate
                sb.AppendLine("<td align=""center"" bgcolor=""#99CCFF"">")
                sb.AppendLine(CStr(StartDate.Date))
                sb.AppendLine("</td>")
                StartDate = DateAdd(DateInterval.Day, 1, StartDate)
            Loop
            sb.AppendLine("</tr>")
            sb.AppendLine("<tr>")
            StartDate = Date.Now
            Do While StartDate < EndDate
                sb.AppendLine("<td align=""center"" bgcolor=""#9966FF"">")
                sb.AppendLine(myDay(StartDate.DayOfWeek))
                sb.AppendLine("</td>")
                StartDate = DateAdd(DateInterval.Day, 1, StartDate)
            Loop
            sb.AppendLine("</tr>")
            Me.ListTable.DefaultView.Sort = "HotelRoomType"
            If Me.ListTable.Columns.Contains("Name") Then
                For Each drv As DataRowView In Me.ListTable.DefaultView
                    StartDate = Date.Now
                    HotelRoomType = drv("HotelRoomType").ToString
                    HotelRoom = drv("ID").ToString
                    sb.AppendLine("<tr>")
                    sb.AppendLine("<td style=""vertical-align:middle"" align=""center"" nowrap=""nowrap"">")
                    sb.AppendLine("<b>" & drv("HotelRoomType").ToString & "</b>")
                    sb.AppendLine("</td>")
                    sb.AppendLine("<td nowrap=""nowrap"">")
                    sb.AppendLine("<b>" & drv("Name").ToString & "</b>")
                    sb.AppendLine("</td>")
                    Do While StartDate < EndDate
                        For Each dr As DataRowView In Me.DataSource.DefaultView
                            If CDate(dr("CheckInDate")) = StartDate Or _
                        (CDate(dr("CheckInDate")) < StartDate And CDate(dr("CheckOutDate")) >= StartDate) _
                        And dr("HotelRoom").ToString = drv("Name").ToString Then
                                isReserved = True

                                If dr("HotelStatusColor").ToString = "" Then
                                    isReserved = False
                                End If
                                ReservationID = dr("ID").ToString
                                ClientName = dr("HotelClient").ToString
                                rColor = dr("HotelStatusColor").ToString

                            End If
                        Next
                        If isReserved = True Then
                            sb.AppendLine("<td bgcolor=""" & rColor & """ nowrap=""nowrap"" style=""color:black"" class=""cell"" id=""" & drv("ID").ToString & """ data-id=""" & ReservationID & """ data-hotelroom=""" & HotelRoom & """ data-hotelroomtype=""" & HotelRoom & """>")
                            sb.AppendLine("<div style=""display:none"">")
                            sb.AppendLine(CStr(StartDate.Date))
                            sb.Append("-" & HotelRoomType)
                            sb.Append("-" & HotelRoom)
                            sb.AppendLine("</div>")
                            sb.AppendLine(ClientName)

                        Else
                            sb.AppendLine("<td bgcolor=""" & myColor & """ nowrap=""nowrap"" style=""color:black"" class=""cell"" id=""" & drv("ID").ToString & """ data-id=""" & ReservationID & """ data-hotelroom=""" & HotelRoom & """ data-hotelroomtype=""" & HotelRoom & """>")
                            sb.AppendLine("<div style=""display:none"">")
                            sb.Append(CStr(StartDate.Date))
                            sb.Append("-" & HotelRoomType)
                            sb.Append("-" & HotelRoom)
                            sb.AppendLine("</div>")
                        End If
                        isReserved = False
                        sb.AppendLine("</td>")
                        StartDate = DateAdd(DateInterval.Day, 1, StartDate)
                    Loop
                    sb.AppendLine("</tr>")
                Next
            End If
            sb.AppendLine("</table>")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        sb.AppendLine(GetHtmlPlotCalendarFooter)
        'GSCOM.SQL.TableQuery("DELETE FROM tHotelRoomReservation WHERE ID_HotelRoomStatus=1", nDB.Connection, True)
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
    Public Function GetHtmlPlotCalendarFooter() As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<script language=""javascript"" type=""text/javascript"">")
        sb.AppendLine("maltbackgroundcolor='" & Me.mAltBackColor & "';")
        sb.AppendLine("mdarkbackgroundcolor='" & Me.CaptionBackColor & "';")
        sb.AppendLine("</script>")
        sb.Append(" <script language=""javascript"" type=""text/javascript"" src=""" & Script2 & """></script>")
        sb.AppendLine("</body>")
        sb.Append("</html>")
        Return sb.ToString
    End Function

End Class
