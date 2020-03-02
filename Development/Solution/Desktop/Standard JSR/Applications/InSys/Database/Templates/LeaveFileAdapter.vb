Namespace Templates


    Partial Public Class LeaveFileAdapter
        Inherits System.ComponentModel.Component
        Private WithEvents _adapter As System.Data.OleDb.OleDbDataAdapter
        Private _connection As System.Data.OleDb.OleDbConnection
        Private _commandCollection() As System.Data.OleDb.OleDbCommand
        Private _clearBeforeFill As Boolean


        Private mConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""{0}"";Extended Properties=""Excel 8.0"""
        Public DataSource As String
        <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Private Sub InitConnection()
            Me._connection = New System.Data.OleDb.OleDbConnection
            Me._connection.ConnectionString = String.Format(mConn, DataSource)
        End Sub

        <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub New()
            MyBase.New()
            Me.ClearBeforeFill = True
        End Sub

        <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Private ReadOnly Property Adapter() As System.Data.OleDb.OleDbDataAdapter
            Get
                If (Me._adapter Is Nothing) Then
                    Me.InitAdapter()
                End If
                Return Me._adapter
            End Get
        End Property

        <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Property Connection() As System.Data.OleDb.OleDbConnection
            Get
                If (Me._connection Is Nothing) Then
                    Me.InitConnection()
                End If
                Return Me._connection
            End Get
            Set(ByVal value As System.Data.OleDb.OleDbConnection)
                Me._connection = value
                If (Not (Me.Adapter.InsertCommand) Is Nothing) Then
                    Me.Adapter.InsertCommand.Connection = value
                End If
                If (Not (Me.Adapter.DeleteCommand) Is Nothing) Then
                    Me.Adapter.DeleteCommand.Connection = value
                End If
                If (Not (Me.Adapter.UpdateCommand) Is Nothing) Then
                    Me.Adapter.UpdateCommand.Connection = value
                End If
                Dim i As Integer = 0
                Do While (i < Me.CommandCollection.Length)
                    If (Not (Me.CommandCollection(i)) Is Nothing) Then
                        CType(Me.CommandCollection(i), System.Data.OleDb.OleDbCommand).Connection = value
                    End If
                    i = (i + 1)
                Loop
            End Set
        End Property

        <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected ReadOnly Property CommandCollection() As System.Data.OleDb.OleDbCommand()
            Get
                If (Me._commandCollection Is Nothing) Then
                    Me.InitCommandCollection()
                End If
                Return Me._commandCollection
            End Get
        End Property

        <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property ClearBeforeFill() As Boolean
            Get
                Return Me._clearBeforeFill
            End Get
            Set(ByVal value As Boolean)
                Me._clearBeforeFill = value
            End Set
        End Property

        <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Private Sub InitAdapter()
            Me._adapter = New System.Data.OleDb.OleDbDataAdapter
            Dim tableMapping As System.Data.Common.DataTableMapping = New System.Data.Common.DataTableMapping
            tableMapping.SourceTable = "Table"
            tableMapping.DataSetTable = "Database"

            tableMapping.ColumnMappings.Add("EmployeeCode", "EmployeeCode")
            tableMapping.ColumnMappings.Add("Employee", "Employee")


            Me._adapter.TableMappings.Add(tableMapping)
            Me._adapter.InsertCommand = New System.Data.OleDb.OleDbCommand
            Me._adapter.InsertCommand.Connection = Me.Connection
            Me._adapter.InsertCommand.CommandText = "INSERT INTO `Database` (`EmployeeCode`,`Employee`) VALUES (?, ?)"
            Me._adapter.InsertCommand.CommandType = System.Data.CommandType.Text
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("EmployeeCode", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Employee", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Employee", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EmployeeCode", System.Data.DataRowVersion.Current, False, Nothing))

        End Sub




        Private Sub InitCommandCollection()
            Me._commandCollection = New System.Data.OleDb.OleDbCommand(0) {}
            Me._commandCollection(0) = New System.Data.OleDb.OleDbCommand
            Me._commandCollection(0).Connection = Me.Connection
            Me._commandCollection(0).CommandText = "SELECT `EmployeeCode`, `Employee` FROM `Database`"
            Me._commandCollection(0).CommandType = System.Data.CommandType.Text
        End Sub



        <System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")> _
        Public Overridable Overloads Function Update(ByVal dataRow As System.Data.DataRow) As Integer
            Return Me.Adapter.Update(New System.Data.DataRow() {dataRow})
        End Function

        <System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")> _
        Public Overridable Overloads Function Update(ByVal dataRows() As System.Data.DataRow) As Integer
            Return Me.Adapter.Update(dataRows)
        End Function

        <System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), _
         System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, True)> _
        Public Overridable Overloads Function Insert(ByVal No As Integer, ByVal Employee As String, ByVal Designation As String, ByVal Department As String, ByVal Gender As String, ByVal EmployeeCode As String, ByVal EmployeeStatus As String, ByVal Date1 As Date)
            Me.Adapter.InsertCommand.Parameters(0).Value = CType(EmployeeCode, String)
            Me.Adapter.InsertCommand.Parameters(1).Value = CType(Employee, String)



            Dim previousConnectionState As System.Data.ConnectionState = Me.Adapter.InsertCommand.Connection.State
            If ((Me.Adapter.InsertCommand.Connection.State And System.Data.ConnectionState.Open) _
                        <> System.Data.ConnectionState.Open) Then
                Me.Adapter.InsertCommand.Connection.Open()
            End If
            Try
                Dim returnValue As Integer = Me.Adapter.InsertCommand.ExecuteNonQuery
                Return returnValue
            Finally
                If (previousConnectionState = System.Data.ConnectionState.Closed) Then
                    Me.Adapter.InsertCommand.Connection.Close()
                End If
            End Try
        End Function
    End Class

End Namespace