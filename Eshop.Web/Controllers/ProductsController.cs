using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eshop.Business.Users;
using Eshop.DomainClass.Product;
using Eshop.Services.Context;
using Eshop.Utilitiy.Convertor;
using Eshop.ViewModel.Products;
using Eshop.DomainClass.Order;

namespace Eshop.Web.Controllers
{
    public class ProductsController : Controller
    {
        #region ctor

        private readonly EshopUOW db;

        public ProductsController(EshopUOW db)
        {
            this.db = db;
        }

        #endregion

        #region Single Sell Products

        [Route("CrumbsProducts", Name = "CrumbsProducts")]
        public ActionResult Index(SearchProductsViewModel search)
        {
            search.Take = 15;
            if (search.PageId <= 0)
            {
                search.PageId = 1;
            }

            search.State = "Active";
            var products = db.ProductRepository.SearchProducts(search);
            return View(products);
        }

        #endregion

        #region Major Sell Products

        [Route("MajorProducts", Name = "MajorProducts")]
        public ActionResult Major(SearchProductsViewModel search)
        {
            search.Take = 15;
            if (search.PageId <= 0)
            {
                search.PageId = 1;
            }

            search.State = "Active";
            var products = db.ProductRepository.SearchProducts(search);
            return View(products);
        }

        #endregion

        #region Search Product Box

        public ActionResult SearchProductBox()
        {
            return PartialView();
        }

        #endregion

        #region MajorBox

        public ActionResult MajorBox()
        {
            return PartialView();
        }

        #endregion

        #region Show Single Product

        [Route("ProductDetail/{id}", Name = "ProductDetail")]
        public ActionResult SingleProcutPage(int id)
        {
            var product = db.ProductRepository.GetSingleProductDetail(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [Route("MajorProductDetail/{id}", Name = "MajorProductDetail")]
        public ActionResult MajorProcutPage(int id)
        {
            var product = db.ProductRepository.GetSingleProductDetail(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.MinimumCount = db.ProductRepository.GetMinimumForPublicProducts();
            return View(product);
        }

        #endregion

        #region Major Box

        public ActionResult MajorProductBox()
        {
            return PartialView();
        }

        #endregion

        #region Create Comment

        [HttpPost]
        public ActionResult CreateCommnet(ProductComment replyComment, string urlReferrer)
        {
            if (ModelState.IsValid)
            {
                replyComment.Comment = FixedText.ConvertNewLineToBr(replyComment.Comment);
                replyComment.CreateDate = DateTime.Now;
                replyComment.UserID = UserManager.GetCurrentUserId();
                db.ProductRepository.InsertProdectComment(replyComment);
                db.Save();
            }

            if (!string.IsNullOrEmpty(urlReferrer))
            {
                return RedirectPermanent(urlReferrer);
            }

            return RedirectToRoute("ProductDetail", new { id = replyComment.ProductId, pageId = 1 });
        }

        #endregion

        #region Order

        #region Create New Order

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewOrderForSingle(List<int> selectedFeatures, int productId, int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (count > 0)
                {
                    var userId = UserManager.GetCurrentUserId();

                    var product = db.ProductRepository.GetProductById(productId);

                    var minimumCount = db.ProductRepository.GetMinimumForPublicProducts();

                    if (!db.ProductRepository.HasUserAnyOpenPrders(userId))
                    {
                        // create new order
                        Order newOrder = new Order()
                        {
                            UserId = userId,
                            CreateDate = DateTime.Now,
                            IsFinally = false
                        };

                        db.ProductRepository.InsertProductOrder(newOrder);
                        db.Save();

                        // create new order detail
                        OrderDetail detail = new OrderDetail()
                        {
                            OrderId = newOrder.OrderId,
                            Count = count,
                            ProductId = productId
                        };
                        if (count <= minimumCount)
                        {
                            detail.Price = product.SinglePrice;
                        }
                        else if (count > minimumCount)
                        {
                            detail.Price = product.PublicPrice;
                        }

                        db.ProductRepository.InsertOrderDetailForOrder(detail);
                        db.Save();

                        // insert order selected features
                        db.ProductRepository.InsertOrderSelectedFeatures(detail.DetailId, selectedFeatures);
                        db.Save();

                        db.ProductRepository.SetOrderDetailPrices(newOrder.OrderId);
                        db.Save();

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var order = db.ProductRepository.GetUserLatestOrder(userId);
                        // create new order detail

                        if (db.ProductRepository.CanInsertDetail(selectedFeatures, order.OrderId))
                        {
                            OrderDetail detail = new OrderDetail()
                            {
                                OrderId = order.OrderId,
                                Count = count,
                                ProductId = productId
                            };

                            if (count <= minimumCount)
                            {
                                detail.Price = product.SinglePrice;
                            }
                            else if (count > minimumCount)
                            {
                                detail.Price = product.PublicPrice;
                            }

                            db.ProductRepository.InsertOrderDetailForOrder(detail);
                            db.Save();

                            db.ProductRepository.SetOrderDetailPrices(order.OrderId);
                            db.Save();

                            // insert order selected features
                            if (selectedFeatures != null)
                            {
                                db.ProductRepository.InsertOrderSelectedFeatures(detail.DetailId, selectedFeatures);

                                db.Save();
                            }

                            return Json(new { status = "Done" }, JsonRequestBehavior.AllowGet);
                        }

                        return Json(new { status = "Exist" }, JsonRequestBehavior.AllowGet);

                    }
                }

                return Json(new { status = "Count" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "NotAuthorize" }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}