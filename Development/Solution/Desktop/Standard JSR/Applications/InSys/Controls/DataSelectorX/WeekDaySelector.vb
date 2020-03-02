Friend Class WeekDaySelector
    Inherits GSCOM.Applications.InSys.DataSelector

    Public Sub New()
        'InitializeComponent()
        With Me
            .ImageList = gImageList
            .ImageKey = nDB.GetMenuValue(Database.Menu.MAINTENANCE_INSYSORBIT_DailySchedule, Database.Tables.tMenu.Field.ImageFile).ToString
            .GroupImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile).ToString
            .GroupCount = 0

        End With

    End Sub






    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'WeekDaySelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.Name = "WeekDaySelector"
        Me.Size = New System.Drawing.Size(598, 405)
        Me.ResumeLayout(False)

    End Sub
End Class
