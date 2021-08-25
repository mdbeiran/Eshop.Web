using System;
using System.Collections.Generic;
using Eshop.DomainClass.Setting;
using Eshop.ViewModel.Site;

namespace Eshop.Services.Repositories
{
    public interface ISiteRepository : IDisposable
    {

        #region Contact Us

        void UpdateContactUs(ContactUs contactUs);
        IEnumerable<ContactUs> GetAllContactUs();
        ContactUs GetContactById(int ContactID);
        /*ContactUsViewModel GetContactUsList(int pageId, string userState, DateTime? fromDate, DateTime? toDate, string name = "", string email = "", string mobile = "");*/
        void InsertContactUs(ContactUs contactUs);
        ContactUsViewModel GetContactUsList(int pageId, string contacState, DateTime? fromDate, DateTime? toDate, string name = "", string email = "", string mobile = "");

        #endregion

        #region Site Settings

        SiteSetting GetDefaultSiteSetting();
        void EditSiteSetting(SiteSetting setting);

        #endregion

    }
}