using InSys.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;

namespace InSys.Helpers
{
    public class EmailAnnouncement
    {
        private string SQLCon { get; set; }
        public EmailAnnouncement(string con)
        {
            try
            {
                SQLCon = con;
                SqlDependency.Start(SQLCon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void Start()
        {
            try
            {
                //if(con == null)con = new SqlConnection(SQLCon);
                //if (con.State == ConnectionState.Closed)
                //    con.Open();
                //StartSending();
                StartCollection();
            }
            catch (Exception ex)
            {
                Thread.Sleep(5000);
                Start();
            }
        }

        SqlConnection con;
        private void StartCollection()
        {
            
            try
            {
                    if (con == null) con = new SqlConnection(SQLCon);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var qry = String.Format("SELECT ID FROM dbo.tAnnouncements where ID_AnnouncementType IN(2,3) AND IsPosted = 1 AND IsSent = 0");
                    using (var cmd = new SqlCommand(qry,con))
                    {
                        var dep = new SqlDependency(cmd);
                        dep.OnChange += delegate (object sender, SqlNotificationEventArgs e)
                        {
                            OnChangeEvent(sender,e);
                        };
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            using (var dt = new DataTable())
                            {
                                da.Fill(dt);
                            }
                        }

                }
                
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Dispose();
                throw ex;
            }
            
        }
        private void OnChangeEvent(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change) {
               StartSending();
            }
            StartCollection();
        }
        private void StartSending()
        {
            try
            {
                var qry = String.Format("SELECT * FROM dbo.tAnnouncements where ID_AnnouncementType IN(2,3) AND IsPosted = 1 AND IsSent = 0 AND StartDate = CAST(GETDATE() AS DATE)");
                using (var cmd = new SqlCommand(qry,con))
                {
                    using (var adpt = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adpt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow x in dt.Rows)
                            {
                                var recipients = GetRecipients(x.Field<int>("ID"), con);
                                if (recipients.Count > 0)
                                {
                                    var c = Math.Ceiling(Convert.ToDecimal(recipients.Count) / 50);
                                    var skp = 0;
                                    for (int i = 1; i <= c; i++)
                                    {
                                        var firstFifty = recipients
                                                        .Skip(skp)
                                                        .Take(50);
                                        var comp = new AnnouncementComponent { Title = x.Field<string>("Name"), Body = x.Field<string>("Comment"), Attch = x.Field<string>("Attachment_GUID"), Recipients = firstFifty.ToList() };
                                        Send(comp);
                                        skp += 50;
                                    }
                                }
                                using (var updAnnouncement = new SqlCommand(String.Format("Update tAnnouncements set IsSent = 1 where ID = {0}", x.Field<int>("ID")), con)) { updAnnouncement.ExecuteNonQuery(); }
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void Send(AnnouncementComponent data)
        {

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("insysannouncements.noreply@gmail.com");
                    foreach (var recipient in data.Recipients)
                    {
                        if(ValidateEmailAddress(recipient))
                            mail.To.Add(recipient);
                    }
                    mail.Subject = data.Title;
                    mail.Body = data.Body;
                    mail.IsBodyHtml = true;
                    Attachment att = null;
                    if (data.Attch != null && data.Attch != "")
                    {
                        var attPath = Path.Combine(StorageSetting.StoragePath, StorageSetting.RootContainer, @"Files", data.Attch);
                        using (var att2 = new System.Net.Mail.Attachment(attPath, MediaTypeNames.Application.Octet))
                        {
                            att = att2;
                            ContentDisposition disposition = att.ContentDisposition;
                            disposition.CreationDate = System.IO.File.GetCreationTime(attPath);
                            disposition.ModificationDate = System.IO.File.GetLastWriteTime(attPath);
                            disposition.ReadDate = System.IO.File.GetLastAccessTime(attPath);
                            disposition.FileName = System.IO.Path.GetFileName(attPath);
                            disposition.Size = new System.IO.FileInfo(attPath).Length;
                            disposition.DispositionType = DispositionTypeNames.Attachment;
                            mail.Attachments.Add(att);
                            using (SmtpClient smtp = new SmtpClient())
                            {
                                smtp.Host = "smtp.gmail.com";
                                smtp.Port = 587;
                                smtp.Credentials = new NetworkCredential(
                                "insysannouncements.noreply@gmail.com", "dev123$%^");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }
                    }
                    else {
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            smtp.Credentials = new NetworkCredential(
                            "insysannouncements.noreply@gmail.com", "dev123$%^");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<string> GetRecipients(int ID_Announcement,SqlConnection con)
        {
            var dt = new DataTable();
            var list = new List<string>();
            var qry = String.Format("SELECT Recipients FROM tAnnouncementQueue where ID_Announcement = {0}",ID_Announcement);
            using (var adpt = new SqlDataAdapter(qry,con))
            {
                adpt.Fill(dt);
            }
            if (dt.Rows.Count > 0)
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row.Field<string>("Recipients"));
                }
            return list;
        }
        private bool ValidateEmailAddress(string email) {
            var result = false;
            try
            {
                MailAddress m = new MailAddress(email);
                result = true;
            } catch (FormatException)
            {
                result = false;
            }
            return result;
        }
    }
    public class AnnouncementComponent
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Attch { get; set; }
        public List<string> Recipients { get; set; }
    }
}