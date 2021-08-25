using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Eshop.Business.Users;
using Eshop.DomainClass.Order;
using Eshop.DomainClass.Setting;
using Eshop.Services.Context;
using Eshop.DomainClass.Product;
using Eshop.Utilitiy.Payment;

namespace Eshop.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Cunstructor

        private EshopUOW db;

        public HomeController(EshopUOW db)
        {
            this.db = db;
        }

        #endregion

        #region Index

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Slider

        #region Show Slider

        public ActionResult ShowSlider()
        {
            var sliders = db.SliderRepository.GetAllSliders().Where(s => s.IsActive);

            return PartialView(sliders);
        }

        #endregion

        #region Add Visit For Slider

        public ActionResult AddSliderVisit(int id)
        {
            var slider = db.SliderRepository.GetSliderById(id);

            if (slider != null)
            {
                slider.Visit++;
                db.Save();
            }

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #endregion

        #region Products

        #region Most Sell Products

        public ActionResult MostSellProducts()
        {
            var products = db.ProductRepository.GetMostSellProducts(12);
            return PartialView(products);
        }

        #endregion

        #region Most sell product box

        public ActionResult MostSellBox()
        {
            return PartialView();
        }

        #endregion

        #region Single Product Sell

        public ActionResult SingleProducts()
        {
            var products = db.ProductRepository.GetSingleSellProducts(12);
            return PartialView(products);
        }

        #endregion

        #region Single Product Box

        public ActionResult SingleProductBox()
        {
            return PartialView();
        }

        #endregion

        #region Public Products

        public ActionResult PublicProducts()
        {
            var products = db.ProductRepository.GetPublicSellProducts(12);
            return PartialView(products);
        }

        #endregion

        #region Public Product Box

        public ActionResult PublicProductBox()
        {
            return PartialView();
        }

        #endregion

        #region Product Category Box

        public ActionResult ProductCategoryBox()
        {
            return PartialView();
        }

        #endregion

        #endregion

        #region Contact Us

        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            var contactUsInfo = db.SiteRepository.GetDefaultSiteSetting();
            return View(contactUsInfo);
        }

        [Route("ContactUs")]
        [HttpPost]
        public ActionResult ContactUs(ContactUs contact)
        {
            if (ModelState.IsValid)
            {
                contact.CreateDate = DateTime.Now;
                if (User.Identity.IsAuthenticated)
                {
                    contact.UserId = UserManager.GetCurrentUserId();
                }

                db.SiteRepository.InsertContactUs(contact);
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return HttpNotFound();
        }

        #endregion

        #region About Us

        [Route("AboutUs")]
        public ActionResult AboutUs()
        {
            var siteSetting = db.SiteRepository.GetDefaultSiteSetting();
            return View(siteSetting);
        }

        #endregion

        #region Footer

        public ActionResult Footer()
        {
            var setting = db.SiteRepository.GetDefaultSiteSetting();
            return PartialView(setting);
        }

        #endregion

        #region Site Rules

        [Route("SiteRules")]
        public ActionResult SiteRule()
        {
            var rule = db.SiteRepository.GetDefaultSiteSetting();

            return View(rule);
        }

        #endregion

        #region User Wish List

        #region List

        public ActionResult WishList()
        {
            List<Product> list = new List<Product>();

            if (Session["PublicWish"] != null)
            {
                list = Session["PublicWish"] as List<Product>;
            }

            List<Product> list2 = new List<Product>();
            if (Session["SingleWish"] != null)
            {
                list2 = Session["SingleWish"] as List<Product>;
            }

            Tuple<List<Product>, List<Product>, List<ProductGroup>> data = new Tuple<List<Product>, List<Product>, List<ProductGroup>>(list, list2, db.ProductRepository.GetAllActiveGroups());

            return View(data);
        }

        #endregion

        #region Add Public

        public ActionResult AddPublicWishList(int id)
        {
            List<Product> list = new List<Product>();

            if (Session["PublicWish"] != null)
            {
                list = Session["PublicWish"] as List<Product>;
            }

            var product = db.ProductRepository.GetProductById(id);

            if (product != null && !list.Any(s => s.ProductId == id))
            {
                list.Add(product);
            }

            Session["PublicWish"] = list;

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #region Delet Public Wish

        public ActionResult DeletePublicWish(int id)
        {
            List<Product> SingelWishes = new List<Product>();
            SingelWishes = Session["PublicWish"] as List<Product>;
            var selected = SingelWishes.SingleOrDefault(s => s.ProductId == id);
            if (selected != null)
            {
                SingelWishes.Remove(selected);
            }

            Session["PublicWish"] = SingelWishes;

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #region Add Single

        public ActionResult AddSingleWishList(int id)
        {
            List<Product> list = new List<Product>();

            if (Session["SingleWish"] != null)
            {
                list = Session["SingleWish"] as List<Product>;
            }

            var product = db.ProductRepository.GetProductById(id);
            if (product != null && !list.Any(s => s.ProductId == id))
            {
                list.Add(product);
            }

            Session["SingleWish"] = list;

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #region Delet Single Wish

        public ActionResult DeleteSingleWish(int id)
        {
            List<Product> SingelWishes = new List<Product>();
            SingelWishes = Session["SingleWish"] as List<Product>;
            var selected = SingelWishes.SingleOrDefault(s => s.ProductId == id);
            if (selected != null)
            {
                SingelWishes.Remove(selected);
            }

            Session["SingleWish"] = SingelWishes;

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #endregion

        #region CkEditor Upload Image


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


        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {

            //http://stackoverflow.com/a/4088194/167670
            //http://arturito.net/2010/11/03/file-and-image-upload-with-asp-net-mvc2-with-ckeditor-wysiwyg-rich-text-editor/
            //http://haacked.com/archive/2010/07/16/uploading-files-with-aspnetmvc.aspx

            if (upload.ContentLength <= 0)
                return null;

            const string uploadFolder = "Upload/EditorUpload/";
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(upload.FileName).ToLower();
            var path = Path.Combine(Server.MapPath(string.Format("~/{0}", uploadFolder)), fileName);
            upload.SaveAs(path);

            var url = string.Format("{0}{1}/{2}{3}", "", Request.ApplicationPath == "/" ? string.Empty : Request.ApplicationPath, uploadFolder, fileName);
            const string message = "Image was saved correctly";
            var output = string.Format("<html><body><script>window.parent.CKEDITOR.tools.callFunction({0}, \"{1}\", \"{2}\");</script></body></html>",
                CKEditorFuncNum, url, message);

            return Json(new { uploaded = true, url });
        }

        #endregion

        #region Verify Payment

        [Route("OnlinePayment/{id}")]
        public ActionResult Verify(int id)
        {
            ViewBag.Sum = 0;
            ViewBag.Maliat = 0;
            ViewBag.Final = 0;
            ViewBag.MinimumCount = 0;
            ViewBag.TotalCount = 0;
            int MinimumCount = db.ProductRepository.GetMinimumForPublicProducts();
            Order userCart = db.ProductRepository.GetUserOrderById(id);

            if (userCart != null && userCart.IsFinally)
            {
                return Redirect("/");
            }

            List<OrderDetail> orderDetails = userCart.OrderDetails.ToList();
            int sum = 0;
            ViewBag.TotalCount = orderDetails.Sum(s => s.Count);
            foreach (var detail in orderDetails)
            {
                if (ViewBag.TotalCount > MinimumCount)
                {
                    sum += detail.Count * detail.Product.PublicPrice;
                }
                else
                {
                    sum += detail.Count * detail.Product.SinglePrice;
                }
            }

            var maliat = (sum * 9) / 100;
            var final = sum + maliat;
            ViewBag.Sum = sum;
            ViewBag.Maliat = maliat;
            ViewBag.Final = final;
            ViewBag.MinimumCount = MinimumCount;

            Tuple<int, long> status = Payments.ComingBackPayment(id, final);
            ViewBag.Status = status.Item2;
            switch (status.Item1)
            {
                case -1:
                    ViewBag.Massage = "اطلاعات ارسال شده ناقص است ";
                    break;
                case -2:
                    ViewBag.Massage = "در خواست مورد نظر یافت نشد ";
                    break;
                case -22:
                    ViewBag.Massage = "تراکنش ناموفق میباشد ";
                    break;
                case -33:
                    ViewBag.Massage = "ملبغ تراکنش با مبلغ پرداخت شده مطاقبت دارد ";
                    break;
                case 100:
                case 101:
                    {
                        ViewBag.Success = true;
                        userCart.IsFinally = true;
                        userCart.StatusCode = status.Item2.ToString();
                        db.Save();
                        break;
                    }
            }

            return View(userCart);
        }

        #endregion

    }
}