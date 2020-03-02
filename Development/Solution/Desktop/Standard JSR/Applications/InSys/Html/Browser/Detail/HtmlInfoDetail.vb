Option Explicit On
Option Strict On


Public Class HtmlInfoDetail
    'Inherits HtmlContent

    Protected mParent As HtmlContent
    Public Property PreLineFeedCount() As Integer

    Public Sub New(ByVal pParent As HtmlContent)
        mParent = pParent
    End Sub

    Public Function GetPreLineFeeds() As String
        Dim sb As New System.Text.StringBuilder
        For i As Integer = 1 To PreLineFeedCount
            sb.AppendLine()
            sb.Append("<br/>")
        Next
        Return sb.ToString
    End Function

    Protected ReadOnly Property TabHeaderColor() As String
        Get
            Return mParent.mMenuRow.DarkColorRGB
        End Get
    End Property
End Class
