Option Explicit On
Option Strict On


Public Class ReportInfo
    Private Property mCompanyMenuInfo As Database.InfoSetBase
    Private Property mInfo As Database.InfoSetBase
    Private mDataSource As DataTable
    Private mMenu As Integer
    Public Sub New(ByVal pCompanyMenuInfo As Database.InfoSetBase, ByVal pInfo As Database.InfoSetBase, ByVal pDataSource As DataTable, ByVal pMenu As Integer)
        mCompanyMenuInfo = pCompanyMenuInfo
        mInfo = pInfo
        mDataSource = pDataSource
        mMenu = pMenu
    End Sub
    Public Property ShowHeader As Boolean = True
#Region "Properties"

    Private ReadOnly Property tCompanyMenuSignatory As DataTable
        Get
            Return mCompanyMenuInfo.mDataset.Tables("tCompanyMenuSignatory")
        End Get
    End Property

    Private ReadOnly Property AltTitle As String
        Get
            Return nDB.GetMenuValue(CType(mMenu, Database.Menu), Database.Tables.tMenu.Field.Name).ToString()
        End Get
    End Property
    Private ReadOnly Property ImageFile As String
        Get
            Return nDB.GetMenuValue(CType(mMenu, Database.Menu), Database.Tables.tMenu.Field.ImageFile).ToString
            ' Return nDB.Session.Get("CompanyImageFile").ToString
        End Get
    End Property
    Private ReadOnly Property Title As String
        Get
            If mCompanyMenuInfo Is Nothing Then Return String.Empty
            Return mCompanyMenuInfo.Row("ReportTitle").ToString
        End Get
    End Property
    Private ReadOnly Property SubTitle As String
        Get
            If mCompanyMenuInfo Is Nothing Then Return String.Empty
            Return mCompanyMenuInfo.Row("ReportSubTitle").ToString()
        End Get
    End Property
    Private ReadOnly Property Landscape As Boolean
        Get
            If mCompanyMenuInfo Is Nothing Then Return False
            Return CBool(mCompanyMenuInfo.Row("IsLandscape"))
        End Get
    End Property
    Private ReadOnly Property ContentHeader As String
        Get
            If mCompanyMenuInfo Is Nothing Then Return String.Empty
            Return mCompanyMenuInfo.Row("ContentHeader").ToString
        End Get
    End Property
    Private ReadOnly Property ContentFooter As String
        Get
            If mCompanyMenuInfo Is Nothing Then Return String.Empty
            Return mCompanyMenuInfo.Row("ContentFooter").ToString
        End Get
    End Property
    Private ReadOnly Property Certification As Boolean
        Get
            If mCompanyMenuInfo Is Nothing Then Return False
            Return CBool(mCompanyMenuInfo.Row("IsCertification"))
        End Get
    End Property
    Private ReadOnly Property Logo As String
        Get
            Return nDB.Session.Rows(0)("ReportImageFile").ToString()
        End Get
    End Property
    Private ReadOnly Property CompanyAddress As String
        Get
            Return nDB.Session.Rows(0)("Address").ToString()
        End Get
    End Property
    Private ReadOnly Property CompanyTelNo As String
        Get
            Return nDB.Session.Rows(0)("TelNo").ToString()
        End Get
    End Property
    Public ReadOnly Property DisplayName() As String
        Get
            If Me.Title = "" Then
                Return AltTitle
            Else
                Return Me.Title
            End If
        End Get
    End Property

#End Region

