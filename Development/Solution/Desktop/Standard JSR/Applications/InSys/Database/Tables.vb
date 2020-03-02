'NOTE:  THIS DOCUMENT CONTAINS MACHINE GENERATED CODE. DO NOT MODIFY MANUALLY. 
'	MANUAL MODIFICATION WOULD BE LOST WHEN THIS DOCUMENT IS REGENERATED.
' GENERATION DATETIME: 4/11/2014 9:59:12 AM

Namespace Tables


#Region "dtproperties"

Public Class dtproperties
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [id]
[objectid]
[property]
[value]
[uvalue]
[lvalue]
[version]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "dtproperties")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "sysdiagrams"

Public Class sysdiagrams
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [name]
[principal_id]
[diagram_id]
[version]
[definition]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "sysdiagrams")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAccount"

Public Class tAccount
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_NormalBalance]
[WithDepartmentCode]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAccount")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAccountability"

Public Class tAccountability
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_JobClass]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAccountability")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAccountabilityChecklist"

Public Class tAccountabilityChecklist
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Accountability]
[ID_Checklist]
[ID_Employee]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAccountabilityChecklist")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAccountCode"

Public Class tAccountCode
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_JournalVoucherType]
[ID_CostCenter]
[Group]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAccountCode")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAccountingParam"

Public Class tAccountingParam
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[Value]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAccountingParam")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAcctType"

Public Class tAcctType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAcctType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAddressType"

Public Class tAddressType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAddressType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlert"

Public Class tAlert
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_User]
[ID_Employee]
[ID_AlertType]
[ID_AlertNotificationPeriod]
[Date]
[SeqNo]
[IsActive]
[Comment]
[IsRead]
[ID_Usergroup]
[ID_Menu]
[ID_Original]
[Message]
[ID_EmployeeMovement]
[ID_Company]
[ID_PrevCompany]
[ID_Branch]
[ID_PrevBranch]
[ID_CostCenter]
[ID_PrevCostCenter]
[ID_Department]
[ID_PrevDepartment]
[ID_Designation]
[ID_PrevDesignation]
[ID_TaxExemption]
[ID_PrevTaxExemption]
[ID_EmployeeStatus]
[ID_PrevEmployeeStatus]
[ID_PayrollScheme]
[ID_PrevPayrollScheme]
[ID_PayrollFrequency]
[ID_PrevPayrollFrequency]
[PreviousMonthlyRate]
[MonthlyRate]
[ID_AlertTypeReferenceDate]
[IsReverted]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAlert")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlertNotificationPeriod"

Public Class tAlertNotificationPeriod
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Period]
[Value]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAlertNotificationPeriod")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlertType"

Public Class tAlertType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Before]
[After]
[ReferenceDate]
[ActualDate]
[EveryMonthNumber]
[Recurring]
[ID_Period]
[ID_AlertTypeReferenceDate]
[ID_AlertTypeReferencePeriod]
[PeriodValue]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ID_Month]
[AlertDate]
[ID_Menu]
[ID_Company]
[ID_MenuTabField]
[SpecificDate]
[Message]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAlertType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlertTypeDesignation"

Public Class tAlertTypeDesignation
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_AlertType]
[ID_Designation]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAlertTypeDesignation")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlertTypeEmployeeStatus"

Public Class tAlertTypeEmployeeStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_AlertType]
[ID_EmployeeStatus]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAlertTypeEmployeeStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlertTypeReferenceDate"

Public Class tAlertTypeReferenceDate
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Menu]
[ID_MenuTabField]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAlertTypeReferenceDate")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlphaList"

Public Class tAlphaList
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_AlphalistType]
[StartDate]
[EndDate]
[Year]
[Comment]
            [ReturnDate]
            IsPosted
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAlphaList")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlphaList_Detail"

Public Class tAlphaList_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Alphalist]
[ID_Employee]
[StartDate]
[EndDate]
[RDOCode]
[BasicSalary]
[Commission]
[Representation]
[Transpo]
[COLA]
[HousingAllowance]
[ProfitSharing]
[DirectorFee]
[HazardPay]
[TotalNonTaxable]
[TotalTaxable]
[TaxableCompIncPres]
[GrossTaxCompIncome]
[TaxCompIncome]
[OTPay]
[Comment]
[OtherRegular]
[OtherSupplementary]
[NonTaxBasicSalary]
[NonTaxSalAndOthers]
[TIN]
[LastName]
[FirstName]
[MiddleName]
[pGrossCompIncome]
[GrossCompIncome]
[pNonTaxBasicSMW]
[pNonTaxHolidayPay]
[pNonTaxOTPay]
[pNonTaxNDPay]
[pNonTaxHazardPay]
[pNonTaxTMonth]
[pNonTaxDeMinimis]
[pContri]
[pNonTaxSalAndOthers]
[pTotalNonTaxCompensation]
[pTaxBasicSalary]
[pTaxTMonth]
[pTaxSalAndOthers]
[pTotalTaxCompensation]
[DailySMW]
[MonthlySMW]
[YearlySMW]
[DaysPerYear]
[NonTaxHolidayPay]
[NonTaxOTPay]
[NonTaxNDPay]
[NonTaxHazardPay]
[NonTaxTMonth]
[NonTaxDeminimis]
[Contri]
[TotalNonTaxCompensation]
[TaxBasicSalary]
[TaxTMonth]
[TaxSalAndOthers]
[TotalTaxCompensation]
[TotalTaxCompPresPrev]
[ExemptionCode]
[ExemptionAmt]
[HealthInsurance]
[NetTaxableCompInc]
[TaxDue]
[pWTaxJanNov]
[WTaxJanNov]
[WTaxDec]
[TaxRefund]
[AmtAsAdjusted]
[SubstitutedFiling]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAlphaList_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlphaListType"

Public Class tAlphaListType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAlphaListType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAlphalistTExtGen"

    Public Class tAlphalistTExtGen
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [Name]
            [Year]
            [DateTimeCreated]
            [DateTimeModified]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tAlphalistTExtGen")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region


#Region "tAttendance"

Public Class tAttendance
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_DailySchedule]
[ID_Leave]
[ID_ImportedAttendance_Detail]
[ID_EmployeeDailySchedule]
[Date]
[TimeIn]
[TimeOut]
[MinuteIn]
[MinuteOut]
[Days]
[Hours]
[Tardy]
[OT]
[ND]
[IsComplete]
[SeqNo]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[ID_EmployeeAttendanceLog]
[ComputedTimeIn]
[ComputedTimeOut]
[TempMinuteIn]
[TempMinuteOut]
[FromOB]
[ID_AttendanceFile_Detail]
[OBIN]
[OBOUT]
[WorkDate]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttendance")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAttendanceDailySummary"

Public Class tAttendanceDailySummary
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_DailySchedule]
[Date]
[REG]
[OT]
[ND]
[NDOT]
[Code]
[Name]
[SeqNo]
[IsModified]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttendanceDailySummary")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAttendanceDailySummaryView"

Public Class tAttendanceDailySummaryView
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[StartDate]
[EndDate]
[ID_Department]
[ID_Designation]
[ID_EmployeeStatus]
[ID_Gender]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttendanceDailySummaryView")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAttendanceFile"

Public Class tAttendanceFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Date]
[ID_Company]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttendanceFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAttendanceFile_Detail"

Public Class tAttendanceFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_AttendanceFile]
[Employee]
[EmployeeCode]
[Date]
[TimeIn]
[TimeOut]
[Comment]
[ComputedTimeIn]
[ComputedTimeOut]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttendanceFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAttendanceLogType"

Public Class tAttendanceLogType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttendanceLogType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAttendanceType"

Public Class tAttendanceType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttendanceType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAttendanceView"

Public Class tAttendanceView
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[StartDate]
[EndDate]
[ID_Employee]
[IsComplete]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ID_Section]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttendanceView")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAttranExport"

Public Class tAttranExport
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_Branch]
[ID_Month]
[Year]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttranExport")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAttranExport_Detail"

Public Class tAttranExport_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_AttranExport]
[ID_Employee]
[EmployeeAmt]
[EmployerAmt]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAttranExport_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAuditTrail"

Public Class tAuditTrail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ParentID]
[ID_AuditTrailType]
[Name]
[OldValue]
[NewValue]
[DateTime]
[Details]
[Comment]
[ImageFile]
[Hostname]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAuditTrail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tAuditTrailType"

Public Class tAuditTrailType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tAuditTrailType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBank"

Public Class tBank
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
[ImageFile]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBank")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBankAcct"

Public Class tBankAcct
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBankAcct")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBankExport"

Public Class tBankExport
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_PayrollPeriod]
[ID_CompanyBankAcct]
[ID_BankAcct]
[Comment]
[ID_UserGroup]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBankExport")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBankExport_Detail"

Public Class tBankExport_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_BankExport]
[ID_Employee]
[LastName]
[FirstName]
[MiddleName]
[BankAcctNo]
[NetAmt]
[PayDate]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBankExport_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBankSetting"

Public Class tBankSetting
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[Value]
[ID_Bank]
[SeqNo]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBankSetting")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBatchFingerDataTransfer"

Public Class tBatchFingerDataTransfer
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[ImagePath]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBatchFingerDataTransfer")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBeneficiary"

Public Class tBeneficiary
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[LastName]
[FirstName]
[MiddleName]
[ID_Persona]
[ID_CivilStatus]
[BirthDate]
[Relationship]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[Name]
[Age]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBeneficiary")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBenefit"

Public Class tBenefit
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[SeqNo]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBenefit")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBenefitSuite"

Public Class tBenefitSuite
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[RequiredYears]
[SeqNo]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ID_Benefit]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBenefitSuite")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBenefitSuite_Detail"

Public Class tBenefitSuite_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_BenefitSuite]
[ID_Benefit]
[SeqNo]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ID_Employee]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBenefitSuite_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBloodType"

Public Class tBloodType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBloodType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBranch"

Public Class tBranch
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[Code]
[Name]
[ID_BranchGroup]
[ID_Area]
[ID_WeeklySchedule]
[EmailAddress]
[SeqNo]
[IsActive]
[Address]
[RDOCode]
[ZipCode]
[TelNo]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBranch")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBranchAssignment"

Public Class tBranchAssignment
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_Branch]
[StartDate]
[EndDate]
[SeqNo]
[Comment]
[DateApplied]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBranchAssignment")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBranchGroup"

Public Class tBranchGroup
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBranchGroup")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tBrand"

Public Class tBrand
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
[ImagePath]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tBrand")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCallType"

Public Class tCallType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCallType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCanvassSheet"

Public Class tCanvassSheet
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[DocumentNo]
[Date]
[ID_PurchaseRequest]
[TotalAmount]
[ImageFile]
[ImagePath]
[SeqNo]
[IsActive]
[Comment]
[ID_CreatedBy]
[DateCreated]
[ID_ApprovedBy]
[DateApproved]
[ID_Company]
[ID_FilingStatus]
[ID_CancelledBy]
[DateCancelled]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCanvassSheet")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCharacterReference"

Public Class tCharacterReference
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Persona]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ContactNo]
[Occupation]
[Relationship]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCharacterReference")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tChatMessage"

Public Class tChatMessage
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[DateTime]
[Comment]
[ID_SenderEmployee]
[Message]
[ID_RecipientEmployee]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tChatMessage")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tChatMessageRecipient"

Public Class tChatMessageRecipient
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ChatMessage]
[ID_Employee]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tChatMessageRecipient")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tChequeStatus"

Public Class tChequeStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tChequeStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCitizenship"

Public Class tCitizenship
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCitizenship")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCity"

Public Class tCity
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_JobMatching]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCity")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCivilStatus"

Public Class tCivilStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCivilStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tClientStatus"

Public Class tClientStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[StatusColor]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tClientStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tClientType"

Public Class tClientType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tClientType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCOA"

Public Class tCOA
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
[ID_AccountType]
[ID_SubsidiaryType]
[ID_Subsidiary]
[ID_NormalBalance]
[WithCostCenterCode]
[PostingKey]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCOA")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCOATemplateFile"

Public Class tCOATemplateFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCOATemplateFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCOATemplateFile_Detail"

Public Class tCOATemplateFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[SeqNo]
[IsActive]
[Comment]
[ID_COATemplateFile]
[AccountType]
[AccountCode]
[AccountName]
[NormalBalance]
[WithCostCenter]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCOATemplateFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tColor"

Public Class tColor
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[SeqNo]
[IsActive]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tColor")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompany"

Public Class tCompany
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_CompanyGroup]
[ID_CompanyClassification]
[ID_CompanyType]
[ID_LogFileFormat]
[Code]
[Name]
[Address]
[ZipCode]
[TIN]
[SSSNo]
[PhilHealthNo]
[HDMFNo]
[TelNo]
[President]
[VicePresident]
[TradeName]
[Business]
[Owner]
[VatRegNo]
[Overview]
[ProductsAndServices]
[ImageFile]
[SeqNo]
[IsActive]
[BusinessNature]
[BranchCode]
[isCOAFinalized]
[AcctPeriod]
[AcctYear]
[isYearly]
[ReportImageFile]
[Comment]
[ID_Branch]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompany")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyAttendees"

Public Class tCompanyAttendees
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_ClientAppointment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyAttendees")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyBankAcct"

Public Class tCompanyBankAcct
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_Company]
[ID_Bank]
[No]
[BankName]
[BankAddress]
[AttentionHeader]
[ReportPreparedBy]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyBankAcct")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyBankAcctSetting"

Public Class tCompanyBankAcctSetting
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[Value]
[ID_CompanyBankAcct]
[SeqNo]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyBankAcctSetting")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyBankSetting"

Public Class tCompanyBankSetting
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[Value]
[ID_CompanyBank]
[SeqNo]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyBankSetting")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyGroup"

Public Class tCompanyGroup
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyGroup")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyMenu"

Public Class tCompanyMenu
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
[ID_Menu]
[ReportTitle]
[ReportSubTitle]
[IsLandscape]
[IsCertification]
[ContentHeader]
[ContentFooter]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyMenu")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyMenuSignatory"

Public Class tCompanyMenuSignatory
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[Label]
[Designation]
[ID_CompanyMenu]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyMenuSignatory")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyPolicy"

Public Class tCompanyPolicy
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Description]
[ID_CompanyPolicyCategory]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyPolicy")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyPolicyCategory"

Public Class tCompanyPolicyCategory
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyPolicyCategory")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyRestDay"

Public Class tCompanyRestDay
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_WeekDay]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyRestDay")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCompanyType"

Public Class tCompanyType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCompanyType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tComputer"

Public Class tComputer
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[IPAddress]
[ID_ComputerStatus]
[ID_ComputerSession]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tComputer")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tComputerSession"

Public Class tComputerSession
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ComputerUser]
[Minutes]
[Amount]
[ID_Computer]
[StartDateTime]
[EndDateTime]
[Comment]
[AmountPerHour]
[Name]
[Password]
[IsActive]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tComputerSession")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tComputerStatus"

Public Class tComputerStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tComputerStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tComputerUser"

Public Class tComputerUser
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tComputerUser")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCostCenter"

Public Class tCostCenter
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[Code]
[Name]
[IsActive]
[Comment]
[ID_CostCenterGroup]
[BusinessArea]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCostCenter")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCostCenterGroup"

Public Class tCostCenterGroup
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCostCenterGroup")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCostCenterGroupAccount"

Public Class tCostCenterGroupAccount
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_CostCenterGroup]
[ID_Account]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCostCenterGroupAccount")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCountry"

