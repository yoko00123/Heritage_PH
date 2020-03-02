Option Explicit On
Option Strict On



Friend Class EmployeePhotoFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tEmployeePhotoFile(gConnection)
    Private mtEmployeePhotoFile_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tEmployeeDailyScheduleFile_Detail(gConnection)
    'Private mControl As New InSys.DataControl 'Private mControl As New nDB.LeaveFileControl
    'Private mImportButton As ToolStripButton
    'Private mGenTemplateButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
        End With
        InitControl(pMenu)
        mtEmployeePhotoFile_Detail = DirectCast(Me.mDataset.Tables("tEmployeePhotoFile_Detail"), GSCOM.SQL.ZDataTable)
        mApplyButton = Me.GetStripButton("Apply")
        AddHandler mApplyButton.Click, AddressOf RenamePhotos
        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtEmployeePhotoFile_Detail)
    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
    End Sub

#End Region


#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeePhotoFile)
        End Set
    End Property



#End Region

    Private Sub RenamePhotos(sender As Object, e As EventArgs)
        Dim dt As New DataTable
        Dim dtemp As New Database.Tables.tPersona(Connection)
        Dim r1 As DataRow()
        dt = mtEmployeePhotoFile_Detail
        dtemp.ClearThenFill("")
        For Each dr As DataRow In dt.Rows
            r1 = dtemp.Select("Code = " & GSCOM.SQL.SQLFormat(dr("EmployeeCode")))
            If System.IO.File.Exists(nDB.GetSetting(Database.SettingEnum.PhotoPath).ToString & dr("ImageFile").ToString) Then
                If r1.Length > 0 Then
                    Dim FileName As String = dr("ImageFile").ToString
                    Dim FileNameElem As String() = FileName.Split("."c)
                    Dim Ext As String = FileNameElem(FileNameElem.Length - 1)
                    Try
                        My.Computer.FileSystem.RenameFile(nDB.GetSetting(Database.SettingEnum.PhotoPath).ToString & dr("ImageFile").ToString, r1(0).Item(Database.Tables.tPersona.Field.GUID).ToString & "." & Ext)
                        GSCOM.SQL.ExecuteNonQuery("UPDATE tPersona SET ImageFile = '" & r1(0).Item(Database.Tables.tPersona.Field.GUID).ToString & "." & Ext & "' WHERE Code = '" & dr.Item("EmployeeCode").ToString & "'", Connection)
                        dr("Comment") = "File Renamed"
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            Else
                If IsDBNull(dr("Comment").ToString) Then
                    dr("Comment") = "File Not Exist!!"
                End If
            End If
        Next
        mForm.SaveButton.PerformClick()
    End Sub


End Class
