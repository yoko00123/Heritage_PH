Public Class HelpClass

    Private mDataSet As DataSet
    Private mHeaderRow As DataRow

#Region "Constructor"
    Public Sub New(ByVal pDataSet As DataSet)
        mDataSet = pDataSet
        mHeaderRow = mDataSet.Tables(0).Rows(0)
    End Sub
#End Region

#Region "Properties"
    Private mStyleSheet As String
    Public Property StyleSheet() As String
        Get
            Return mStyleSheet
        End Get
        Set(ByVal value As String)
            mStyleSheet = value
        End Set
    End Property

    Private mResourcePath As String
    Public Property ResourcePath() As String
        Get
            Return mResourcePath
        End Get
        Set(ByVal value As String)
            mResourcePath = value
        End Set
    End Property

#End Region

    Public Function GetHTML() As String
        Dim sb As New System.Text.StringBuilder
        Dim a As String
        'Dim o As Object
        sb.AppendLine("<html>")
        sb.AppendLine("<head>")
        sb.AppendLine("<link rel=""stylesheet"" type=""text/css"" href=""" & mStyleSheet & """/>")
        sb.AppendLine("</head>")
        sb.AppendLine("<body>")

        sb.Append("<img src=""")
        If mHeaderRow.Table.Columns.Contains("ImageFile") Then
            sb.Append(IO.Path.Combine(ResourcePath, mHeaderRow.Item("ImageFile").ToString))

        End If
        sb.AppendLine(""" width=""64px"" height=""64px""/>")




        sb.AppendLine("&nbsp;")
        sb.AppendLine("<span class=""zname"">")
        Try
            sb.AppendLine(mHeaderRow.Item("Name").ToString)

        Catch ex As Exception

        End Try
        sb.AppendLine("</span>")
        sb.AppendLine("<hr/>")
        sb.AppendLine("<font face=""Verdana"" size=""2"">")
        sb.AppendLine("<b>")
        sb.AppendLine(mHeaderRow.Item("Name").ToString)
        sb.AppendLine("</b>")
        sb.AppendLine("</font>")
        sb.AppendLine("<br/>")
        sb.AppendLine("<br/>")
        sb.AppendLine("<br/>")

        sb.AppendLine("<div CLASS=""zhelpgroup"">")
        sb.AppendLine("Description")
        sb.AppendLine("</div>")
        sb.AppendLine("<pre>")
        sb.AppendLine(mHeaderRow.Item("Description").ToString)
        sb.AppendLine("</pre>")
        sb.AppendLine("<br/>")

        For Each dr As DataRow In mDataSet.Tables("tMenuTab").Select
            sb.AppendLine("<br/>")
            sb.AppendLine("<div class=""zhelpgroup"">" & dr("Name").ToString & "</div>")
            sb.AppendLine("<br/>")

            If dr.Item("Description").ToString <> "" Then
                sb.AppendLine("<pre>")
                sb.AppendLine(dr.Item("Description").ToString)
                sb.AppendLine("</pre>")
                sb.AppendLine("<br/>")
            End If


            sb.AppendLine("<table border=""1"" cellpadding=""2"" cellspacing=""0"" width=""100%"">")
            sb.AppendLine("<tr>")
            sb.AppendLine("<th>")
            sb.AppendLine("Field Name")
            sb.AppendLine("</th>")
            sb.AppendLine("<th width=""16px"">")
            sb.AppendLine("</th>")
            sb.AppendLine("<th>")
            sb.AppendLine("Description")
            sb.AppendLine("</th>")
            sb.AppendLine("</tr>")
            For Each dr2 As DataRow In mDataSet.Tables("tMenuTabField").Select("ID_Menutab=" & dr.Item("ID").ToString)
                a = dr2.Item("Label").ToString
                If a = "" Then a = dr2.Item("Name").ToString
                'If CBool(dr2.Item("ShowInBrowser")) And a <> "ID" Then
                sb.AppendLine("<tr height=""22px"">")
                sb.AppendLine("<td width=""160px"" style=""text-align: right;"">")
                sb.AppendLine(dr2.Item("Label").ToString())
                sb.AppendLine("</td>")
                sb.AppendLine("<td>")
                If dr2.Item("ImageFile").ToString <> "" Then
                    sb.AppendLine("<IMG width=""16"" height=""16"" src=""" & IO.Path.Combine(ResourcePath, dr2.Item("ImageFile").ToString) & """/>")
                End If
                sb.AppendLine("</td>")
                sb.AppendLine("<td>")
                sb.AppendLine(dr2.Item("Description").ToString)
                sb.AppendLine("</td>")
                sb.AppendLine("</tr>")

                'End If
            Next
            sb.AppendLine("</table>")

        Next

        '            <TABLE BORDER="0" CELLPADDING="0" CELLSPACING="4">

        '              <TR height="18">
        '                <td class="zproperty">
        '        Last(Name)
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/LastName"/>
        '                </td>
        '                <td class="zproperty">
        'Nationality:
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/Nationality"/>
        '                </td>

        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        '        First(Name)
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/FirstName"/>
        '                </td>
        '                <td class="zproperty">
        'Citizenship:
        '                </td>
        '                <td class="zvalue" align="left">
        '                  <xsl:value-of select="tPersona/Citizenship"/>
        '                </td>

        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        '        Middle(Name)
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/MiddleName"/>
        '                </td>
        '                <td class="zproperty">
        'Religion:
        '                </td>
        '                <td class="zvalue" align="left">
        '                  <xsl:value-of select="tPersona/Religion"/>
        '                </td>
        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        '        Nick(Name)
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/NickName"/>
        '                </td>
        '                <td class="zproperty">
        '        Civil(Status)
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/CivilStatus"/>
        '                </td>
        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        'Company:
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/Company"/>
        '                </td>
        '                <td class="zproperty">
        'Height:
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/Height"/>
        '                </td>
        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        '                  Birth Date:
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="ms:format-date(tPersona/BirthDate, 'MMM dd, yyyy')"/>
        '                </td>
        '                <td class="zproperty">
        'Weight:
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/Weight"/>
        '                </td>

        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        'Age:
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/Age"/>
        '                </td>
        '                <td class="zproperty">
        '        Email(Address)
        '                </td>
        '                <td class="zvalue" align="left">
        '                  <xsl:value-of select="tPersona/EmailAddress"/>
        '                </td>
        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        'Gender:
        '                </td>
        '                <td class="zvalue" align="left">
        '                  <xsl:value-of select="tPersona/Gender"/>
        '                </td>
        '                <td class="zproperty">
        '                  Alternate Email Add:
        '                </td>
        '                <td class="zvalue" align="left">
        '                  <xsl:value-of select="tPersona/AlternateEmailAddress"/>
        '                </td>
        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        'Spouse:
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/Spouse"/>
        '                </td>
        '              </TR>

        '            </TABLE>

        '              <hr color="rgb(70,130,180)"/>


        '            <table>

        '              <TR height="18">
        '                <td class="zproperty">
        '        SSS(No)
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/SSSNo"/>
        '                </td>
        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        '        HDMF(No)
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/HDMFNo"/>
        '                </td>
        '              </TR>
        '              <TR height="18">
        '                <td class="zproperty">
        '        PhilHealth(No)
        '                </td>
        '                <td class="zvalue" align="right">
        '                  <xsl:value-of select="tPersona/PhilHealthNo"/>
        '                </td>
        '              </TR>
        '            </table>

        '          </P>

        '          <BR/>



        '          <xsl:if test="tEmploymentHistory">
        '            <DIV CLASS="zgroup">
        '            Employment(History)
        '            </DIV>
        '                    <table border="1" cellpadding="4" cellspacing="0" style="width: 100%; height: 18px">
        '              <tr style="height:18">
        '                <td align="right" class="zvalue" style="width: 32%; text-align: center;">
        '                  <strong>Company</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 32%; text-align: center;">
        '                  <strong>Designation</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 18%; text-align: center">
        '                  <strong>From</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 18%; text-align: center">
        '                  <strong>To</strong>
        '                </td>
        '              </tr>
        '              <xsl:for-each select="tEmploymentHistory">
        '                <tr style="height:18">
        '                  <td align="left" class="zvalue" style="width: 667px; height: 18px; text-align: center;">
        '                    <xsl:value-of select="Company"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="width: 1363px; height: 18px;">
        '                    <xsl:value-of select="Designation"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="width: 432px; height: 18px; text-align: right;">
        '                    <xsl:value-of select="ms:format-date(StartDate, 'MMM dd, yyyy')"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="width: 430px; height: 18px; text-align: right;">
        '                    <xsl:if test="(EndDate)">
        '                      <xsl:value-of select="ms:format-date(EndDate, 'MMM dd, yyyy')"/>
        '                    </xsl:if>
        '                    <xsl:if test="not(EndDate)">
        '                       -
        '                    </xsl:if>
        '                  </td>
        '                </tr>
        '              </xsl:for-each>
        '            </table>
        '            <BR/>
        '            <BR/>
        '          </xsl:if>








        '          <xsl:if test="tEducationAttainment">
        '            <DIV CLASS="zgroup">
        '                                            Educational(Attainment)
        '            </DIV>

        '            <table border="1" cellpadding="4" cellspacing="0" style="width: 100%">
        '              <tr style="height:18">
        '                <td class="zproperty" style="height: 18px; width: 32%; text-align: center;">
        '                                            Education(Level)
        '                </td>
        '                <td align="right" class="zvalue" style="width: 48%; text-align: center; height: 18px;">
        '                  <strong>School</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 10%; height: 18px; text-align: center">
        '                  <strong>From</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 10%; height: 18px; text-align: center">
        '                  <strong>To</strong>
        '                </td>
        '              </tr>
        '              <xsl:for-each select="tEducationAttainment">
        '                <tr style="height:18">
        '                  <td align="left" class="zvalue" style="width: 667px; height: 18px; text-align: center;">
        '                    <xsl:value-of select="EducationLevel"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="width: 1363px; height: 18px;">
        '                    <xsl:value-of select="School"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="width: 432px; height: 18px; text-align: right;">
        '                    <xsl:value-of select="YearFrom"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="width: 430px; height: 18px; text-align: right;">
        '                    <xsl:value-of select="YearTo"/>
        '                  </td>
        '                </tr>
        '              </xsl:for-each>
        '            </table>
        '            <BR/>
        '            <BR/>
        '          </xsl:if>


        '          <xsl:if test="tAddress">
        '            <DIV CLASS="zgroup">
        '                                                            Address()
        '            </DIV>

        '            <table border="1" cellpadding="4" cellspacing="0" style="width: 100%">
        '              <tr style="height: 18px">
        '                <td class="zvalue" style="width: 20%; text-align: center; height: 18px;">
        '                  <strong>Type</strong>
        '                </td>
        '                <td class="zvalue" style="width: 60%; text-align: center; height: 18px;">
        '                  <strong>Address</strong>
        '                </td>
        '                <td class="zvalue" style="width: 20%; height: 18px; text-align: center">
        '                  <strong>Contact No.</strong>
        '                </td>
        '              </tr>
        '              <xsl:for-each select="tAddress">
        '                <tr style="height: 18px">
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Type"/>
        '                  </td>
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Address"/>
        '                  </td>
        '                  <td class="zvalue" style="text-align: right;">
        '                    <xsl:value-of select= "ContactNo"/>
        '                  </td>
        '                </tr>
        '              </xsl:for-each>
        '            </table>


        '            <BR/>
        '            <BR/>
        '          </xsl:if>




        '          <xsl:if test="tDependent">
        '            <DIV CLASS="zgroup">
        '                                                                            Dependents()
        '            </DIV>

        '            <table border="1" cellpadding="4" cellspacing="0" style="width: 100%">
        '              <tr style="height:18">
        '                <td class="zproperty" style="height: 18px; width: 50%; text-align: center;">
        '                                                                            Name()
        '                </td>
        '                <td align="right" class="zvalue" style="width: 30%; text-align: center; height: 18px;">
        '                  <strong>Relationship</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 20%; height: 18px; text-align: center">
        '                  <strong>Birthdate</strong>
        '                </td>
        '              </tr>
        '              <xsl:for-each select="tDependent">
        '                <tr style="height:18">
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Name"/>
        '                  </td>
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Relationship"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="text-align: right;">
        '                    <xsl:value-of select="ms:format-date(BirthDate, 'MMM dd, yyyy')"/>
        '                  </td>
        '                </tr>
        '              </xsl:for-each>
        '            </table>
        '            <BR/>
        '            <BR/>
        '          </xsl:if>



        '          <xsl:if test="tBeneficiary">
        '            <DIV CLASS="zgroup">
        '                                                                                            Beneficiary()
        '            </DIV>

        '            <table border="1" cellpadding="4" cellspacing="0" style="width: 100%">
        '              <tr style="height:18">
        '                <td class="zproperty" style="height: 18px; width: 50%; text-align: center;">
        '                                                                                            Name()
        '                </td>
        '                <td align="right" class="zvalue" style="width: 30%; text-align: center; height: 18px;">
        '                  <strong>Relationship</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 20%; height: 18px; text-align: center">
        '                  <strong>Birthdate</strong>
        '                </td>
        '              </tr>
        '              <xsl:for-each select="tBeneficiary">
        '                <tr style="height:18">
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Name"/>
        '                  </td>
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Relationship"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="text-align: right;">
        '                    <xsl:value-of select="ms:format-date(BirthDate, 'MMM dd, yyyy')"/>
        '                  </td>
        '                </tr>
        '              </xsl:for-each>
        '            </table>
        '            <BR/>
        '            <BR/>
        '          </xsl:if>

        '          <xsl:if test="tPersonaEmergencyContact">
        '            <DIV CLASS="zgroup">
        '                                                                                                            Emergency(Contact)
        '            </DIV>

        '            <table border="1" cellpadding="4" cellspacing="0" style="width: 100%">
        '              <tr style="height:18">
        '                <td class="zproperty" style="height: 18px; width: 50%; text-align: center;">
        '                                                                                                            Name()
        '                </td>
        '                <td align="right" class="zvalue" style="width: 30%; text-align: center; height: 18px;">
        '                  <strong>Relationship</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 30%; text-align: center; height: 18px;">
        '                  <strong>Adress</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 20%; height: 18px; text-align: center">
        '                  <strong>ContactNo</strong>
        '                </td>
        '              </tr>
        '              <xsl:for-each select="tPersonaEmergencyContact">
        '                <tr style="height:18">
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Name"/>
        '                  </td>
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Relationship"/>
        '                  </td>
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Address"/>
        '                  </td>
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="ContactNo"/>
        '                  </td>

        '                </tr>
        '              </xsl:for-each>
        '            </table>
        '            <BR/>
        '            <BR/>
        '          </xsl:if>

        '          <xsl:if test="tMedicalHistory">
        '            <DIV CLASS="zgroup">
        '                                                                                                                            Medical(History)
        '            </DIV>

        '            <table border="1" cellpadding="4" cellspacing="0" style="width: 100%">
        '              <tr style="height:18">
        '                <td class="zproperty" style="height: 18px; width: 50%; text-align: center;">
        '                                                                                                                            Medical(Condition)
        '                </td>
        '                <td align="right" class="zvalue" style="width: 30%; text-align: center; height: 18px;">
        '                  <strong>Date Diagnosed</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 20%; height: 18px; text-align: center">
        '                  <strong>Last Check-Up Date</strong>
        '                </td>
        '                <td align="right" class="zvalue" style="width: 20%; height: 18px; text-align: center">
        '                  <strong>Status</strong>
        '                </td>
        '              </tr>
        '              <xsl:for-each select="tMedicalHistory">
        '                <tr style="height:18">
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="MedicalCondition"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="text-align: right;">
        '                    <xsl:value-of select="ms:format-date(DateDiagnosed, 'MMM dd, yyyy')"/>
        '                  </td>
        '                  <td align="left" class="zvalue" style="text-align: right;">
        '                    <xsl:value-of select="ms:format-date(LastCheckUpDate, 'MMM dd, yyyy')"/>
        '                  </td>
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="Status"/>
        '                  </td>
        '                </tr>
        '              </xsl:for-each>
        '            </table>
        '            <BR/>
        '            <BR/>
        '          </xsl:if>

        '          <xsl:if test="tPersonaEmploymentRequirement">
        '            <DIV CLASS="zgroup">
        '                                                                                                                                            Employment(Requirement)
        '            </DIV>

        '            <table border="1" cellpadding="4" cellspacing="0" style="width: 100%">
        '              <tr style="height: 18px">
        '                <td class="zvalue" style="width: 20%; text-align: center; height: 18px;">
        '                  <strong>Requirement</strong>
        '                </td>

        '              </tr>
        '              <xsl:for-each select="tPersonaEmploymentRequirement">
        '                <tr style="height: 18px">
        '                  <td align="left" class="zvalue">
        '                    <xsl:value-of select="EmploymentRequirement"/>
        '                  </td>
        '                </tr>
        '              </xsl:for-each>
        '            </table>
        '            <BR/>
        '            <BR/>
        '          </xsl:if>
        '        </xsl:for-each>
        sb.AppendLine("</body>")
        sb.AppendLine("</html>")
        '  </xsl:template>
        '</xsl:stylesheet>


        Return sb.ToString
    End Function


End Class
