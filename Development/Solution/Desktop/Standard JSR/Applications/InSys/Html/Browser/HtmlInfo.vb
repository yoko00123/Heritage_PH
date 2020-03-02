Option Explicit On
Option Strict On


Public Class HtmlInfo
    'Inherits HtmlContent

    Protected mParent As HtmlContent
    Private mIsForEmail As Boolean
    Public ReportInfo As Html.ReportInfo
    Public Property HelpMode As Boolean

    Public Sub New(ByVal pParent As HtmlContent, ByVal pIsForEmail As Boolean)
        mParent = pParent
        mIsForEmail = pIsForEmail
    End Sub

#Region "Properties"

    Public EditMode As Boolean = False


    Public ReadOnly Property StyleSheet() As String
        Get
            Return nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & "main.css"
        End Get
    End Property

    Public ReadOnly Property ResourcePath() As String
        Get
            Return nDB.GetSetting(Database.SettingEnum.ResourcePath).ToString
        End Get
    End Property

    Public ReadOnly Property TabHeaderColor() As String
        Get
            Return mParent.mMenuRow.DarkColorRGB
        End Get
    End Property
#End Region

#Region "GetHTML"
    Public Function GetHtml() As String
        Dim sb As New System.Text.StringBuilder
        Dim vName As String = ""
        If mParent.mHeaderRow.RowState = DataRowState.Added Or Me.HelpMode Then
            vName = mParent.mMenuRow.Name
        Else
            If mParent.mHeaderRow.Table.Columns.Contains("Name") Then
                vName = mParent.mHeaderRow.Item("Name").ToString
            Else
                vName = mParent.mMenuRow.Name
            End If
        End If
        sb.AppendLine("<html xmlns=""http://www.w3.org/1999/xhtml"">")
        sb.AppendLine("<head>")
        sb.Append("<title>")
        sb.Append(mParent.mMenuRow.Name)
        sb.AppendLine("</title>")
        If Me.mIsForEmail Then
            Dim mFileReader As IO.StreamReader
            Dim CSS As String
            mFileReader = IO.File.OpenText(Me.StyleSheet)
            CSS = mFileReader.ReadToEnd
            sb.AppendLine("<style type=""text/css"">")
            sb.AppendLine(CSS)
            sb.AppendLine("</style>")
        Else
            sb.AppendLine("<link rel=""stylesheet"" type=""text/css"" href=""" & Me.StyleSheet & """/>")
        End If
        If Me.HelpMode Then
            sb.AppendLine("<style type=""text/css"">")
            sb.AppendLine("td {background-color:#ffffff;}")
            sb.AppendLine("</style>")
        End If
        sb.AppendLine("</head>")
        If Me.HelpMode Then
            sb.AppendLine("<body style=""background-color:#" & mParent.mMenuRow.ColorRGB & ";"">")
        Else
            sb.AppendLine("<body>")
        End If
        'sb.Append("<div class=""htmlinfo_container"">")

        '''''''''''''''''''''''''BILLY PRINT PREVIEW COMPANY LOGO (Oct 29, 2010)
        If Me.ReportInfo IsNot Nothing Then 'IN CASE OF WEB APP

            Me.ReportInfo.ShowHeader = False
            sb.AppendLine(Me.ReportInfo.GetHTMLPrintHeader)
        End If
        '''''''''''''''''''''''''BILLY PRINT PREVIEW COMPANY LOGO



        sb.Append("<img src=""")
        Dim tmp As String

        If Me.HelpMode Then
            tmp = nDB.ImagePath(mParent.mMenuRow.ImageFile)
        Else
            If mParent.mHeaderRow.Table.Columns.Contains("ImageFile") Then
                tmp = mParent.mHeaderRow("ImageFile").ToString
                If tmp = "" Then
                    tmp = nDB.ImagePath(mParent.mMenuRow.ImageFile)
                Else
                    tmp = nDB.ImagePath(tmp)
                End If
            Else
                tmp = nDB.ImagePath(mParent.mMenuRow.ImageFile)
            End If
        End If


        If Me.mIsForEmail Then
            tmp = "cid:ImageID"
        End If

        sb.Append(tmp)
        sb.AppendLine(""" width=""64px"" height=""64px"" alt=""" & vName & """/>")

        sb.Append("<span class=""zname"">")
        sb.Append("&nbsp;")
        sb.Append(vName)
        sb.AppendLine("</span>")
        sb.AppendLine("<hr/>")
        sb.Append("<span class=""zCaption"">")
        If Me.HelpMode Then
            sb.Append(My.Application.Info.Title)
        Else
            sb.Append(mParent.mMenuRow.Name)
        End If
        sb.AppendLine("</span>")
        sb.AppendLine("<br/>")
        sb.AppendLine("<br/>")

        sb.AppendLine("<p>")
        If Me.HelpMode Then
            sb.AppendLine(mParent.mMenuRow.Description)
        End If
        sb.AppendLine("</p>")


        If mParent.mMenuSet.tMenuTab.Rows.Count > 1 Then
            sb.AppendLine("<table border=""1px"" cellpadding=""0px"" cellspacing=""0px"" width=""100%"" bordercolor=""#" & TabHeaderColor & """>")
        Else
            sb.AppendLine("<table border=""1px"" cellpadding=""0px"" cellspacing=""0px"" width=""33%"" bordercolor=""#" & TabHeaderColor & """>")
        End If
        sb.Append("<tr>")
        sb.Append("<td style=""width:33%;"">")
        Dim a As HtmlTab
        Dim mtr As New Database.MenuTabRow
        For Each dr As DataRow In mParent.mMenuSet.tMenuTab.Select("Name='General'")
            a = New HtmlTab(mParent, EditMode, Me.HelpMode)
            mtr.InnerRow = dr
            sb.Append(a.GetHtml(mtr, True))
        Next
        sb.Append("</td>")
        If mParent.mMenuSet.tMenuTab.Rows.Count > 1 Then
            sb.Append("<td>")
            For Each dr As DataRow In mParent.mMenuSet.tMenuTab.Select("Name<>'General'", "SeqNo,ID")
                a = New HtmlTab(mParent, EditMode, Me.HelpMode)
                mtr.InnerRow = dr
                sb.Append(a.GetHtml(mtr, Not mtr.HasTable))
            Next
            sb.Append("</td>")
        End If
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")

        sb.Append(Me.GetDetails)
        If Me.ReportInfo IsNot Nothing Then 'consider web app
            sb.AppendLine(Me.ReportInfo.GetHTMLPrintFooter)
        End If
        
        sb.Append("</body>")
        sb.Append("</html>")
        Return sb.ToString
    End Function
#End Region

#Region "GetDetails"
    Private Function GetDetails() As String
        Dim sb As New System.Text.StringBuilder
        Dim dv As New DataView(mParent.mMenuSet.tMenuDetailTab)
        dv.RowFilter = "ShowInBrowser=1 AND Name<>'ImageFile'"
        'NOTE: ERROR!!!! DataTable.Select does not return correct order. Use DataView instead
        For Each dr As DataRowView In dv 'mParent.mMenuSet.tMenuDetailTab.Select("ShowInBrowser=1 AND Name<>'ImageFile'")
            Dim mdtr As New Database.MenuDetailTabRow(dr.Row)
            Select Case mdtr.ID_MenuDetailTabType
                Case Database.MenuDetailTabTypeEnum.Grid
                    If mdtr.ParentTableName = "" Then
                        Dim a As HtmlTableInfoDetail
                        a = New HtmlTableInfoDetail(mParent, Me.HelpMode)
                        a.PreLineFeedCount = 2
                        sb.Append(a.GetHtml(mdtr))
                    End If
                Case Database.MenuDetailTabTypeEnum.TreeView
                    Dim a As TreeGrid
                    a = New TreeGrid(mParent)
                    a.PreLineFeedCount = 2
                    sb.Append(a.PrintDetail(mdtr.InnerRow))
            End Select
        Next
        Return sb.ToString
    End Function
#End Region

End Class
