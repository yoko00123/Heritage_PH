Friend Class SystemApplicationSelector
    Inherits GSCOM.UI.DataSelector.DataSelector

    Public Sub New()
        With Me
            .ImageList = gImageList
            .ImageKey = nDB.GetMenuValue(Database.Menu.SYSTEM_SystemApplication, Database.Tables.tMenu.Field.ImageFile).ToString
            .GroupImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile).ToString
            '.ImageKeys.Add(nDB.GetMenuValue(Database.Menu.Menu.Maintenance_Company_Company, Database.Tables.tMenu.Field.ImageFile).ToString, "Company")
            '.ImageKeys.Add(nDB.GetMenuValue(Database.Menu.Maintenance_Company_Branch, Database.Tables.tMenu.Field.ImageFile).ToString, "Branch")
            '.ImageKeys.Add(nDB.GetMenuValue(Database.Menu.MasterFile_JobClass, Database.Tables.tMenu.Field.ImageFile).ToString, "JobClass")
            .GroupCount = 0
        End With
    End Sub
End Class
