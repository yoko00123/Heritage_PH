

Option Explicit On
Option Strict On



Friend Class EmployeeMovementInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tEmployeeMovement(Connection)
    Private mControl As New InSys.DataControl
    Private mApplyButton As ToolStripButton

    Private WithEvents _ID_Company As GSCOM.UI.DataLookUp.DataLookUp

    Private WithEvents _ID_Branch As GSCOM.UI.DataLookUp.DataLookUp
    Private WithEvents _ID_Designation As GSCOM.UI.DataLookUp.DataLookUp
    Private WithEvents _ID_CostCenter As GSCOM.UI.DataLookUp.DataLookUp
    Private WithEvents _ID_Department As GSCOM.UI.DataLookUp.DataLookUp
    Private WithEvents _ID_Employee As GSCOM.UI.DataLookUp.DataLookUp
    Private WithEvents _ID_BenefitSuite As GSCOM.UI.DataLookUp.DataLookUp
    Private WithEvents _ID_EmployeeStatus As ComboBox
    Private WithEvents _MonthlyRate As TextBox
    Private _MonthlyRateText As Label

    Dim b As Boolean = False





    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
        End With
        InitControl(pMenu)

        'mf
        '  myDT.Columns(Database.Tables.tEmployeeMovement.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        'Andrew
        'mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("misc.a.ico"), AddressOf ApplyFile)
        AfterNew()
        _ID_Company = CType(Me.GetControl("_ID_Company"), GSCOM.UI.DataLookUp.DataLookUp)
        _ID_Branch = CType(Me.GetControl("_ID_Branch"), GSCOM.UI.DataLookUp.DataLookUp)
        _ID_CostCenter = CType(Me.GetControl("_ID_CostCenter"), GSCOM.UI.DataLookUp.DataLookUp)
        _ID_Department = CType(Me.GetControl("_ID_Department"), GSCOM.UI.DataLookUp.DataLookUp)
        _ID_Designation = CType(Me.GetControl("_ID_Designation"), GSCOM.UI.DataLookUp.DataLookUp)
        _ID_Employee = CType(Me.GetControl("_ID_Employee"), GSCOM.UI.DataLookUp.DataLookUp)

        _ID_BenefitSuite = CType(Me.GetControl("ID_BenefitSuite"), GSCOM.UI.DataLookUp.DataLookUp)
        _ID_EmployeeStatus = CType(Me.GetControl("_ID_EmployeeStatus"), ComboBox)
        _MonthlyRate = CType(Me.GetControl("_MonthlyRate"), TextBox)
        _MonthlyRateText = CType(Me.GetControl("Monthly Rate"), Label)


        'AddHandler _ID_Branch.DroppingDown, AddressOf CheckCompanyDroppingDown
        'AddHandler _ID_CostCenter.DroppingDown, AddressOf CheckCompanyDroppingDown

        'AddHandler _ID_Department.DroppingDown, AddressOf CheckCompanyDroppingDown
        'AddHandler _ID_Designation.DroppingDown, AddressOf CheckCompanyDroppingDown

        b = CBool(GSCOM.SQL.ExecuteScalar("SELECT CanViewEmployeeSalary FROM tUserGroup ug INNER JOIN tUser u on ug.id=u.ID_UserGroup WHERE u.ID=" & nDB.GetUserID.ToString, Connection))
        If Not b Then
            _MonthlyRate.Text = "0.00"
            _MonthlyRate.Enabled = False
        End If



    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeMovement)
        End Set
    End Property

    'Protected Overrides Property Control() As Control
    '    Get
    '        Return mControl
    '    End Get
    '    Set(ByVal value As Control)
    '        mControl = CType(value, InSys.DataControl)
    '    End Set
    'End Property


    Private Sub ApplyFile(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pUpdateFromEmployeeMovement " & myDT.Get(Database.Tables.tEmployeeMovement.Field.ID).ToString, Connection)
            MsgBox("Finished applying the file.", MsgBoxStyle.Information)
        End If
    End Sub


#Region "DropDown"
    Private Sub CheckCompanyDroppingDown(ByVal sender As Object, ByVal e As GSCOM.UI.DataLookUp.DroppingDownEventArgs)
        If Not _ID_Company.Worker.HasData Then
            MsgBox("Must select a company first", MsgBoxStyle.Information)
            e.Cancel = True
        End If
    End Sub


#End Region

#Region "SelectedValueChanged"

    Private Sub _ID_Company_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles _ID_Company.SelectedValueChanged Andrew 20110517
        Dim s As String
        Try
            s = _ID_Company.Worker.GetFilter("ID_Company")
            If _ID_Branch IsNot Nothing Then _ID_Branch.Worker.ValidateEntry(s, False)
            If _ID_CostCenter IsNot Nothing Then _ID_CostCenter.Worker.ValidateEntry(s, False)
            If _ID_Department IsNot Nothing Then _ID_Department.Worker.ValidateEntry(s, False)
            If _ID_Designation IsNot Nothing Then _ID_Designation.Worker.ValidateEntry(s, False)
        Catch ex As Exception
        End Try

    End Sub




    Private Sub _ID_Employee_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ID_Employee.SelectedValueChanged
        Try
            If CInt(_ID_Employee.SelectedValue.ToString) > 0 Then
                _ID_Company.SelectedValue = GetSingleValue("SELECT ID_Company FROM temployee WHERE ID = " & _ID_Employee.SelectedValue.ToString)
                _ID_Branch.SelectedValue = GetSingleValue("SELECT ID_Branch FROM temployee WHERE ID = " & _ID_Employee.SelectedValue.ToString)
                _ID_CostCenter.SelectedValue = GetSingleValue("SELECT ID_CostCenter FROM temployee WHERE ID = " & _ID_Employee.SelectedValue.ToString)
                _ID_Department.SelectedValue = GetSingleValue("SELECT ID_Department FROM temployee WHERE ID = " & _ID_Employee.SelectedValue.ToString)
                _ID_Designation.SelectedValue = GetSingleValue("SELECT ID_Designation FROM temployee WHERE ID = " & _ID_Employee.SelectedValue.ToString)
                _ID_EmployeeStatus.SelectedValue = GetSingleValue("SELECT ID_EmployeeStatus FROM temployee WHERE ID = " & _ID_Employee.SelectedValue.ToString)
                If Not b Then
                    _MonthlyRate.Text = "0.00"
                    _MonthlyRate.Enabled = False
                Else
                    _MonthlyRate.Text = GetSingleValue("SELECT MonthlyRate FROM temployee WHERE ID = " & _ID_Employee.SelectedValue.ToString)
                End If

                If Not IsNothing(GetSingleValue("SELECT ID_BenefitSuite FROM temployee WHERE ID = " & _ID_Employee.SelectedValue.ToString)) Then
                    _ID_BenefitSuite.SelectedValue = GetSingleValue("SELECT ID_BenefitSuite FROM temployee WHERE ID = " & _ID_Employee.SelectedValue.ToString)
                End If

            End If
        Catch ex As Exception

        End Try


        'dim i as 
    End Sub

#End Region






End Class