Public Class tCountry
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[Code]
[ID_Persona_President]
[ID_Transaction_Created]
[ID_Transaction_Modified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCountry")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCurrency"

Public Class tCurrency
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ImageFile]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCurrency")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCurrencyRate"

Public Class tCurrencyRate
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Currency]
[Rate]
[Date]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCurrencyRate")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tCustomer"

Public Class tCustomer
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tCustomer")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDailyAutomation"

Public Class tDailyAutomation
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Date]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDailyAutomation")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDailySchedule"

Public Class tDailySchedule
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[StartMinute]
[WorkingHours]
[WorkingMinutes]
[TimeIn]
[FirstHalfTimeOut]
[FirstHalfWorkingHours]
[FirstHalfWorkingMinutes]
[SecondHalfTimeIn]
[SecondHalfWorkingHours]
[SecondHalfWorkingMinutes]
[TimeOut]
[MinuteIn]
[FirstHalfMinuteOut]
[SecondHalfMinuteIn]
[MinuteOut]
[Flexible]
[FlexibleHours]
[SeqNo]
[IsActive]
[Comment]
[TardyAsHalfDay]
[TardyAsAbsent]
[UTAsHalfDay]
[UTAsAbsent]
[NDAMStartMinute]
[NDAMEndMinute]
[NDPMStartMinute]
[NDPMEndMinute]
[Days]
[IsExpirableFlexiHours]
[ID_ShiftType]
[WithBrokenTime]
[FirstInLastOut]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDailySchedule")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDailySchedule_Detail"

Public Class tDailySchedule_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_DailySchedule]
[ID_HourType]
[Day]
[StartTime]
[StartMinute]
[EndMinute]
[SeqNo]
[IsActive]
[Comment]
[Hours]
[EndTime]
[BreakMinutes]
[FirstIn]
[LastOut]
[FlexibleMinutes]
[FlexibleHours]
[WithPay]
[AutoApprove]
[LBoundStartMinute]
[UBoundEndMinute]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDailySchedule_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDailyScheduleFlexibleBreak"

Public Class tDailyScheduleFlexibleBreak
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_DailySchedule]
[StartTime]
[EndTime]
[StartMinute]
[EndMinute]
[BreakMinutes]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[Hours]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDailyScheduleFlexibleBreak")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDate"

Public Class tDate
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[Date]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDate")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDayType"

Public Class tDayType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDayType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDeliveryMethod"

Public Class tDeliveryMethod
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
[ImagePath]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDeliveryMethod")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDepartment"

Public Class tDepartment
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_Division]
[ID_HeadDesignation]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Branch]
[Series]
[ID_Employee]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDepartment")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDependent"

Public Class tDependent
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[LastName]
[FirstName]
[MiddleName]
[BirthDate]
[Relationship]
[ID_Persona]
[IsOtherDependent]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[Name]
[Age]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDependent")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDesignation"

Public Class tDesignation
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[ID_JobClass]
[GraceMinutes]
[ImageFile]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[Budgeted]
[Current]
[TurnOverDays]
[MinOtHours]
[JobSummary]
[MonthlyRate]
[DailyRate]
[AutoApproveHoliday]
[ID_UserGroup]
[Actual]
[Vacancy]
[MaxPRFApprover]
[MaxInterviewApprover]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDesignation")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDesignationApplicationRequirement"

Public Class tDesignationApplicationRequirement
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ApplicationRequirement]
[Required]
[ID_Designation]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDesignationApplicationRequirement")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDesignationEmploymentRequirement"

Public Class tDesignationEmploymentRequirement
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Designation]
[ID_EmploymentRequirement]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDesignationEmploymentRequirement")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDesignationInterviewQuestion"

Public Class tDesignationInterviewQuestion
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_Question]
[SeqNo]
[IsActive]
[Required]
[ID_Designation]
[ID_QuestionType]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDesignationInterviewQuestion")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDesignationMedianSalary"

Public Class tDesignationMedianSalary
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Level]
[UpperBound]
[LowerBound]
[Code]
[SeqNo]
[ID_Designation]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDesignationMedianSalary")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDesignationPerformanceCriteria"

Public Class tDesignationPerformanceCriteria
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[PerformanceAppraisalType]
[PerformanceCriteria]
[ID_Designation]
[LBound]
[UBound]
[SeqNo]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDesignationPerformanceCriteria")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDesignationSkill"

Public Class tDesignationSkill
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[SeqNo]
[IsActive]
[ID_Designation]
[ID_Skill]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDesignationSkill")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDesignationTask"

Public Class tDesignationTask
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Designation]
[Description]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDesignationTask")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDiary"

Public Class tDiary
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Subject]
[DateTime]
[Details]
[Comment]
[GUID_Parent]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDiary")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDisciplinaryAction"

Public Class tDisciplinaryAction
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[Description]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDisciplinaryAction")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDivision"

Public Class tDivision
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[ID_Branch]
[SeqNo]
[IsActive]
[Comment]
[ID_Employee]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDivision")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDocumentationSystem"

Public Class tDocumentationSystem
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[GUID]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDocumentationSystem")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDocumentControl"

Public Class tDocumentControl
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_DocumentationSystem]
[Separator]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDocumentControl")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDocumentControl_Detail"

Public Class tDocumentControl_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_DocumentControl]
[ID_DocumentReference]
[ID_DocumentationSystem]
[Value]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDocumentControl_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDocumentProperties"

Public Class tDocumentProperties
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Table]
[ID_Menu]
[ID_Original]
[ID_Company]
[ID_Branch]
[DateCreated]
[DateModified]
[DateAccessed]
[ID_CreatedBy]
[ID_ModifiedBy]
[ID_AccessedBy]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDocumentProperties")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tDocumentSeries"

Public Class tDocumentSeries
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_Menu]
[Prefix]
[CurrentTransNo]
[AddDate]
[SeqNo]
[IsActive]
[Comment]
[DigitCount]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tDocumentSeries")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEducationAttainment"

Public Class tEducationAttainment
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[YearFrom]
[YearTo]
[Comment]
[School]
[ID_Persona]
[ID_EducationLevel]
[ID_EducationAttainmentStatus]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[ID_School]
[ID_EducationDegree]
[ID_Courses]
[ID_Month]
[Year]
[Course]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEducationAttainment")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEducationAttainmentStatus"

Public Class tEducationAttainmentStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEducationAttainmentStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEducationLevel"

Public Class tEducationLevel
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEducationLevel")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmailBlast"

Public Class tEmailBlast
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmailBlast")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmailContent"

Public Class tEmailContent
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[MailFrom]
[MailTo]
[Subject]
[Password]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmailContent")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployee"

Public Class tEmployee
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[BankAcctNo]
[AccessNo]
[AccessCode]
[Dependents]
[IsRequiredToLog]
[StartDate]
[EndDate]
[RegularizationDate]
[SCStartDate]
[ID_Persona]
[ID_Company]
[ID_Branch]
[ID_Department]
[ID_Section]
[ID_Designation]
[ID_CostCenter]
[ID_Level]
[ID_EmployeeStatus]
[ID_PayrollScheme]
[ID_PayrollFrequency]
[ID_CompanyBankAcct]
[ID_TaxExemption]
[ID_PaymentMode]
[ID_Parameter]
[ID_ShiftSchedule]
[ID_Unit]
[ID_LeaveParameter]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[MonthlyRate]
[DailyRate]
[HourlyRate]
[DMonthlyRate]
[DDailyRate]
[DHourlyRate]
[TaxRate]
[IsTerminated]
[IsHired]
[YearsOfService]
[MonthsOfService]
[ID_WeeklySchedule]
[LockerNo]
[KeyNo]
[ShirtSize]
[BadgeNo]
[PrevEmpTaxableAmt]
[PrevEmpWitholdingTax]
[PrevEmpEndDate]
[Password]
[OldCode]
[ID_Currency]
[ID_Agency]
[ID_BenefitSuite]
[BankAcctNo2]
[ID_CompanyBankAcct2]
[ID_SalaryGrade]
[ID_Step]
[BonusRate]
[Prev13thMonth]
[PrevCompensation]
[NonTaxPrevContribution]
[NonTaxPrev13thMonth]
[NonTaxPrevCompensation]
[DailySMW]
[MonthlySMW]
[MWE]
[SubstitutedFiling]
[WifeClaimsAdditionalExemption]
[CompanyEmail]
[EmailPassword]
[LogPassword]
[CardNo]
[HasFingerPrint]
[VendorAccount]
[StartTaxableCutOffDate]
[ID_ShiftType]
[ID_UnionMembershipType]
[ID_Union]
[ID_EmployeeUnion]
[UnionDues]
[isEligeble]
[Reason]
[ID_DevicePrivilege]
[SalaryIncreaseLookup]
            [ID_PieceWorkGroup]
            [ID_PayrollClassifi]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployee")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeAccountabilityChecklist"

Public Class tEmployeeAccountabilityChecklist
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Employee]
[ControlNo]
[Amount]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeAccountabilityChecklist")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeAttendanceLog"

Public Class tEmployeeAttendanceLog
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Source]
[AccessNo]
[DateTime]
[ID_Employee]
[ID_AttendanceLogType]
[ID_EmployeeAttendanceLogFile]
[WorkDate]
[Date]
[Minute]
[SeqNo]
[IsActive]
[Comment]
[ID_DailySchedule]
[ID_EditedByUser]
[DateTimeCreated]
[DateTimeModified]
[ID_EmployeeAttendanceLogCreditDate]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeAttendanceLog")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeAttendanceLog_HO"

Public Class tEmployeeAttendanceLog_HO
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_EmployeeAttendanceLogFile]
[DateTime]
[CardNo]
[ID_Employee]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeAttendanceLog_HO")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeAttendanceLogCreditDate"

Public Class tEmployeeAttendanceLogCreditDate
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeAttendanceLogCreditDate")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeAttendanceLogFile"

Public Class tEmployeeAttendanceLogFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Path]
[ID_Company]
[ID_Branch]
[StartDateTime]
[EndDateTime]
[SeqNo]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeAttendanceLogFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeAttendanceLogFile_Unapplied"

Public Class tEmployeeAttendanceLogFile_Unapplied
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Source]
[AccessNo]
[DateTime]
[ID_Employee]
[ID_AttendanceLogType]
[ID_EmployeeAttendanceLogFile]
[WorkDate]
[Date]
[Minute]
[SeqNo]
[IsActive]
[Comment]
[ID_DailySchedule]
[ID_EditedByUser]
[DateTimeCreated]
[DateTimeModified]
[ID_EmployeeAttendanceLogCreditDate]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeAttendanceLogFile_Unapplied")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeChangeOfSchedule"

Public Class tEmployeeChangeOfSchedule
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[DateFiled]
[StartDate]
[EndDate]
[ID_FilingStatus]
[ApproverStatus]
[FileDate]
[Comment]
[ApprovalDate]
[IsPosted]
[DateCreated]
[Reason]
[ApproverComment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeChangeOfSchedule")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeChangeOfSchedule_DayOff"

Public Class tEmployeeChangeOfSchedule_DayOff
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_EmployeeChangeOfSchedule]
[ID_EmployeeDailySchedule]
[PreviousRD]
[NewRD]
[isRD]
[COSComment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeChangeOfSchedule_DayOff")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeChangeOfSchedule_Detail"

Public Class tEmployeeChangeOfSchedule_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_EmployeeChangeOfSchedule]
[SchedDate]
[Comment]
[OldSched]
[ID_NewSched]
[ID_ForRDSD]
[IsRD]
[IsSD]
[ReqRD]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeChangeOfSchedule_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeCodePrefix"

Public Class tEmployeeCodePrefix
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_EmployeeCodePrefixType]
[Code]
[Name]
[Value]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeCodePrefix")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeCodePrefixType"

Public Class tEmployeeCodePrefixType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeCodePrefixType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeCostCenterAssignment"

Public Class tEmployeeCostCenterAssignment
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Employee]
[StartDate]
[EndDate]
[ID_CostCenter]
[FileDate]
[ApproverStatus]
[ID_FilingStatus]
[ApprovalDate]
[ID_ApproverEmployee]
[ID_ShiftType]
[ID_User]
[Reason]
[IsPosted]
[ID_Department]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeCostCenterAssignment")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeCostCenterAssignmentFile"

Public Class tEmployeeCostCenterAssignmentFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
[ID_FilingStatus]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeCostCenterAssignmentFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeCostCenterAssignmentFile_Detail"

Public Class tEmployeeCostCenterAssignmentFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_EmployeeCostCenterAssignmentFile]
[EmployeeCode]
[CostCenterCode]
[Date]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeCostCenterAssignmentFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region


#Region "tEmployeeMissedLog"

    Public Class tEmployeeMissedLog
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [SeqNo]
            [IsActive]
            [Comment]
            [ID_Employee]
            [WorkDate]
            [ID_FilingStatus]
            [ApproverComment]
            [FileDate]
            [DateCreated]
            [IsPosted]
            [ApproverStatus]
            [ID_EmployeeMissedLogFile_Detail]
            [Reason]
            [Department]
            [Division]
            [ApprovalDate]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tEmployeeMissedLog")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region

#Region "tEmployeeMissedLog_Detail"

    Public Class tEmployeeMissedLog_Detail
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [ImageFile]
            [SeqNo]
            [IsActive]
            [Comment]
            [ID_EmployeeMissedLog]
            [LogDate]
            [LogTime]
            [ID_AttendanceLogType]
            [ComputedTime]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tEmployeeMissedLog_Detail")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region


#Region "tEmployeePhotoFile"

    Public Class tEmployeePhotoFile
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [Code]
            [Name]
            [IsActive]
            [Comment]
            [isApplied]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tEmployeePhotoFile")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region

#Region "tEmployeePhotoFile_Detail"

    Public Class tEmployeePhotoFile_Detail
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [ID_EmployeePhotoFile]
            [EmployeeCode]
            [ImageFile]
            [Comment]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tEmployeePhotoFile_Detail")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region

#Region "tEmployeeDailySchedule"

Public Class tEmployeeDailySchedule
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Date]
[ID_DailySchedule]
[ID_Employee]
[ID_ImportedSchedule]
[ID_Attribute]
[REG]
[EXT]
[OT]
[ND]
[NDOT]
[TARDY]
[UT]
[IsRD]
[TimeIn]
[TimeOut]
[IsForComputation]
[ID_LeavePayrollItem]
[ID_FirstHalfLeavePayrollItem]
[ID_SecondHalfLeavePayrollItem]
[Comment]
[IsAbsent]
[ActualTardy]
[IsActualAbsent]
[Absences]
[LeaveWithPay]
[FirstHalfLeaveWithPay]
[SecondHalfLeaveWithPay]
[OffsetREG]
[OffsetOT]
[OffsetND]
[OffsetNDOT]
[ComputedREG]
[ComputedOT]
[ComputedND]
[ComputedNDOT]
[RatedREG]
[RatedOT]
[RatedND]
[RatedNDOT]
[OffsetRate]
[ActualREG]
[ActualOT]
[ActualND]
[ActualNDOT]
[ForPerfectAttendance]
[StraightDuty]
[IsHDAbsent]
[MealAllowance]
[IsNoAttendance]
[ID_CostCenter]
[IsTentativeAbsent]
[HasStopEmail]
[HasSchedule]
[ActualUT]
[ID_Branch]
[Posted]
[ID_DayType]
[TardyAsLeavePayrollItem]
[UTAsLeavePayrollItem]
[TardyAsLeave]
[UTAsLeave]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeDailySchedule")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeDailySchedule_Detail"

