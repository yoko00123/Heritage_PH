Option Explicit On
Option Strict On

Imports System.Collections.Generic
Imports System.Linq

Friend Class InfoForm

#Region "Declarations"
    'Private WithEvents mControl As Control
    Public Event PrintButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event NewButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event SaveButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event RefreshButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event PlotValuesButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event EmailButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event PropertyButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event CreateCopyButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event GenerateXMLFileButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event ImportXMLFileButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)

#End Region

#Region "Constructors"
    Public Sub New()
        InitializeComponent()
        mStatusLabel.Image = My.Resources.BlueBall
        Me.NewButton.Image = GSCOM.UI.Common.NewButtonImage
        Me.SaveButton.Image = GSCOM.UI.Common.SaveButtonImage
        Me.RefreshButton.Image = GSCOM.UI.Common.RequeryButtonImage
        Me.PrintButton.Image = GSCOM.UI.Common.PrintButtonImage
        Me.HeaderButton.Image = GSCOM.UI.Common.HeaderButtonImage
        Me.EmailButton.Image = GSCOM.UI.Common.EmailButtonImage
        Me.PlotValuesButton.Image = My.Resources.Menu
        Me.KeyPreview = True
        Dim ws As Boolean = False

        If CBool(nDB.GetSetting(Database.SettingEnum.InfoFormMaximized)) Then
            ws = True
        End If


        If ws Then
            Me.WindowState = FormWindowState.Maximized
        End If
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub

#End Region

#Region "Navigate"
    'Public Sub Navigate(ByVal mDS As DataSet, ByVal pStyleSheetPath As String)
    '    Try
    '        Dim f As String
    '        Dim t As String
    '        Dim xw As Xml.XmlWriter
    '        Dim xs As New Xml.XmlWriterSettings()
    '        xs.Indent = True
    '        f = IO.Path.GetTempFileName()
    '        xw = Xml.XmlWriter.Create(f, xs)
    '        t = "type=""text/xsl"" href=""" & pStyleSheetPath & """"
    '        xw.WriteProcessingInstruction("xml-stylesheet", t)
    '        mDS.WriteXml(xw)
    '        Me.Browser.Navigate(f)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    '20061105
    'Public Sub Navigate(ByVal urlString As String)
    '    Dim bf As New BrowserForm

    '    Me.Browser.Navigate(urlString)
    'End Sub

    Public Sub SetDocumentText(ByVal pText As String)
        'Me.Browser.DocumentText = pText
    End Sub

    Public Sub Navigate(ByVal mDS As DataSet, ByVal pStyleSheetPath As String, ByVal pArgumentList As System.Xml.Xsl.XsltArgumentList)
        'Try
        '    If mDS.Tables(0).Rows(0).RowState = DataRowState.Added Then
        '        Me.Browser.Navigate("")
        '    Else
        '        Dim f As String = IO.Path.GetTempFileName()
        '        Dim sb As New System.Text.StringBuilder
        '        Dim newxml As IO.StringWriter
        '        Dim doc As New Xml.XmlDataDocument()
        '        Dim xslt As New Xml.Xsl.XslCompiledTransform
        '        Dim w As Xml.XmlWriter
        '        w = Xml.XmlWriter.Create(sb)
        '        mDS.WriteXml(w)
        '        doc.LoadXml(Replace(sb.ToString, "+08:00", ""))
        '        sb = New System.Text.StringBuilder
        '        newxml = New IO.StringWriter(sb)
        '        xslt.Load(pStyleSheetPath)
        '        xslt.Transform(doc, pArgumentList, newxml)
        '        doc = Nothing
        '        GSCOM.Common.FileFromString(sb.ToString, f)
        '        Me.Browser.Navigate(f)
        '    End If
        'Catch ex As Exception
        '    'do not throw exception
        'End Try


    End Sub


#End Region

#Region "ToggleHeaderVisible"
    Private Sub ToggleHeaderVisible()
        HeaderButton.Checked = Not HeaderButton.Checked
        Me.spcMain.Panel1Collapsed = Not HeaderButton.Checked
    End Sub

