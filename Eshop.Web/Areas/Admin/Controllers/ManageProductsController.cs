using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Web.Areas.Admin.Controllers
{

    using Eshop.Business.Filter;
    using Eshop.Business.StaticTools;
    using Eshop.DomainClass.Order;
    using Eshop.DomainClass.Product;
    using Eshop.Services.Context;
    using Eshop.Utilitiy.Convertor;
    using Eshop.ViewModel.Products;

    [Authorize]
    [EshopPermission("ManageProducts")]
    public class ManageProductsController : Controller
    {
        #region ctor

        private EshopUOW db;

        public ManageProductsController(EshopUOW db)
        {
            this.db = db;
        }

        #endregion

        #region Specific Methods

        private void AddImageToServer(HttpPostedFileBase image, string imageName, string orginalPath, int? width, int? height, string thumbPath = null, string deleteImageName = null)
        {
            if (image != null)
            {
                // remove image
                if (!string.IsNullOrEmpty(deleteImageName))
                {
                    if (System.IO.File.Exists(orginalPath + deleteImageName))
                    {
                        System.IO.File.Delete(orginalPath + deleteImageName);
                    }

                    if (!string.IsNullOrEmpty(thumbPath))
                    {
                        if (System.IO.File.Exists(thumbPath + deleteImageName))
                        {
                            System.IO.File.Delete(thumbPath + deleteImageName);
                        }
                    }
                }


                // add image
                image.SaveAs(orginalPath + imageName);
                if (!string.IsNullOrEmpty(thumbPath))
                {
                    image.SaveAs(thumbPath + imageName);
                    ImageResizer img = new ImageResizer();
                    if (width != null && height != null)
                    {
                        img.ResizeImageFromFile(thumbPath + imageName, height.Value, width.Value, false, false);
                    }
                    else
                    {
                        img.ResizeImageFromFile(thumbPath + imageName, 200, 200, false, false);
                    }

                }
            }
        }

        #endregion

        #region Product Groups

        #region Index

        public ActionResult ProductGroups(int? id)
        {
            if (id != null)
                ViewBag.parentId = id.Value;

            var groups = db.ProductRepository.GetProductGroups(id);
            return View(groups);
        }

        #endregion

        #region Create

        [EshopPermission("CreateProductGroups")]
        public ActionResult CreateGroup(int? id)
        {
            ViewBag.UrlError = false;
            if (id != null) ViewBag.parentId = id.Value;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(ProductGroup group)
        {
            if (ModelState.IsValid)
            {
                if (!db.ProductRepository.IsExistUrlTitleInProductGroups(group.NameInUrl))
                {
                    db.ProductRepository.Insertgroup(group);
                    db.Save();
                    return RedirectToAction("ProductGroups", "ManageProducts", new { id = group.ParentId });
                }
                else
                {
                    ViewBag.UrlError = true;
                }

            }

            return View(group);
        }

        #endregion Group

        #region Edit Group

        [EshopPermission("EditProductGroups")]
        public ActionResult EditGroup(int id)
        {
            ViewBag.UrlError = false;
            var group = db.ProductRepository.GetgroupById(id);
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup(ProductGroup group, string oldUrlName)
        {
            if (ModelState.IsValid)
            {
                if (!db.ProductRepository.IsExistUrlTitleInProductGroups(group.NameInUrl) || group.NameInUrl == oldUrlName)
                {
                    db.ProductRepository.UpdateProductgroup(group);
                    db.Save();
                    return RedirectToAction("ProductGroups", "ManageProducts", new { id = group.ParentId });
                }
                else
                {
                    ViewBag.UrlError = true;
                }
            }

            return View(group);
        }

        #endregion

        #region Delete Group

        [EshopPermission("DeleteProductGroups")]
        public ActionResult DeleteGroup(int id)
        {
            var group = db.ProductRepository.GetgroupById(id);
            if (group != null)
            {
                group.IsDelete = true;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }


        #endregion

        #region Return Group

        [EshopPermission("ReturnProductGroups")]
        public ActionResult ReturnGroup(int id)
        {
            var group = db.ProductRepository.GetgroupById(id);
            if (group != null)
            {
                group.IsDelete = false;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #endregion

        #region Features

        #region Index

        [EshopPermission("ManageProductFeatures")]
        public ActionResult ProductFeatures()
        {
            var features = db.ProductRepository.GetProductFeatures();
            return View(features);
        }

        #endregion

        #region Create Feature

        [EshopPermission("CreateProductFeatures")]
        public ActionResult CreateFeature()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFeature(ProductFeatures feature)
        {
            if (ModelState.IsValid)
            {
                db.ProductRepository.InsertProductFeature(feature);
                db.Save();
                return RedirectToAction("ProductFeatures", "ManageProducts");
            }

            return View(feature);
        }

        #endregion

        #region Edit Feature

        [EshopPermission("EditProductFeatures")]
        public ActionResult EditFeature(int id)
        {
            var feature = db.ProductRepository.GetFeatureById(id);
            if (feature == null)
            {
                return HttpNotFound();
            }

            return View(feature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFeature(ProductFeatures feature)
        {
            if (ModelState.IsValid)
            {
                db.ProductRepository.UpdateProductFeature(feature);
                db.Save();
                return RedirectToAction("ProductFeatures", "ManageProducts");
            }

            return View(feature);
        }

        #endregion

        #region Delete Feature

        [EshopPermission("DeleteProductFeatures")]
        public ActionResult DeleteFeature(int id)
        {
            var feature = db.ProductRepository.GetFeatureById(id);
            if (feature != null)
            {
                feature.IsDelete = true;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #region Return Feature

        [EshopPermission("ReturnProductFeatures")]
        public ActionResult ReturnFeature(int id)
        {
            var feature = db.ProductRepository.GetFeatureById(id);
            if (feature != null)
            {
                feature.IsDelete = false;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #endregion

        #region Products

        #region Index

        public ActionResult Index(FilterProductsViewModel filter)
        {
            filter.Take = 15;
            var products = db.ProductRepository.GetProductsByFilter(filter);
            return View(products);
        }

        #endregion

        #region Create Product

        public ActionResult CreateProduct()
        {
            Session["Features"] = null;

            List<ProductGroup> groups = new List<ProductGroup>()
            {
                new ProductGroup() {GroupId = 0,GroupTitle = "-- لطفا گروه اصلی انتخاب کنید --"}
            };
            groups.AddRange(db.ProductRepository.GetActiveMainGroups());
            ViewBag.MainGroupId = new SelectList(groups, "GroupId", "GroupTitle");

            List<ProductGroup> subgroups = new List<ProductGroup>()
            {
                new ProductGroup() {GroupId = 0,GroupTitle = "-- لطفا زیرگروه را انتخاب کنید --"}
            };

            ViewBag.SubGroupId = new SelectList(subgroups, "GroupId", "GroupTitle");

            ViewBag.Features = new SelectList(db.ProductRepository.GetProductFeatures(), "FeatureId", "FeatureTitle");
            CreateProductViewModel create = new CreateProductViewModel();

            return View(create);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(CreateProductViewModel product,
            HttpPostedFileBase singleProductImage, HttpPostedFileBase publicProductImage,
            HttpPostedFileBase[] productGallery)
        {
            #region Check Error

            bool hasError = false;
            ViewBag.SelectGroup = false;
            ViewBag.FillText = false;


            if (product.MainGroupId == 0 || product.SubGroupId == 0)
            {
                hasError = false;
                ViewBag.SelectGroup = true;
            }

            if (string.IsNullOrEmpty(product.Text))
            {
                hasError = true;
                ViewBag.FillText = true;
            }

            if (hasError)
            {
                List<ProductGroup> groups = new List<ProductGroup>()
                {
                    new ProductGroup() {GroupId = 0,GroupTitle = "-- لطفا گروه اصلی انتخاب کنید --"}
                };
                groups.AddRange(db.ProductRepository.GetMainGroups());
                ViewBag.MainGroupId = new SelectList(groups, "GroupId", "GroupTitle");

                List<ProductGroup> subgroups = new List<ProductGroup>()
                {
                    new ProductGroup() {GroupId = 0,GroupTitle = "-- لطفا زیرگروه را انتخاب کنید --"}
                };

                ViewBag.SubGroupId = new SelectList(subgroups, "GroupId", "GroupTitle");

                ViewBag.Features = new SelectList(db.ProductRepository.GetProductFeatures(), "FeatureId", "FeatureTitle");

                return View(product);
            }

            #endregion

            if (ModelState.IsValid)
            {
                // insert product
                Product newProduct = new Product()
                {
                    PublicPrice = product.PublicPrice,
                    CreateDate = DateTime.Now,
                    IsActive = product.IsActive,
                    IsExist = product.IsExist,
                    MainGroupId = product.MainGroupId,
                    SubGroupId = product.SubGroupId,
                    ProductCode = product.ProductCode,
                    SinglePrice = product.SinglePrice,
                    ShortDescription = FixedText.ConvertNewLineToBr(product.ShortDescription),
                    Title = product.Title,
                    Text = product.Text,
                    SellCount = 0
                };

                db.ProductRepository.InsertProduct(newProduct);
                db.Save();

                // images
                if (singleProductImage != null)
                {
                    string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(singleProductImage.FileName);
                    AddImageToServer(singleProductImage, imageName, PathTools.ProductImageFullPath, 200, 200, PathTools.ProductImageThumbFullPath);
                    newProduct.SingleProductImageName = imageName;
                }

                if (publicProductImage != null)
                {
                    string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(singleProductImage.FileName);
                    AddImageToServer(publicProductImage, imageName, PathTools.ProductImageFullPath, 200, 200, PathTools.ProductImageThumbFullPath);
                    newProduct.PUblicProductImageName = imageName;
                }

                // gallery
                if (productGallery != null)
                {
                    foreach (var image in productGallery)
                    {
                        if (image != null)
                        {
                            string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(image.FileName);
                            AddImageToServer(image, imageName, PathTools.ProductImageGalleryFullPath, 100, 100, PathTools.ProductImageGalleryThumbFullPath);
                            ProductGallery gallery = new ProductGallery()
                            {
                                ProductId = newProduct.ProductId,
                                ImageName = imageName
                            };
                            db.ProductRepository.InsertGalleryImage(gallery);
                        }
                    }
                }

                db.Save();

                if (Session["Features"] != null)
                {
                    List<ProductFeatureViewModel> list = Session["Features"] as List<ProductFeatureViewModel>;

                    foreach (var fe in list)
                    {
                        db.ProductRepository.InsertProductFeature(new ProductSelectedFeatures()
                        {
                            FeatureId = fe.Id,
                            Value = fe.Value,
                            ProductId = newProduct.ProductId
                        });
                    }
                }
                db.Save();
                Session["Features"] = null;

                // tags
                if (product.Tags != null)
                {
                    db.ProductRepository.InsertProductTags(product.Tags, newProduct.ProductId);
                    db.Save();
                }

                return RedirectToAction("Index", "ManageProducts", new { area = "Admin" });

            }

            return View(product);
        }

        #endregion

        #region Edit Product

        public ActionResult EditProduct(int id)
        {
            var product = db.ProductRepository.GetProductForEdit(id);
            product.ShortDescription = FixedText.ConvertBrToNewLine(product.ShortDescription);
            if (string.IsNullOrEmpty(product.Title))
            {
                return HttpNotFound();
            }

            List<ProductGroup> groups = new List<ProductGroup>()
            {
                new ProductGroup() {GroupId = 0,GroupTitle = "-- لطفا گروه اصلی انتخاب کنید --"}
            };
            groups.AddRange(db.ProductRepository.GetActiveMainGroups());
            ViewBag.MainGroupId = new SelectList(groups, "GroupId", "GroupTitle", product.MainGroupId);

            List<ProductGroup> subgroups = new List<ProductGroup>();
            subgroups.AddRange(product.ProductSubGroups);
            ViewBag.SubGroupId = new SelectList(subgroups, "GroupId", "GroupTitle", product.SubGroupId);

            ViewBag.Features = new SelectList(db.ProductRepository.GetProductFeatures(), "FeatureId", "FeatureTitle");

            Session["Features"] = null;

            if (product.ProductSelectedFeatureses.Any())
            {
                Session["Features"] = product.ProductSelectedFeatureses.Where(s => !s.IsDelete).Select(f => new ProductFeatureViewModel()
                {
                    Id = f.FeatureId,
                    Title = f.ProductFeatures.FeatureTitle,
                    Value = f.Value,
                    PF_Id = f.PF_Id
                }).ToList();
            }

            ViewBag.UrlReferer = Request.UrlReferrer.ToString();

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(EditProductViewModel product, HttpPostedFileBase singleProductImage, HttpPostedFileBase publicProductImage, HttpPostedFileBase[] productGallery, string UrlReferer)
        {
            #region Check Error

            bool hasError = false;
            ViewBag.SelectGroup = false;
            ViewBag.FillText = false;


            if (product.MainGroupId == 0 || product.SubGroupId == 0)
            {
                hasError = false;
                ViewBag.SelectGroup = true;
            }

            if (string.IsNullOrEmpty(product.Text))
            {
                hasError = true;
                ViewBag.FillText = true;
            }

            if (hasError)
            {
                List<ProductGroup> groups = new List<ProductGroup>()
                {
                    new ProductGroup() {GroupId = 0,GroupTitle = "-- لطفا گروه اصلی انتخاب کنید --"}
                };
                groups.AddRange(db.ProductRepository.GetActiveMainGroups());
                ViewBag.MainGroupId = new SelectList(groups, "GroupId", "GroupTitle");

                List<ProductGroup> subgroups = new List<ProductGroup>()
                {
                    new ProductGroup() {GroupId = 0,GroupTitle = "-- لطفا زیرگروه را انتخاب کنید --"}
                };
                subgroups.AddRange(db.ProductRepository.GetActiveSubGroups(product.MainGroupId));
                ViewBag.SubGroupId = new SelectList(subgroups, "GroupId", "GroupTitle");

                ViewBag.Features = new SelectList(db.ProductRepository.GetProductFeatures(), "FeatureId", "FeatureTitle");

                return View(product);
            }

            #endregion

            if (ModelState.IsValid)
            {
                var mainProduct = db.ProductRepository.GetProductById(product.ProductId);
                mainProduct.IsExist = product.IsExist;
                mainProduct.IsActive = product.IsActive;
                mainProduct.Title = product.Title;
                mainProduct.ShortDescription = FixedText.ConvertNewLineToBr(product.ShortDescription);
                mainProduct.Text = product.Text;
                mainProduct.SinglePrice = product.SinglePrice;
                mainProduct.PublicPrice = product.PublicPrice;
                mainProduct.ProductCode = product.ProductCode;
                mainProduct.MainGroupId = product.MainGroupId;
                mainProduct.SubGroupId = product.SubGroupId;

                // manage tags
                db.ProductRepository.DeleteProductTags(product.ProductId);
                db.ProductRepository.InsertProductTags(product.Tags, product.ProductId);
                db.Save();

                // images
                if (singleProductImage != null)
                {
                    string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(singleProductImage.FileName);
                    AddImageToServer(singleProductImage, imageName, PathTools.ProductImageFullPath, 200, 200, PathTools.ProductImageThumbFullPath, mainProduct.SingleProductImageName);
                    mainProduct.SingleProductImageName = imageName;
                }

                if (singleProductImage != null)
                {
                    string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(publicProductImage.FileName);
                    AddImageToServer(publicProductImage, imageName, PathTools.ProductImageFullPath, 200, 200, PathTools.ProductImageThumbFullPath, mainProduct.PUblicProductImageName);
                    mainProduct.PUblicProductImageName = imageName;
                }
                db.Save();

                // gallery
                if (productGallery != null)
                {
                    foreach (var image in productGallery)
                    {
                        if (image != null)
                        {
                            string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(image.FileName);
                            AddImageToServer(image, imageName, PathTools.ProductImageGalleryFullPath, 100, 100, PathTools.ProductImageGalleryThumbFullPath);
                            ProductGallery gallery = new ProductGallery()
                            {
                                ProductId = product.ProductId,
                                ImageName = imageName
                            };
                            db.ProductRepository.InsertGalleryImage(gallery);
                        }
                    }
                }

                db.Save();

                Session["Features"] = null;

                if (!string.IsNullOrEmpty(UrlReferer))
                    return RedirectPermanent(UrlReferer);

                return RedirectToAction("Index", "ManageProducts", new { area = "Admin" });
            }

            return View();
        }

        #endregion

        #region Get Sub Groups

        public JsonResult GetSubGroups(int id)
        {
            var list = db.ProductRepository.GetSubGroups(id).Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString()
            });

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Features

        public ActionResult AddFeature(int id, string title, string value)
        {
            List<ProductFeatureViewModel> list = new List<ProductFeatureViewModel>();

            if (Session["Features"] != null)
            {
                list = Session["Features"] as List<ProductFeatureViewModel>;
            }

            list.Add(new ProductFeatureViewModel()
            {
                Id = id,
                Title = title,
                Value = value
            });
            Session["Features"] = list;
            return PartialView("ListFeatures", list);
        }

        public ActionResult ListFeatures()
        {
            List<ProductFeatureViewModel> list = new List<ProductFeatureViewModel>();

            if (Session["Features"] != null)
            {
                list = Session["Features"] as List<ProductFeatureViewModel>;
            }

            return PartialView(list);
        }


        public ActionResult RemoveFeature(int id, string value)
        {
            List<ProductFeatureViewModel> list = new List<ProductFeatureViewModel>();

            if (Session["Features"] != null)
            {
                list = Session["Features"] as List<ProductFeatureViewModel>;
                int index = list.FindIndex(f => f.Id == id && f.Value == value.Replace("-", " "));
                list.RemoveAt(index);
                Session["Features"] = list;
            }

            return PartialView("ListFeatures", list);
        }

        #endregion

        #region Features For Edit

        public ActionResult ListFeaturesForEdit()
        {
            List<ProductFeatureViewModel> list = new List<ProductFeatureViewModel>();

            if (Session["Features"] != null)
            {
                list = Session["Features"] as List<ProductFeatureViewModel>;
            }

            return PartialView(list);
        }

        public ActionResult EditFeatures(string featureId, string featureValue)
        {
            int id = Convert.ToInt32(featureId);

            var feature = db.ProductRepository.GetSelectedFeatureById(id);

            if (feature != null)
            {
                feature.Value = featureValue;
                db.Save();
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveFeatureForEdit(int id, string value, int PfID)
        {
            List<ProductFeatureViewModel> list = new List<ProductFeatureViewModel>();

            if (Session["Features"] != null)
            {
                list = Session["Features"] as List<ProductFeatureViewModel>;
                int index = list.FindIndex(f => f.Id == id && f.Value == value.Replace("-", " "));
                list.RemoveAt(index);
                Session["Features"] = list;
            }

            var selectedFeature = db.ProductRepository.GetSelectedFeatureById(PfID);

            selectedFeature.IsDelete = true;

            db.Save();

            return PartialView("ListFeaturesForEdit", list);
        }


        public ActionResult AddFeatureForEdit(int id, string title, string value, int productId)
        {

            var feature = new ProductSelectedFeatures()
            {
                ProductId = productId,
                FeatureId = id,
                Value = value
            };

            db.ProductRepository.InsertProductFeature(feature);

            db.Save();

            List<ProductFeatureViewModel> list = new List<ProductFeatureViewModel>();

            if (Session["Features"] != null)
            {
                list = Session["Features"] as List<ProductFeatureViewModel>;
            }

            list.Add(new ProductFeatureViewModel()
            {
                Id = id,
                Title = title,
                Value = value,
                PF_Id = feature.PF_Id
            });
            Session["Features"] = list;

            return PartialView("ListFeaturesForEdit", list);
        }

        #endregion

        #region Delete Product

        public ActionResult DeleteProduct(int productId)
        {
            var product = db.ProductRepository.GetProductById(productId);
            if (product != null)
            {
                product.IsDelete = true;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #region Return Product

        public ActionResult ReturnProduct(int productId)
        {
            var product = db.ProductRepository.GetProductById(productId);
            if (product != null)
            {
                product.IsDelete = false;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #endregion

        #region Product Gallery

        public ActionResult DeleteProductGallery(int galleryId)
        {
            var gallery = db.ProductRepository.GetGalleryById(galleryId);

            if (gallery != null)
            {
                if (System.IO.File.Exists(PathTools.ProductImageGalleryFullPath + gallery.ImageName))
                {
                    System.IO.File.Delete(PathTools.ProductImageGalleryFullPath + gallery.ImageName);
                }

                if (System.IO.File.Exists(PathTools.ProductImageGalleryThumbFullPath + gallery.ImageName))
                {
                    System.IO.File.Delete(PathTools.ProductImageGalleryThumbFullPath + gallery.ImageName);
                }

                db.ProductRepository.DeleteGalleryImage(gallery);
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #region Manage Orders

        [EshopPermission("ShopCart")]
        public ActionResult ShowNewBuys(FilterOrders filter)
        {
            var orders = db.ProductRepository.GetOrders(filter);

            return View(orders);
        }

        [EshopPermission("ShopCart")]
        public ActionResult ShowOrderDetail(int id)
        {
            ViewBag.Sum = 0;
            ViewBag.Maliat = 0;
            ViewBag.Final = 0;
            ViewBag.MinimumCount = 0;
            ViewBag.TotalCount = 0;
            int MinimumCount = db.ProductRepository.GetMinimumForPublicProducts();
            Order userCart = db.ProductRepository.GetUserOrderById(id);

            if (userCart == null || !userCart.IsFinally)
            {
                return HttpNotFound();
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

            var final = sum;
            ViewBag.Sum = sum;
            ViewBag.Final = final;
            ViewBag.MinimumCount = MinimumCount;

            return View(userCart);
        }

        public ActionResult SetSendForOrder(int orderId)
        {
            db.ProductRepository.SendOrderToUser(orderId);

            db.Save();

            return Json(new { status = "Done" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }

}