Public Class tEmployeeDailySchedule_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_EmployeeDailySchedule]
[ID_Hourtype]
[StartTime]
[EndTime]
[Minutes]
[ComputedHours]
[ConsideredHours]
[Approved]
[ApprovedMinutes]
[Tardy]
[ActualTardy]
[Comment]
[ID_VerifierEmployee]
[ID_ApproverEmployee]
[VerificationDate]
[ApprovalDate]
[ForApproval]
[IsBasic]
[NDAMMinuteIn]
[NDAMMinuteOut]
[NDAMMinutes]
[NDPMMinuteIn]
[NDPMMinuteOut]
[NDPMMinutes]
[NDMinutes]
[NDHours]
[ID_WorkCredit]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeDailySchedule_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeDailyScheduleFile"

Public Class tEmployeeDailyScheduleFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeDailyScheduleFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeDailyScheduleFile_Detail"

Public Class tEmployeeDailyScheduleFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_EmployeeDailyScheduleFile]
[EmployeeCode]
[Date]
[REG]
[EXT]
[OT]
[ND]
[NDOT]
[TARDY]
[UT]
[IsRD]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeDailyScheduleFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeDailyScheduleView"

Public Class tEmployeeDailyScheduleView
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[StartDate]
[EndDate]
[ID_Company]
[ID_Branch]
[ID_PayrollFrequency]
[ID_Department]
[ID_Designation]
[ID_EmployeeStatus]
            [ID_Gender]
            [ID_PayrollClassifi]
[ID_Employee]
[ID_Month]
[Year]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[IsFinalized]
[SalesProjection]
[ManPowerBudgetPercentage]
[ManPowerBudgetAmt]
[ManPowerComputedAmt]
[ManPowerDifferenceAmt]
[ManPowerDifferencePercentage]
[ID_CostCenter]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeDailyScheduleView")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeDocument"

Public Class tEmployeeDocument
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeDocument")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeFingerPrint"

Public Class tEmployeeFingerPrint
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[F1]
[F2]
[F3]
[F4]
[F5]
[F6]
[F7]
[F8]
[F9]
[F10]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeFingerPrint")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeLeaveCreditFile"

Public Class tEmployeeLeaveCreditFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[Date]
[ID_Company]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeLeaveCreditFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeLeaveCreditFile_Detail"

Public Class tEmployeeLeaveCreditFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[EmployeeCode]
[Employee]
[SeqNo]
[IsActive]
[Comment]
[ID_EmployeeLeaveCreditFile]
[VL]
[SL]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeLeaveCreditFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeLogDevice"

Public Class tEmployeeLogDevice
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_LogDevice]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeLogDevice")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeMovement"

Public Class tEmployeeMovement
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_Employee]
[ID_Company]
[ID_Branch]
[ID_CostCenter]
[ID_EmployeeStatus]
[ID_Department]
[ID_Designation]
[PreviousMonthlyRate]
[MonthlyRate]
[ApprovedBy]
[ID_FilingStatus]
[EffectivityDate]
[Comment]
[StartDate]
[EndDate]
[RegularizationDate]
[ID_EmployeeMovementType]
[ID_PrevCompany]
[ID_PrevBranch]
[ID_PrevCostCenter]
[ID_PrevEmployeeStatus]
[ID_PrevDepartment]
[ID_PrevDesignation]
[ID_Separation]
[ID_TaxExemption]
[ID_PrevTaxExemption]
[ID_PayrollScheme]
[ID_PrevPayrollScheme]
[ID_PayrollFrequency]
[ID_PrevPayrollFrequency]
[OtherReason]
[EffectivityEndDate]
[ID_Parameter]
[ID_PrevParameter]
[IsApplied]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeMovement")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeePreviousEmployer"

Public Class tEmployeePreviousEmployer
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[CompanyName]
[CompanyAddress]
[EndDate]
[MonthlyRate]
[TMonthAndOtherBenefits_N]
[DeMinimisBenefits_N]
[ContributionsAndUnionDues_N]
[SalariesAndOtherFormsOfComp_N]
[BasicSalary]
[TMonthAndOtherBenefits]
[SalariesAndOtherFormsOfComp]
[TaxWithHeld]
[TaxWithHeldNov]
[NetTaxableCompIncome]
[GrossCompIncome_N]
[BasicSMW]
[HolidayPay]
[OvertimePay]
[NDPay]
[HazardPay]
[IsActive]
[Comment]
[ID_JobMatching]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeePreviousEmployer")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeRestDay"

Public Class tEmployeeRestDay
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_WeekDay]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeRestDay")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeRestDayFile"

Public Class tEmployeeRestDayFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[StartDate]
[SeqNo]
[IsActive]
[Comment]
[ID_FilingStatus]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeRestDayFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeRestDayFile_Detail"

Public Class tEmployeeRestDayFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_RestDayFile]
[SeqNo]
[EmployeeCode]
[Employee]
[Day1]
[Day2]
[Day3]
[Day4]
[Day5]
[Day6]
[Day7]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeRestDayFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeStatus"

Public Class tEmployeeStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsProcessPayroll]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[SeqNo]
[IsTerminated]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeTemplateFile"

Public Class tEmployeeTemplateFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeTemplateFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEmployeeTemplateFile_Detail"

Public Class tEmployeeTemplateFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_EmployeeTemplateFile]
[EmployeeCode]
[AccessNo]
[LastName]
[FirstName]
[MiddleName]
[NickName]
[Nationality]
[HDMFNo]
[PreviousDesignation]
[Course]
[Concept]
[Company]
[Branch]
[CostCenter]
[Designation]
[Department]
[JobClass]
[DateValidated]
[HomeAddress]
[HomePhoneNo]
[ProvincialAddress]
[ProvincialPhoneNo]
[BirthDate]
[Gender]
[Citizenship]
[ContactPerson]
[ContactAddress]
[ContactNumber]
[SSSNo]
[TinNo]
[PhilHealthNo]
[CompanyBank]
[CompanyBankNo]
[AcctNo]
[CompanyBank2]
[CompanyBankNo2]
[AcctNo2]
[PreviousEmployer]
[PreviousEmployerAddress]
[PreviousEmployerNo]
[Elementary]
[ElementaryAddress]
[YearGraduated1]
[HighSchool]
[HighSchoolAddress]
[YearGraduated2]
[College]
[CollegeAddress]
[YearGraduated3]
[Vocational]
[VocationalAddress]
[YearGraduated4]
[Others]
[OthersAddress]
[YearGraduted5]
[Scheme]
[PayrollFrequency]
[ExemptionStatus]
[Status]
[MonthlySalary]
[DailySalary]
[HourlySalary]
[Name1]
[Relationship1]
[BirthDate1]
[Name2]
[Relationship2]
[BirthDate2]
[Name3]
[Relationship3]
[BirthDate3]
[Name4]
[Relationship4]
[BirthDate4]
[DateRegularized]
[Allowance]
[SP]
[EducationalAttainment]
[Remarks]
[CellphoneAllowance]
[IsRequiredToLog]
[DaysperYear]
[HoursperDay]
[Father]
[Mother]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmployeeTemplateFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region


#Region "tEmployeeTemplateFileDetail_PersonalInfo"

    Public Class tEmployeeTemplateFileDetail_PersonalInfo
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field

            [ID]
            [ID_EmployeeTemplateFile]
            [EmployeeCode]
            [AccessNo]
            [LastName]
            [FirstName]
            [MiddleName]
            [Suffix]
            [NickName]
            [Nationality]
            [Citizenship]
            [BirthDate]
            [BirthPlace]
            [Gender]
            [Height]
            [Weight]
            [CivilStatus]
            [SSSStatus]
            [BloodType]
            [HomeAddress]
            [HomePhoneNo]
            [MobileNo]
            [HomeAddressRegion]
            [HomeAddressArea]
            [HomeAddressCity]
            [ProvincialAddress]
            [ProvincialPhoneNo]
            [ProvincialAddressRegion]
            [ProvincialAddressArea]
            [ProvincialAddressCity]
            [EmailAddress1]
            [EmailAddress2]
            [HDMFNo]
            [SSSNo]
            [GSISNo]
            [TinNo]
            [PhilHealthNo]
            [DriversLicenseNo]
            [PassportNo]
            [ContactPerson]
            [ContactAddress]
            [ContactNumber]
            [ContactPersonRelationship]
            [Spouse]
            [SpouseBirthdate]
            [SpouseEmployer]
            [SpouseOccupation]
            [Father]
            [FatherBirthday]
            [FatherOccupation]
            [Mother]
            [MotherBirthday]
            [MotherOccupation]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tEmployeeTemplateFileDetail_PersonalInfo")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region

#Region "tEmployeeTemplateFileDetail_EducationalBackground"

    Public Class tEmployeeTemplateFileDetail_EducationalBackground
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [ID_EmployeeTemplateFile]
            [EmployeeCode]
            [School]
            [SchoolAddress]
            [SchoolLevel]
            [Couse]
            [YearGraduated]
            [EducationalAttainment]
            [Month]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tEmployeeTemplateFileDetail_EducationalBackground")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region

#Region "tEmployeeTemplateFileDetail_Dependent"

    Public Class tEmployeeTemplateFileDetail_Dependent
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [ID_EmployeeTemplateFile]
            [EmployeeCode]
            [DependentName]
            [DependentRelationship]
            [DependentBirthDate]
            [DependentGender]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tEmployeeTemplateFileDetail_Dependent")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region

#Region "tEmployeeTemplateFileDetail_CompanyInfo"

    Public Class tEmployeeTemplateFileDetail_CompanyInfo
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [ID_EmployeeTemplateFile]
            [EmployeeCode]
            [Company]
            [Address]
            [ZipCode]
            [TIN]
            [SSSNo]
            [PhilHealthNo]
            [HDMFNo]
            [Branch]
            [Division]
            [Department]
            [Designation]
            [EmployeeStatus]
            [JobClass]
            [CostCenter]
            [IsRequiredToLog]
            [DateHired]
            [DateRegularized]
            [PayrollScheme]
            [PayrollFrequency]
            [PayrollStatus]
            [MonthlySalary]
            [DailySalary]
            [HourlySalary]
            [DaysperYear]
            [HoursperDay]
            [CompanyBank]
            [CompanyBankNo]
            [AcctNo]
            [CompanyBank2]
            [CompanyBankNo2]
            [AcctNo2]
            [DaysperMonth]
            [email]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tEmployeeTemplateFileDetail_CompanyInfo")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region

#Region "tEmployeeTemplateFileDetail_EmploymentHistory"

    Public Class tEmployeeTemplateFileDetail_EmploymentHistory
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [ID_EmployeeTemplateFile]
            [EmployeeCode]
            [PreviousEmployer]
            [PreviousEmployerSpecialization]
            [PreviousEmployerCompanyIndustry]
            [PreviousDepartment]
            [PreviousDesignation]
            [PreviousJobClass]
            [PreviousJobRole]
            [PreviousEmployerPositionLevel]
            [StartDate]
            [EndDate]
            [PreviousMonthlyRate]
            [PreviousEmployerNo]
            [PreviousEmployerAddress]
            [PreviousEmployerZipCode]
            [PreviousEmployerImmediateSupervisor]
            [PreviousEmployerContactNo]
            [PreviousEmployerTIN]
            [PreviousEmployerBenefits]
            [ReasonForLeaving]
            [YearsOfExperience]

        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tEmployeeTemplateFileDetail_EmploymentHistory")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region


#Region "tEmploymentHistory"

Public Class tEmploymentHistory
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Designation]
[ID_JobClass]
[StartDate]
[EndDate]
[MonthlyRate]
[ImmediateSupervisor]
[ContactNo]
[EmployerName]
[Department]
[Benefits]
[Company]
[CompanyTIN]
[CompanyAddress]
[CompanyZipCode]
[SeqNo]
[IsActive]
[Comment]
[ID_Persona]
[ReasonForLeaving]
[ID_Specialization]
[ID_CurrentPositionLevel]
[JobRole]
[ID_CompanyIndustry]
[Experience]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEmploymentHistory")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEntitlement"

Public Class tEntitlement
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_EntitlementType]
[ID_FilingStatus]
[Date]
[Amount]
[ORNo]
[ORDate]
[Comment]
[Balance]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEntitlement")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEntitlementFile"

Public Class tEntitlementFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[Path]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEntitlementFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEntitlementFile_Detail"

Public Class tEntitlementFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_EntitlementFile]
[EmployeeCode]
[Employee]
[EntitlementType]
[Date]
[ORDate]
[ORNo]
[Amount]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEntitlementFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tEntitlementType"

Public Class tEntitlementType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tEntitlementType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFile"

Public Class tFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[FileName]
[GUID_Parent]
[OriginalPath]
[Comment]
[FileExtension]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFileType"

Public Class tFileType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFileType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFilingStatus"

Public Class tFilingStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[ReadOnly]
[Comment]
[SeqNo]
[HasComment]
[Editable]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFilingStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFinalPay"

Public Class tFinalPay
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[Year]
[StartDate]
[EndDate]
[TMonthStartDate]
[TMonthEndDate]
[UseMonthlyRate]
[Months]
[LCStartDate]
[LCEndDate]
[DailyRate]
[MonthlyRate]
[HourlyRate]
[ID_TaxExemption]
[ID_CostCenter]
[ID_Company]
[GrossAmt]
[DeductionAmt]
[NetAmt]
[PayDate]
[IsActive]
[Comment]
[ID_Month]
[ContributionYear]
[MWE]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFinalPay")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFinalPay_Annualization"

Public Class tFinalPay_Annualization
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_FinalPay]
[NonTaxTMonthAndOtherBenefits]
[NonTaxDeMinimis]
[Contri]
[NonTaxSalAndOtherFormsOfCompensation]
[TotalNonTax]
[TaxBasicSalary]
[TaxTMonthAndOtherBenefits]
[TaxSalAndOtherFormsOfCompensation]
[TotalTax]
[ExemptionAmount]
[HealthPremium]
[NetTaxableCompIncome]
[TaxOn]
[TaxInExcess]
[TaxDue]
[TaxWithHeldNov]
[TaxWithHeldDec]
[TaxWithHeld]
[TaxRefund]
[AdjustedTaxWithHeld]
[Representation]
[Transportation]
[COLA]
[HousingAllownace]
[OtherRegular]
[Commission]
[ProfitSharing]
[DirectorFee]
[HazardPay]
[OvertimePay]
[OtherSupplementary]
[SSS]
[PHIC]
[HDMF]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFinalPay_Annualization")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFinalPay_Detail"

Public Class tFinalPay_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_FinalPay]
[ID_PayrollItem]
[Days]
[Hours]
[Minutes]
[TotalHours]
[Amount]
[Adj]
[Total]
[Comment]
[Taxable]
[NonTaxable]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFinalPay_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFinalPay_LeaveConversion"

Public Class tFinalPay_LeaveConversion
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_LeavePayrollItem]
[DaysComputed]
[DaysToConvert]
[DailyRate]
[Amount]
[ID_FinalPay]
[SeqNo]
[IsActive]
[Comment]
[Taxable]
[NonTaxable]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFinalPay_LeaveConversion")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFinalPay_PayrollItemRate"

Public Class tFinalPay_PayrollItemRate
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Parameter]
[ID_PayrollItem]
[Rate]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFinalPay_PayrollItemRate")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFinalPayLoan"

Public Class tFinalPayLoan
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_FinalPay]
[ID_Loan]
[LoanAmount]
[PaidAmount]
[Amount]
[IsActive]
[Comment]
[IsApplied]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFinalPayLoan")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tFormOfBusiness"

