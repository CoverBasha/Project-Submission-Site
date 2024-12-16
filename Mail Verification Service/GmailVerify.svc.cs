using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Mail_Verification_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GmailVerify" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GmailVerify.svc or GmailVerify.svc.cs at the Solution Explorer and start debugging.
    public class GmailVerify : IGmailVerify
    {
		private static Random Ranomizer = new Random();

        public string SendMail(string email, string username)
        {
			string key = "";
			int i = 0;

			while (i++ < 4)
			{
				key += Ranomizer.Next(10).ToString();
			}

			try
			{
				var fromAddress = new MailAddress("projectsubmissionp@gmail.com", "Project Submission");
				var toAddress = new MailAddress(email, username);
				const string fromPassword = "xnhx rtag fmfr csga";
				const string subject = "Verify Email - Registration";

				string body = $"Dear {username},<br><br>You requested to use this email address to access your account.<br><br>Please enter this code to verify your email: <b>{key}</b>";

				var smtp = new SmtpClient
				{
					Host = "smtp.gmail.com",
					Port = 587,
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
					Timeout = 20000
				};

				using (var message = new MailMessage(fromAddress, toAddress)
				{
					Subject = subject,
					Body = body,
					IsBodyHtml = true,
				})
				{
					smtp.Send(message);
				}
			}
			catch (Exception ex)
			{
				return null;
			}

			return key;
		}
    }
}
