Option Explicit On
Option Strict On

Public Class MenuSetClass

    Private mInnerDataSet As DataSet

    Private mtMenu As DataTable
    Private mtMenuTab As DataTable
    Private mtMenuTabField As DataTable
    Private mtMenuDetailTab As DataTable
    Private mtMenuDetailTabField As DataTable
    Private mtMenuDetailTabProperty As DataTable
    Private mtMenuButton As DataTable
    Private mtConstraint As DataTable
    Private mtSystemImage As DataTable
    Public Property tMenuSubDataSource As DataTable

    Public Property tMenu() As DataTable
        Get
            Return mtMenu
        End Get
        Set(ByVal value As DataTable)
            mtMenu = value
        End Set
    End Property

    Public Property tMenuTab() As DataTable
        Get
            Return mtMenuTab
        End Get
        Set(ByVal value As DataTable)
            mtMenuTab = value
        End Set
    End Property

    Public Property tMenuTabField() As DataTable
        Get
            Return mtMenuTabField
        End Get
        Set(ByVal value As DataTable)
            mtMenuTabField = value
        End Set
    End Property
    Public Property tMenuDetailTab() As DataTable
        Get
            Return mtMenuDetailTab
        End Get
        Set(ByVal value As DataTable)
            mtMenuDetailTab = value
        End Set
    End Property
    Public Property tMenuDetailTabField() As DataTable
        Get
            Return mtMenuDetailTabField
        End Get
        Set(ByVal value As DataTable)
            mtMenuDetailTabField = value
        End Set
    End Property

    Public Property tMenuDetailTabProperty() As DataTable
        Get
            Return mtMenuDetailTabProperty
        End Get
        Set(ByVal value As DataTable)
            mtMenuDetailTabProperty = value
        End Set
    End Property

    Public Property tMenuButton() As DataTable
        Get
            Return mtMenuButton
        End Get
        Set(ByVal value As DataTable)
            mtMenuButton = value
        End Set
    End Property

    Public Property tConstraint() As DataTable
        Get
            Return mtConstraint
        End Get
        Set(ByVal value As DataTable)
            mtConstraint = value
        End Set
    End Property

    Public Property tSystemImage() As DataTable
        Get
            Return mtSystemImage
        End Get
        Set(ByVal value As DataTable)
            mtSystemImage = value
        End Set
    End Property

#Region "InnerDataSet"
    Public Property InnerDataSet() As DataSet
        Get
            Return mInnerDataSet
        End Get
        Set(ByVal value As DataSet)
            mInnerDataSet = value
        End Set
    End Property
#End Region

    Public Sub New()
        Me.InnerDataSet = New DataSet

    End Sub

    Public Sub AddTables()
        With Me.InnerDataSet
            .Tables.Add(mtMenu)
            .Tables.Add(mtMenuTab)
            .Tables.Add(mtMenuDetailTab)
            .Tables.Add(mtMenuTabField)
            .Tables.Add(mtMenuDetailTabField)
            .Tables.Add(mtMenuDetailTabProperty)
            .Tables.Add(mtMenuButton)
            If mtConstraint IsNot Nothing Then
                .Tables.Add(mtConstraint)
            End If
            If mtSystemImage IsNot Nothing Then
                .Tables.Add(mtSystemImage)
            End If
            If tMenuSubDataSource IsNot Nothing Then
                .Tables.Add(tMenuSubDataSource)
            End If
        End With
    End Sub
    Private mSession As UserSession
    Public Sub New(ByVal pSession As UserSession, ByVal pSessionID As Integer)
        Me.New()
        mSession = pSession
        Dim s As String
        s = "EXEC pMenuDataSet " & pSessionID.ToString
        Dim c As New SqlClient.SqlCommand(s, mSession.Connection)
        Dim a As New SqlClient.SqlDataAdapter(c)
        a.Fill(Me.InnerDataSet)

        With Me.InnerDataSet
            mtMenu = .Tables(0)
            mtMenuTab = .Tables(1)
            mtMenuDetailTab = .Tables(2)
            mtMenuTabField = .Tables(3)
            mtMenuDetailTabField = .Tables(4)
            mtMenuDetailTabProperty = .Tables(5)
            mtMenuButton = .Tables(6)
            mtConstraint = .Tables(7)
            mtSystemImage = .Tables(8)
            tMenuSubDataSource = .Tables(9)
        End With

        mtMenu.DefaultView.Sort = "ID_Menu,SeqNo,ID"
        mtMenu.TableName = "tMenu"
        mtMenuTab.TableName = "tMenuTab"
        mtMenuDetailTab.TableName = "tMenuDetailTab"
        mtMenuTabField.TableName = "tMenuTabField"
        mtMenuDetailTabField.TableName = "tMenuDetailTabField"
        mtMenuDetailTabProperty.TableName = "tMenuDetailTabProperty"
        mtMenuButton.TableName = "tMenuButton"
        mtConstraint.TableName = "tConstraint"
        mtSystemImage.TableName = "tSystemImage"
        tMenuSubDataSource.TableName = "tMenuSubDataSource"
        For Each dr As DataRow In mtMenuDetailTab.Rows
            s = dr("ListSource").ToString
            dr("ListSource") = gPassParameters(mSession.gParameterTable, s)
        Next
    End Sub

End Class