Public Class tFormOfBusiness
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tFormOfBusiness")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tGender"

Public Class tGender
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
[SeqNo]
[ImageFile]
[ImagePath]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tGender")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tHalfDay"

Public Class tHalfDay
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tHalfDay")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tHDMF"

Public Class tHDMF
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[LBound]
[UBound]
[EERate]
[ERRate]
[EE]
[ER]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[Total]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tHDMF")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tHDMFExport"

Public Class tHDMFExport
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_Branch]
[ID_Month]
[Year]
[SeqNo]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[RefNo]
[DatePaid]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tHDMFExport")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tHDMFExport_Detail"

Public Class tHDMFExport_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_HDMFExport]
[ID_Employee]
[EmployeeAmt]
[EmployerAmt]
[SeqNo]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tHDMFExport_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tHDMFLoanExport"

Public Class tHDMFLoanExport
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[SeqNo]
[IsActive]
[Comment]
[ID_PayrollItem]
[Amount]
[ID_HDMFExport]
[ID_Employee]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tHDMFLoanExport")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tHelpForms"

Public Class tHelpForms
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[FileName]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tHelpForms")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tHoliday"

Public Class tHoliday
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Date]
[ID_HolidayType]
[ID_Area]
[Comment]
[Year]
[ID_Month]
[ID_Date]
[IsWorking]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tHoliday")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tHourType"

Public Class tHourType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[IsForApproval]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tHourType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tImportCostSheet"

Public Class tImportCostSheet
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tImportCostSheet")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tImportedAttendance_Detail"

Public Class tImportedAttendance_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ImportedAttendance]
[EmployeeCode]
[EmployeeName]
[Date]
[TimeIn]
[TimeOut]
[Tardy]
[OT]
[TotalHours]
[ND]
[Remarks]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tImportedAttendance_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tImportedLoan"

Public Class tImportedLoan
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[Code]
[Name]
[SeqNo]
[IsActive]
[IsPosted]
[IsVoided]
[Comment]
[DateTimeCreated]
[DateTimeModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tImportedLoan")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tImportedLoan_Detail"

Public Class tImportedLoan_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ImportedLoan]
[EmployeeCode]
[DateGranted]
[CollectionStartDate]
[LoanPayrollItemCode]
[PrincipalAmt]
[Interest]
[Penalty]
[AmtPaid]
[Amortization]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tImportedLoan_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tImportedSchedule"

Public Class tImportedSchedule
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[Code]
[Name]
[StartDate]
[SeqNo]
[IsActive]
[IsPosted]
[IsVoided]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tImportedSchedule")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tImportedSchedule_Detail"

Public Class tImportedSchedule_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ImportedSchedule]
[EMPREM]
[EMPNAME]
[MONTIME]
[MONTOT]
[TUETIME]
[TUETOT]
[WEDTIME]
[WEDTOT]
[THUTIME]
[THUTOT]
[FRITIME]
[FRITOT]
[SATTIME]
[SATTOT]
[SUNTIME]
[SUNTOT]
[TOTAL]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tImportedSchedule_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tImportedSchedule_Employee"

Public Class tImportedSchedule_Employee
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ImportedSchedule]
[ACCESSNO]
[CODE]
[LASTNAME]
[MIDDLENAME]
[FIRSTNAME]
[NICKNAME]
[DEPARTMENT]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tImportedSchedule_Employee")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tIncident"

Public Class tIncident
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_offense]
[ID_IncidentStatus]
[Date]
[Time]
[ResolutionDate]
[Location]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tIncident")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tIncidentStatus"

Public Class tIncidentStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tIncidentStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tIncome"

Public Class tIncome
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tIncome")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tIncomeAndDeduction"

Public Class tIncomeAndDeduction
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_PayrollItem]
[Date]
[Code]
[Name]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[Year]
[ID_Month]
[ID_PayrollSchedule]
[ID_EmployeeDailyScheduleView]
[TotalAmt]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tIncomeAndDeduction")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tIncomeAndDeduction_Detail"

Public Class tIncomeAndDeduction_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_IncomeAndDeduction]
[ID_Employee]
[ID_PayrollItem]
[Date]
[Amount]
[TaxAmount]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[IsVoided]
[Hours]
[UnDeductedAmt]
[PayrollItemCode]
[EmployeeCode]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tIncomeAndDeduction_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tITR"

Public Class tITR
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Year]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tITR")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tITR_Detail"

Public Class tITR_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_Employee]
[ID_ITR]
[Amount]
[Approved]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tITR_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tJobApplication"

Public Class tJobApplication
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[LastName]
[FirstName]
[MiddleName]
[NickName]
[BirthDate]
[BirthPlace]
[Father]
[Mother]
[Spouse]
[SpouseBirthDate]
[SpouseEmployer]
[SpouseOccupation]
[SSSNo]
[PhilHealthNo]
[HDMFNo]
[TIN]
[Height]
[Weight]
[EmailAddress]
[AlternateEmailAddress]
[ContactNo]
[MobileNo]
[Hobbies]
[IsBlackListed]
[IsApplicant]
[IsActive]
[ID_Company]
[ID_Gender]
[ID_Religion]
[ID_Nationality]
[ID_Citizenship]
[ID_CivilStatus]
[ID_BloodType]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[Name]
[Birthday]
[Age]
[Name1]
[Name2]
[Comment]
[MiddleInitial]
[HasCompleteEmploymentRequirements]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[KnownEmployee]
[KnownEmployee_Location]
[KnownEmployee_Designation]
[KnownEmployee_Relationship]
[CriminalRecord]
[Illness]
[GSISNo]
[Eligibility]
[ID_SSSStatus]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tJobApplication")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tJobClass"

Public Class tJobClass
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tJobClass")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tJollibeeTextFile"

Public Class tJollibeeTextFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[ID_Branch]
[StartDate]
[EndDate]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tJollibeeTextFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tJollibeeTextFile_Detail"

Public Class tJollibeeTextFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_JollibeeTextFile]
[ID_Employee]
[Date]
[TimeIn]
[TimeOut]
[A]
[B]
[D]
[J]
[N]
[T]
[P]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tJollibeeTextFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tJournalVoucher"

Public Class tJournalVoucher
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
[ID_PayrollPeriod]
[DocumentNo]
[Date]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tJournalVoucher")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tJournalVoucher_Detail"

Public Class tJournalVoucher_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_JournalVoucher]
[ID_Account]
[Amount]
[ID_NormalBalance]
[ID_CostCenter]
[SeqNo]
[IsActive]
[Comment]
[ID_Employee]
[ID_Payrollitem]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tJournalVoucher_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tJournalVoucherType"

Public Class tJournalVoucherType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tJournalVoucherType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLanguage"

Public Class tLanguage
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLanguage")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeave"

Public Class tLeave
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_LeavePayrollItem]
[ID_FilingStatus]
[Emergency]
[FileDate]
[StartDate]
[EndDate]
[Days]
[Comment]
[IsActive]
[ApprovalDate]
[ID_Region]
[BereavementDays]
[ApproverStatus]
[IsPosted]
[ID_LeaveFile_Detail]
[DateCreated]
[DaysWithPay]
[Reason]
[ApproverComment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeave")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeave_Detail"

Public Class tLeave_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Leave]
[ID_HalfDay]
[Date]
[Days]
[Comment]
[WithPay]
[ID_LeaveConversion]
[ForTardy]
[ForUT]
[HOURS]
[Minutes]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeave_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveAccrualType"

Public Class tLeaveAccrualType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveAccrualType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveApproval"

Public Class tLeaveApproval
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[StartDate]
[EndDate]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveApproval")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveConversion"

Public Class tLeaveConversion
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_LeavePayrollItem]
[StartDate]
[EndDate]
[Comment]
[ID_LeaveConversionType]
[Year]
[ID_Month]
[ID_PayrollSchedule]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveConversion")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveConversion_Detail"

Public Class tLeaveConversion_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_LeaveConversion]
[ID_Employee]
[Days]
[Amount]
[Comment]
[Alloted]
[Used]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveConversion_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveConversionType"

Public Class tLeaveConversionType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveConversionType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveCredit"

Public Class tLeaveCredit
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_JobClass]
[ID_EmployeeStatus]
[ID_LeavePayrollItem]
[YearlyValue]
[InitialValue]
[IncrementStep]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveCredit")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveCreditAccrual"

Public Class tLeaveCreditAccrual
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_LeavePayrollItem]
[Date]
[Value]
[Adj]
[Total]
[OldValue]
[NewValue]
[ID_LeaveConversionType]
[Forfeited]
[Converted]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveCreditAccrual")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveCreditFile"

Public Class tLeaveCreditFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[Date]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveCreditFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveCreditFile_Detail"

Public Class tLeaveCreditFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_LeaveCreditFile]
[EmployeeCode]
[LeavePayrollItemCode]
[Value]
[Remarks]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveCreditFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveFile"

Public Class tLeaveFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
[IsExecuted]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveFile_Detail"

Public Class tLeaveFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[EmployeeCode]
[SeqNo]
[IsActive]
[Comment]
[ID_LeaveFile]
[LeavePayrollItem]
[StartDate]
[EndDate]
[Days]
[AmPm]
[Reason]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveParameter"

Public Class tLeaveParameter
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveParameter")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveParameterItem"

Public Class tLeaveParameterItem
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_LeavePayrollItem]
[InitialValue]
[ID_LeaveAccrualType]
[ID_LeaveParameterItemReferenceDate]
[ID_LeaveParameter]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveParameterItem")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveParameterItemReferenceDate"

Public Class tLeaveParameterItemReferenceDate
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveParameterItemReferenceDate")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveParameterItemValue"

Public Class tLeaveParameterItemValue
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_LeaveParameterItem]
[Year]
[Value]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveParameterItemValue")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLeaveType"

Public Class tLeaveType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLeaveType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLevel"

Public Class tLevel
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[SeqNo]
[IsActive]
[Comment]
[BonusParameter]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLevel")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLoan"

Public Class tLoan
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_PayrollItem]
[ID_ImportedLoan_Detail]
[RefNo]
[PrincipalAmt]
[AmortizationAmt]
[InterestAmt]
[PenaltyAmt]
[PaidAmt]
[Terms]
[InterestRate]
[DateGranted]
[StartDate]
[IsOnHold]
[IsActive]
[Comment]
[Priority]
[DateTimeCreated]
[DateTimeModified]
[ID_GuarantorEmployee]
[ID_GuarantorEmployee2]
[LoanAmt]
[BalanceAmt]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLoan")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLoan_Detail"

Public Class tLoan_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Loan]
[Amount]
[YTDAmount]
[AmountToBePaidFor]
[YTDAmountToBePaidFor]
[PayDate]
[ID_PayrollDetail]
[ID_LoanPayment]
[IsPaid]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLoan_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLoanPayment"

Public Class tLoanPayment
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Loan]
[Amt]
[RefNo]
[Date]
[Comment]
[IsApplied]
[ID_Payroll]
[ID_PayrollPeriod]
[UnDeductedAmt]
[IsAffected]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLoanPayment")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLogDevice"

Public Class tLogDevice
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IPAddress]
[SerialNo]
[SeqNo]
[IsActive]
[Comment]
[WithCard]
[IsConnected]
[Color]
[MacAddress]
[Firmware]
[IsEnabled]
[AdminCount]
[RegUserCount]
[FingerCount]
[PassCount]
[AttCount]
[FingerCap]
[UserCap]
[AttCap]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLogDevice")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tLogFileFormat"

Public Class tLogFileFormat
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tLogFileFormat")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tManualAttendanceLog"

Public Class tManualAttendanceLog
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[StartDate]
[EndDate]
[FileDate]
[ID_EditedByUser]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tManualAttendanceLog")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tManualAttendanceLog_Detail"

Public Class tManualAttendanceLog_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Date]
[Time]
[ID_AttendanceLogType]
[ID_EmployeeAttendanceLogCreditDate]
[ID_ManualAttendanceLog]
[SeqNo]
[IsActive]
[Comment]
[TempMinute]
[ComputedTime]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tManualAttendanceLog_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMealLog"

Public Class tMealLog
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[AccessNo]
[LogDateTime]
[LogDate]
[LogMinute]
[LogYear]
[LogMonth]
[LogYearMonth]
[LogYearMonthLastYear]
[LogMonthDate]
[IsVoided]
[ID_MealType]
[Amount]
[Comment]
[ID_MealLogFile]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMealLog")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMealLogFile"

Public Class tMealLogFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Path]
[ID_Company]
[ID_Branch]
[StartDateTime]
[EndDateTime]
[SeqNo]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMealLogFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMealSched"

Public Class tMealSched
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[Code]
[StartMinute]
[LastCallMinute]
[EndMinute]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[Active]
[Comment]
[StartTime]
[LastCallTime]
[EndTime]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[MealAmt]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMealSched")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMealType"

Public Class tMealType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMealType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMedia"

Public Class tMedia
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMedia")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMedicalCondition"

Public Class tMedicalCondition
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMedicalCondition")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMedicalHistory"

Public Class tMedicalHistory
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Persona]
[ID_MedicalCondition]
[DateDiagnosed]
[LastCheckUpDate]
[Status]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMedicalHistory")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenu"

Public Class tMenu
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[Code]
[DataSource]
[BaseDataSource]
[ID_Menu]
[SeqNo]
[ReportFile]
[IsActive]
[IsVisible]
[Comment]
[ID_MenuType]
[AllowNew]
[TableName]
[AllowDelete]
[AllowOpen]
[ReportTitle]
[ReportSubTitle]
[Description]
[ReadOnly]
[ColorRGB]
[DarkColorRGB]
[ImageFile]
[Sort]
[ID_ListMenu]
[ListRowFieldHeader]
[ListRowCategoryHeader]
[ListRowField]
[ListRowCategory]
[IsUserData]
[IsSpanView]
[ListFixedFilter]
[StatusTable]
[IsPOS]
[XField]
[YField]
[SaveTrigger]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenu")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuButton"

Public Class tMenuButton
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[CommandText]
[ID_Menu]
[ConfirmationText]
[SuccessInfoText]
[DisabledOnNewInfo]
[ImageFile]
[ID_MenuDetailTab]
[ID_MenuButtonType]
[MustSaveFirst]
[EnabledIf]
[ListSource]
[IsGeneratedTextFile]
[DefaultFileName]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuButton")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuButtonType"

Public Class tMenuButtonType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuButtonType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuDetailTab"

Public Class tMenuDetailTab
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Menu]
[ImageFile]
[SeqNo]
[IsActive]
[TableName]
[ChildColumn]
[ParentColumn]
[Description]
[Comment]
[DataSource]
[ListSource]
[CheckBoxes]
[ParentTableName]
[ID_DetailMenu]
[ReportFile]
[Sort]
[Sortable]
[ShowInBrowser]
[ID_MenuDetailTabType]
[ParentLookUp]
[ID_ListMenu]
[ListMenuFixedFilter]
[ListMenuDetailSource]
[ShowInInfo]
[Label]
[DetailTabFilter]
[AllowDuplicateList]
[SaveTrigger]
[ImportFile]
[AllowNewRow]
[AllowDeleteRow]
[FileReferenceDataSource]
[FileReferenceSort]
[IsSalaryAuthenticatedTab]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuDetailTab")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuDetailTabField"

