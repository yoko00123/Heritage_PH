
Imports System.Text
Imports System.Web
Public Class CtrlOrganizationalChart




    Function loadHTML() As String
        Try
            Dim sb As New System.Text.StringBuilder

            Dim filepath As String = Application.StartupPath & "\Files\"

            sb.AppendLine("<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">" & _
                              "<head>" & _
                                  "<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8""/>" & _
                                  "<title>Spacetree - Tree Animation</title>" & _
                                  "<!-- CSS Files -->" & _
                                  "<link type=""text/css"" href=""" & filepath & "base.css"" rel=""stylesheet""/>" & _
                                  "<link type=""text/css"" href=""" & filepath & "Spacetree.css"" rel=""stylesheet""/>" & _
                                  "<!-- JIT Library File -->" & _
                            "<script language=""javascript"" type=""text/javascript"" src=""" & filepath & "excanvas.js""></script>" & _
                                  "<script language=""javascript"" type=""text/javascript"" src=""" & filepath & "JIT.js""></script>" & _
                                  "<!-- Example File -->" & _
                                  "<script language=""javascript"" type=""text/javascript"" src=""" & filepath & "Example1.js""></script>" & _
                             " </head>" & _
                              "<body onload=""init();"">" & _
                                  "<div id=""container"">" & _
                                      "<div id=""left-container"">" & _
                                          "<div class=""text"">" & _
                                              "<h4>" & _
"Organizational Chart" & _
"</h4> " & _
            " <!-- A static JSON Tree structure is used as input for this animation.<br/><br/>" & _
                                              "<b>Click</b> on a node to select it.<br/><br/>" & _
            "You can <b>select the tree orientation</b> by changing the select box in the right column.<br/><br/>" & _
            "You can <b>change the selection mode</b> from <em>Normal</em> selection (i.e. center the selected node) to " & _
            "<em>Set as Root</em>.<br/><br/>" & _
                                              "<b>Drag and Drop the canvas</b> to do some panning.<br/><br/>" & _
            "Leaves color depend on the number of children they actually have." & _
"</div>" & _
                                          "<div id=""id-list""></div>" & _
                                          "<div style=""text-align:center;""><a href=""example1.code.html"">See the Example Code</a>--></div>" & _
                                      "</div>" & _
                                      "<div id=""center-container"">" & _
                                          "<div id=""infovis""></div>" & _
                                      "</div>" & _
                                      "<div id=""right-container"" class=""Tree"">" & _
                                        "<h4><b>Organizational Chart</b></h4>" & _
                                          "<h4>Tree Orientation</h4>" & _
                                          "<table>" & _
                                              "<tr>" & _
                                                 " <td>" & _
                                                      "<label for=""r-left"">Left </label>" & _
                                                  "</td>" & _
                                                  "<td>" & _
                                                     " <input type=""radio"" id=""r-left"" name=""orientation"" checked=""checked"" value=""left""/>" & _
                                                  "</td>" & _
                                              "</tr>" & _
                                            "  <tr>" & _
                                              "    <td>" & _
                                                "      <label for=""r-top"">Top </label>" & _
                                                 " </td>" & _
                                                "  <td>" & _
                                                 "     <input type=""radio"" id=""r-top"" name=""orientation"" value=""top""/>" & _
                                                 " </td>" & _
                                             " </tr>" & _
                                             " <tr>" & _
                                                "  <td>" & _
                                                 "     <label for=""r-bottom"">Bottom </label>" & _
                                               "   </td>" & _
                                              "    <td>" & _
                                             "         <input type=""radio"" id=""r-bottom"" name=""orientation"" value=""bottom""/>" & _
                                             "     </td>" & _
                                             " </tr>" & _
                                            "  <tr>" & _
                                                "  <td>" & _
                                               "       <label for=""r-right"">Right </label>" & _
                                             "     </td>" & _
                                             "     <td>" & _
                                              "        <input type=""radio"" id=""r-right"" name=""orientation"" value=""right""/>" & _
                                             "     </td>" & _
                                          "    </tr>" & _
                                        "  </table>" & _
                                         " <h4>Selection Mode</h4>" & _
                                          "<table>" & _
                                          "    <tr>" & _
                                           "       <td>" & _
                                           "           <label for=""s-normal"">Normal </label>" & _
                                            "      </td>" & _
                                           "       <td>" & _
                                            "          <input type=""radio"" id=""s-normal"" name=""selection"" checked=""checked"" value=""normal""/>" & _
                                            "      </td>" & _
                                           "   </tr>" & _
                                           "   <!--<tr>" & _
                                           "       <td>" & _
                                            "          <label for=""s-root"">Set as Root </label>" & _
                                            "      </td>" & _
                                            "      <td>" & _
                                             "         <input type=""radio"" id=""s-root"" name=""selection"" value=""root""/>" & _
                                            "      </td>" & _
                                          "    </tr>-->" & _
                                        "  </table>" & _
                                   "   </div>" & _
                                    "  <div id=""log""></div>" & _
                                "  </div>" & _
                            "  </body>" & _
                        "  </html>")
            Return sb.ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub Reload()

        GroupBox1.Visible = True
        Dim filepath As String = Application.StartupPath & "\Files\Example1.js"
        If System.IO.File.Exists(filepath) Then System.IO.File.Delete(filepath)
        If Not System.IO.File.Exists(filepath) Then System.IO.File.Create(filepath).Dispose()

        Dim objWriter As New System.IO.StreamWriter(filepath, True)
        objWriter.WriteLine(DataReturn(Step_1))
        objWriter.Close()

        Dim strBuilder As New StringBuilder
        strBuilder.Append(loadHTML)
        Dim s As String = HttpUtility.UrlDecode(strBuilder.ToString)

        Dim p As String = IO.Path.GetTempFileName
        Dim tf As New IO.StreamWriter(p, False, System.Text.Encoding.UTF8)
        tf.Write(s)
        tf.Flush()
        Try
            If Not Me.IsDisposed Then
                wb1.Navigate(p)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Dim xix As Integer
   


    Dim Panel() As Panel
    Dim Label() As Label
    Dim Button() As Button

    Dim strHandler() As String
    Friend WithEvents thisBtn As New Button
    Dim ixx As Integer

    Sub RegisterValues()
        Dim sqlBuilder As New StringBuilder
        sqlBuilder.Append("SELECT mdtf.Name,mdtf.ID_MenuDetailTab,mdtf.ParentLookUp,mdtf.ParentLookUpChildColumn ")
        sqlBuilder.Append("FROM dbo.tMenu m,dbo.tMenuDetailTab mdt,dbo.tMenuDetailTabField mdtf ")
        sqlBuilder.Append("WHERE m.ID = mdt.ID_Menu AND ")
        sqlBuilder.Append("mdt.ID = mdtf.ID_MenuDetailTab AND ")
        sqlBuilder.Append("m.ID = 909")

        Dim ii As Integer = sqlDataset(sqlBuilder.ToString).Rows.Count - 1
        ixx = sqlDataset("SELECT * FROM dbo.tMenuDetailTab WHERE ID_Menu = 909").Rows.Count - 1
        Dim xi As Integer = 0
        Dim xii As Integer = 0
        Dim height As Integer = 0
        ReDim Panel(ixx)
        ReDim Button(ixx)
        ReDim Label(ii)
        ReDim strHandler(ixx)


        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * FROM dbo.tMenuDetailTab WHERE ID_Menu = 909").Rows

            height = 0

            Panel(xi) = New Panel
            Panel(xi).Dock = DockStyle.Top
            Panel(xi).BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            Panel(xi).Height = 23
            pnlOption.Controls.Add(Panel(xi))

            Button(xi) = New Button
            Button(xi).Dock = DockStyle.Top
            Button(xi).Font = New Font("Verdana", 9)
            Button(xi).TextAlign = ContentAlignment.MiddleCenter
            Button(xi).Height = 23
            Button(xi).Text = myRow.Item("Name").ToString
            Panel(xi).Controls.Add(Button(xi))

            strHandler(xi) = myRow.Item("Name").ToString

            height += 23
            Dim xix As Integer = 0
            Dim myRow2 As DataRow
            For Each myRow2 In sqlDataset(sqlBuilder.ToString & " and ID_MenuDetailTab = " & CInt(myRow.Item("ID"))).Rows

                Label(xii) = New Label

                If Int(xix / 2) = Val(xix / 2) Then
                    Label(xii).BackColor = Color.White
                Else
                    Label(xii).BackColor = Color.PeachPuff
                End If

                Label(xii).Dock = DockStyle.Top
                Label(xii).TextAlign = ContentAlignment.MiddleLeft
                Label(xii).Height = 23
                Label(xii).Text = "● " & myRow2.Item("Name").ToString
                Label(xii).Font = New Font("Verdana", 9)
                Panel(xi).Controls.Add(Label(xii))
                Label(xii).BringToFront()

                height += 23
                xii += 1
                xix += 1

            Next

            Panel(xi).Height = height
            Panel(xi).BringToFront()
            Button(xi).SendToBack()

            AddHandler Button(xi).Click, AddressOf btnGenerate_click
            xi += 1

        Next

    End Sub

    Dim sql() As String
    Dim sqlID() As String


    Private Sub btnGenerate_click(ByVal sender As System.Object, ByVal e As System.EventArgs)



        Dim i As Integer
        thisBtn = DirectCast(sender, Button)

        For i = 0 To ixx
            If strHandler(i) = thisBtn.Text Then

                Dim myRow As DataRow
                For Each myRow In sqlDataset("SELECT ID FROM dbo.tMenuDetailTab WHERE ID_Menu = 909 AND Name = '" & thisBtn.Text & "'").Rows
                    Dim xiixi As Integer = sqlDataset(getSQLSort(CInt(myRow.Item(0)))).Rows.Count - 1
                    ReDim sql(xiixi)
                    ReDim sqlID(xiixi)
                    Dim xiixii As Integer = 0
                    Dim myRow2 As DataRow
                    For Each myRow2 In sqlDataset(getSQLSort(CInt(myRow.Item(0)))).Rows

                        sql(xiixii) = myRow2.Item("ParentLookUp").ToString
                        sqlID(xiixii) = myRow2.Item("ParentLookUpChildColumn").ToString

                        xiixii += 1
                    Next
                Next

                Reload()
                Exit For
            End If
        Next

    End Sub

    Function getSQLSort(ByVal ID As Integer) As String
        Dim sqlBuilder As New StringBuilder
        sqlBuilder.Append("SELECT mdtf.Name,mdtf.ID_MenuDetailTab,mdtf.ParentLookUp,mdtf.ParentLookUpChildColumn ")
        sqlBuilder.Append("FROM dbo.tMenu m,dbo.tMenuDetailTab mdt,dbo.tMenuDetailTabField mdtf ")
        sqlBuilder.Append("WHERE m.ID = mdt.ID_Menu AND ")
        sqlBuilder.Append("mdt.ID = mdtf.ID_MenuDetailTab AND  ")
        sqlBuilder.Append("m.ID = 909 AND ID_MenuDetailTab = " & ID)

        Return sqlBuilder.ToString

    End Function



    Function Step_1() As String
        xix = 1
        Step_1 = ""
        Dim myRow As DataRow
        For Each myRow In sqlDataset("Select TOP 1 ID,Name from " & sql(0) & " Where " & sqlID(0) & " = " & ID_Company_).Rows
            Step_1 = CreateNodeHasChild("0." & xix, myRow.Item("Name").ToString, Step_2(ID_Company_))
            xix += 1
        Next
        Return Step_1
    End Function

    Function Step_2(ByVal ID As Integer) As String
        Step_2 = ""
        Dim Count As Integer = sqlDataset("SELECT * from " & sql(1) & " Where " & sqlID(1) & " = " & ID).Rows.Count - 1
        Dim xCount As Integer = 0
        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * from " & sql(1) & " Where " & sqlID(1) & " = " & ID).Rows
            Step_2 = Step_2 & "{" & CreateNodeHasChild("1." & xix, myRow.Item("Name").ToString, "" & If(sqlID.Length - 1 = 1, "[]", Step_3(CInt(myRow.Item("ID")))) & "") & "}"
            If xCount = Count Then
            Else
                Step_2 = Step_2 & ","
            End If
            xix += 1
            xCount += 1
        Next
        Return "[" & Step_2 & "]"
    End Function

    Function Step_3(ByVal ID As Integer) As String
        Step_3 = ""
        Dim Count As Integer = sqlDataset("SELECT * from " & sql(2) & " Where " & sqlID(2) & " = " & ID).Rows.Count - 1
        Dim xCount As Integer = 0
        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * from " & sql(2) & " Where " & sqlID(2) & " = " & ID).Rows
            Step_3 = Step_3 & "{" & CreateNodeHasChild("2." & xix, myRow.Item("Name").ToString, "" & If(sqlID.Length - 1 = 2, "[]", Step_4(CInt(myRow.Item("ID")))) & "") & "}"
            If xCount = Count Then
            Else
                Step_3 = Step_3 & ","
            End If
            xix += 1
            xCount += 1
        Next
        Return "[" & Step_3 & "]"
    End Function

    Function Step_4(ByVal ID As Integer) As String
        Step_4 = ""
        Dim Count As Integer = sqlDataset("SELECT * from " & sql(3) & " Where " & sqlID(3) & " = " & ID).Rows.Count - 1
        Dim xCount As Integer = 0
        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * from " & sql(3) & " Where " & sqlID(3) & " = " & ID).Rows
            Step_4 = Step_4 & "{" & CreateNodeHasChild("4." & xix, myRow.Item("Name").ToString, "" & If(sqlID.Length - 1 = 3, "[]", Step_5(CInt(myRow.Item("ID")))) & "") & "}"
            If xCount = Count Then
            Else
                Step_4 = Step_4 & ","
            End If
            xix += 1
            xCount += 1
        Next
        Return "[" & Step_4 & "]"
    End Function

    Function Step_5(ByVal ID As Integer) As String
        Step_5 = ""
        Dim Count As Integer = sqlDataset("SELECT * from " & sql(4) & " Where " & sqlID(4) & " = " & ID).Rows.Count - 1
        Dim xCount As Integer = 0
        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * from " & sql(4) & " Where " & sqlID(4) & " = " & ID).Rows
            Step_5 = Step_5 & "{" & CreateNodeHasChild("5." & xix, myRow.Item("Name").ToString, "" & If(sqlID.Length - 1 = 4, "[]", Step_6(CInt(myRow.Item("ID")))) & "") & "}"
            If xCount = Count Then
            Else
                Step_5 = Step_5 & ","
            End If
            xix += 1
            xCount += 1
        Next
        Return "[" & Step_5 & "]"
    End Function

    Function Step_6(ByVal ID As Integer) As String
        Step_6 = ""
        Dim Count As Integer = sqlDataset("SELECT * from " & sql(5) & " Where " & sqlID(5) & " = " & ID).Rows.Count - 1
        Dim xCount As Integer = 0
        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * from " & sql(5) & " Where " & sqlID(5) & " = " & ID).Rows
            Step_6 = Step_6 & "{" & CreateNodeHasChild("5." & xix, myRow.Item("Name").ToString, "" & If(sqlID.Length - 1 = 4, "[]", Step_7(CInt(myRow.Item("ID")))) & "") & "}"
            If xCount = Count Then
            Else
                Step_6 = Step_6 & ","
            End If
            xix += 1
            xCount += 1
        Next
        Return "[" & Step_6 & "]"
    End Function

    Function Step_7(ByVal ID As Integer) As String
        Step_7 = ""
        Dim Count As Integer = sqlDataset("SELECT * from " & sql(6) & " Where " & sqlID(6) & " = " & ID).Rows.Count - 1
        Dim xCount As Integer = 0
        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * from " & sql(6) & " Where " & sqlID(6) & " = " & ID).Rows
            Step_7 = Step_7 & "{" & CreateNodeHasChild("5." & xix, myRow.Item("Name").ToString, "" & If(sqlID.Length - 1 = 4, "[]", Step_8(CInt(myRow.Item("ID")))) & "") & "}"
            If xCount = Count Then
            Else
                Step_7 = Step_7 & ","
            End If
            xix += 1
            xCount += 1
        Next
        Return "[" & Step_7 & "]"
    End Function

    Function Step_8(ByVal ID As Integer) As String
        Step_8 = ""
        Dim Count As Integer = sqlDataset("SELECT * from " & sql(7) & " Where " & sqlID(7) & " = " & ID).Rows.Count - 1
        Dim xCount As Integer = 0
        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * from " & sql(7) & " Where " & sqlID(7) & " = " & ID).Rows
            Step_8 = Step_8 & "{" & CreateNodeHasChild("5." & xix, myRow.Item("Name").ToString, "" & If(sqlID.Length - 1 = 4, "[]", Step_9(CInt(myRow.Item("ID")))) & "") & "}"
            If xCount = Count Then
            Else
                Step_8 = Step_8 & ","
            End If
            xix += 1
            xCount += 1
        Next
        Return "[" & Step_8 & "]"
    End Function

    Function Step_9(ByVal ID As Integer) As String
        Step_9 = ""
        Dim Count As Integer = sqlDataset("SELECT * from " & sql(8) & " Where " & sqlID(8) & " = " & ID).Rows.Count - 1
        Dim xCount As Integer = 0
        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * from " & sql(8) & " Where " & sqlID(8) & " = " & ID).Rows
            Step_9 = Step_9 & "{" & CreateNodeHasChild("5." & xix, myRow.Item("Name").ToString, "" & If(sqlID.Length - 1 = 4, "[]", Step_10(CInt(myRow.Item("ID")))) & "") & "}"
            If xCount = Count Then
            Else
                Step_9 = Step_9 & ","
            End If
            xix += 1
            xCount += 1
        Next
        Return "[" & Step_9 & "]"
    End Function


    Function Step_10(ByVal ID As Integer) As String
        Step_10 = ""
        Dim Count As Integer = sqlDataset("SELECT * from " & sql(9) & " Where " & sqlID(9) & " = " & ID).Rows.Count - 1
        Dim xCount As Integer = 0
        Dim myRow As DataRow
        For Each myRow In sqlDataset("SELECT * from " & sql(9) & " Where " & sqlID(9) & " = " & ID).Rows
            Step_10 = Step_10 & "{" & CreateNodeHasChild("5." & xix, myRow.Item("Name").ToString, "" & If(sqlID.Length - 1 = 4, "[]", Step_11(CInt(myRow.Item("ID")))) & "") & "}"
            If xCount = Count Then
            Else
                Step_10 = Step_10 & ","
            End If
            xix += 1
            xCount += 1
        Next
        Return "[" & Step_10 & "]"
    End Function

    Function Step_11(ByVal ID As Integer) As String
        Return Nothing
    End Function

    Function CreateNodeHasChild(ByVal ID As String, ByVal Name As String, ByVal Child As String) As String
        Dim strTextNode As New StringBuilder
        strTextNode.AppendLine("id: """ & ID & """,")
        strTextNode.AppendLine("name: """ & Name & """,")
        strTextNode.AppendLine("data: {},")
        strTextNode.AppendLine("children:")
        strTextNode.AppendLine(Child)
        strTextNode.AppendLine("")
        Return strTextNode.ToString
    End Function

    Private Sub btnCBDD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Reload()
    End Sub


    Sub CreateFolders()

    
        If (Not System.IO.Directory.Exists(Application.StartupPath & "\Files")) Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\Files")
        End If

        If (Not System.IO.Directory.Exists(Application.StartupPath & "\Files\base.css")) Then

            System.IO.File.WriteAllText(Application.StartupPath & "\Files\base.css", Base_CSS)

        Else
            System.IO.File.Delete(Application.StartupPath & "\Files\base.css")
            System.IO.File.WriteAllText(Application.StartupPath & "\Files\base.css", Base_CSS)

        End If

        If (Not System.IO.Directory.Exists(Application.StartupPath & "\Files\excanvas.js")) Then
            System.IO.File.WriteAllText(Application.StartupPath & "\Files\excanvas.js", ExCanvas_JS)
        Else

            System.IO.File.Delete(Application.StartupPath & "\Files\excanvas.js")
            System.IO.File.WriteAllText(Application.StartupPath & "\Files\excanvas.js", ExCanvas_JS)

        End If

        If (Not System.IO.Directory.Exists(Application.StartupPath & "\Files\JIT.js")) Then

            System.IO.File.WriteAllText(Application.StartupPath & "\Files\JIT.js", JIT_JS)

        Else

            System.IO.File.Delete(Application.StartupPath & "\Files\JIT.js")
            System.IO.File.WriteAllText(Application.StartupPath & "\Files\JIT.js", JIT_JS)
        End If

        If (Not System.IO.Directory.Exists(Application.StartupPath & "\Files\Spacetree.css")) Then
           
            System.IO.File.WriteAllText(Application.StartupPath & "\Files\Spacetree.css", SpaceTree_CSS)

        Else
            System.IO.File.Delete(Application.StartupPath & "\Files\Spacetree.css")
            System.IO.File.WriteAllText(Application.StartupPath & "\Files\Spacetree.css", SpaceTree_CSS)
        End If

     


    End Sub

    Private Sub CtrlOrganizationalChart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateFolders()
        RegisterValues()
        Me.BringToFront()
    End Sub

    Dim frm As New Form
   
End Class
