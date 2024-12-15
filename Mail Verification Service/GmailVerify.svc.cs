using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;


namespace Mail_Verification_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GmailVerify" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GmailVerify.svc or GmailVerify.svc.cs at the Solution Explorer and start debugging.
    public class GmailVerify : IGmailVerify
    {
        public string SendMail(string email, string username)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("PSS", ""));
            message.To.Add(new MailboxAddress(username, email));

            string key = "";
            int i = 0; 
            
            while(i<4)
            {
                key += new Random().Next(10).ToString();
            }

            message.Subject = key;


            try
            {
                using(var smtpClient= new SmtpClient())
                {
                    smtpClient.Connect("smtp.gmail.com", 465, true);
                    smtpClient.Authenticate("projectsubmissionp@gmail.com", "PSPemail2024");
                    smtpClient.Send(message);
                    smtpClient.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return key;
        }
    }
}
