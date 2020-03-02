
''' <summary>
''' LJ 20140325
''' </summary>
''' <remarks></remarks>
Public Class NetGlobal

    Private nSession As UserSession
    Public IsNetGlobal As Int32 = 0 'LJ 20140325
    Private gConnection As System.Data.SqlClient.SqlConnection

    Public ReportPath As String
    Public ResourcePath As String
    Public PhotosPath As String
    Public StyleSheetPath As String
    Public TemplatesPath As String
    Public FilesPath As String

    Public Sub New(Session As UserSession, con As System.Data.SqlClient.SqlConnection)
        nSession = Session
        gConnection = con
        LoadSettingTableIfNull()
        Me.Update()
    End Sub

    Public Sub LoadSettingTableIfNull(Optional force As Boolean = False)
        If nSession.SettingTable Is Nothing OrElse force Then
            Dim s As String = "SELECT Name,Value FROM tSetting WHERE (Active=1)"
            nSession.SettingTable = GSCOM.SQL.TableQuery(s, gConnection)
        End If
    End Sub

    Public Function GetNetGlobal() As Int32
        Dim rec As Object = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('RESOURCE UPDATE')", gConnection)
        Return Convert.ToInt32(IIf(IsDBNull(rec), 0, rec))
    End Function

    Public Function GetNetPath() As String
        Return Convert.ToString(GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('RESOURCE UPDATE PATH')", gConnection))
    End Function

    Public Sub Update()
        IsNetGlobal = GetNetGlobal()

        If IsNetGlobal = 0 Then
            ReportPath = nSession.GetSetting(SettingEnum.ReportPath)
            ResourcePath = nSession.GetSetting(SettingEnum.ResourcePath)
            PhotosPath = nSession.GetSetting(SettingEnum.PhotoPath)
            StyleSheetPath = nSession.GetSetting(SettingEnum.StyleSheetPath)
            TemplatesPath = nSession.GetSetting(SettingEnum.ExcelTemplatePath)
            FilesPath = nSession.GetSetting(SettingEnum.FilePath)
        Else
            ReportPath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData, "Contents\Reports\")
            ResourcePath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData, "Contents\Resources\")
            PhotosPath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData, "Contents\Photos\")
            StyleSheetPath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData, "Contents\StyleSheets\")
            TemplatesPath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData, "Contents\ExcelTemplates\")
            FilesPath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData, "Contents\Files\")
        End If
    End Sub

End Class
