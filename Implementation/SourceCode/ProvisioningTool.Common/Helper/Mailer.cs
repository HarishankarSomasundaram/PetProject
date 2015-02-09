using System;
using System.Collections.Generic;
using System.Net.Mail;
using Library;


namespace ProvisioningTool.Common
{
    /// <summary>
    /// Represents the Email Message
    /// </summary>
    public class Mailer
    {

        #region "Private Variables"
        string from;
        string fromName;
        string to;
        string cc;
        string subject;
        string host;
        string port;
        string body;
        string userName;
        string password;
        #endregion "Private Variables"

        #region "Public Variables"
        public bool UseSSL { get; set; }
        public string From { get; set; }
        public string FromName
        {
            get { return fromName; }
            set { fromName = value; }
        }
        public string To
        {
            get { return to; }
            set { to = value; }
        }
        public string Cc
        {
            get { return cc; }
            set { cc = value; }
        }
        public string Bcc { get; set; }
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        public string Host
        {
            get { return host; }
            set { host = value; }
        }
        public string Body
        {
            get { return body; }
            set { body = value; }
        }
        #endregion "Public Variables"

        public Mailer()
        {
            try
            {
                string sUseSSL = System.Configuration.ConfigurationManager.AppSettings["useSSL"];
                if (sUseSSL == null || sUseSSL == "")
                    UseSSL = false;
                else
                    UseSSL = ConvertHelper.ConvertToBoolean(sUseSSL);
            }
            catch (Exception)
            {
                UseSSL = false;
            }
            from = System.Configuration.ConfigurationManager.AppSettings["from"];
            fromName = from;
            host = System.Configuration.ConfigurationManager.AppSettings["smtphost"];
            port = System.Configuration.ConfigurationManager.AppSettings["smtpport"];
            userName = System.Configuration.ConfigurationManager.AppSettings["emailUsername"];
            password = System.Configuration.ConfigurationManager.AppSettings["emailPassword"];
            Bcc = ConvertHelper.ConvertToString(System.Configuration.ConfigurationManager.AppSettings["BCC"], "");
        }

        public void SendMail(List<string> AttachFile)
        {
            SendMail(from, AttachFile);
        }


        public void SendMail(string _from, List<string> AttachFile)
        {
            SendMail(_from, null, AttachFile);
        }
        public void SendMail(string _from, string to, List<string> AttachFile)
        {
            //Mail will not be sent if from address or host or port not available 
            if (string.IsNullOrEmpty(_from) || string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port))
                return;
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            //try
            //{
            MailAddress fromAddress = new MailAddress(from, _from, System.Text.Encoding.UTF8);
            smtpClient.Host = host;
            smtpClient.Port = Convert.ToInt32(port);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = UseSSL;

            smtpClient.Credentials = new System.Net.NetworkCredential(from, password);

            message.From = fromAddress;
            if (!string.IsNullOrEmpty(to))
                message.To.Add(to);
            else
                return;
            message.Subject = subject;
            if (to != null)
            {
                if ((to != "") && (to.LastIndexOf(";") >= 0))
                {
                    string[] strSplit = to.Split(';');

                    int _intValue = 0;
                    for (_intValue = 0; _intValue < strSplit.Length; _intValue++)
                    {
                        if (!string.IsNullOrEmpty(strSplit[_intValue]))
                            message.To.Add(strSplit[_intValue]);
                    }
                }
                else if ((to != "") && (to.LastIndexOf(",") >= 0))
                {
                    string[] strSplit = to.Split(',');

                    int _intValue = 0;
                    for (_intValue = 0; _intValue < strSplit.Length; _intValue++)
                    {
                        if (!string.IsNullOrEmpty(strSplit[_intValue]))
                            message.To.Add(strSplit[_intValue]);
                    }
                }
                else
                {
                    if (to != "")
                        message.To.Add(to);
                }
            }
            if (cc != null)
            {
                if ((cc != "") && (cc.LastIndexOf(",") >= 0))
                {
                    string[] strSplit = cc.Split(',');

                    int _intValue = 0;
                    for (_intValue = 0; _intValue < strSplit.Length; _intValue++)
                    {
                        if (!string.IsNullOrEmpty(strSplit[_intValue]))
                            message.CC.Add(strSplit[_intValue]);
                    }
                }
                else
                {
                    if (cc != "")
                        message.CC.Add(cc);
                }
            }
            if (Bcc != null)
            {
                if ((Bcc != "") && (Bcc.LastIndexOf(",") >= 0))
                {
                    string[] strSplit = Bcc.Split(',');

                    int _intValue = 0;
                    for (_intValue = 0; _intValue < strSplit.Length; _intValue++)
                    {
                        if (!string.IsNullOrEmpty(strSplit[_intValue]))
                            message.Bcc.Add(strSplit[_intValue]);
                    }
                }
                else
                {
                    if (Bcc != "")
                        message.Bcc.Add(Bcc);
                }
            }

            if (AttachFile != null && AttachFile.Count > 0)
            {
                for (int i = 0; i < AttachFile.Count; i++)
                {
                    /* Create the email attachment with the uploaded file */
                    Attachment attach = new Attachment(AttachFile[i].ToString());
                    /* Attach the newly created email attachment */
                    message.Attachments.Add(attach);
                }
            }

            //message.Bcc.Add(new MailAddress("mailercopy2@einztion.com"));
            message.IsBodyHtml = true;
            message.Body = body;
            smtpClient.Send(message);

        }

        public static void SendEmail(string subject, string to, string cc, string body, List<string> AttachFiles)
        {
            SendEmail(subject, null, to, cc, body, AttachFiles);
        }
        public static void SendEmail(string subject, string from, string to, string cc, string body)
        {
            SendEmail(subject, from, to, cc, body, null);
        }
        public static void SendEmail(string subject, string _from, string to, string cc, string body, List<string> AttachFiles)
        {
            string toMailAddress = string.Empty;
            string CCMailAddress = string.Empty;
            toMailAddress = to;
            CCMailAddress = cc;
            bool isTesting = ConvertHelper.ConvertToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsTesting"], false);

            if (isTesting)
            {

                toMailAddress = ConvertHelper.ConvertToString(System.Configuration.ConfigurationManager.AppSettings["ToEmail"]);
                CCMailAddress = ConvertHelper.ConvertToString(System.Configuration.ConfigurationManager.AppSettings["CCEmailTesting"]);
                //For testing: we can't change from name when we use gmail as smtp, so we assigned testing from name to send email.
                _from = ConvertHelper.ConvertToString(System.Configuration.ConfigurationManager.AppSettings["from"], "");

            }
            else
            {
                toMailAddress = to;
                CCMailAddress = cc;
            }
            Mailer objMailer = new Mailer();
            if (ConvertHelper.ConvertToString(_from) != null)
            {
                objMailer.from = _from;
                objMailer.fromName = _from;
            }
            objMailer.To = toMailAddress;
            objMailer.Cc = CCMailAddress;
            objMailer.Subject = subject;
            objMailer.Body = body;
            if (ConvertHelper.ConvertToString(_from) != null)
                objMailer.SendMail(_from, to, AttachFiles);
            else
                objMailer.SendMail(AttachFiles);
        }
    }
}
