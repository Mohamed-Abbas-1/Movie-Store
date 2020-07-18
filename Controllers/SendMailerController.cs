using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MovieStore.Models;


namespace MovieStore.Controllers
{
    public class SendMailerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: SendMailer
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>

        /// Send Mail with Gmail

        /// </summary>

        /// <param name="objModelMail">MailModel Object, keeps all properties</param>

        /// <param name="fileUploader">Selected file data, example-filename,content,content type(file type- .txt,.png etc.),length etc.</param>

        /// <returns></returns>

        [HttpPost]

        public ActionResult Index(MovieStore.Models.Mail objModelMail, HttpPostedFileBase fileUploader,string name)

        {

            if (ModelState.IsValid)

            {

                string from = "mohamed_a56207@cic-cairo.com"; //example:- sourabh9303@gmail.com

                using (MailMessage mail = new MailMessage(from, "mohamed.abbas.cic@gmail.com"))

                {

                    mail.Subject = objModelMail.Subject; 

                    mail.Body = "Name:" + name + "\r\n" + "Message:" + objModelMail.Body;

                    if (fileUploader != null)

                    {

                        string fileName = Path.GetFileName(fileUploader.FileName);

                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));

                    }

                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential(from, "20156207");

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = networkCredential;

                    smtp.Port = 587;

                    smtp.Send(mail);

                    ViewBag.Message = "Sent";
                    db.Mails.Add(objModelMail);
                    db.SaveChanges();

                    return View("Index", objModelMail);

                }

            }

            else

            {

                return View();

            }

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}