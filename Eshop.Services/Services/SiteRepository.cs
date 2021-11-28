using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Eshop.DataLayer.Contexts;
using Eshop.DomainClass.Setting;
using Eshop.Services.Repositories;
using Eshop.ViewModel.Site;

namespace Eshop.Services.Services
{
    public class SiteRepository : ISiteRepository
    {
        #region Ctor

        private EshopDbContext db;

        public SiteRepository(EshopDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Contact Us

        public void UpdateContactUs(ContactUs contactUs)
        {
            var local = db.Set<ContactUs>()
                .Local
                .FirstOrDefault(f => f.UserId == contactUs.ContactId);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }

            db.Entry(contactUs).State = EntityState.Modified;

        }

        public IEnumerable<ContactUs> GetAllContactUs()
        {
            return db.ContactUs.ToList();
        }

        public ContactUs GetContactById(int ContactID)
        {
            return db.ContactUs.Find(ContactID);
        }

        public void InsertContactUs(ContactUs contactUs)
        {
            db.ContactUs.Add(contactUs);
        }

        public ContactUsViewModel GetContactUsList(int pageId, string contacState, DateTime? fromDate, DateTime? toDate, string name = "", string email = "", string mobile = "")
        {
            int take = 10;
            int skip = (pageId - 1) * take;
            IQueryable<ContactUs> contactList = db.ContactUs;

            #region User State

            switch (contacState)
            {
                case "Read":
                    {
                        contactList = contactList.Where(u => u.IsRead);
                        break;
                    }

                case "NotRead":
                    {
                        contactList = contactList.Where(u => !u.IsRead);
                        break;
                    }
                case "Answered":
                    {
                        contactList = contactList.Where(u => !string.IsNullOrEmpty(u.Answer));
                        break;
                    }
                case "NotAnswered":
                    {
                        contactList = contactList.Where(u => string.IsNullOrEmpty(u.Answer) && u.IsRead);
                        break;
                    }
            }

            #endregion

            ContactUsViewModel returnContacts = new ContactUsViewModel();

            #region implementation filters

            if (!string.IsNullOrEmpty(name))
            {
                contactList = contactList.Where(u => u.Name.Contains(name));
            }


            if (!string.IsNullOrEmpty(mobile))
            {
                contactList = contactList.Where(u => u.Mobile.Contains(mobile));
            }

            if (fromDate != null)
            {
                DateTime fromdate = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, new PersianCalendar());
                contactList = contactList.Where(u => u.CreateDate >= fromdate);
                returnContacts.fromDate = fromDate.Value;
            }

            if (toDate != null)
            {
                DateTime todate = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, new PersianCalendar());
                contactList = contactList.Where(u => u.CreateDate <= todate);
                returnContacts.toDate = toDate.Value;
            }

            #endregion

            #region paging

            int thisPageCount = contactList.Count();
            if (thisPageCount % take > 0)
            {
                returnContacts.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                returnContacts.PageCount = thisPageCount / take;
            }


            #endregion

            #region fill filters fields

            returnContacts.ActivePage = pageId;
            returnContacts.Name = name;
            returnContacts.ContactState = contacState;
            returnContacts.FilterEmail = email;
            returnContacts.FilterMobile = mobile;
            returnContacts.StartPage = pageId - 3;
            returnContacts.EndPage = returnContacts.ActivePage + 3;
            if (returnContacts.StartPage <= 0) returnContacts.StartPage = 1;
            if (returnContacts.EndPage > returnContacts.PageCount) returnContacts.EndPage = returnContacts.PageCount;

            #endregion

            returnContacts.Contact = contactList.OrderByDescending(u => u.UserId).Skip(skip).Take(take).AsNoTracking().ToList();

            return returnContacts;
        }

        #endregion

        #region Site Settings

        public SiteSetting GetDefaultSiteSetting()
        {
            var setting = db.SiteSettings.FirstOrDefault();
            if (setting != null)
            {

                return setting;
            }

            return new SiteSetting()
            {
                SiteName = "Test",
                SiteEmail = "test@test.com",
                SiteDescription = "test"
            };
        }

        public void EditSiteSetting(SiteSetting setting)
        {
            var local = db.Set<SiteSetting>()
              .Local
              .FirstOrDefault(f => f.SettingID == setting.SettingID);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }

            db.Entry(setting).State = EntityState.Modified;

        }


        #endregion

        #region Dispose
        public void Dispose()
        {
            db.Dispose();
        }
        #endregion
    }
}
