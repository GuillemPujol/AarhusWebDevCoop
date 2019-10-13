using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Umbraco.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;


namespace AarhusWebDevCoop.Controllers
{
    //inherits from the Surface Controller
    public class ContactFormSurfaceController : SurfaceController
    {
        [HttpGet]
        public ActionResult Index()
        {
            //display the partial view contact form
            return PartialView("_ContactForm", new ContactForm());
        }

        // post method that creates a new message, adds it to my email and selects the subject, email and the message from the form.
        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            MailMessage message = new MailMessage();
            message.To.Add("guille8.bcn@gmail.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message;

            // if we get errors we return the same page with the validation warnings and not an error page
            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }

            
            // we use the smtp in order to send the message to my email
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("guille8.bcn@gmail.com", "*********");
                // send mail 
                smtp.Send(message);
            }

            // We use the temporal variable to use it in the view so we know if the message was sent or not
            TempData["success"] = true;

            // This code creates a comment type document behind the message document and save all the messages received
                //gets the GuidUi of the current page
                GuidUdi currentPageUdi = new GuidUdi(CurrentPage.ContentType.ItemType.ToString(), CurrentPage.Key);

                // Creates the new content type
                IContent msg = Services.ContentService.CreateContent(model.Subject, currentPageUdi, "message");
                msg.SetValue("messageName", model.Name);
                msg.SetValue("email", model.Email);
                msg.SetValue("subject", model.Subject);
                msg.SetValue("messageContent", model.Message);
                msg.SetValue("umbracoNaviHide", true);

                Services.ContentService.Save(msg);

            return RedirectToCurrentUmbracoPage();
        }
        

    }
}