#End Region

    Public Sub SetDetailStatusLabelText(ByVal pTabPageText As String, ByVal pLabelText As String)
        Dim tp As TabPage
        tp = Me.GetTabPage(pTabPageText)
        If tp IsNot Nothing Then
            If tp.Controls.Count >= 2 Then 'unkwon error
                'CType(tp.Controls(2), StatusStrip).Items(0).Text = pLabelText
                With CType(tp.Controls(1), ToolStrip).Items(1)
                    .Text = " (" & pLabelText & ")"
                End With

            End If
        End If
    End Sub

    'Private mReadOnly As Boolean = False
    'Public Property [ReadOnly]() As Boolean
    '    Get

    '    End Get
    '    Set(ByVal value As Boolean)

    '    End Set
    'End Property

    Public Sub MakeReadOnly()
        For Each a As System.Windows.Forms.ToolStripItem In tsMain.Items
            If TypeOf a Is ToolStripButton Then
                If (a IsNot PrintButton) And (a IsNot RefreshButton) Then
                    a.Visible = False
                End If
            End If
        Next
        Dim cc() As System.Windows.Forms.Control = Nothing
        GSCOM.UI.AllControls(cc, Me)
        For Each c As Control In cc
            If TypeOf c Is DataGridView Then
                Dim o As DataGridView
                o = CType(c, DataGridView)
                o.ReadOnly = True
            ElseIf TypeOf c Is TextBox Then
                Dim o As TextBox
                o = CType(c, TextBox)
                o.ReadOnly = True
                'ElseIf TypeOf c Is masTextBox Then
                '    Dim o As textbox
                '    o = CType(c, textbox)
                'o.ReadOnly = True
            ElseIf TypeOf c Is ComboBox _
                    Or TypeOf c Is CheckBox _
                    Or TypeOf c Is GSCOM.UI.DataLookUp.DataLookUp _
                    Or TypeOf c Is GSCOM.UI.DataImage.DataImage _
                    Then
                c.Enabled = False
            End If
        Next
    End Sub

    Public Sub ToggleReadOnly(ByVal pReadOnly As Boolean)
        For Each a As System.Windows.Forms.ToolStripItem In tsMain.Items
            If TypeOf a Is ToolStripButton Then
                If (a IsNot PrintButton) And (a IsNot RefreshButton) Then
                    a.Visible = False
                End If
            End If
        Next
        Dim cc() As System.Windows.Forms.Control = Nothing
        GSCOM.UI.AllControls(cc, Me)
        For Each c As Control In cc
            If TypeOf c Is DataGridView Then
                Dim o As DataGridView
                o = CType(c, DataGridView)
                o.ReadOnly = True
            ElseIf TypeOf c Is TextBox Then
                Dim o As TextBox
                o = CType(c, TextBox)
                o.ReadOnly = True
            ElseIf TypeOf c Is ComboBox _
      Or TypeOf c Is CheckBox _
      Or TypeOf c Is GSCOM.UI.DataLookUp.DataLookUp _
      Or TypeOf c Is GSCOM.UI.DataImage.DataImage _
      Then
                c.Enabled = False
            End If
        Next
    End Sub

    Public Sub EnableExtraButtons(Optional ByVal pEnable As Boolean = True)
        For Each a As System.Windows.Forms.ToolStripItem In tsMain.Items
            If TypeOf a Is ToolStripButton Then
                If _
                (a IsNot NewButton) _
                And (a IsNot SaveButton) _
                And (a IsNot PrintButton) _
                And (a IsNot RefreshButton) _
                And (a IsNot HeaderButton) _
                And (a IsNot TranslucentButton) _
                Then
                    a.Enabled = pEnable
                End If
            End If
        Next
    End Sub

    Public Sub HideNewAndSaveButtons()
        'For Each a As System.Windows.Forms.ToolStripItem In tsMain.Items
        ' If TypeOf a Is ToolStripButton Then
        ' If (a Is NewButton) Or (a Is SaveButton) Then
        ' a.Visible = False
        ' End If
        'End If
        'Next
        NewButton.Visible = False
        SaveButton.Visible = False
    End Sub


    'Public Sub SetStatusLabelText(ByVal pTabPageText As String, ByVal pLabelText As String)
    '    Dim tp As TabPage
    '    tp = Me.GetDataGridView(pTabPageText)
    '    tp.Controls(1).Controls(0).Text = pLabelText

    'End Sub

    'Public Sub AddDataGridView(ByVal dgv As DataGridView, ByVal dt As DataTable)
    '    Dim tp As TabPage
    '    tp = New TabPage(Strings.Right(Replace(dt.TableName, "_", " "), dt.TableName.Length - 1))
    '    dgv.DataSource = dt
    '    tp.Controls.Add(dgv)
    '    Me.AddTabPage(tp)
    '    '20070321 must be after the grid and tabpage is added
    '    'AddHandler dt.DefaultView.ListChanged, AddressOf ListChanged
    '    ' UpdateFilteredRowCount(dt.DefaultView)
    'End Sub