Public Class tMenuDetailTabField
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[ID_MenuDetailTab]
[ID_SystemControlType]
[ID_Menu]
[Label]
[SeqNo]
[IsActive]
[Header]
[Description]
[Comment]
[ShowInBrowser]
[Formula]
[ReadOnly]
[Width]
[ListKey]
[ListColumn]
[IsGroup]
[Text]
[ListText]
[Sort]
[IsColumn]
[CopyFromList]
[ImageFile]
[ShowInInfo]
[Expression]
[ParentLookUp]
[ParentLookUpChildColumn]
[IsRequired]
[FixedFilter]
[Defaultvalue]
[IsFrozen]
[ParentLookUpListColumn]
[IsWordWrap]
[ReadOnlyIf]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuDetailTabField")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuDetailTabProperty"

Public Class tMenuDetailTabProperty
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[Value]
[ID_MenuDetailTab]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuDetailTabProperty")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuDetailTabType"

Public Class tMenuDetailTabType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuDetailTabType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuSubDataSource"

Public Class tMenuSubDataSource
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[ID_Menu]
[DataSource]
[SeqNo]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuSubDataSource")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuTab"

Public Class tMenuTab
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[ID_Menu]
[HasTable]
[ImageFile]
[SeqNo]
[IsActive]
[Description]
[Comment]
[ImagePath]
[IsSalaryAuthenticatedTab]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuTab")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuTabField"

Public Class tMenuTabField
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[ID_MenuTab]
[ID_SystemControlType]
[ID_Menu]
[Label]
[SeqNo]
[IsActive]
[Header]
[Description]
[Comment]
[ShowInBrowser]
[ReadOnly]
[Panel]
[ShowInInfo]
[ShowInList]
[StringFormat]
[Sort]
[ParentLookUp]
[ParentLookUpChildColumn]
[Expression]
[ID_SystemAggregateFunction]
[DefaultValue]
[IsRequired]
[FixedFilter]
[ListColumn]
[WritableIf]
[VisibleIf]
[Height]
[RequiredIf]
[IsSalaryAuthenticatedField]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuTabField")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuTabType"

Public Class tMenuTabType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuTabType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMenuType"

Public Class tMenuType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMenuType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMinutesOfTheMeeting"

Public Class tMinutesOfTheMeeting
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[Minutes]
[ID_ClientAppointment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMinutesOfTheMeeting")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMissedLogApproval"

Public Class tMissedLogApproval
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[StartDate]
[EndDate]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMissedLogApproval")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tMonth"

Public Class tMonth
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Quarter]
[Code]
[Name]
[IsActive]
[Comment]
[SeqNo]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tMonth")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tNationality"

Public Class tNationality
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tNationality")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tNormalBalance"

Public Class tNormalBalance
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tNormalBalance")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOB"

Public Class tOB
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[StartDate]
[EndDate]
[FileDate]
[ID_FilingStatus]
[ApprovalDate]
[SeqNo]
[IsActive]
[Comment]
[ID_OBType]
[ApproverStatus]
[IsPosted]
[Reason]
[DateCreated]
[ApproverComment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOB")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOB_Detail"

Public Class tOB_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Date]
[StartTime]
[EndTime]
[StartMinute]
[EndMinute]
[ComputedTimeIn]
[ComputedTimeOut]
[ID_OB]
[IsActive]
[Comment]
[TempStartMinute]
[TempEndMinute]
[ID_Reason]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOB_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOBApproval"

Public Class tOBApproval
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[StartDate]
[EndDate]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOBApproval")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOBFile"

Public Class tOBFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[ID_Company]
[IsActive]
[Comment]
[ID_VerifierEmployee]
[VerificationDate]
[ID_ApprovalEmployee]
[ApprovalDate]
[FileDate]
[ID_OBType]
[IsExecuted]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOBFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOBFile_Detail"

Public Class tOBFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_OBFile]
[EmployeeCode]
[Date]
[StartTime]
[EndTime]
[Reason]
[StartMinute]
[EndMinute]
[IsApplied]
[IsActive]
[Comment]
[ComputedTimeIn]
[ComputedTimeOut]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOBFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOBType"

Public Class tOBType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOBType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOffense"

Public Class tOffense
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_OffenseClearanceTracking]
[ID_OffenseClearanceMethod]
[ID_OffenseType]
[ID_CompanyPolicy]
[Description]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOffense")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOffenseClearanceMethod"

Public Class tOffenseClearanceMethod
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOffenseClearanceMethod")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOffenseClearanceTracking"

Public Class tOffenseClearanceTracking
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOffenseClearanceTracking")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOffenseType"

Public Class tOffenseType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Weight]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOffenseType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOffenseTypeDisciplinaryAction"

Public Class tOffenseTypeDisciplinaryAction
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_OffenseType]
[ID_DisciplinaryAction]
[InstanceNo]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOffenseTypeDisciplinaryAction")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOperationItem"

Public Class tOperationItem
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SAM]
[PricePerPiece]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOperationItem")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOTApproval"

Public Class tOTApproval
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[ID_Company]
[Comment]
[ID_Original]
[ID_Server]
[DateModified]
[StartDate]
[EndDate]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOTApproval")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOvertime"

Public Class tOvertime
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[ID_Attendance]
[ID_PayrollItem]
[ID_FilingStatus]
[Date]
[EffectivityDate]
[ComputedHours]
[ConsideredHours]
[ApprovedHours]
[StartMinute]
[EndMinute]
[TempStartMinute]
[TempEndMinute]
[ComputedStartTime]
[ComputedEndTime]
[WorkDate]
[SeqNo]
[IsPaid]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[StartTime]
[EndTime]
[ID_Department]
[EncodingDate]
[IsBasic]
[FollowingDay]
[ID_WorkCredit]
[ID_WebOvertime]
[ApproverStatus]
[ApproverComment]
[FileDate]
[StartDate]
[EndDate]
[ApprovalDate]
[IsPosted]
[TotalOTHour]
[Reason]
[DateCreated]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOvertime")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOverTimeFile"

Public Class tOverTimeFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[IsExecuted]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOverTimeFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tOverTimeFile_Detail"

Public Class tOverTimeFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[EmployeeCode]
[Employee]
[WorkDate]
[StartTime]
[EndTime]
[IsBasic]
[FollowingDay]
[DepartmentCode]
[IsActive]
[Comment]
[ID_OverTimeFile]
[ComputedTimeIn]
[ComputedTimeOut]
[ForOffSet]
[Reason]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tOverTimeFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tParameter"

Public Class tParameter
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_TaxComputation]
[Code]
[Name]
[DaysPerYear]
[HoursPerDay]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[SeqNo]
[FirstHalfMonthlyRate]
[SecondHalfMonthlyRate]
[MonthsPerYear]
[CompressedWorkWeek]
[MinTakeHomePayPerc]
[MinTakeHomePayAmt]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tParameter")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPasswordExpired"

Public Class tPasswordExpired
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_User]
[SaveDate]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPasswordExpired")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPaymentMode"

Public Class tPaymentMode
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[SeqNo]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPaymentMode")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPaymentTerm"

Public Class tPaymentTerm
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[InDays]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
[ImagePath]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPaymentTerm")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPaymentVoucher_Advances_Detail"

Public Class tPaymentVoucher_Advances_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_PaymentVoucher]
[ID_PurchaseOrder]
[AmountPaid]
[ConvertedAmountPaid]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPaymentVoucher_Advances_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPaymentVoucher_BankTransfer_Detail"

Public Class tPaymentVoucher_BankTransfer_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_PaymentVoucher]
[ID_Bank]
[TransferAmount]
[BankDestination]
[BankDestinationAccount]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPaymentVoucher_BankTransfer_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPaymentVoucher_Cheque_Detail"

Public Class tPaymentVoucher_Cheque_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_PaymentVoucher]
[ID_Bank]
[ChequeNo]
[ChequeDate]
[ChequeAmount]
[ID_ChequeStatus]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPaymentVoucher_Cheque_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPaymentVoucher_Detail"

Public Class tPaymentVoucher_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_PaymentVoucher]
[ID_VoucherPayable]
[GrossAmount]
[VatPerc]
[VatAmount]
[DiscPerc]
[DiscAmount]
[NetAmount]
[PaidAmount]
[Balance]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPaymentVoucher_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPaymentVoucherJV"

Public Class tPaymentVoucherJV
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Amount]
[ID_NormalBalance]
[ID_CostCenter]
[SeqNo]
[IsActive]
[Comment]
[AccountName]
[Debit]
[Credit]
[ID_COA]
[ID_PaymentVoucher]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPaymentVoucherJV")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayroll"

Public Class tPayroll
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_PayrollPeriod]
[ID_Employee]
[ID_Company]
[ID_Branch]
[ID_Department]
[ID_Designation]
[ID_JobClass]
            [ID_CostCenter]
[ID_EmployeeStatus]
[ID_PayrollScheme]
[ID_PayrollFrequency]
[ID_CompanyBankAcct]
[ID_TaxExemption]
[ID_PaymentMode]
[ID_Parameter]
[Comment]
[MonthlyRate]
[DailyRate]
[HourlyRate]
[DMonthlyRate]
[DDailyRate]
[DHourlyRate]
[TaxableAmt]
[TaxAmt]
[GrossAmt]
[DeductionAmt]
[NetAmt]
[IsBasicPay]
[IsAnnualize]
[Is13Month]
[IsFinalPay]
[IsAdjustment]
[IsRetro]
[IsPreviousEmployer]
[IsProcessed]
[IsHold]
[IsRequiredToLog]
[IsActive]
            [DateTimeCreated]
            [DateTimeModified]
            [DateCreated]
            [DateModified]
            [ForAnnualization]
[Basic]
[BasicDeduction]
[Premium]
[OtherIncome]
[BasicPay]
[SSSSubjectGross]
[PHICSubjectGross]
[HDMFSubjectGross]
[WithTaxSubjectGross]
[TotalHoursWorked]
[LeaveWithPay]
[EPayslipDateTime]
[IsSentEPayslip]
[HasBasic]
[MWE]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayroll")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayroll_Detail"

Public Class tPayroll_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Payroll]
[ID_PayrollItem]
[ID_Loan]
[Days]
[Hours]
[Minutes]
[TotalHours]
[Amt]
[Adj]
[Total]
[TaxAmount]
[Taxable]
[Nontaxable]
[IsScheduled]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ActualTaxable]
[Processed]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayroll_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollFile"

Public Class tPayrollFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_Company]
[ID_PayrollPeriod]
[Name]
[SeqNo]
[IsActive]
[Comment]
[Year]
[ID_Month]
[ID_PayrollSchedule]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollFile_Detail"

Public Class tPayrollFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_PayrollFile]
[EmployeeCode]
[Employee]
[REG]
[RD]
[SH]
[SHR]
[LH]
[LHR]
[OT]
[RDOT]
[SHOT]
[SHROT]
[LHOT]
[LHROT]
[ND]
[RDND]
[SHND]
[SHRND]
[LHND]
[LHRND]
[NDOT]
[RDNDOT]
[SHNDOT]
[SHRNDOT]
[LHNDOT]
[LHRNDOT]
[TARDY]
[UT]
[LWOP]
[VL]
[SL]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollFrequency"

Public Class tPayrollFrequency
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[SeqNo]
[Code]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollFrequency")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollItem"

Public Class tPayrollItem
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Income]
[ID_PayrollItemType]
[ID_PayrollItemCategory]
[ID_PayrollItemGroup]
[ID_NormalBalance]
[IsActive]
[Comment]
[Priority]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ForSSS]
[ForPHIC]
[ForHDMF]
[ForTMonth]
[ForGSIS]
[BonusPriority]
[SpecialGL]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollItem")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollItemAccount"

Public Class tPayrollItemAccount
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_PayrollItem]
[SeqNo]
[IsActive]
[Comment]
[ID_CreditAccount]
[ID_DebitAccount]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollItemAccount")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollItemCategory"

Public Class tPayrollItemCategory
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollItemCategory")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollItemGroup"

Public Class tPayrollItemGroup
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollItemGroup")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollItemRate"

Public Class tPayrollItemRate
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Parameter]
[ID_PayrollItem]
[Rate]
[Amt]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollItemRate")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollItemSetup"

Public Class tPayrollItemSetup
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Parameter]
[ID_PayrollItem]
[ID_Employee]
[ID_PayrollItemSetupOption]
[Amt]
[Period1]
[Period2]
[Period3]
[Period4]
[Period5]
[Period6]
[Period7]
[Period8]
[Period9]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollItemSetup")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollItemSetupOption"

Public Class tPayrollItemSetupOption
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[SeqNo]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollItemSetupOption")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollItemType"

Public Class tPayrollItemType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollItemType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollPeriod"

Public Class tPayrollPeriod
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_EmployeeDailyScheduleView]
[ID_PayrollPeriodType]
[ID_PayrollSchedule]
[ID_Employee]
[PayDate]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[ID_Month]
[Year]
[ID_Company]
[ID_PayrollFrequency]
[StartDate]
[EndDate]
[PayStartDate]
[PayEndDate]
[TermNo]
[RemainingTerms]
[ID_ContributionMonth]
[ContributionYear]
[ForAnnualization]
[IsPaused]
[CurrentID]
[ID_Branch]
            [ID_Department]
            [ID_PayrollClassifi]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollPeriod")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollPeriodType"

Public Class tPayrollPeriodType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollPeriodType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollSchedule"

Public Class tPayrollSchedule
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollSchedule")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollScheme"

Public Class tPayrollScheme
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[SeqNo]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollScheme")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollTransitionFile"

Public Class tPayrollTransitionFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Year]
[ID_Month]
[ID_PayrollSchedule]
[PayDate]
[ID_PayrollPeriodType]
[StartDate]
[EndDate]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollTransitionFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPayrollTransitionFile_Detail"

Public Class tPayrollTransitionFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_PayrollTransitionFile]
[EmployeeCode]
[REG]
[RD]
[SH]
[SHR]
[LH]
[LHR]
[OT]
[RDOT]
[SHOT]
[SHROT]
[LHOT]
[LHROT]
[ND]
[RDND]
[SHND]
[SHRND]
[LHND]
[LHRND]
[NDOT]
[RDNDOT]
[SHNDOT]
[SHRNDOT]
[LHNDOT]
[LHRNDOT]
[Basic13thMonth]
[VL]
[SL]
[WithholdingTax]
[EL]
[ML]
[SSSEE]
            [SSSER]
            [PHICEE]
            [PHICER]
            [HDMFEE]
            [HDMFER]
[LWOP]
[UT]
[Tardy]
[VLConversion]
[SLConversion]
[DeMinimisBenefits]
[HazardPay]
[HospitalInsurance]
[Representation]
[TransportationAllowance]
[HousingAllowance]
[ProfitSharing]
[DirectorFee]
[TaxRefund]
[ECOLA]
[Commission]
[CanteenDues]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPayrollTransitionFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPeriod"

Public Class tPeriod
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPeriod")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPersona"

