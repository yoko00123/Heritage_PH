Friend Class EmployeeStatusSelector
    Inherits GSCOM.UI.DataSelector.DataSelector

    Public Sub New()
        With Me
            .ImageList = gImageList
            '.ImageKey = nDB.GetMenuValue(Database.Menu.MAINTENANCE_HumanResource_EmployeeStatus, Database.Tables.tMenu.Field.ImageFile).ToString
            .ImageKey = nDB.GetMenuValue(Database.Menu.MAINTENANCE_INSYSPEOPLE_EmployeeStatus, Database.Tables.tMenu.Field.ImageFile).ToString
            .GroupImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile).ToString

            '.GroupCount = 4
        End With
    End Sub
End Class
