Option Explicit On
Option Strict On

Friend Class DetailClass
    Private mDetailTable As DataTable
    Private mDetailParentColumn As String
    Private mDetailChildColumn As String

    Public Sub SetDetailInfo(ByVal pTable As DataTable, ByVal pParentColumn As String, ByVal pChildColumn As String)
        mDetailParentColumn = pParentColumn
        mDetailChildColumn = pChildColumn
        mDetailTable = pTable
        UpdateDetail(Nothing)
    End Sub


    Public Sub UpdateDetail(ByVal n As DataRow)
        Dim vFilter As String = "1=0"
        Dim vValue As Object = DBNull.Value
        If mDetailTable IsNot Nothing And mDetailChildColumn <> "" Then
            If n IsNot Nothing Then
                vFilter = mDetailChildColumn & "=" & GSCOM.SQL.SQLFormat(n(mDetailParentColumn))
                vValue = n(mDetailParentColumn)
            End If
        Else
        End If
        mDetailTable.DefaultView.RowFilter = vFilter
        mDetailTable.Columns(mDetailChildColumn).DefaultValue = vValue
    End Sub

End Class
