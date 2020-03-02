Public Class LogDeviceSelector
    Inherits GSCOM.UI.DataSelector.DataSelector
    Friend nDB As Database.UserSession
    Public Sub New()
        With Me
            .ImageList = gImageList
            '.ImageKey = nDB.GetMenuValue(Database.Menu.FingerprintEnrollment_SetUp_LogDevice, Database.Tables.tMenu.Field.ImageFile).ToString
            '.GroupImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile).ToString
            '.ImageKeys.Add(Database.GetMenuValue(Database.Menu.Menu.Maintenance_Company_Company, Database.Tables.tMenu.Field.ImageKey).ToString, "Company")
            '.ImageKeys.Add(Database.GetMenuValue(Database.Menu.Maintenance_Company_Branch, Database.Tables.tMenu.Field.ImageKey).ToString, "Branch")
            .GroupCount = 0
        End With
    End Sub
End Class