Public Class tPersona
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[LastName]
[FirstName]
[MiddleName]
[NickName]
[BirthDate]
[BirthPlace]
[Father]
[Mother]
[Spouse]
[SpouseBirthDate]
[SpouseEmployer]
[SpouseOccupation]
[SSSNo]
[PhilHealthNo]
[HDMFNo]
[TIN]
[Height]
[Weight]
[EmailAddress]
[AlternateEmailAddress]
[ContactNo]
[MobileNo]
[Hobbies]
[ImageFile]
[IsApplicant]
[ID_Company]
[ID_Gender]
[ID_Religion]
[ID_Nationality]
[ID_Citizenship]
[ID_CivilStatus]
[ID_BloodType]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[Name]
[Birthday]
[Age]
[Name1]
[Name2]
[Comment]
[MiddleInitial]
[HasCompleteEmploymentRequirements]
[KnownEmployee]
[KnownEmployee_Location]
[KnownEmployee_Designation]
[KnownEmployee_Relationship]
[CriminalRecord]
[Illness]
[GSISNo]
[Eligibility]
[ID_SSSStatus]
[GUID]
[Hired]
[SalaryDesired]
[ApplicationDate]
[DriversLicenseNo]
[ACRNo]
[WorkPermitNo]
[PassportNo]
[Suffix]
[FatherOccupation]
[FatherBirthDate]
[MotherOccupation]
[MotherBirthDate]
[ID_Designation1]
[ID_Designation2]
[ID_Designation3]
[ID_EducationBackground]
[ResumeFile]
[ID_EmployeeReportCard]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPersona")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPersonaAddress"

Public Class tPersonaAddress
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Address]
[ID_Area]
[ID_AddressType]
[SeqNo]
[ID_Persona]
[ContactNo]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPersonaAddress")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPersonaEmergencyContact"

Public Class tPersonaEmergencyContact
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Address]
[ContactNo]
[Relationship]
[IsActive]
[Comment]
[ID_Persona]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPersonaEmergencyContact")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPersonaFamily"

Public Class tPersonaFamily
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[Relationship]
[ID_CivilStatus]
[Age]
[Occupation]
[Company]
[ID_Persona]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPersonaFamily")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPHIC"

Public Class tPHIC
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Bracket]
[LBound]
[UBound]
[EE]
[ER]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[Total]
[DateCreated]
[DateModified]
[Year]
[YearTo]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPHIC")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPieceRateProductionOrder"

Public Class tPieceRateProductionOrder
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_PieceRateDesign]
[JOCode]
[JOQty]
[ID_Department]
[Date]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPieceRateProductionOrder")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPostClosing"

Public Class tPostClosing
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[Period]
[Year]
[ID_FilingStatus]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPostClosing")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPriority"

Public Class tPriority
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPriority")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tProblemSolution"

Public Class tProblemSolution
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[Solution]
[ID_ClientAppointment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tProblemSolution")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tProvince"

Public Class tProvince
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[Code]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[Active]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tProvince")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPurchaseOrderReference"

Public Class tPurchaseOrderReference
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Reference]
[ID_SystemTable]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPurchaseOrderReference")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPurchaseRequestType"

Public Class tPurchaseRequestType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_OriginType]
[ImageFile]
[ImagePath]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPurchaseRequestType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPurchaseReturn_Detail"

Public Class tPurchaseReturn_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_PurchaseReturn]
[ID_ReceivingReport_Detail]
[ID_WarehouseArea]
[UnitPrice]
[QuotedPrice]
[Qty]
[LineTotal]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ImagePath]
[ID_Item]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPurchaseReturn_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPurchaseReturnJV"

Public Class tPurchaseReturnJV
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_JournalVoucher]
[Amount]
[ID_NormalBalance]
[ID_CostCenter]
[Group]
[SeqNo]
[IsActive]
[Comment]
[AccountName]
[Debit]
[Credit]
[ID_COA]
[ID_PurchaseReturn]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPurchaseReturnJV")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tPurpose"

Public Class tPurpose
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_MinutesOfTheMeeting]
[ID_ClientAppointment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tPurpose")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tQuarter"

Public Class tQuarter
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[MinMonth]
[MaxMonth]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tQuarter")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tReceivingReport_Detail"

Public Class tReceivingReport_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ReceivingReport]
[ID_Item]
[QTY]
[UnitCost]
[LineTotal]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ImagePath]
[ID_PurchaseOrder_Detail]
[ID_WarehouseArea]
[WithPay]
[Price]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tReceivingReport_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tReceivingReportJV"

Public Class tReceivingReportJV
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_JournalVoucher]
[Amount]
[ID_NormalBalance]
[ID_CostCenter]
[Group]
[SeqNo]
[IsActive]
[Comment]
[AccountName]
[Debit]
[Credit]
[ID_COA]
[ID_ReceivingReport]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tReceivingReportJV")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tReligion"

Public Class tReligion
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tReligion")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tRequest"

Public Class tRequest
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tRequest")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tRequest_Detail"

Public Class tRequest_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Request]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tRequest_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tRetailReceipt"

Public Class tRetailReceipt
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_Customer]
[SubTotal]
[DiscountRate]
[TaxRate]
[Discount]
[Tax]
[Total]
[DateTime]
[ID_User]
[Qty]
[Comment]
[ID_RetailReceiptType]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tRetailReceipt")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tRetailReceipt_Detail"

Public Class tRetailReceipt_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_RetailReceipt]
[ID_Item]
[SRP]
[Qty]
[DiscountRate]
[Discount]
[Total]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tRetailReceipt_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tRetailReceiptType"

Public Class tRetailReceiptType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tRetailReceiptType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tReturnType"

Public Class tReturnType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tReturnType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tRF1"

Public Class tRF1
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_Quarter]
[Year]
[M1RefNo]
[M2RefNo]
[M3RefNo]
[M1DatePaid]
[M2DatePaid]
[M3DatePaid]
[CertifiedBy]
[CertifiedByPosition]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tRF1")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tRF1_Detail"

Public Class tRF1_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_RF1]
[ID_Employee]
[LastName]
[FirstName]
[MiddleName]
[RefNo]
[MonthlyRate]
[PS1Amt]
[ES1Amt]
[PS2Amt]
[ES2Amt]
[PS3Amt]
[ES3Amt]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tRF1_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tRF1Monthly"

Public Class tRF1Monthly
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_Month]
[Year]
[MRefNo]
[MDatePaid]
[CertifiedBy]
[CertifiedByPosition]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tRF1Monthly")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tRF1Monthly_Detail"

Public Class tRF1Monthly_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_RF1Monthly]
[ID_Employee]
[LastName]
[FirstName]
[MiddleName]
[RefNo]
[MonthlyRate]
[PSAmt]
[ESAmt]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[MonthlyRate2]
[StartDate]
[EndDate]
[ID_EmployeeStatus]
[Suffix]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tRF1Monthly_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSalaryIncrease"

Public Class tSalaryIncrease
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Percentage]
[Date]
[ID_FilingStatus]
[ID_Company]
[ID_SalaryIncreaseType]
[IsActive]
[Comment]
[Amount]
            [ID_PayrollScheme]
            [isApplied]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSalaryIncrease")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSalaryIncrease_Detail"

Public Class tSalaryIncrease_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_SalaryIncrease]
[ID_Employee]
[PreviousMonthlyRate]
            [NewMonthlyRate]
            [fromTemplate]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSalaryIncrease_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSalaryIncreaseType"

Public Class tSalaryIncreaseType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSalaryIncreaseType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSale"

Public Class tSale
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Comment]
[Name]
[ID_Client]
[Date]
[SubTotalAmt]
[AdditionalDiscountAmt]
[GrossAmt]
[VatAmt]
[NetAmt]
[ID_Sale_Detail]
[ID_Employee]
[ImageFile]
[ImagePath]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSale")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSale_Detail"

Public Class tSale_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Comment]
[ID_Sale]
[QTY]
[SRP]
[DiscountAmt]
[TotalAmt]
[ID_ProjectType]
[ID_ProjectStatus]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSale_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSCDistributionType"

Public Class tSCDistributionType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSCDistributionType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tScheduleAssignment"

Public Class tScheduleAssignment
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[ID_Company]
[StartDate]
[EndDate]
[EmployeeDefault]
[ID_DailyScheduleMon]
[ID_DailyScheduleTue]
[ID_DailyScheduleWed]
[ID_DailyScheduleThu]
[ID_DailyScheduleFri]
[ID_DailyScheduleSat]
[ID_DailyScheduleSun]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tScheduleAssignment")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tScheduleAssignment_Detail"

Public Class tScheduleAssignment_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ScheduleAssignment]
[ID_Employee]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tScheduleAssignment_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tScheduleFile"

Public Class tScheduleFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[StartDate]
[SeqNo]
[IsActive]
[Comment]
[ApproverStatus]
[ID_FilingStatus]
[IsPosted]
[ApprovalDate]
[isDefault]
[DateCreated]
[DateFiled]
[ID_Employee]
[ApproverComment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tScheduleFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tScheduleFile_Detail"

Public Class tScheduleFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ScheduleFile]
[SeqNo]
[EmployeeCode]
[Employee]
[Schedule1]
[Schedule2]
[Schedule3]
[Schedule4]
[Schedule5]
[Schedule6]
[Schedule7]
[Comment]
[ID_Employee]
[RD1]
[RD2]
[RD3]
[RD4]
[RD5]
[RD6]
[RD7]
[ID_SectionAssignment]
[Straight1]
[Straight2]
[Straight3]
[Straight4]
[Straight5]
[Straight6]
[Straight7]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tScheduleFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tScheduleMatrix"

Public Class tScheduleMatrix
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tScheduleMatrix")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSchool"

Public Class tSchool
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[ID_JobMatching]
[ImageFile]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSchool")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSchoolLevel"

Public Class tSchoolLevel
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[isActive]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSchoolLevel")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSection"

Public Class tSection
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[SeqNo]
[IsActive]
[Comment]
[ID_Department]
[ID_Branch]
[ID_Division]
[Series]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSection")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSeparation"

Public Class tSeparation
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSeparation")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tServiceCharge"

Public Class tServiceCharge
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[StartDate]
[EndDate]
[NetAmount]
[TotalHours]
[AmountPerHour]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[Year]
[ID_Month]
[ID_PayrollSchedule]
[ID_SCDistributionType]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tServiceCharge")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tServiceCharge_Detail"

Public Class tServiceCharge_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_ServiceCharge]
[ID_Employee]
[Hours]
[AmountPerHour]
[IsActive]
[Comment]
[AdjHours]
[TotalAmount]
[PrevAdjHours]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tServiceCharge_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tServiceChargePaymentSchedule"

Public Class tServiceChargePaymentSchedule
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Year]
[ID_Month]
[ID_PayrollSchedule]
[Percentage]
[ID_ServiceCharge]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tServiceChargePaymentSchedule")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tServiceTicketStatus"

Public Class tServiceTicketStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[Color]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tServiceTicketStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSession"

Public Class tSession
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[StartDateTime]
[EndDateTime]
[ID_User]
[ID_Company]
[ElapsedTime]
[Comment]
[GUID]
[ID_Employee]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSession")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSetting"

Public Class tSetting
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Value]
[Active]
[ID_SettingType]
[DateTimeCreated]
[DateTimeModified]
[ForAdmin]
[Comment]
[ID_SettingGroup]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSetting")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSettingGroup"

Public Class tSettingGroup
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSettingGroup")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSettingType"

Public Class tSettingType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Active]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[ID_Transaction_Created]
[ID_Transaction_Modified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSettingType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tShippingAddress"

Public Class tShippingAddress
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tShippingAddress")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tShippingMethod"

Public Class tShippingMethod
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tShippingMethod")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSkill"

Public Class tSkill
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSkill")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSourceCode"

Public Class tSourceCode
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSourceCode")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSpecialization"

Public Class tSpecialization
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSpecialization")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSSS"

Public Class tSSS
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Bracket]
[LBound]
[UBound]
[Credit]
[ER]
[EC]
[EE]
[IsActive]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[Total]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[YearFrom]
[YearTo]
[MonthlySalaryCredit]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSSS")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSSSLoanExport"

Public Class tSSSLoanExport
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[Year]
[ID_Month]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSSSLoanExport")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSSSLoanExport_Detail"

Public Class tSSSLoanExport_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_SSSLoanExport]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSSSLoanExport_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSSSLoanRemittance"

Public Class tSSSLoanRemittance
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_Month]
[Year]
[RefNo]
[DatePaid]
[SeqNo]
[IsActive]
[Comment]
[AuthorizedRepresentative]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSSSLoanRemittance")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSSSLoanRemittance_Detail"

Public Class tSSSLoanRemittance_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_SSSLoanRemittance]
[ID_Employee]
[LastName]
[FirstName]
[MiddleName]
[SSSNo]
[ID_LoanPayrollItem]
[DateGranted]
[LoanAmt]
[PenaltyAmt]
[PaidAmt]
[BalanceAmt]
[SeqNo]
[IsActive]
[Comment]
[Remarks]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSSSLoanRemittance_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSSSRemittance"

Public Class tSSSRemittance
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Company]
[ID_Month]
[Year]
[RefNo]
[DatePaid]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[Name]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSSSRemittance")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSSSRemittance_Detail"

Public Class tSSSRemittance_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_SSSRemittance]
[ID_Employee]
[LastName]
[FirstName]
[MiddleName]
[SSSNo]
[DateHired]
[TotalSSS]
[SSSEC]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
[SSSEE]
[SSSER]
[Birthdate]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSSSRemittance_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSSSStatus"

Public Class tSSSStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSSSStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tStraightDutyApproval"

Public Class tStraightDutyApproval
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[StartDate]
[EndDate]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tStraightDutyApproval")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tStraightDutyFiling"

Public Class tStraightDutyFiling
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_Employee]
[Date]
[ID_FilingStatus]
[ImageFile]
[SeqNo]
[IsActive]
[ID_Company]
[Comment]
[ID_Original]
[ID_Server]
[DateModified]
[ForBatchApproval]
[Reason]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tStraightDutyFiling")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSubject"

Public Class tSubject
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[ImagePath]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSubject")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSubsidiary"

Public Class tSubsidiary
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSubsidiary")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSubsidiaryType"

Public Class tSubsidiaryType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSubsidiaryType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemAgent"

Public Class tSystemAgent
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemAgent")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemAggregateFunction"

Public Class tSystemAggregateFunction
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemAggregateFunction")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemApplication"

Public Class tSystemApplication
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemApplication")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemApplicationMenu"

Public Class tSystemApplicationMenu
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_SystemApplication]
[ID_Menu]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemApplicationMenu")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemApplicationMenuDetailTab"

Public Class tSystemApplicationMenuDetailTab
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_SystemApplication]
[ID_MenuDetailTab]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemApplicationMenuDetailTab")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemApplicationMenuTab"

Public Class tSystemApplicationMenuTab
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_SystemApplication]
[ID_MenuTab]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemApplicationMenuTab")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemArchiving"

Public Class tSystemArchiving
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[StartDate]
[EndDate]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemArchiving")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemControlType"

Public Class tSystemControlType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
[SeqNo]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemControlType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemDataLookUp"

Public Class tSystemDataLookUp
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Menu]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemDataLookUp")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemFileExtension"

Public Class tSystemFileExtension
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_SystemFileType]
[Name]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemFileExtension")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemFileType"

Public Class tSystemFileType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemFileType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemImage"

Public Class tSystemImage
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ImageFile]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemImage")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemMessage"

Public Class tSystemMessage
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Description]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemMessage")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemQueryParameter"

Public Class tSystemQueryParameter
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Label]
[DefaultValue]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemQueryParameter")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tSystemTable"

Public Class tSystemTable
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[IsForCodeGen]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tSystemTable")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTable"

Public Class tTable
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[IsSystemTable]
[IsForCodeGen]
[IsForArchive]
[Reseed]
[IsTemporaryTable]
[HasAuditTrail]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTable")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTardinessRounding"

Public Class tTardinessRounding
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[LBound]
[UBound]
[Value]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTardinessRounding")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTask"

Public Class tTask
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_MinutesOfTheMeeting]
[ID_ClientAppointment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTask")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTax"

Public Class tTax
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_TaxExemption]
[ID_PayrollFrequency]
[Bracket]
[LBound]
[UBound]
[Fix]
[Rate]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTax")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTaxComputation"

