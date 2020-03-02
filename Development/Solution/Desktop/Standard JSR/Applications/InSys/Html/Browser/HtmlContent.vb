Option Explicit On
Option Strict On



Public Class HtmlContent

    Public mMenuSet As Database.MenuSetClass
    Public mMenuRow As Database.MenuRow
    Public mDataSet As DataSet
    Public mHeaderRow As DataRow

#Region "Constructor"
    Public Sub New(ByVal pMenuSet As Database.MenuSetClass, ByVal pDataSet As DataSet)
        mMenuSet = pMenuSet
        mDataSet = pDataSet
        mMenuRow = New Database.MenuRow(mMenuSet.tMenu.Rows(0))
        mHeaderRow = mDataSet.Tables(0).Rows(0)
    End Sub

    'Protected MustOverride Function GetHtml() As String


#End Region

End Class
