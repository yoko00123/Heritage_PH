Friend Class DesignationSelector
    Inherits GSCOM.UI.DataSelector.DataSelector

    Public Sub New()
        With Me
            .ImageList = gImageList
            '.ImageKey = nDB.GetMenuValue(Database.Menu.Maintenance_Company_Designation, Database.Tables.tMenu.Field.ImageFile).ToString
            .ImageKey = nDB.GetMenuValue(Database.Menu.ORGANIZATIONALMANAGEMENT_Position, Database.Tables.tMenu.Field.ImageFile).ToString
            .GroupImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile).ToString
            '.ImageKeys.Add(nDB.GetMenuValue(Database.Menu.Maintenance_Company_Company, Database.Tables.tMenu.Field.ImageFile).ToString, "Company")
            .ImageKeys.Add(nDB.GetMenuValue(Database.Menu.ORGANIZATIONALMANAGEMENT_CompanyProfile, Database.Tables.tMenu.Field.ImageFile).ToString, "Company")
            '.ImageKeys.Add(nDB.GetMenuValue(Database.Menu.Maintenance_Company_JobClass, Database.Tables.tMenu.Field.ImageFile).ToString, "JobClass")
            .ImageKeys.Add(nDB.GetMenuValue(Database.Menu.ORGANIZATIONALMANAGEMENT_PositionLevel, Database.Tables.tMenu.Field.ImageFile).ToString, "JobClass")
            .GroupCount = 3
        End With
    End Sub
End Class
