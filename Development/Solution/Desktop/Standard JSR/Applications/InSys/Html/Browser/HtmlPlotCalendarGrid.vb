Option Explicit On
Option Strict On

Public Class HtmlPlotCalendarGrid
    Inherits HtmlTreeGrid
    Public Property RowFieldHeader As String
    Public Property RowCategoryHeader As String
    Public Property RowCategory As String
    Public Property RowField As String
    Public Property RowFieldContent As String = Nothing
    Public Property xStartDate As String = "StartDate"
    Public Property xEndDate As String = "EndDate"
    Public Property StartDate As Date
    Public Property EndDate As Date
    Public Property mColor As String = "StatusColor"
    Public Property CellContent As String = "Name"
    Public Property ListSource As String
    Public Property StatusTable As String
    Private Property ListTable As DataTable
    Private Property FieldTable As DataTable
    Private Property ColorTable As DataTable

    Public Overrides Function GetHtml() As String

        If Not (Me.DataSource.Columns.Contains("StartDate") And Me.DataSource.Columns.Contains("EndDate")) Then
            MsgBox("Please include START DATE and END DATE fields in the COLUMNS SELECTION", MsgBoxStyle.Exclamation, "InSys")
            Return ""
        End If

        Dim sb As New System.Text.StringBuilder
        Dim myDateNow As Date = StartDate
        Dim myDay() As String
        Dim myColor As String = "white"
        Dim isReserved As Boolean = False
        Dim ClientName As String = ""
        Dim HotelRoomType As String = ""
        Dim HotelRoom As String
        Dim rColor As String = ""
        Dim ReservationID As String = ""
        Dim myCol As Integer
        Dim s As String

        If RowCategory IsNot Nothing Then
            's = "SELECT *  FROM " & Me.ListSource & " INNER JOIN dbo.vClientAppointment ca ON a.ID = ca.ID_Employee " & " WHERE a.IsActive = 1"
            s = "SELECT *  FROM " & Me.ListSource & " WHERE IsActive = 1"
        Else
            's = "SELECT * FROM " & Me.ListSource & " INNER JOIN dbo.vClientAppointment ca ON a.ID = ca.ID_Employee " & " WHERE a.IsActive = 1"
            s = "SELECT * FROM " & Me.ListSource & " WHERE IsActive = 1"
        End If

        If RowFieldContent <> "" Then
            s &= " AND Name LIKE '" & RowFieldContent & "%'"
        End If
        Me.ListTable = GSCOM.SQL.TableQuery(s, nDB.Connection, True)
        If mColor IsNot Nothing Then
            Dim ss As String = "SELECT * FROM v" & Right(StatusTable, StatusTable.Length - 1)
            If RowField = "HotelRoom" Then
                ss &= " where ID<>8 "
            End If
            Me.ColorTable = GSCOM.SQL.TableQuery(ss, nDB.Connection, True)
            Me.ColorTable.DefaultView.Sort = "SeqNo"
        End If

        myDay = {"SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT"}

        sb.AppendLine(GetHtmlHeader)
        Try

            sb.AppendLine("<table style=""float:left"" width=""100%"">")
            sb.AppendLine("<tr>")
            For Each crv As DataRowView In Me.ColorTable.DefaultView
                myCol += 1
            Next
            sb.AppendLine("<td align=""center"" style=""border:0px"" colspan=""" & myCol.ToString & """>")
            sb.AppendLine("<b>LEGEND</b>")
            sb.AppendLine("</td>")
            sb.AppendLine("</tr>")
            sb.AppendLine("<tr align=""center"">")
            For Each crv As DataRowView In Me.ColorTable.DefaultView

                sb.AppendLine("<td nowrap=""nowrap"" bgcolor=""" & crv(mColor).ToString & """>&nbsp;")
                sb.AppendLine(crv("Name").ToString)
                sb.AppendLine("</td>")

            Next
            sb.AppendLine("</tr>")
            sb.AppendLine("<tr style=""border:0px"" align=""center"">")
            sb.AppendLine("<td style=""border:0px"" align=""center"" colspan=""" & myCol.ToString & """>&nbsp;")
            sb.AppendLine("</td>")
            sb.AppendLine("</tr>")
            sb.AppendLine("</table>")

            sb.AppendLine("<table id=""plotcalendar"" style=""float:left;"">")
            sb.AppendLine("<tr>")
            sb.AppendLine("<td bgcolor=""#878481"" nowrap=""nowrap"" style=""color:white"" rowspan=2 align=""center"" style=""vertical-align:middle"">" & RowFieldHeader & "</td>")
            sb.AppendLine("<td bgcolor=""#ede7e1"" nowrap=""nowrap"" rowspan=2 align=""center"" style=""vertical-align:middle"">" & RowCategoryHeader & "</td>")

            Do While StartDate <= EndDate
                If RowField = "HotelRoom" Then
                    sb.AppendLine("<td align=""center"" colspan=""2"" bgcolor=""#878481"" style=""color:white"">")
                Else
                    sb.AppendLine("<td align=""center"" bgcolor=""#878481"" style=""color:white"">")
                End If

                sb.AppendLine(CStr(StartDate.Date))
                sb.AppendLine("</td>")
                StartDate = DateAdd(DateInterval.Day, 1, StartDate)
            Loop
            sb.AppendLine("</tr>")
            sb.AppendLine("<tr>")

            StartDate = myDateNow
            Dim DateContent As Date = StartDate
            DateContent = StartDate

            Do While StartDate <= EndDate
                If RowField = "HotelRoom" Then
                    sb.AppendLine("<td align=""center"" colspan=""2"" bgcolor=""#ede7e1"">")
                Else
                    sb.AppendLine("<td align=""center"" bgcolor=""#ede7e1"">")
                End If

                sb.AppendLine(myDay(StartDate.DayOfWeek))
                sb.AppendLine("</td>")
                StartDate = DateAdd(DateInterval.Day, 1, StartDate)
            Loop
            sb.AppendLine("</tr>")

            Me.ListTable.DefaultView.Sort = "Name"

            If Me.ListTable.Columns.Contains("Name") Then
                For Each drv As DataRowView In Me.ListTable.DefaultView
                    StartDate = myDateNow.Date
                    DateContent = StartDate
                    If RowCategory IsNot Nothing Then
                        HotelRoomType = drv("ID_" & RowCategory).ToString
                    End If
                    HotelRoom = drv("ID").ToString
                    sb.AppendLine("<tr>")
                    sb.AppendLine("<td style=""vertical-align:middle;color=white;"" bgcolor=""#878481"" align=""center"" nowrap=""nowrap"">")
                    sb.AppendLine("<b>" & drv("Name").ToString & "</b>")
                    sb.AppendLine("</td>")
                    sb.AppendLine("<td nowrap=""nowrap"" bgcolor=""#ede7e1"" align=""center"">")

                    If RowCategory IsNot Nothing Then
                        sb.AppendLine("<b>" & drv(RowCategory).ToString & "</b>")
                    End If

                    sb.AppendLine("</td>")

                    Dim f As Integer = CInt(DateDiff(DateInterval.Day, StartDate, EndDate))
                    If RowField = "HotelRoom" Then
                        f = f * 2
                        f = f + 2
                    Else
                        f = f
                    End If

                    Dim g As Integer = 0
                    Dim h As Integer
                    Dim ampm As String = "-AM"
                    Dim bgcolor As String = Nothing

                    Do While g < f
                        For Each dr As DataRow In Me.DataSource.Select
                            If dr(xStartDate) IsNot DBNull.Value Then
                                If RowField = "HotelRoom" Then
                                    If dr(RowField).ToString = drv("Name").ToString And ((DateContent > CDate(dr(xStartDate)) And DateContent < CDate(dr(xEndDate))) Or (DateContent = CDate(dr(xStartDate)) And g Mod 2 = 1) Or (DateContent = CDate(dr(xEndDate)) And g Mod 2 = 0)) Then
                                        isReserved = True
                                        If dr(mColor).ToString = "" Or dr(mColor) Is DBNull.Value Then
                                            isReserved = False
                                        End If
                                        ReservationID = dr("ID").ToString
                                        ClientName = dr(CellContent).ToString
                                        rColor = dr(mColor).ToString
                                    End If
                                Else
                                    If dr(RowField).ToString = drv("Name").ToString And DateContent.ToShortDateString >= CDate(dr(xStartDate)).ToShortDateString And DateContent.ToShortDateString <= CDate(dr(xEndDate)).ToShortDateString Then
                                        isReserved = True
                                        ReservationID = dr("ID").ToString
                                        ClientName &= "<a href="""" onclick=""fselectedrow(" & dr("ID").ToString & ")"" id=""" & dr("ID").ToString & """ data-id=""" & ReservationID & """>" & dr(CellContent).ToString & "<br/><br/>"
                                        rColor = dr(mColor).ToString
                                    End If
                                End If
                            End If
                        Next

                        If RowField = "HotelRoom" Then
                            If Math.Ceiling(g / 2) Mod 2 = 0 Then
                                bgcolor = "#F8F8F8"
                                myColor = ""
                            Else
                                bgcolor = ""
                                myColor = "white"
                            End If
                        End If

                        If isReserved = True Then
                            sb.AppendLine("<td bgcolor=""" & rColor & """ nowrap=""nowrap"" style=""color:black;"" class=""cell"" id=""" & drv("ID").ToString & """ data-id=""" & ReservationID & """ data-hotelroom=""" & HotelRoom & """ data-hotelroomtype=""" & HotelRoomType & """>")
                            sb.AppendLine("<div style=""display:none"">")
                            sb.AppendLine(CStr(DateContent))
                            sb.Append("-" & HotelRoomType)
                            sb.Append("-" & HotelRoom)
                            sb.AppendLine("</div>")
                            sb.AppendLine(ClientName)
                        Else
                            sb.AppendLine("<td bgcolor=""" & myColor & bgcolor & """ nowrap=""nowrap"" style=""color:black;width:50px"" class=""cell"" id=""" & drv("ID").ToString & """ data-id=""" & ReservationID & """ data-hotelroom=""" & HotelRoom & """ data-hotelroomtype=""" & HotelRoomType & """>")
                            sb.AppendLine("<div style=""display:none"">")
                            sb.Append(CStr(DateContent))
                            sb.Append("-" & HotelRoomType)
                            sb.Append("-" & HotelRoom)
                            sb.AppendLine("</div>")
                        End If
                        isReserved = False
                        ClientName = ""
                        sb.AppendLine("</td>")
                        StartDate = DateAdd(DateInterval.Day, 1, StartDate)
                        g += 1
                        If RowField = "HotelRoom" Then
                            h = g Mod 2
                            If h = 0 Then
                                DateContent = DateAdd(DateInterval.Day, 1, DateContent)
                            End If
                        Else
                            DateContent = DateAdd(DateInterval.Day, 1, DateContent)
                        End If


                        ampm = "-PM"
                    Loop
                    sb.AppendLine("</tr>")
                Next
            End If
            sb.AppendLine("</table>")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        sb.AppendLine(GetHtmlPlotCalendarFooter)
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
