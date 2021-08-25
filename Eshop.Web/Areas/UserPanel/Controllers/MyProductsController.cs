using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eshop.Business.Users;
using Eshop.Services.Context;
using Eshop.Utilitiy.Payment;

namespace Eshop.Web.Areas.UserPanel.Controllers
{
    using Eshop.DomainClass.Order;

    [RouteArea("UserPanel", AreaPrefix = "User")]
    [RoutePrefix("")]
    [Authorize]
    public class MyProductsController : Controller
    {
        #region ctor

        private EshopUOW db;
        public MyProductsController(EshopUOW db)
        {
            this.db = db;
        }

        #endregion

        #region Shop Cart

        #region Index

        [Route("ShopCart", Name = "ShopCart")]
        public ActionResult ShopCart()
        {
            ViewBag.Sum = 0;
            ViewBag.Maliat = 0;
            ViewBag.Final = 0;
            ViewBag.MinimumCount = 0;
            ViewBag.TotalCount = 0;
            int userId = UserManager.GetCurrentUserId();
            int MinimumCount = db.ProductRepository.GetMinimumForPublicProducts();
            Order userCart = db.ProductRepository.GetUserOpenOrder(userId);
            if (userCart != null)
            {
                List<OrderDetail> orderDetails = userCart.OrderDetails.ToList();
                long sum = 0;
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

                var final = sum;
                ViewBag.Sum = sum;
                ViewBag.Final = final;
                ViewBag.MinimumCount = MinimumCount;
            }

            return View(userCart);
        }

        #endregion

        #region Edit Order Detail

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrderDetail(int DetailId, int count, List<int> selectedFeatures, int order)
        {
            var isNumeric = int.TryParse(count.ToString(), out count);

            if (!isNumeric || count <= 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var detail = db.ProductRepository.GetOrderDetailById(DetailId);

            int userId = UserManager.GetCurrentUserId();

            if (detail.Order.UserId != userId)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            detail.Count = count;
            db.Save();

            db.ProductRepository.RemoveOrderDetailSelectedFeatures(DetailId);
            db.Save();

            if (selectedFeatures != null)
            {
                db.ProductRepository.InsertOrderSelectedFeatures(DetailId, selectedFeatures);
                db.Save();
            }

            db.ProductRepository.SetOrderDetailPrices(order);

            db.Save();


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Delete Order Detail

        public ActionResult DeleteOrderDetail(int id)
        {
            var detail = db.ProductRepository.GetOrderDetailById(id);
            if (detail != null)
            {
                db.ProductRepository.RemoveOrderDetail(detail);
                db.Save();
                db.ProductRepository.SetOrderDetailPrices(detail.OrderId);
                db.Save();

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Pay Ordr Price


        [Route("Payment", Name = "Payment")]
        public ActionResult Payment()
        {
            int userId = UserManager.GetCurrentUserId();
            int MinimumCount = db.ProductRepository.GetMinimumForPublicProducts();
            Order userCart = db.ProductRepository.GetUserOpenOrder(userId);

            if (userCart != null)
            {
                if (string.IsNullOrEmpty(userCart.User.Address))
                {
                    return RedirectToRoute("EditInformationGet");
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

                bool status = Payments.ChargePayment(sum, userCart.OrderId);
            }

            return Redirect("/");
        }

        #endregion
    }
}