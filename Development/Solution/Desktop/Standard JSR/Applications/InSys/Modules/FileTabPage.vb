Option Explicit On
Option Strict On



Friend Class FileTabPage
    Inherits InfoSetDetailMenu

    Protected Overrides Sub ButtonClick(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs)
        Dim pTabPage As GSCOM.Interfaces.ZIDataTabPageList = CType(sender, GSCOM.Interfaces.ZIDataTabPageList)
        Try
            Select Case e.ButtonText
                Case "New"
                    NewFile()
                Case "Open"
                    OpenFile(e.SelectedID)
                Case "Open Info"
                    If e.SelectedID <> 0 Then
                        Dim pInfoSet As InfoSet
                        pInfoSet = GetInfoSet(Database.Menu.ADMINISTRATIVE_File)
                        If pInfoSet Is Nothing Then
                            pInfoSet = NewInfo(Database.Menu.ADMINISTRATIVE_File, Nothing, e.SelectedID)
                        Else
                            pInfoSet.LoadInfo(e.SelectedID)
                        End If
                        Application.DoEvents()
                        If pInfoSet IsNot Nothing Then
                            pInfoSet.ShowDialog()
                        End If
                    End If
                Case Else
                    Dim dt As DataTable = CType(pTabPage.MainList.DataSource, DataTable)
                    MainModule.SelectStandardMenu(pTabPage, dt, e, pTabPage)

            End Select


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private ReadOnly Property FilePath As String
        Get
            Return nDB.GetSetting(Database.SettingEnum.FilePath)
        End Get
    End Property

    Private Sub NewFile()
        Dim ofd As New OpenFileDialog
        With ofd
            .CheckFileExists = True
            .CheckPathExists = True
            .AddExtension = True
            .Filter = "All files (*.*)|*.*"
            .ShowDialog()
        End With
        Dim fn As String
        fn = ofd.FileName
        ofd = Nothing
        If fn <> "" Then
            'If Me.Image IsNot Nothing Then
            ' Me.Image.Dispose()
            ' GC.Collect()
            ' End If
            'Delete()
            Application.DoEvents()
            Dim FileName As String
            FileName = "F-" & Guid.NewGuid.ToString & IO.Path.GetExtension(fn)
            Dim FullPath As String
            FullPath = IO.Path.Combine(FilePath, FileName)
            IO.File.Copy(fn, FullPath, True)
            Application.DoEvents()
            'LoadImage()

            Dim dtx As New Database.Tables.tFile(gConnection)
            Dim dr As DataRow = dtx.NewRow
            dr("GUID_Parent") = mID
            dr("Name") = IO.Path.GetFileNameWithoutExtension(fn)
            dr("FileName") = FileName
            dr("FileExtension") = IO.Path.GetExtension(fn)
            dr("OriginalPath") = fn
            dtx.Rows.Add(dr)
            dtx.Update()

            Me.Go()

        End If
    End Sub

    Private Sub OpenFile(ByVal pID As Integer)
        Dim vFileName As String
        Dim vFile As New Database.Tables.tFile(gConnection)
        vFile.ClearThenFill("ID=" & pID.ToString)
        vFileName = vFile.Get("FileName").ToString
        Process.Start(IO.Path.Combine(FilePath, vFileName))
    End Sub

    Public Sub New(ByVal pInfoSet As InfoSet)
        MyBase.New(pInfoSet, Nothing, Database.Menu.ADMINISTRATIVE_File, "GUID", "GUID_Parent")
        'RemoveHandler mPage.ButtonClick, AddressOf MainModule.ButtonClick
        'AddHandler mPage.ButtonClick, AddressOf BrowseFile
        mList.OpenInfoButton.Visible = True

    End Sub
End Class
