using System.Net;
using System.Net.Mail;

namespace Company.Project.PresentationLayer.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            try
            {


                // Mail Server: Gmail
                //SMTP Protocol to transfer emails 

                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                //sender
                client.Credentials = new NetworkCredential("mariamsamy811@gmail.com", "dzcngmxyyqoycpgo");
                //dzcn gmxy yqoy cpgo
                //dzcn gmxy yqoy cpgo

                client.Send("mariamsamy811@gmail.com", email.To, email.Subject, email.Body);


                return true;
            }
            catch(Exception e) {
                return false;
                    }
        }
    }
}
