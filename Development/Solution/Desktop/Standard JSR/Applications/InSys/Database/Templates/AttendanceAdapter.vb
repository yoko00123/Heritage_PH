
Namespace Templates



    Partial Public Class AttendanceAdapter
        Inherits System.ComponentModel.Component

        Private mConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""{0}"";Extended Properties=""Excel 8.0"""
        Public DataSource As String
        <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
                     Private Sub InitConnection()
            Me._connection = New System.Data.OleDb.OleDbConnection
            Me._connection.ConnectionString = String.Format(mConn, DataSource)
        End Sub

        Private WithEvents _adapter As System.Data.OleDb.OleDbDataAdapter

        Private _connection As System.Data.OleDb.OleDbConnection

        Private _commandCollection() As System.Data.OleDb.OleDbCommand

        Private _clearBeforeFill As Boolean

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
            tableMapping.DataSetTable = "Sheet1$"
            tableMapping.ColumnMappings.Add("Department", "Department")
            tableMapping.ColumnMappings.Add("EmployeeCode", "EmployeeCode")
            tableMapping.ColumnMappings.Add("EmployeeName", "EmployeeName")
            tableMapping.ColumnMappings.Add("Date", "Date")
            tableMapping.ColumnMappings.Add("TimeIn", "TimeIn")
            tableMapping.ColumnMappings.Add("TimeOut", "TimeOut")
            tableMapping.ColumnMappings.Add("ActualHours", "ActualHours")
            tableMapping.ColumnMappings.Add("Tardy", "Tardy")
            tableMapping.ColumnMappings.Add("OT", "OT")
            tableMapping.ColumnMappings.Add("TotalHours", "TotalHours")
            tableMapping.ColumnMappings.Add("ND", "ND")
            tableMapping.ColumnMappings.Add("Remarks", "Remarks")
            Me._adapter.TableMappings.Add(tableMapping)
            Me._adapter.InsertCommand = New System.Data.OleDb.OleDbCommand
            Me._adapter.InsertCommand.Connection = Me.Connection
            Me._adapter.InsertCommand.CommandText = "INSERT INTO `Sheet1$` (`Department`, `EmployeeCode`, `EmployeeName`, `Date`, `TimeIn`, `TimeOut" & _
                "`, `ActualHours`, `Tardy`, `OT`, `TotalHours`, `ND`, `Remarks`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?," & _
                " ?, ?)"
            Me._adapter.InsertCommand.CommandType = System.Data.CommandType.Text
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Department", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Department", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("EmployeeCode", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EmployeeCode", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("EmployeeName", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EmployeeName", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Date", System.Data.OleDb.OleDbType.[Date], 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Date", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("TimeIn", System.Data.OleDb.OleDbType.[Date], 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "TimeIn", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("TimeOut", System.Data.OleDb.OleDbType.[Date], 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "TimeOut", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("ActualHours", System.Data.OleDb.OleDbType.[Double], 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ActualHours", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Tardy", System.Data.OleDb.OleDbType.[Double], 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Tardy", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("OT", System.Data.OleDb.OleDbType.[Double], 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "OT", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("TotalHours", System.Data.OleDb.OleDbType.[Double], 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "TotalHours", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("ND", System.Data.OleDb.OleDbType.[Double], 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ND", System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Remarks", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Remarks", System.Data.DataRowVersion.Current, False, Nothing))
        End Sub

        '<System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        'Private Sub InitConnection()
        '    Me._connection = New System.Data.OleDb.OleDbConnection
        '    Me._connection.ConnectionString = Global.GSCOM.Applications.InSys.Database.My.MySettings.Default.AttendanceTemplateConnectionString
        'End Sub

        <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Private Sub InitCommandCollection()
            Me._commandCollection = New System.Data.OleDb.OleDbCommand(0) {}
            Me._commandCollection(0) = New System.Data.OleDb.OleDbCommand
            Me._commandCollection(0).Connection = Me.Connection
            Me._commandCollection(0).CommandText = "SELECT Department, EmployeeCode, EmployeeName, [Date], TimeIn, TimeOut, ActualHours, Tardy, OT, TotalHours" & _
                ", ND, Remarks FROM [Sheet1$]"
            Me._commandCollection(0).CommandType = System.Data.CommandType.Text
        End Sub

        <System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), _
         System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Fill, True)> _
        Public Overridable Overloads Function Fill(ByVal dataTable As AttendanceTable) As Integer
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function

        '<System.Diagnostics.DebuggerNonUserCodeAttribute(), _
        ' System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), _
        ' System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.[Select], True)> _
        'Public Overridable Overloads Function GetData() As DataSet1._Sheet1_DataTable
        '    Me.Adapter.SelectCommand = Me.CommandCollection(0)
        '    Dim dataTable As DataSet1._Sheet1_DataTable = New DataSet1._Sheet1_DataTable
        '    Me.Adapter.Fill(dataTable)
        '    Return dataTable
        'End Function

        <System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")> _
        Public Overridable Overloads Function Update(ByVal dataTable As AttendanceTable) As Integer
            Return Me.Adapter.Update(dataTable)
        End Function

        '<System.Diagnostics.DebuggerNonUserCodeAttribute(), _
        ' System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")> _
        'Public Overridable Overloads Function Update(ByVal dataSet As DataSet1) As Integer
        '    Return Me.Adapter.Update(dataSet, "Sheet1$")
        'End Function

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
        Public Overridable Overloads Function Insert(ByVal Department As String, ByVal EmployeeCode As String, ByVal EmployeeName As String, ByVal _Date As System.Nullable(Of Date), ByVal TimeIn As System.Nullable(Of Date), ByVal TimeOut As System.Nullable(Of Date), ByVal ActualHours As System.Nullable(Of Double), ByVal Tardy As System.Nullable(Of Double), ByVal OT As System.Nullable(Of Double), ByVal TotalHours As System.Nullable(Of Double), ByVal ND As System.Nullable(Of Double), ByVal Remarks As String) As Integer
            If (Department Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(0).Value = System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(0).Value = CType(Department, String)
            End If
            If (EmployeeCode Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(1).Value = System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(1).Value = CType(EmployeeCode, String)
            End If
            If (EmployeeName Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(2).Value = System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(2).Value = CType(EmployeeName, String)
            End If
            If (_Date.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(3).Value = CType(_Date.Value, Date)
            Else
                Me.Adapter.InsertCommand.Parameters(3).Value = System.DBNull.Value
            End If
            If (TimeIn.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(4).Value = CType(TimeIn.Value, Date)
            Else
                Me.Adapter.InsertCommand.Parameters(4).Value = System.DBNull.Value
            End If
            If (TimeOut.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(5).Value = CType(TimeOut.Value, Date)
            Else
                Me.Adapter.InsertCommand.Parameters(5).Value = System.DBNull.Value
            End If
            If (ActualHours.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(6).Value = CType(ActualHours.Value, Double)
            Else
                Me.Adapter.InsertCommand.Parameters(6).Value = System.DBNull.Value
            End If

            If (Tardy.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(7).Value = CType(Tardy.Value, Double)
            Else
                Me.Adapter.InsertCommand.Parameters(7).Value = System.DBNull.Value
            End If
            If (OT.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(8).Value = CType(OT.Value, Double)
            Else
                Me.Adapter.InsertCommand.Parameters(8).Value = System.DBNull.Value
            End If
            If (TotalHours.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(9).Value = CType(TotalHours.Value, Double)
            Else
                Me.Adapter.InsertCommand.Parameters(9).Value = System.DBNull.Value
            End If
            If (ND.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(10).Value = CType(ND.Value, Double)
            Else
                Me.Adapter.InsertCommand.Parameters(10).Value = System.DBNull.Value
            End If
            If (Remarks Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(11).Value = System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(11).Value = CType(Remarks, String)
            End If
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