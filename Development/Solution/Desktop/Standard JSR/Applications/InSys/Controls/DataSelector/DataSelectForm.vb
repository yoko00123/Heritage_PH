Option Explicit On
Option Strict On

Public Class DataSelectForm
    Public Property CheckedTable As DataTable

    'Public Event ButtonClick(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs)
    Public Sub New()
        InitializeComponent()
        MainList.FilterBarVisible = True
        ' MainList.SelectionModeButton.Enabled = False
        With MainList
            .GetStripButton("New").Enabled = False
            .Mode = GSCOM.UI.DataList.DataList.ViewMode.Selection
        End With
    End Sub

    Friend ReadOnly Property MainSelector() As DataSelectorList
        Get
            Return Me.MainList
        End Get
    End Property

    Private Sub MainList_ButtonClick(ByVal sender As Object, ByVal e As Interfaces.ZIDataList.ButtonClickEventArgs) Handles MainList.ButtonClick
        Select Case e.ButtonText
            Case "Open"
                If GetSelectedIDs() Is Nothing Then
                    MsgBox("No items selected")
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                End If
            Case "Close"
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
        End Select
    End Sub

    Public Function GetSelectedIDs() As Integer()
        Return MainList.GetSelectedIDs
    End Function

    Public Sub Init(ByVal pTableName As String, ByVal pConnection As SqlClient.SqlConnection, ByVal pSelectString As DataTable, ByVal pSort As String)
        MainList.Init(pTableName, pConnection, "", pSelectString, "", pSort)
    End Sub

    Private Sub SelectForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Static b As Boolean
        If Not b Then
            MainSelector.Go()
            CheckNodes()
            b = True
        End If
    End Sub
    ' Public Property ColumnName As String
    Public Sub CheckNodes()
        '  MainList.CheckNodes(CheckedTable, ColumnName)
    End Sub

    Private Sub DataSelectForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MainList.SaveSettings()
    End Sub
End Class
