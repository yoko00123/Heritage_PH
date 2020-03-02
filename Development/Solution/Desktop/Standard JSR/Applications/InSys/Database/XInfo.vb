Option Explicit On
Option Strict On



Public Class XInfo
    Inherits InfoSetBase

    Private myDT As GSCOM.SQL.ZDataTable
    'Private mControl As New InSys.DataControl

    Public Sub New(ByVal pSession As Database.UserSession, ByVal pMenu As Integer, ByVal pID As Integer)
        MyBase.New(pSession, pID)
        Dim s As String
        s = mSession.GetMenuValue(CType(pMenu, Database.Menu), Database.Tables.tMenu.Field.TableName).ToString
        myDT = New GSCOM.SQL.ZDataTable(pSession.Connection, s)
        With mDataset.Tables
            .Add(Table)
        End With
        InitControl(pMenu)

        'AfterNew()
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, GSCOM.SQL.ZDataTable)
        End Set
    End Property

    Private Property CanSave As Boolean = True

    Public Function Save() As Boolean
        Dim tran As SqlClient.SqlTransaction = Nothing
        'Dim t As New System.Threading.Thread(AddressOf Navigate)
        Dim r As Boolean
        Dim i As Integer
        Try
            'SetDocumentNo()
            'SetCodeNo()
            'ValidData()
            If CanSave() Then
                Me.Row.EndEdit()
                Connection.Open()
                If UseTransaction Then
                    tran = Connection.BeginTransaction
                End If
                Try
                    'NOTE: MUST SAVE THE ROWSTATE BEFORE SAVING
                    'NOTE: THIS WOULD BE USED ON THE "SAVED" EVENT
                    Dim rs As DataRowState
                    rs = Me.Row.RowState


                    If UseTransaction Then
                        Table.Adapter.InsertCommand.Transaction = tran
                        Table.Adapter.UpdateCommand.Transaction = tran
                        Table.Adapter.DeleteCommand.Transaction = tran
                        'SetDocumentNo(tran)
                    Else
                        'SetDocumentNo()
                    End If
                    i = Table.Update()

                    '  Dim dg As DataGridView

                    For Each dt As GSCOM.SQL.ZDataTable In mDataset.Tables
                        If dt IsNot Table Then
                            'If InStr(NoTransactionTables, dt.TableName) = 0 Then
                            '    dg = Me.GetDataGridView(dt) 'for fast operation-----\
                            '    If dg IsNot Nothing Then dg.DataSource = Nothing
                            If UseTransaction Then
                                dt.Adapter.InsertCommand.Transaction = tran
                                dt.Adapter.UpdateCommand.Transaction = tran
                                dt.Adapter.DeleteCommand.Transaction = tran
                            End If
                            Try
                                dt.Update()
                            Catch ex As Exception
                                Throw ex
                            Finally
                                '    If dg IsNot Nothing Then dg.DataSource = dt
                            End Try
                        End If
                        'End If
                    Next
                    '  RaiseEvent Saved(Me, New SavedEventArgs(tran, rs))
                    If UseTransaction Then
                        tran.Commit()
                    End If
                    'moved from here
                    r = True
                    Save = r
                Catch ex As Exception
                    Try
                        If UseTransaction Then
                            tran.Rollback()
                        End If


                        ''this is to check constraints-------------------------\
                        Dim s As String = ""
                        '  s = getSystemMessage(ex.Message)
                        ''to make sure the rows are ordered by name use this: 
                        ''.Select("", "Name"). but this is already done in query
                        'For Each dr As DataRow In nDB.SystemMessageTable.Rows
                        '    s = dr("Name").ToString
                        '    If InStr(ex.Message, s, CompareMethod.Text) > 0 Then
                        '        s = dr("Description").ToString
                        '        Throw New Exception(s, ex)
                        '    End If
                        'Next
                        ''this is to check constraints-------------------------/

                        Throw New Exception(s, ex)
                        Throw ex

                    Catch ex2 As Exception
                        Throw ex2
                    End Try
                End Try

                If r Then 'robbie 20061102
                    'moved here
                    SetDefaultValues() 'ROBBIE 20060629 Implicitly call setdafualtvalues because IDs are changed
                    'Dim e As New CommitedEventArgs
                    'e.MainTableChanged = i > 0
                    'RaiseEvent Commited(Me, e)
                    'If mReloadAfterCommit Then
                    '    Application.DoEvents()
                    LoadInfo(CInt(Me.Row(0)))
                    'Else
                    '    Dim tg As TreeGrid
                    '    Dim cc() As Control = Nothing
                    '    GSCOM.UI.AllControls(cc, mForm)
                    '    For Each c As Control In cc
                    '        If TypeOf c Is TreeGrid Then
                    '            tg = DirectCast(c, TreeGrid)
                    '            tg.LoadTree(tg.DataSourceFilter, False, "") 'no need to update listsource
                    '        End If
                    '    Next
                    '    EnableButtons(CInt(Me.Row("ID"))) 'else because this is also called on loadinfo
                    '    EnableDetailMenus()
                    '    'LoadDetailMenus()
                    '    Navigate() 'ROBBIE|20091119|refresh browser
                    'End If
                End If


                ' RefreshListing()
            Else
                MsgBox("Invalid input", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
            'If tran.Connection Is Nothing Then
            '    Console.WriteLine("nothing")
            'End If
        End Try
    End Function



End Class
