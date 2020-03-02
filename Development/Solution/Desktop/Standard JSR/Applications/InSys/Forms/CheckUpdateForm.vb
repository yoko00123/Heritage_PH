Imports z.Web.Service
Imports System.Threading
Imports GSCOM.Applications.InSys.Database
Imports System.Collections.Generic
Imports System.IO

Public Class CheckUpdateForm

    Private Const strini As String = "InSysUpdate.ini"
    Private AppName As String
    Private AppVersion As String
    Private AppBuild As Int32

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CheckLogUpdate()

    End Sub

    Private Sub CheckUpdateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.lblProduct.Text = AppName
        Me.lblVersion.Text = AppVersion
        Me.lblBuild.Text = CStr(AppBuild)

        Dim thrd As New Thread(Sub() CheckUpdate())
        thrd.IsBackground = True
        thrd.Start()
    End Sub

    Sub CheckLogUpdate()
        Try
            Dim pth As String = Path.Combine(Environment.CurrentDirectory, strini)
            If File.Exists(pth) Then
                Dim k() As String = File.ReadAllLines(pth)
                For Each j As String In k
                    Dim l() As String = j.Split(CChar("="))
                    Select Case l(0)
                        Case "AppName" : AppName = l(1)
                        Case "AppVersion" : AppVersion = l(1)
                        Case "AppBuild" : AppBuild = CInt(l(1))
                    End Select
                Next
            Else
                AppName = Application.ProductName
                AppVersion = Application.ProductVersion
                AppBuild = 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CheckUpdate()
        Try
            Progress(0, 10)
            Progress(1)
            Status("Checking Connection")

            If nDB.nGlobal.GetNetPath = "" Then Throw New Exception("Please Specify Resource Path from Settings or Contact your system administrator")

            Using srvce As New WebClient(nDB.nGlobal.GetNetPath, "UPDATE")
                Progress(2)
                Status("Checking Network Availability")
                srvce.Connect()

                Progress(4)
                Status("Checking Product Updates")

                Dim dc As New Dictionary(Of String, Object)
                dc.Add("app", AppName)
                dc.Add("version", AppVersion)
                dc.Add("curupdate", AppBuild)
                Dim rs As Result = srvce.Get("Check", dc)
                Select Case rs.Status
                    Case 0
                        Status(String.Format("{0} is available", rs.ResultSet))
                        Progress(10)
                        Me.Invoke(New MethodInvoker(Sub() Me.btnStartUpdate.Visible = True))
                    Case 2
                        Status("You already have the latest build")
                        Progress(10)
                End Select
            End Using
        Catch ex As Exception
            Status(ex.Message)
            Progress(10)
            MsgBox(ex.Message)
        End Try
    End Sub

    Delegate Sub dgStatus(msg As String)
    Sub Status(msg As String)
        If lblStatus.InvokeRequired Then
            lblStatus.Invoke(New dgStatus(AddressOf Status), msg)
        Else
            lblStatus.Text = msg
        End If
    End Sub

    Delegate Sub dgProgress(i As Int32, max As Int32)
    Sub Progress(i As Int32, Optional max As Int32 = 0)
        If InvokeRequired Then
            Invoke(New dgProgress(AddressOf Progress), i, max)
        Else
            If max > 0 Then
                pbar.Value = 0
                pbar.Maximum = max
            Else
                pbar.Value = i
            End If
        End If
    End Sub

    Delegate Sub dgShowUpdate(str As String)
    Sub ShowNewUpdate(str As String)
        If Me.InvokeRequired Then
            Me.Invoke(New dgShowUpdate(AddressOf ShowNewUpdate), str)
        Else
            Me.lblBuild.Text = String.Format("Current: {0} - New: {1}", AppBuild, str)
            Me.btnStartUpdate.Visible = True
        End If
    End Sub

    Private Sub btnCheckUlit_Click(sender As Object, e As EventArgs) Handles btnCheckUlit.Click
        Dim thrd As New Thread(Sub() CheckUpdate())
        thrd.IsBackground = True
        thrd.Start()
    End Sub

    Private Sub btnStartUpdate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles btnStartUpdate.LinkClicked
        Using prc As New Process()
            prc.StartInfo.FileName = Path.Combine(Environment.CurrentDirectory, "GSCOM.Applications.InSysPatcher.exe")
            prc.StartInfo.Arguments = String.Format("-n""{0}"" -v""{1}"" -b""{2}"" -p""{3}"" -s""{4}""",
                                                        AppName,
                                                        AppVersion,
                                                        AppBuild,
                                                        System.Reflection.Assembly.GetExecutingAssembly().Location,
                                                        nDB.nGlobal.GetNetPath
                                                        )
            prc.Start()
            System.Diagnostics.Process.GetCurrentProcess.Kill()
        End Using
    End Sub

End Class