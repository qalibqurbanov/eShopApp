using System.Globalization;
using System;
using System.Net;
using System.Net.Mail;
using eShopApp.WebUI.Identity.Services.Abstract;

namespace eShopApp.WebUI.Identity.Services.Concrete
{
    /// <summary>
    /// Hotmail ile mail gonderme funksionalligini saxlayan sinif.
    /// </summary>
    public class HotmailEmailSender : IEmailSender
    {
        /* Ferqli/pullu bir smtp serverini iwletmek istesem onun host adresini yazacam  */
        private string _hostAddress { get; set; }
        private ushort _port        { get; set; }
        private bool _enableSSL     { get; set; }
        private string _devEmail    { get; set; }
        private string _devPassword { get; set; }

        public HotmailEmailSender(string HostAddress, ushort Port, bool EnableSSL, string DevEmail, string DevPassword)
        {
            this._hostAddress = HostAddress;
            this._port        = Port;
            this._enableSSL   = EnableSSL;
            this._devEmail    = DevEmail;
            this._devPassword = DevPassword;
        }

        public async Task SendEmailAsync(string UserEmailAddress, string MailSubject, string MessageContent)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Host                  = this._hostAddress;
                client.Port                  = this._port;
                client.EnableSsl             = this._enableSSL;
                client.Timeout               = 60000; /* 60 saniye */
                client.UseDefaultCredentials = false;
                client.Credentials           = new NetworkCredential(this._devEmail, this._devPassword);

                await client.SendMailAsync
                (
                    new MailMessage(this._devEmail, UserEmailAddress, MailSubject, MessageContent)
                    {
                        IsBodyHtml = true
                    }
                );
            };
        }
    }
}