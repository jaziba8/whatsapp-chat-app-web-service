using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public class student
        {
            public string id, name;
        }
        WhatsAppEntities1 db = new WhatsAppEntities1();
       
        


        public student[] getcountry(string id, string name)
        {
            student std = new student();
            std.id = id;
            std.name = name;

            
            student[] a = new student[3];
            a[0] = std;
            a[1] = std;

            a[2] = std;

            return a;
        }

        string IService1.checkuserfrombiitdb(string phone, string email)
        {
           var record =  db.Biit_Data.Where(e => e.mobile_no == phone && e.Email.ToLower() == email.ToLower()).FirstOrDefault();
            if (record == null)
            {
                return "false";
            }
            return "true";
        }





        public String SendMail(String To)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["FromEmail"];//add refenence using System.Configuration;

                string pass = ConfigurationManager.AppSettings["Password"];//add refenence using System.Configuration;

                string host = ConfigurationManager.AppSettings["Host"];//add refenence using System.Configuration;

               Random r1 = new Random();
               int verifcationcode = r1.Next(100000, 999999);



                Biit_Data var = new Biit_Data();
                var = db.Biit_Data.FirstOrDefault(e => e.Email == To);
                var.VerificationCode = verifcationcode.ToString();
                db.SaveChanges();


                string to = To.ToString(); //To address    
                string from = fromEmail.ToString(); //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "Your Verification Code is "+verifcationcode;

                message.Subject = "BIIT WhatsApp Verification Code";
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(host.ToString(), 587);      
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential(fromEmail.ToString(), pass.ToString());

                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;

                client.Send(message);
            }

            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return "true";

        }

        public string isverfieduser(string phone)
        {
            try
            {
                var mobile = db.Biit_Data.FirstOrDefault(e => e.mobile_no == phone);
                if (mobile!=null)
                {
                    string isverified = mobile.IsVerified;
                    if (isverified == "1")
                    {
                        return "true";
                    }
                    else
                        return "false";
                }

            }
            catch (Exception)
            {

                
            }
            return "false";
        }



        public string verifyusercode(string code,string phone)
        {
            try
            {
                Biit_Data var = new Biit_Data();
                var = db.Biit_Data.FirstOrDefault(e => e.VerificationCode == code && e.mobile_no == phone);

                var.IsVerified = "1";
                db.SaveChanges();
                return "true";
            }
            catch (Exception)
            {
                return "false";
            }
     
        }
    }


}