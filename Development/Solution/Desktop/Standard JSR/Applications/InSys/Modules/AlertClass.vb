Option Explicit On
Option Strict On




Friend Class AlertClass
    Protected m_Connection As Data.SqlClient.SqlConnection
    Dim ImagePath, AgentName, strSql As String
    Dim Counter As Integer
    Dim xPos, yPos As Integer


    'Friend Sub NewChar(ByVal mobjController As AgentObjects.Agent, ByVal mobjCharacter As AgentObjects.IAgentCtlCharacterEx)
    '    Try
    '        strSql = "SELECT COUNT(*) counter from fAlertCounter(" & gUser & ",'" & Date.Now & "') "
    '        Counter = CInt(GSCOM.SQL.ExecuteScalar(strSql, gConnection))

    '    Catch ex As Exception
    '        Counter = 0
    '    End Try

    '    If Counter > 0 Then
    '        Try
    '            strSql = "SELECT dbo.fGetSystemAgent(" & gUser & ")"
    '            AgentName = GSCOM.SQL.ExecuteScalar(strSql, gConnection).ToString
    '        Catch ex As Exception
    '            AgentName = "James"
    '        End Try
    '        Try
    '            ' AgentName = "James"
    '            'mobjController = New AgentObjects.Agent()
    '            ImagePath = nDB.GetSetting(Database.SettingEnum.ResourcePath)
    '            With mobjController
    '                .Connected = True
    '                .Characters.Load("Agent", ImagePath & "MSAgent\" & AgentName & ".acs")
    '                mobjCharacter = .Characters("Agent")
    '            End With

    '            With mobjCharacter

    '                ' .Width = .OriginalWidth
    '                ' .Height = .OriginalHeight
    '                .MoveTo(CShort(xPos), CShort(yPos))
    '                .Show()
    '                .Balloon.Style = 1
    '                If Counter = 1 Then
    '                    .Speak("You have an unread alert")
    '                Else
    '                    .Speak("You have " & Counter & " unread alerts")
    '                End If
    '            End With
    '        Catch ex As Exception
    '            MsgBox("There was an error loading the Alert Agent", MsgBoxStyle.Exclamation)
    '        End Try

    '    End If


    'End Sub
    Friend Sub NewChar(ByVal pMainForm As MainForm)

        Try
            strSql = "SELECT COUNT(*) counter from fAlertCounter(" & gUser & ",'" & Date.Now & "') "
            Counter = CInt(GSCOM.SQL.ExecuteScalar(strSql, gConnection))

        Catch ex As Exception
            Counter = 0
        End Try

        If Counter > 0 Then
            'Try
            '    strSql = "SELECT dbo.fGetSystemAgent(" & gUser & ")"
            '    AgentName = GSCOM.SQL.ExecuteScalar(strSql, gConnection).ToString
            'Catch ex As Exception
            '    AgentName = "James"
            'End Try
            Try
                ' AgentName = "James"
                'mobjController = New AgentObjects.Agent()
                'ImagePath = nDB.GetSetting(Database.SettingEnum.ResourcePath)
                'With mobjController
                '    .Connected = True
                '    .Characters.Load("Agent", ImagePath & "MSAgent\" & AgentName & ".acs")
                '    mobjCharacter = .Characters("Agent")
                'End With

                'With mobjCharacter

                ' .Width = .OriginalWidth
                ' .Height = .OriginalHeight
                '.MoveTo(CShort(xPos), CShort(yPos))
                '.Show()
                '.Balloon.Style = 1
                Dim msg As String
                If Counter = 1 Then
                    msg = "You have an unread alert. Do you want to read it now?"
                Else
                    msg = "You have " & Counter & " unread alerts. Do you want to read them now?"
                End If
                If MsgBox(msg, MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
                    pMainForm.LoadList(63)
                Else

                End If
                'End With
            Catch ex As Exception
                MsgBox("There was an error loading the Alert Agent", MsgBoxStyle.Exclamation)
            End Try

        End If


    End Sub

    Public Sub SetPosition(ByVal x As Integer, ByVal y As Integer)
        xPos = x
        yPos = y
    End Sub


End Class
