Option Explicit On
Option Strict On

Public Class HtmlTreeGrid
    Inherits GSCOM.Html.HtmlTreeGrid

    Public Sub New()
        MyBase.New()
        'MyBase.StyleSheet1 = nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & MyBase.StyleSheet1
        'MyBase.StyleSheet2 = nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & MyBase.StyleSheet2
        'MyBase.Script1 = nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & "scripts\" & MyBase.Script1
        'MyBase.Script2 = nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & "scripts\" & MyBase.Script2
        'MyBase.ExpandedImage = nDB.GetSetting(Database.SettingEnum.ResourcePath).ToString & MyBase.ExpandedImage
        'MyBase.CollapsedImage = nDB.GetSetting(Database.SettingEnum.ResourcePath).ToString & MyBase.CollapsedImage
        'MyBase.AscendingImage = nDB.GetSetting(Database.SettingEnum.ResourcePath).ToString & MyBase.AscendingImage
        'MyBase.DescendingImage = nDB.GetSetting(Database.SettingEnum.ResourcePath).ToString & MyBase.DescendingImage

        MyBase.StyleSheet1 = nDB.nGlobal.StyleSheetPath & MyBase.StyleSheet1
        MyBase.StyleSheet2 = nDB.nGlobal.StyleSheetPath & MyBase.StyleSheet2
        MyBase.Script1 = nDB.nGlobal.StyleSheetPath & "scripts\" & MyBase.Script1
        MyBase.Script2 = nDB.nGlobal.StyleSheetPath & "scripts\" & MyBase.Script2
        MyBase.ExpandedImage = nDB.nGlobal.ResourcePath & MyBase.ExpandedImage
        MyBase.CollapsedImage = nDB.nGlobal.ResourcePath & MyBase.CollapsedImage
        MyBase.AscendingImage = nDB.nGlobal.ResourcePath & MyBase.AscendingImage
        MyBase.DescendingImage = nDB.nGlobal.ResourcePath & MyBase.DescendingImage

    End Sub

    Public Overrides Function ImagePath(ByVal pImageFile As String) As String
        Return nDB.ImagePath(pImageFile)
    End Function
    Public xID As Integer

    Public ReportInfo As Html.ReportInfo

    Public Overrides Function GetHTMLPrintFooter() As String
        Return ReportInfo.GetHTMLPrintFooter
    End Function

 

    'Public Property DisplaySubName As String


    Public Overrides Function GetHTMLPrintHeader() As String

        Return ReportInfo.GetHTMLPrintHeader()
    End Function

End Class