Public Class tTaxComputation
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Name]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTaxComputation")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTaxExemption"

Public Class tTaxExemption
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Amt]
[IsActive]
[Comment]
[SeqNo]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTaxExemption")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTaxFile"

Public Class tTaxFile
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[Date]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTaxFile")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTaxFile_Detail"

Public Class tTaxFile_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_TaxFile]
[TaxExcemptionCode]
[PayrollFrequencyCode]
[Bracket]
[LBound]
[UBound]
[Fix]
[Rate]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTaxFile_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTimeSheet"

Public Class tTimeSheet
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTimeSheet")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTMonth"

Public Class tTMonth
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_PayrollPeriod]
[StartDate]
[EndDate]
[UseMonthlyRate]
[ID_Month]
[Year]
[ID_PayrollSchedule]
[ID_Company]
[ID_PayrollScheme]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTMonth")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTMonth_Detail"

Public Class tTMonth_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[ID_TMonth]
[ID_Employee]
[Amount]
[SeqNo]
[IsActive]
[Comment]
[MonthlyRate]
[Months]
[LWOP]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTMonth_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTMonthAccrual"

Public Class tTMonthAccrual
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Employee]
[ID_Month]
[Amount]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTMonthAccrual")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTransaction"

Public Class tTransaction
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Session]
[Active]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tTransaction")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUnit"

Public Class tUnit
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_Company]
[SeqNo]
[IsActive]
[Comment]
[ID_Section]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUnit")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUser"

Public Class tUser
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[LogInName]
[Name]
[Password]
[IsActive]
[Comment]
[ID_UserGroup]
[ID_SystemAgent]
[DateTimeCreated]
[DateTimeModified]
[ImageFile]
[ID_Persona]
[ID_Employee]
[ID_Approver1]
[ID_Approver2]
[InvalidLogCount]
[IsFirstLog]
[LastPasswordChangeDate]
[IsBlocked]
[IsExpired]
[ForScheduler]
[LoginCount]
[ID_Applicant]
[IsApprover]
[BlockedDate]
[ID_SecretQuestion]
[SecretAnswer]
[ID_WebUserGroup]
[ID_UserType]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUser")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserFavMenu"

Public Class tUserFavMenu
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[SeqNo]
[Comment]
[ID_User]
[ID_Menu]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserFavMenu")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserGroup"

Public Class tUserGroup
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[CanViewEmployeeSalary]
[IsActive]
[Comment]
[CanViewDoorAccessDevice]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserGroup")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserGroupCompany"

Public Class tUserGroupCompany
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Comment]
[ID_UserGroup]
[ID_Company]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserGroupCompany")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserGroupDesignation"

Public Class tUserGroupDesignation
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_UserGroup]
[ID_Designation]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserGroupDesignation")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserGroupMenu"

Public Class tUserGroupMenu
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_UserGroup]
[ID_Menu]
[AllowEdit]
[Comment]
[AllowNew]
[AllowDelete]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserGroupMenu")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserGroupMenuButton"

Public Class tUserGroupMenuButton
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_UserGroupMenu]
[ID_MenuButton]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserGroupMenuButton")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserGroupMenuDetailTab"

Public Class tUserGroupMenuDetailTab
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_UserGroupMenu]
[ID_MenuDetailTab]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserGroupMenuDetailTab")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserGroupMenuTab"

Public Class tUserGroupMenuTab
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_UserGroupMenu]
[ID_MenuTab]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserGroupMenuTab")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserMenu"

Public Class tUserMenu
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_User]
[ID_Menu]
[GUID_Parent]
[GroupCount]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserMenu")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserMenuTabField"

Public Class tUserMenuTabField
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_User]
[ID_MenuTabField]
[SeqNo]
[GroupSeqNo]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserMenuTabField")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserModuleType"

Public Class tUserModuleType
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserModuleType")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserModuleType_Detail"

Public Class tUserModuleType_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_UserModuleType]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserModuleType_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserPayrollPeriod"

Public Class tUserPayrollPeriod
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[ID_User]
[ID_PayrollPeriod]
[Comment]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tUserPayrollPeriod")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tUserAlphalist"

    Public Class tUserAlphalist
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            ID
            ID_User
            ID_Alphalist
            DateTimeCreated
            DateTimeModified
            DateCreated
            DateModified
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tUserAlphalist")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region




#Region "tValidReason"

Public Class tValidReason
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tValidReason")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tVatStatus"

Public Class tVatStatus
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tVatStatus")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tVoucherPayable_Detail"

Public Class tVoucherPayable_Detail
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ImageFile]
[SeqNo]
[IsActive]
[Comment]
[ID_VoucherPayable]
[ID_ReceivingReport]
[GrossAmount]
[VatAmount]
[VatPerc]
[DiscPerc]
[DiscAmount]
[NetAmount]
[ReceivingReport]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tVoucherPayable_Detail")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tVoucherPayableJV"

Public Class tVoucherPayableJV
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Amount]
[ID_NormalBalance]
[ID_CostCenter]
[SeqNo]
[IsActive]
[Comment]
[AccountName]
[Debit]
[Credit]
[ID_COA]
[ID_VoucherPayable]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tVoucherPayableJV")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tWebParameters"

Public Class tWebParameters
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Paramname]
[ParamValue]
[Description]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tWebParameters")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tWebSecurityQuestion"

Public Class tWebSecurityQuestion
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Description]
[isActive]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tWebSecurityQuestion")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tWeekDay"

Public Class tWeekDay
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
[ID_Transaction_Created]
[ID_Transaction_Modified]
[DateTimeCreated]
[DateTimeModified]
[DateCreated]
[DateModified]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tWeekDay")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tWeeklySchedule"

Public Class tWeeklySchedule
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[ID_DailyScheduleMon]
[ID_DailyScheduleTue]
[ID_DailyScheduleWed]
[ID_DailyScheduleThu]
[ID_DailyScheduleFri]
[ID_DailyScheduleSat]
[ID_DailyScheduleSun]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tWeeklySchedule")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tWorkCredit"

Public Class tWorkCredit
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[SeqNo]
[IsActive]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tWorkCredit")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tYear"

Public Class tYear
    Inherits GSCOM.SQL.ZDataTable

    Public Enum Field
        [ID]
[Code]
[Name]
[Year]
[Comment]
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(ByVal c As SqlClient.SqlConnection)
        MyBase.New(c, "tYear")
    End Sub

#Region "Basic"

#Region "Get"
    Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
        Try
            Return Me.Rows(rowIndex).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As Field) As Object
        Try
            Return Me.Rows(0).Item(f.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function [Get](ByVal f As String) As Object
        Try
            Return Me.Rows(0).Item(f)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Set"
    Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(rowIndex).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub [Set](ByVal f As Field, ByVal value As Object)
        Try
            Me.Rows(0).Item(f.ToString) = value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Columns"
    Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
        Get
            Return Me.Columns.Item(f.ToString)
        End Get
    End Property
#End Region

#End Region

End Class

#End Region

#Region "tTimekeepingFile"

    Public Class tTimekeepingFile
        Inherits GSCOM.SQL.ZDataTable

        Public Enum Field
            [ID]
            [Name]
            [StartDate]
            [EndDate]
            [IsApplied]
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tTimekeepingFile")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region

#Region "tTimekeepingFile_Detail"

    Public Class tTimekeepingFile_Detail
        Inherits GSCOM.SQL.ZDataTable
        Public Enum Field
            ID
            ID_TimeKeepingFile
            EmployeeCode
            Employee
            [Date]
            DefaultSched
            LogsTimeIn
            LogsTimeOut
            OTTimeIn
            OTTimeOut
            ConsideredHours
            CostCenterCode
            OBTimeIn
            OBTimeOut
            Leave
            Schedule
            IsRD
            IsSD
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal c As SqlClient.SqlConnection)
            MyBase.New(c, "tTimekeepingFile_Detail")
        End Sub

#Region "Basic"

#Region "Get"
        Public Function [Get](ByVal rowIndex As Integer, ByVal f As Field) As Object
            Try
                Return Me.Rows(rowIndex).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As Field) As Object
            Try
                Return Me.Rows(0).Item(f.ToString)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function [Get](ByVal f As String) As Object
            Try
                Return Me.Rows(0).Item(f)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Set"
        Public Sub [Set](ByVal rowIndex As Integer, ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(rowIndex).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Set](ByVal f As Field, ByVal value As Object)
            Try
                Me.Rows(0).Item(f.ToString) = value
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Columns"
        Public Overloads ReadOnly Property Columns(ByVal f As Field) As DataColumn
            Get
                Return Me.Columns.Item(f.ToString)
            End Get
        End Property
#End Region

#End Region

    End Class

#End Region

End Namespace

Public Enum Menu
[ACCOUNTING] = 145
[ACCOUNTING_ChartOfAccountTemplate] = 651
[ACCOUNTING_ChartofAccounts] = 518
[ACCOUNTING_CompanySetUp] = 146
[ACCOUNTING_JournalVoucher] = 522
[ADMINISTRATIVE] = 27
[ADMINISTRATIVE_Activity] = 435
[ADMINISTRATIVE_Activity_AuditTrail] = 614
[ADMINISTRATIVE_Activity_DailyAutomation] = 162
[ADMINISTRATIVE_Activity_OnLineUser] = 85
[ADMINISTRATIVE_Activity_Session] = 26
[ADMINISTRATIVE_Archive] = 652
[ADMINISTRATIVE_DocumentSeries] = 499
[ADMINISTRATIVE_EmployeeTemplate] = 352
[ADMINISTRATIVE_File] = 442
[ADMINISTRATIVE_JournalVoucherType] = 226
[ADMINISTRATIVE_Reports] = 436
[ADMINISTRATIVE_Setting] = 218
[ADMINISTRATIVE_User] = 15
[ADMINISTRATIVE_UserGroup] = 14
[ADMINISTRATIVE_UserGroup_UserGroupMenu] = 612
[ADMINISTRATIVE_UserGroup_UserGroupMenuTab] = 611
[ADMINISTRATIVE_UserGroup_UserGroupMenuXx] = 459
[ADMINISTRATIVE_UserGroup_xxxxUsergroupMenuTab] = 610
[ADMINISTRATIVE_WebIONS] = 839
[ADMINISTRATIVE_WebIONS_ApproverListing] = 820
[ADMINISTRATIVE_WebIONS_SchedulerListing] = 676
[ALERT] = 323
[ALERT_Alert] = 63
[ALERT_Alert_AlertType] = 92
[ALERT_Alert_AlertTypeReferenceDate] = 628
[ALERT_Alert_NotificationPeriod] = 324
[INSYSBIOMETRICDEVICES] = 642
[INSYSBIOMETRICDEVICES_BatchFingerDataTransfer] = 645
[INSYSBIOMETRICDEVICES_DeviceManager] = 846
[INSYSBIOMETRICDEVICES_Employee] = 644
[INSYSBIOMETRICDEVICES_SetUp] = 643
[INSYSORBIT] = 806
[INSYSORBIT_LogReports] = 275
[INSYSORBIT_MonthlyReports] = 308
[INSYSORBIT_RegularReports] = 211
[INSYSORBIT_RegularReports_DailyScheduleReport] = 368
[INSYSORBIT_RegularReports_MissedLogReport] = 812
[INSYSORBIT_RegularReports_PerfectAttendanceReport] = 297
[INSYSORBIT_TimeandAttendance] = 809
[INSYSORBIT_TimeandAttendance_EmployeeAttendanceLogFile] = 106
[INSYSORBIT_TimeandAttendance_EmployeeTimeRecord] = 114
[INSYSORBIT_TimeandAttendance_EmployeeTimesheetFile] = 340
[INSYSORBIT_TimeandAttendance_ManualAttendanceInput] = 365
[INSYSORBIT_TimekeepingItems] = 810
[INSYSORBIT_TimekeepingItems_EmployeeChangeofSchedule] = 678
[INSYSORBIT_TimekeepingItems_EmployeeDailySchedule] = 188
[INSYSORBIT_TimekeepingItems_Leave] = 51
[INSYSORBIT_TimekeepingItems_LeaveFile] = 169
[INSYSORBIT_TimekeepingItems_MissedLog] = 926
[INSYSORBIT_TimekeepingItems_MissedLogFile] = 928
[INSYSORBIT_TimekeepingItems_OfficialBusiness] = 355
[INSYSORBIT_TimekeepingItems_OfficialBusinessFile] = 159
[INSYSORBIT_TimekeepingItems_Overtime] = 61
[INSYSORBIT_TimekeepingItems_OvertimeFile] = 362
    [INSYSORBIT_TimekeepingItems_TimeandAttendanceProcessing] = 96

[INSYSORBIT_WorkSchedule] = 807
[INSYSORBIT_WorkSchedule_BranchAssignment] = 255
[INSYSORBIT_WorkSchedule_CostCenterAssignment] = 639
[INSYSORBIT_WorkSchedule_CostCenterAssignmentFile] = 640
[INSYSORBIT_WorkSchedule_RestDayFile] = 191
[INSYSORBIT_WorkSchedule_ScheduleAssignment] = 104
[INSYSORBIT_WorkSchedule_ScheduleFile] = 149
[INSYSPAYROLL] = 811
[INSYSPAYROLL_13thMonthPay] = 202
[INSYSPAYROLL_AnnualReports] = 155
[INSYSPAYROLL_AnnualReports_AlphaList] = 46
[INSYSPAYROLL_BankExport] = 74
[INSYSPAYROLL_EmployeeWorkedHoursFile] = 342
[INSYSPAYROLL_FinalPay] = 160
[INSYSPAYROLL_HDMFReports] = 272
    [INSYSPAYROLL_HDMFReports_HDMFExport] = 115
    [INSYSPAYROLL_HDMFLoanReports_HDMFExport] = 957
[INSYSPAYROLL_HDMFReports_MCRF] = 377
[INSYSPAYROLL_HDMFReports_MonthlyContribution] = 396
[INSYSPAYROLL_IncomeAndDeduction] = 933
[INSYSPAYROLL_IncomeAndDeductionhardcoded] = 90
[INSYSPAYROLL_LeaveConversion] = 150
[INSYSPAYROLL_Loans] = 154
[INSYSPAYROLL_Loans_EmployeeAdvances] = 872
[INSYSPAYROLL_Loans_EmployeeAdvancesFile] = 902
[INSYSPAYROLL_Loans_Loan] = 49
[INSYSPAYROLL_Loans_LoanFile] = 932
[INSYSPAYROLL_Loans_LoanFilehardcoded] = 78
[INSYSPAYROLL_MonthlyReports] = 163
[INSYSPAYROLL_PayrollProcessing] = 42
[INSYSPAYROLL_PayrollProcessing_Payroll] = 43
[INSYSPAYROLL_PayrollTransitionFile] = 418
[INSYSPAYROLL_PHICReports] = 273
[INSYSPAYROLL_PHICReports_MonthlyContribution] = 394
[INSYSPAYROLL_PHICReports_MonthlyRemittance] = 395
[INSYSPAYROLL_PHICReports_PhilhealthMonthlyRF1] = 356
[INSYSPAYROLL_RegularReports] = 4
[INSYSPAYROLL_RegularReports_WithholdingTaxReports] = 899
[INSYSPAYROLL_ServiceCharge] = 75
[INSYSPAYROLL_SSSReports] = 271
[INSYSPAYROLL_SSSReports_MonthlyContribution] = 393
[INSYSPAYROLL_SSSReports_MonthlyRemittance] = 392
[INSYSPAYROLL_SSSReports_SSSClaims] = 868
[INSYSPAYROLL_SSSReports_SSSLoan] = 375
    [INSYSPAYROLL_SSSReports_SSSNet] = 70
    [INSYSPAYROLL_AnnualReports_AlphalistTextGenerator] = 983
