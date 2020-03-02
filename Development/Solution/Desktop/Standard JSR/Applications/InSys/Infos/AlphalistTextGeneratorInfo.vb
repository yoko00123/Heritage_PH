Option Explicit On
Option Strict On


Friend Class AlphalistTextGeneratorInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tAlphalistTExtGen(Connection)
    Private mGenerateFileAllCompanyButton As ToolStripButton
    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
        End With
        Dim pdc As DataColumn
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tAlphalistTExtGen.Field.ID)
        


        mGenerateFileAllCompanyButton = MyBase.AddButton("Generate File", gMainForm.imgList.Images("GenerateFile.png"), AddressOf Generate_TextFileAll)
        Me.GetStripButton("Generate File")
        AddHandler mGenerateFileAllCompanyButton.Click, AddressOf Generate_TextFileAll
        Me.ReloadAfterCommit = True
        AfterNew()
    End Sub
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        mGenerateFileAllCompanyButton.Enabled = Not pID = 0
        MyBase.LoadInfo(pID)
    End Sub
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tAlphalistTExtGen)
        End Set
    End Property
    Private Sub Generate_TextFileAll(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()
        Dim ExportFileName As String

        ExportFileName = GSCOM.SQL.ExecuteScalar("SELECT REPLICATE('0',9-LEN(ISNULL(LEFT(REPLACE(c.TIN,'-',''),9),'') )) + ISNULL(LEFT(REPLACE(c.TIN,'-',''),9),'') + REPLICATE('0', 4 - (LEN(LEFT(ISNULL(c.BranchCode,''),3)))) +LEFT(ISNULL(c.BranchCode,''),3)  +CAST(DATEPART(mm,a.ReturnDAte) AS VARCHAR(4)) +CAST(DATEPART(dd,a.ReturnDAte) AS VARCHAR(4)) +CAST(DATEPART(yyyy,a.ReturnDAte) AS VARCHAR(6)) + '1604CF' FROM tCompany c INNER JOIn tAlphalist a ON a.ID_Company = c.ID WHERE a.ID =" & myDT.Get(Database.Tables.tAlphalistTExtGen.Field.ID).ToString, Connection).ToString

        MyDialog.FileName = ExportFileName

        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            Save_FileAll(MyDialog.FileName)
        End If
    End Sub
    Private Sub Save_FileAll(ByVal FileName As String)
        Dim fnum As Integer
   
        Dim vNow As Date = nDB.GetServerDate
        Try
            If (Not (myDT Is Nothing)) Then
                If myDT.Rows.Count > 0 Then
                    fnum = FreeFile()
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)

                    FileSystem.Print(fnum, Me.GetTextAll(CInt(myDT.Get(Database.Tables.tAlphalistTExtGen.Field.ID)), gConnection))

                    MsgBox("File has been exported successfully.", MsgBoxStyle.Information)
                Else
                    MsgBox("No record found.", MsgBoxStyle.Information)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            FileClose(fnum)
        End Try
    End Sub
    Public Overloads Function GetTextAll(ByVal pID As Integer, ByVal gConnection As SqlClient.SqlConnection) As String
        Dim s As String = ""
        Dim dt As New DataTable

        GSCOM.SQL.FillTable(dt, "EXEC pGenerateTextFileAlphalist_AllCompany " & pID.ToString, gConnection)
        For Each dr As DataRow In dt.Rows
            s &= dr(0).ToString & vbCrLf
        Next
        s = s.Trim
        Return s
    End Function
End Class