#Region "AddTabPage"
    Public Sub AddTabPage(ByVal pTabPage As TabPage)
        'Dim a As New Label
        Dim a As New ToolStrip
        a.ImageList = gImageList
        a.Stretch = True
        a.GripStyle = ToolStripGripStyle.Hidden
        a.Font = New Font("Verdana", 8)
        a.Dock = DockStyle.Top

        Dim l As New ToolStripStatusLabel(pTabPage.Text.ToUpper)
        'l.TextAlign = ContentAlignment.MiddleCenter
        l.Alignment = ToolStripItemAlignment.Left
        'l.Font = New Font(l.Font, FontStyle.Bold)
        a.Items.Add(l)

        Dim sl As New ToolStripStatusLabel()
        With sl
            .Spring = True
            a.Items.Add(sl)
        End With


        'Dim b As New StatusStrip
        'b.Font = New Font("Verdana", 8, FontStyle.Regular)
        'b.Height = 16
        'b.Dock = DockStyle.Bottom
        'Dim StatusLabel As New ToolStripStatusLabel
        'b.Items.Add(StatusLabel)

        pTabPage.Controls.Add(a)
        ' pTabPage.Controls.Add(b)


        pTabPage.Tag = a
        AddHandler pTabPage.TextChanged, AddressOf TabPage_TextChanged
        tcMain.TabPages.Add(pTabPage)


    End Sub

    Private Sub TabPage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tp As TabPage
        tp = TryCast(sender, TabPage)
        If tp IsNot Nothing Then
            Dim lb As Label
            lb = TryCast(tp.Tag, Label)
            If lb IsNot Nothing Then
                lb.Text = tp.Text
            End If
        End If

    End Sub

    ''' <summary>
    ''' LJ 20140526
    ''' </summary>
    ''' <param name="orderbyascending"></param>
    ''' <remarks></remarks>
    Public Sub RearrangeTab(Optional orderbyascending As Boolean = True)
        Dim tp As New List(Of TabPage)
        For Each t As TabPage In tcMain.TabPages
            tp.Add(t)
        Next

        tcMain.TabPages.Clear()

        Dim k As List(Of TabPage)
        If orderbyascending Then
            k = (From j As TabPage In tp
              Order By j.Text Ascending).ToList()
        Else
            k = (From j As TabPage In tp
              Order By j.Text Descending).ToList()
        End If
      
        For Each j As TabPage In k
            tcMain.TabPages.Add(j)
        Next

    End Sub

#End Region

#Region "EndEdit"
    Public Sub EndEdit()
        tsMain.Focus()
        'mControl.Focus()
        BasicTab.Focus()
        tcMain.Focus()
        tsMain.Focus()
    End Sub

#End Region

#Region "AddStripButton"
    Public Function AddStripButton(ByVal text As String, ByVal image As System.Drawing.Image, ByVal onClick As System.EventHandler) As System.Windows.Forms.ToolStripButton
        Dim a As System.Windows.Forms.ToolStripButton
        a = New System.Windows.Forms.ToolStripButton(text, image, onClick)
        tsMain.Items.Add(a)
        Return a
    End Function

#End Region

    Public Function GetSaveButton() As ToolStripButton
        Return SaveButton
    End Function

#Region "GetStripButton"
    Public Function GetStripButton(ByVal text As String) As System.Windows.Forms.ToolStripButton
        For Each a As System.Windows.Forms.ToolStripItem In tsMain.Items
            If TypeOf a Is ToolStripButton Then
                If a.Text = text Then
                    Return CType(a, ToolStripButton)
                End If
            End If
        Next
        Dim cc As Control() = Nothing
        GSCOM.UI.AllControls(cc, Me)
        For Each c As Control In cc
            If TypeOf c Is ToolStrip Then


                For Each a As System.Windows.Forms.ToolStripItem In DirectCast(c, ToolStrip).Items
                    If TypeOf a Is ToolStripButton Then
                        If a.Text = text Then
                            Return CType(a, ToolStripButton)
                        End If
                    End If
                Next

            End If
        Next
        Return Nothing
    End Function

#End Region

    Public Sub SelectTab(ByVal pText As String)
        Me.tcMain.SelectTab(Me.GetTabPage(pText))
    End Sub

    Public Sub ChangeTabPageText(ByVal pText As String, ByVal pNewText As String)
        For Each tp As TabPage In tcMain.TabPages
            If tp.Text = pText Then
                tp.Text = pNewText
            End If
        Next
    End Sub

#Region "GetDataGridView"
    Public Function GetDataGridView(ByVal pTable As DataTable) As DataGridView
        Dim dgv As DataGridView
        Dim cc() As Control = Nothing
        GSCOM.UI.AllControls(cc, Me)
        For Each c As Control In cc
            If TypeOf c Is DataGridView Then
                dgv = CType(c, DataGridView)
                If dgv.DataSource Is pTable Then
                    Return dgv
                End If
            End If
        Next
        Return Nothing
    End Function

    Public Function GetControl(ByVal pName As String) As System.Windows.Forms.Control
        Dim cc() As Control = Nothing
        GSCOM.UI.AllControls(cc, Me)
        For Each c As Control In cc
            If c.Name = pName Then
                Return c
            End If
        Next
        Return Nothing
    End Function

#End Region

#Region "GetDataGridView"
    Public Function GetTabPage(ByVal pText As String) As TabPage
        Dim tp As TabPage
        Dim cc() As Control = Nothing
        GSCOM.UI.AllControls(cc, Me)
        For Each c As Control In cc
            If TypeOf c Is TabPage Then
                tp = CType(c, TabPage)
                If tp.Text = pText Then
                    Return tp
                End If
            End If
        Next
        Return Nothing
    End Function

#End Region

    Public Function GetTabPage(ByVal pControl As Control) As TabPage
        Dim tp As TabPage
        Dim cc() As Control = Nothing
        GSCOM.UI.AllControls(cc, Me)
        For Each c As Control In cc
            If TypeOf c Is TabPage Then
                tp = CType(c, TabPage)
                If tp.Controls.Contains(pControl) Then
                    Return tp
                End If
            End If
        Next
        Return Nothing
    End Function



#Region "CancelEdit"
    Public Sub CancelEdit()
        Dim dgv As DataGridView
        Dim cc() As Control = Nothing
        Try
            GSCOM.UI.AllControls(cc, Me)
            For Each c As Control In cc
                If TypeOf c Is DataGridView Then
                    dgv = CType(c, DataGridView)
                    'this is used to handle the error in DataGridView. 
                    'an InvalidOperation exception (about SetCurrentCell)is generated when you close the window while the grid is in editting mode
                    dgv.CancelEdit()
                    dgv.EndEdit()
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


#Region "Events"
    'Private Sub Browser_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Browser.PreviewKeyDown
    '    If e.KeyCode = Keys.Escape Then
    '        Me.Close()
    '    End If
    'End Sub

    Private Sub NewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton.Click
        RaiseEvent NewButtonClicked(sender, e)
    End Sub

    Private Sub PrintButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintButton.Click
        'Browser.ShowPrintPreviewDialog()
        RaiseEvent PrintButtonClicked(sender, e)
    End Sub

    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        RaiseEvent SaveButtonClicked(sender, e)
    End Sub

    Private Sub RefreshButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshButton.Click
        RaiseEvent RefreshButtonClicked(sender, e)
    End Sub

    '#Region "ClickButton"
    '        Private Sub ClickButton(ByVal sender As ToolStripButton)
    '            'ROBBIE NOTE: this function should only raise the event ButtonClicked..
    '            '.. only if the user is able to manually click the button 
    '            Dim vID As Integer
    '            Try
    '                If sender.Enabled AndAlso sender.Visible Then
    '                    Select Case sender.Text
    '                        Case RequeryButton.Text
    '                            FillDatabase(True, False)
    '                    End Select
    '                    If MainView.HasSelectedRow Then
    '                        vID = CInt(MainView.SelectedRowColumnValue("ID"))
    '                    End If
    '                    Dim e As New ButtonClickEventArgs
    '                    With e
    '                        .ButtonText = sender.Text
    '                        .ListCaption = MyCaption.Text
    '                        .SelectedID = vID
    '                    End With
    '                    RaiseEvent ButtonClick(Me, e)
    '                End If
    '            Catch ex As Exception
    '                MsgBox(ex.Message)
    '            End Try
    '        End Sub



    '#End Region


    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HeaderButton.Click
        ToggleHeaderVisible()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TranslucentButton.Click
        If TranslucentButton.Checked Then
            Me.Opacity = 0.75
        Else
            Me.Opacity = 1
        End If
    End Sub

    Private mKeyDownIsHandledInPreview As Boolean
    Private Sub InfoForm_PrevewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown, tcMain.PreviewKeyDown, tsMain.PreviewKeyDown, spcMain.PreviewKeyDown ', mControl.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            mKeyDownIsHandledInPreview = True
        Else
            mKeyDownIsHandledInPreview = False
        End If
    End Sub

    Private Sub InfoForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            If Not mKeyDownIsHandledInPreview Then
                mKeyDownIsHandledInPreview = False
                Me.Close()
            Else
                mKeyDownIsHandledInPreview = False
            End If
        Else
            mKeyDownIsHandledInPreview = False
        End If
    End Sub

#End Region

#Region "BeginProcess"
    Public Sub BeginProcess(Optional ByVal vMessage As String = "")
        Me.Cursor = Cursors.WaitCursor
        mStatusLabel.Text = vMessage & ".."
        mStatusLabel.Image = My.Resources.GreenBall
        Application.DoEvents()
    End Sub

#End Region

    Public Sub SetStatusLabel(Optional ByVal vMessage As String = "")
        mStatusLabel.Text = vMessage
        Application.DoEvents()
    End Sub


#Region "EndProcess"
    Public Sub EndProcess(Optional ByVal vMessage As String = "", Optional ByVal vGood As Boolean = True)
        If vMessage <> "" Then
            Dim s As MsgBoxStyle
            s = CType(IIf(vGood, MsgBoxStyle.Information, MsgBoxStyle.Exclamation), MsgBoxStyle)
            If Not vGood Then
                MsgBox(vMessage, s)
            End If
        End If
        mStatusLabel.Text = vMessage
        If vGood Then
            mStatusLabel.Image = My.Resources.BlueBall
        Else
            mStatusLabel.Image = My.Resources.RedBall
        End If
        Me.Cursor = Cursors.Default
    End Sub

#End Region




    'Private Sub ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
    '    Console.WriteLine(e.ListChangedType.ToString)
    '    Select Case e.ListChangedType
    '        Case _
    '        System.ComponentModel.ListChangedType.ItemAdded _
    '        , System.ComponentModel.ListChangedType.ItemChanged _
    '        , System.ComponentModel.ListChangedType.ItemDeleted _
    '        , System.ComponentModel.ListChangedType.Reset
    '            UpdateFilteredRowCount(CType(sender, DataView))
    '    End Select
    'End Sub


    Private Sub PlotValuesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlotValuesButton.Click
        RaiseEvent PlotValuesButtonClicked(sender, e)
    End Sub

    Private Sub EmailButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmailButton.Click
        RaiseEvent EmailButtonClicked(sender, e)
    End Sub

    Event TabPageChanged As EventHandler

    Private Sub tcMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcMain.SelectedIndexChanged
        RaiseEvent TabPageChanged(Me, EventArgs.Empty)
    End Sub

    Private Sub PropoertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropoertiesToolStripMenuItem.Click
        RaiseEvent PropertyButtonClicked(sender, e)
    End Sub

    Private Sub CreateCopyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreateCopyToolStripMenuItem.Click
        RaiseEvent CreateCopyButtonClicked(sender, e)
    End Sub

    Private Sub GenerateXMLFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateXMLFileToolStripMenuItem.Click
        RaiseEvent GenerateXMLFileButtonClicked(sender, e)
    End Sub

    Private Sub ImportXMLFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportXMLFileToolStripMenuItem.Click
        RaiseEvent ImportXMLFileButtonClicked(sender, e)
    End Sub
End Class
