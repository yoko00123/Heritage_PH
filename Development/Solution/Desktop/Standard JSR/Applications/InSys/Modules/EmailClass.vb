Option Explicit On
Option Strict On

Imports System.Net.Mail
Imports ndb = GSCOM.Applications.InSys
Imports System.Net.Mime

Public Class EmailClass

    Private Server As String
    Private Port As Integer
    Private SSLEnabled As Boolean

    Public MailFrom As String
    Public MailTo As String
    Public CC As String
    Public Subject As String
    Public Body As String
    Public IsBodyHTML As Boolean
    Public Attachments As ArrayList
    Public SenderEMail As String
    Public SenderPassword As String
    Public mm As New MailMessage

    Public Sub SetServerProperties() 'ByVal Server As String, ByVal Port As Integer, ByVal SSLEnabled As Boolean
        Me.Server = nDB.GetSetting("SMTPServer")
        Me.Port = CInt(nDB.GetSetting("Port"))
        Me.SSLEnabled = CBool(nDB.GetSetting("SSLEnabled"))
    End Sub

    Public Sub SetMailProperties(ByVal MailFrom As String _
                                 , ByVal MailTo As String _
                                 , ByVal Subject As String _
                                 , ByVal IsBodyHTML As Boolean _
                                 , ByVal Attachments As ArrayList _
                                 , ByVal SenderEMail As String _
                                 , ByVal SenderPassword As String _
                                 , ByVal CC As String) ', ByVal Body As String _
        Me.MailFrom = MailFrom
        Me.MailTo = MailTo
        Me.Subject = Subject
        Me.Body = Body
        Me.IsBodyHTML = IsBodyHTML
        Me.Attachments = Attachments
        Me.SenderEMail = SenderEMail
        Me.SenderPassword = SenderPassword
        Me.CC = CC
    End Sub
    Public Function CreateEmailView(ByVal pImage As String, ByVal HtmlContent As String) As String
        Dim HtmlView As AlternateView = AlternateView.CreateAlternateViewFromString(HtmlContent, Nothing, MediaTypeNames.Text.Html)


        Dim ImageView As LinkedResource = New LinkedResource(pImage, MediaTypeNames.Image.Gif)


        ImageView.ContentId = "ImageID"
        ImageView.TransferEncoding = TransferEncoding.Base64
        HtmlView.LinkedResources.Add(ImageView)
        mm.AlternateViews.Add(HtmlView)

        Return mm.Body
    End Function

    Public Sub Send()
        'Dim mm As New MailMessage '(MailFrom, MailTo, Subject, Body)


        With mm
            .From = New MailAddress(MailFrom)
            For Each s As String In MailTo.Split(CChar(";"))
                If s <> "" Then
                    .To.Add(New MailAddress(s))
                End If
            Next

            For Each ss As String In CC.Split(CChar(";"))
                If ss <> "" Then
                    .Bcc.Add(New MailAddress(ss))
                End If
            Next
            .CC.Add(MailFrom)

            '.To.Add(MailTo)

            .Subject = Me.Subject
            '.Body = Me.Body
            .IsBodyHtml = Me.IsBodyHTML
            .Priority = MailPriority.High
            .BodyEncoding = System.Text.Encoding.Default

            If Attachments IsNot Nothing Then
                For Each a As Attachment In Me.Attachments
                    mm.Attachments.Add(a)
                Next
            End If
        End With



        Dim SMTPServer As New SmtpClient(Me.Server, Me.Port)
        SMTPServer.Credentials = New Net.NetworkCredential(Me.SenderEMail, Me.SenderPassword)
        SMTPServer.EnableSsl = Me.SSLEnabled

        'SMTPServer.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIi
        SMTPServer.Send(mm)

    End Sub


End Class