#Region "GetHTMLPrintHeader"
    Public Function GetHTMLPrintHeader() As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<div id=""topDiv"" class=""htmllist_topdiv"">")
        sb.AppendLine("<center>")
        If Me.Logo <> "" Then
            sb.Append("<img id=""logo"" src=""" & nDB.ImagePath(Me.Logo) & """>")
        Else
            sb.Append("<span id=""report_company"">" & nDB.Session.Rows(0)("Company").ToString & "</span>")
        End If
        sb.AppendLine("<br />")
        sb.AppendLine(Me.CompanyAddress & "<br />")
        sb.AppendLine(Me.CompanyTelNo & "<br />")
        sb.AppendLine("</center>")
        If ShowHeader Then
            sb.AppendLine("<img id=""icon"" class=""htmllist_icon"" src=""" & nDB.ImagePath(Me.ImageFile) & """  alt=""""/>")
            sb.AppendLine("<span class=""zname"">&nbsp;")
            sb.AppendLine(DisplayName)
            sb.AppendLine("</span>")
            sb.AppendLine("</div>")
            sb.AppendLine("<hr />")
        End If
        If Me.mCompanyMenuInfo IsNot Nothing Then

            '''''''''''''''''''''''''20101119------------------------\
            If Me.Certification Then
                If mInfo IsNot Nothing Then
                    Dim s As String
                    s = Me.ContentHeader
                    s = mInfo.PassParametersHTML(s)
                    sb.AppendLine(s)
                End If
                If mDataSource IsNot Nothing AndAlso mDataSource.Rows.Count > 0 Then
                    Dim s As String
                    s = Me.ContentHeader

                    Dim dt As New Database.ParameterDataTable
                    Dim dr As DataRow
                    For Each dc As DataColumn In mDataSource.Columns
                        dr = dt.NewRow
                        dr("Parameter") = "$" & dc.ColumnName
                        dr("Value") = "<b>" & mDataSource.Rows(0)(dc).ToString & "</b>"
                        dt.Rows.Add(dr)
                    Next
                    s = Database.gPassParameters(nDB.gParameterTable, s)
                    s = Database.gPassParameters(dt, s)
                    sb.AppendLine("<span class=""contenttext"">")
                    sb.AppendLine(s)
                    sb.AppendLine("</span>")

                End If
            End If
            '''''''''''''''''''''''''20101119-------------------------/

            sb.AppendLine("<a class=""HtmlSubTitle"">")
            sb.AppendLine(Me.SubTitle)
            sb.AppendLine("</a><br /><br />")
        End If

        Return sb.ToString
    End Function

#End Region

#Region "GetHTMLPrintFooter"

    Public Function GetHTMLPrintFooter() As String
        If Me.mCompanyMenuInfo Is Nothing Then Return String.Empty

        Dim sb As New System.Text.StringBuilder

        '''''''''''''''''''''''''20101119------------------------\
        If Me.Certification Then
            If mInfo IsNot Nothing Then
                Dim s As String
                s = Me.ContentFooter
                s = mInfo.PassParametersHTML(s)
                sb.AppendLine(s)
            End If
            If mDataSource IsNot Nothing AndAlso mDataSource.Rows.Count > 0 Then
                Dim s As String
                s = Me.ContentFooter

                Dim dt As New Database.ParameterDataTable
                Dim dr As DataRow
                For Each dc As DataColumn In mDataSource.Columns
                    dr = dt.NewRow
                    dr("Parameter") = "$" & dc.ColumnName
                    dr("Value") = "<b>" & mDataSource.Rows(0)(dc).ToString & "</b>"
                    dt.Rows.Add(dr)
                Next
                s = Database.gPassParameters(nDB.gParameterTable, s)
                s = Database.gPassParameters(dt, s)
                sb.AppendLine("<span class=""contenttext"">")
                sb.AppendLine(s)
                sb.AppendLine("</span>")

            End If
        End If
        '''''''''''''''''''''''''20101119-------------------------/
        sb.AppendLine("<br /><br /><br /><table style=""width:100%;"">")
        If Me.Landscape Then
            sb.AppendLine("<tr>")
            For Each r As DataRow In tCompanyMenuSignatory.Rows
                sb.AppendLine("<td style=""border:0px;"">")
                sb.AppendLine(r("Label").ToString)
                sb.AppendLine("<br /><br />")
                sb.AppendLine(r("Name").ToString)
                sb.AppendLine("<br />")
                sb.AppendLine(r("Designation").ToString)
                sb.AppendLine("<br /><br /><br />")
                sb.AppendLine("</td>")
            Next r
            sb.AppendLine("</tr>")
        Else
            For Each r As DataRow In tCompanyMenuSignatory.Rows
                sb.AppendLine("<tr>")
                sb.AppendLine("<td style=""border:0px;"">")
                sb.AppendLine(r("Label").ToString)
                sb.AppendLine("<br /><br />")
                sb.AppendLine(r("Name").ToString)
                sb.AppendLine("<br />")
                sb.AppendLine(r("Designation").ToString)
                sb.AppendLine("<br /><br /><br />")
                sb.AppendLine("</td>")
                sb.AppendLine("</tr>")
            Next r
        End If
        sb.AppendLine("</table>")

        Return sb.ToString
    End Function
#End Region

End Class
