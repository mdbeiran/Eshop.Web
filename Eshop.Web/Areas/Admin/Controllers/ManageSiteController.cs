using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Eshop.Business.Filter;
using Eshop.DomainClass.Setting;
using Eshop.Services.Context;
using Eshop.Utilitiy.Convertor;

namespace Eshop.Web.Areas.Admin.Controllers
{
    public class ManageSiteController : Controller
    {
        #region Ctor

        private EshopUOW db;
        public ManageSiteController(EshopUOW db)
        {
            this.db = db;
        }

        #endregion

        #region ContactUs

        #region Contact List

        [EshopPermission("ManageContactUs")]
        public ActionResult ContactUs(DateTime? fromDate, DateTime? toDate, string Name = "", string FilterEmail = "", string FilterMobile = "", string contactState = "NotRead", int pageId = 1)
        {
            var contacts =
                db.SiteRepository.GetContactUsList(pageId, contactState, fromDate, toDate, Name, FilterEmail, FilterMobile);
            
            return View(contacts);
        }

        #endregion

        #region Show Message

        [EshopPermission("ManageContactUs")]
        public ActionResult ShowMessage(int id)
        {
            var contact = db.SiteRepository.GetContactById(id);
            contact.IsRead = true;
            db.SiteRepository.UpdateContactUs(contact);
            db.Save();

            return View(contact);
        }

        #endregion

        #region Answer To Contact

        [EshopPermission("AnswerToContact")]
        public ActionResult AnswerContact(int id)
        {
            var contact = db.SiteRepository.GetContactById(id);
            if (contact != null)
            {
                return View(contact);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerContact(ContactUs contact)
        {
            ContactUs editedContact = db.SiteRepository.GetContactById(contact.ContactId);

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(contact.Answer))
                {
                    editedContact.Answer = contact.Answer;
                    db.SiteRepository.UpdateContactUs(editedContact);
                    db.Save();

                    return RedirectToAction("ContactUs");
                }
            }

            return View(editedContact);
        }

        #endregion

        #endregion

        #region Edit Site Settings

        [EshopPermission("SiteSetting")]
        public ActionResult EditSiteSetting()
        {
            ViewBag.Success = "null";
            var siteSetting = db.SiteRepository.GetDefaultSiteSetting();

            return View(siteSetting);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSiteSetting(SiteSetting setting)
        {
            var siteSetting = db.SiteRepository.GetDefaultSiteSetting();

            if (ModelState.IsValid)
            {
                List<string> preImages = FetchLinksFromSource(siteSetting.SiteDescription);
                List<string> lastImages = FetchLinksFromSource(setting.SiteDescription);
                foreach (var pre in preImages)
                {
                    var x = lastImages.Find(p => p == pre);
                    if (x == null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(pre)))
                        {
                            System.IO.File.Delete(Server.MapPath(pre));
                        }
                    }
                }

                db.SiteRepository.EditSiteSetting(setting);
                db.Save();
                ViewBag.Success = "yes";
                return View("EditSiteSetting", setting);
            }

            ViewBag.Success = "no";
            return View(setting);
        }

        #endregion
        
        #region Delete CkEditor Image


        /*      List<string> links = FetchLinksFromSource(Text that Have Images);
                foreach (var image in links)
                { 
                    if (System.IO.File.Exists(Server.MapPath(image)))
                    {
                        System.IO.File.Delete(Server.MapPath(image));
                    }
                }
        */

        public List<string> FetchLinksFromSource(string htmlSource)
        {
            List<string> links = new List<string>();
            string regexImgSrc = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match m in matchesImgSrc)
            {
                string href = m.Groups[1].Value;
                links.Add(href);
            }
            return links;
        }


        #endregion

    }
}