﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO; 

namespace nhietdokhovacxin
{
    public class Email
    {
        public MailMessage Message;
        public SmtpClient Smtp;
        public bool Error = false;
        protected string _Host = "smtp.gmail.com";
        protected int _Port = 587;
        protected int _TimeOut = 10000;
        protected string _CredentialsUser = "";
        protected string _CredentialsPass = "";
        protected List<string>  _FileLocs = new List<string>();
        protected bool _EnableSSL = true;

        [Description("Email Host, Default is for Gmail")]
        [Browsable(true), Category("ATSCADA Settings")]
        public string Host
        {
            get { return _Host; }
            set { _Host = value; }
        }

        [Description ("Email Port, Default for Gmail Port is 587")]
        [Browsable(true), Category("ATSCADA Settings")]
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        [Description("Email Sending Timeout, Default is 10000")]
        [Browsable(true), Category("ATSCADA Settings")]
        public int TimeOut
        {
            get { return _TimeOut; }
            set { _TimeOut = value; }
        }

        [Description("Enscrypted connection SSL, Default is true")]
        [Browsable(true), Category("ATSCADA Settings")]
        public bool EnableSSL
        {
            get { return _EnableSSL; }
            set { _EnableSSL = value; }
        }
        [Description("Attached Files Locations")]
        [Browsable(false), Category("ATSCADA Settings")]
        public List<string>  AttachFiles
        {            
            get { return _FileLocs; }
            set { _FileLocs = value; }
        }

        [Description("Credential Email, if not set, the Default Account will be used")]
        [Browsable(true), Category("ATSCADA Settings")]
        public string CredentialEmail
        {
            get { return _CredentialsUser; }
            set { _CredentialsUser = value; }
        }

        [Description("Credential Password")]
        [Browsable(true), Category("ATSCADA Settings")]
        public string CredentialPass
        {
            get { return _CredentialsPass; }
            set { _CredentialsPass = value; }
        }

        public Email()
        {
            Message = new MailMessage();            
            Smtp = new SmtpClient();
        }

        /// <summary>
        /// Gửi Email
        /// </summary>
        public void SendEmail()
        {
            try
            {
                
                Smtp.Host = _Host;
                Smtp.Port = _Port;

                Smtp.EnableSsl = _EnableSSL;

                Smtp.Timeout = _TimeOut;

                Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                
                Smtp.UseDefaultCredentials = false;

                if (_CredentialsUser == "" && _Host == "smtp.gmail.com")
                {
                    Error = true;
                }
                else
                {
                    Smtp.Credentials = new System.Net.NetworkCredential(_CredentialsUser, _CredentialsPass);
                }
                Smtp.Send(Message);       
                Error = false; 
            }
            catch (SmtpException ex) { Error = true; }
        }
    }
}