[INSYSPEOPLE] = 804
[INSYSPEOPLE_EmployeeInfo] = 16
[INSYSPEOPLE_EmployeeRecords201File] = 9
[INSYSPEOPLE_MassAssignment] = 835
[INSYSPEOPLE_Reports] = 805
[INSYSPEOPLE_Reports_BirthdayList] = 198
[INSYSPEOPLE_SalaryAdjustment] = 283
[INSYSPEOPLE_SeparatedEmployees] = 161
[MAINTENANCE] = 366
[MAINTENANCE_ACCOUNTING] = 509
[MAINTENANCE_ACCOUNTING_AccountType] = 512
[MAINTENANCE_ACCOUNTING_NormalBalance] = 514
[MAINTENANCE_ACCOUNTING_Subsidiaries] = 585
[MAINTENANCE_ACCOUNTING_SubsidiaryType] = 513
[MAINTENANCE_INSYSORBIT] = 367
[MAINTENANCE_INSYSORBIT_DailySchedule] = 102
[MAINTENANCE_INSYSORBIT_DayType] = 650
[MAINTENANCE_INSYSORBIT_HourType] = 361
[MAINTENANCE_INSYSORBIT_LeaveConversionType] = 347
[MAINTENANCE_INSYSORBIT_LeaveCreditFiling] = 930
[MAINTENANCE_INSYSORBIT_LeaveCreditFilinghardcoded] = 296
    [MAINTENANCE_INSYSORBIT_LogDevice] = 646
[MAINTENANCE_INSYSORBIT_TardinessRounding] = 298
[MAINTENANCE_INSYSORBIT_WeeklySchedule] = 103
[MAINTENANCE_INSYSPAYROLL] = 52
[MAINTENANCE_INSYSPAYROLL_AdvancesDeductionBracket] = 871
[MAINTENANCE_INSYSPAYROLL_Currency] = 341
[MAINTENANCE_INSYSPAYROLL_Currency_CurrencyRate] = 409
[MAINTENANCE_INSYSPAYROLL_HDMFTable] = 6
[MAINTENANCE_INSYSPAYROLL_Income] = 38
[MAINTENANCE_INSYSPAYROLL_IncomeTaxExemption] = 37
[MAINTENANCE_INSYSPAYROLL_IncomeTaxTable] = 39
[MAINTENANCE_INSYSPAYROLL_IncomeTaxTable_IncomeTaxTableFile] = 635
[MAINTENANCE_INSYSPAYROLL_PayFrequency] = 40
[MAINTENANCE_INSYSPAYROLL_PayScheme] = 173
[MAINTENANCE_INSYSPAYROLL_PayrollItem] = 33
[MAINTENANCE_INSYSPAYROLL_PayrollItem_LeavePayrollItem] = 68
[MAINTENANCE_INSYSPAYROLL_PayrollItem_LoanPayrollItem] = 69
[MAINTENANCE_INSYSPAYROLL_PayrollItem_PayrollItemGroup] = 204
[MAINTENANCE_INSYSPAYROLL_PayrollParameter] = 30
[MAINTENANCE_INSYSPAYROLL_PhilhealthTable] = 7
[MAINTENANCE_INSYSPAYROLL_SSSTable] = 25
[MAINTENANCE_INSYSPAYROLL_TaxExemption] = 803
[MAINTENANCE_INSYSPEOPLE] = 274
    [MAINTENANCE_INSYSPEOPLE_EMPLOYEEINFO] = 929
    [MAINTENANCE_INSYSPEOPLE_EMPLOYEEPHOTOFILEINFO] = 979
[MAINTENANCE_INSYSPEOPLE_EMPLOYEEINFO_BloodType] = 261
[MAINTENANCE_INSYSPEOPLE_EMPLOYEEINFO_Citizenship] = 121
[MAINTENANCE_INSYSPEOPLE_EMPLOYEEINFO_CivilStatus] = 86
[MAINTENANCE_INSYSPEOPLE_EMPLOYEEINFO_EducationAttainmentStatus] = 13
[MAINTENANCE_INSYSPEOPLE_EMPLOYEEINFO_EducationLevel] = 122
[MAINTENANCE_INSYSPEOPLE_EMPLOYEEINFO_Nationality] = 28
[MAINTENANCE_INSYSPEOPLE_EmployeeMovementType] = 658
[MAINTENANCE_INSYSPEOPLE_EmployeeStatus] = 55
[MAINTENANCE_INSYSPEOPLE_LeaveParameter] = 373
[MAINTENANCE_INSYSPEOPLE_LeaveParameterItem] = 374
[MAINTENANCE_INSYSPEOPLE_SalaryIncreaseType] = 284
[MAINTENANCE_ORGANIZATION] = 925
[MAINTENANCE_ORGANIZATION_BranchGroup] = 257
[MAINTENANCE_ORGANIZATION_CompanyGroup] = 258
[MAINTENANCE_ORGANIZATION_CompanyType] = 263
[MAINTENANCE_ORGANIZATION_CostCenterGroup] = 408
[MAINTENANCE_ORGANIZATION_Unit] = 183
[MAINTENANCE_SupplementarySettings] = 5
[MAINTENANCE_SupplementarySettings_CityAddress] = 656
[MAINTENANCE_SupplementarySettings_CityProvince] = 193
[MAINTENANCE_SupplementarySettings_Degree] = 818
[MAINTENANCE_SupplementarySettings_FilingStatus] = 515
[MAINTENANCE_SupplementarySettings_Holiday] = 400
[MAINTENANCE_SupplementarySettings_HolidayX] = 54
[MAINTENANCE_SupplementarySettings_Licenses] = 817
[MAINTENANCE_SupplementarySettings_MedicalHistory] = 135
[MAINTENANCE_SupplementarySettings_Month] = 72
[MAINTENANCE_SupplementarySettings_Period] = 325
[MAINTENANCE_SupplementarySettings_Quarter] = 379
[MAINTENANCE_SupplementarySettings_Reason] = 657
[MAINTENANCE_SupplementarySettings_ReasonforSeparation] = 689
[MAINTENANCE_SupplementarySettings_Region] = 816
[MAINTENANCE_SupplementarySettings_Religion] = 22
[MAINTENANCE_SupplementarySettings_School] = 641
[MAINTENANCE_SupplementarySettings_Skill] = 647
[MEALLOG] = 19
[MEALLOG_MealLog] = 20
[MEALLOG_MealLogByDate] = 35
[MEALLOG_MealLogFile] = 399
[MEALLOG_MealSchedule] = 18
[ORGANIZATIONALMANAGEMENT] = 2
[ORGANIZATIONALMANAGEMENT_Agency] = 292
[ORGANIZATIONALMANAGEMENT_BankAccounts] = 59
[ORGANIZATIONALMANAGEMENT_Branch] = 53
[ORGANIZATIONALMANAGEMENT_CompanyProfile] = 45
[ORGANIZATIONALMANAGEMENT_CostCenter] = 41
[ORGANIZATIONALMANAGEMENT_Department] = 8
[ORGANIZATIONALMANAGEMENT_Division] = 79
[ORGANIZATIONALMANAGEMENT_Position] = 12
[ORGANIZATIONALMANAGEMENT_PositionLevel] = 11
[ORGANIZATIONALMANAGEMENT_Section] = 120
[SYSTEM] = 32
[SYSTEM_Bank] = 427
[SYSTEM_CompanyMenu] = 577
[SYSTEM_DocumentProperties] = 630
[SYSTEM_Menu] = 36
[SYSTEM_Menu_MenuButton] = 655
[SYSTEM_Menu_MenuDetailTab] = 425
[SYSTEM_Menu_MenuDetailTabField] = 844
[SYSTEM_Menu_MenuTab] = 249
[SYSTEM_Menu_MenuTabField] = 629
[SYSTEM_Setting] = 23
[SYSTEM_Setting_SettingGroup] = 439
[SYSTEM_SystemApplication] = 91
[SYSTEM_SystemFileType] = 443
[SYSTEM_SystemImage] = 426
[SYSTEM_SystemMessage] = 334
[SYSTEM_SystemVersion] = 831
[SYSTEM_SystemVersion_SystemVersionMenu] = 843
[SYSTEM_SystemVersion_SystemversionMenuDetailTab] = 842
[SYSTEM_SystemVersion_SystemVersionMenuTab] = 841
[SYSTEM_Tables] = 632
[SYSTEM_Web] = 912
[SYSTEM_Web_WebMenu] = 913
[SYSTEM_Web_WebParameters] = 833
[SYSTEM_Web_WebSystemApplication] = 914
[SYSTEM_Web_WebTypes] = 915
[SYSTEM_Web_WebTypes_ButtonTypes] = 917
[SYSTEM_Web_WebTypes_ControlTypes] = 918
[SYSTEM_Web_WebTypes_FilingTypes] = 924
[SYSTEM_Web_WebTypes_MenuTypes] = 916
[SYSTEM_Web_WebTypes_ShortcutLinks] = 921
[SYSTEM_Web_WebTypes_ShortcutLinks_LinkTypes] = 922
[SYSTEM_Web_WebTypes_SummaryTypes] = 919
    [SYSTEM_Web_WebTypes_Widgets] = 923
    [INSYSORBIT_TimekeepingItems_TimeKeepingFile] = 953
End Enum


Public Enum BankEnum
[AlliedBank] = 7
[AsiaUnitedBank] = 11
[BancodeOroCashCardDerick] = 13
[BancoDeOroSavings] = 4
[BankofthePhilippineIslands] = 9
[EastWestBank] = 1
[EquitablePCI] = 3
[MetroBank] = 6
[PBCom] = 10
[RizalCommercialBankingCorporation] = 8
[SecurityBank] = 12
[UnitedCoconutPlantersBank] = 2
End Enum


Public Enum SettingEnum
[ActivationKey] = 121
[AllowAdmin] = 29
[AllowAllGracePeriod] = 83
[ApplyAddress] = 21
[ArchiveDatabaseName] = 98
[AskHRAddress] = 15
[AuditTrail] = 91
[AutoDetectAttendanceLogType] = 40
[BackUpPath] = 8
[CanteenDuesID] = 75
[Color] = 13
[CommissionID] = 74
[Concept] = 1
[ConceptCode] = 9
[ContriComputation] = 112
[DailyAutomationStartDate] = 48
[DateFormat] = 42
[DateTimeFormat] = 44
[DeductGraceMinutes] = 60
[DefaultCompanyID] = 111
[DefaultOTFilingStatus] = 37
[DeMinimisBenefitID] = 64
[DirectorFeeID] = 71
[ECOLAID] = 73
[EmployeeCode] = 10
[EPayslip] = 115
[ExcelTemplatePath] = 47
[FailedLogInCount] = 107
[FavoritesBGCOlor] = 128
[FilePath] = 81
[FillRegHoursFirst] = 41
[FirstInLastOutOnly] = 54
[FolderImageFile] = 12
[GraceMinutesComputation] = 79
[GraceMinutesSetUp] = 53
[GracePeriodEndMinute] = 51
[GracePeriodFirstInOnly] = 94
[GSISEEID] = 58
[GSISERID] = 59
[HazardPayID] = 65
[HeightUnit] = 45
[HolidayAndRestdayIsForApproval] = 50
[HospitalInsuranceID] = 66
[HousingAllowanceID] = 69
[HRInfoAddress] = 30
[HRTardyGracePeriod] = 82
[IconHeight] = 3
[IconWidth] = 2
[ID_Admin] = 22
[ID_CorpTraining] = 27
[ID_Employee] = 26
[ID_Manager] = 28
[ID_Managerial] = 19
[ID_Public] = 18
[ID_SuperUser] = 17
[InfoFormMaximized] = 97
[IsEncrypted] = 100
[IsRetail] = 113
[IsSimplePassword] = 114
[IsWebUnderMaintenance] = 137
[Licensing] = 120
[LoginBG] = 122
[LoginBGColor] = 129
[LoginButtonForeColor] = 132
[LoginForeColor] = 130
[LoginStatusBGColor] = 131
[LoginStatusForeColor] = 133
[MacAddress] = 117
[MainBG] = 123
[MainBGColor] = 124
[MainTreeBGColor] = 134
[MaxConnection] = 116
[MaxEmployeeCount] = 136
[MenuBGColor] = 125
[MinEmployeeCount] = 135
[MinExpandedAmount] = 140
[MinOTHours] = 63
[NewlyHiredPerfectAttendance] = 35
[NonTaxableLeaveDays] = 109
[OTRoundingFactor] = 49
[PasswordExpirationDays] = 108
[PasswordHistoryValidationCount] = 106
[PasswordLength] = 105
[PayslipSender] = 101
[PayslipSenderDeliveryMethod] = 104
[PayslipSenderPassword] = 103
[PayslipSenderSMTP] = 102
[PhotoPath] = 6
[Port] = 87
[ProductKey] = 118
[ProductRegistered] = 119
[ProfitSharingID] = 70
[ReadOnlyFieldBackColor] = 80
[ReportPath] = 4
[RepresentationID] = 67
[RequiredFieldBackColor] = 11
[Reserved01] = 38
[Reserved02] = 24
[RESOURCEUPDATE] = 138
[RESOURCEUPDATEPATH] = 139
[ResourcePath] = 5
[SearchMode] = 89
[ServiceChargeID] = 16
[SingleCompanyOnly] = 110
[SLConversionID] = 77
[SMTPServer] = 25
[SSLEnabled] = 88
[StartPage] = 20
[StatusBGColor] = 126
[StatusForeColor] = 127
[StyleSheetPath] = 7
[TardinessID] = 33
[TardyHoursAsHalfDay] = 52
[TardyRoundingFactor] = 57
[Taxable13thMonth] = 95
[TaxableDeMinimis] = 96
[TaxRefundID] = 72
[TimeFormat] = 43
[TimeInAllowanceMinutes] = 31
[TransportationAllowanceID] = 68
[UndertimeID] = 32
[UseAlert] = 55
[UseAuditTrail] = 84
[UseHoursAndMinutesFormat] = 23
[UseLastEmployeeDailyScheduleAsDefault] = 39
[UseLoanPriority] = 34
[UseMonochrome] = 14
[UseNonWorkingDaySetUp] = 78
[UTHoursAsHalfDay] = 62
[UTRoundingFactor] = 36
[VLConversionID] = 76
[WeightUnit] = 46
End Enum


Public Enum SystemControlTypeEnum
[CheckBox] = 3
[ComboBox] = 2
[DataColor] = 10
[DataDate] = 11
[DataImage] = 7
[DataLookUp] = 4
[DataTime] = 12
[DockedTextBox] = 5
[EmailTextBox] = 6
[FileUpload] = 16
[GSTextBox] = 9
[HiddenField] = 15
[LabelBox] = 14
[LinkButton] = 13
[MaskedTextBox] = 8
[TextBox] = 1
End Enum


Public Enum MenuDetailTabTypeEnum
[Grid] = 1
[List] = 3
[Report] = 4
    [TreeView] = 2
    [Form] = 5
End Enum


Public Enum AuditTrailTypeEnum
[AddedRecord] = 3
[DeletedRecord] = 5
[Field] = 6
[ModifiedRecord] = 4
[Save] = 2
[Session] = 1
[Table] = 7
End Enum
