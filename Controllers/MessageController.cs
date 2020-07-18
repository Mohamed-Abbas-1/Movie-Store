using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string name ,string email, string subject, string message )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("mohamed_a56207@cic-cairo.com", name);
                    var receiverEmail = new MailAddress("mohamed.abbas.cic@gmail.com", "Mohamed");
                    var userName = name; 
                      var userEmail = email;
                    var password = "20156207";
                    var sub = subject;
                    var body ="Name:" + name + "\r\n" + "Message:"+ message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)

                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View("index");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View("index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}