using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eshop.Business.Filter;
using Eshop.Business.StaticTools;
using Eshop.DomainClass.Public;
using Eshop.Services.Context;
using Eshop.Utilitiy.Convertor;

namespace Eshop.Web.Areas.Admin.Controllers
{
    [Authorize]
    [EshopPermission("ManageSliders")]
    public class ManageSliderController : Controller
    {

        #region constructor

        private EshopUOW db;
        public ManageSliderController(EshopUOW db)
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
                        img.ResizeImageFromFile((thumbPath + imageName), height.Value, width.Value, false, false);
                    }
                    else
                    {
                        img.ResizeImageFromFile((thumbPath + imageName), 200, 200, false, false);
                    }

                }
            }
        }

        #endregion

        #region CRUD

        #region Index

        [EshopPermission("ManageSliders")]
        public ActionResult Index()
        {
            return View(db.SliderRepository.GetAllSliders());
        }

        #endregion

        #region Create

        [EshopPermission("CreateSlider")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Url,ImageName,Visit,IsActive,Text")] Slider slider, HttpPostedFileBase sliderImage)
        {
            if (ModelState.IsValid)
            {
                if (sliderImage == null)
                {
                    ModelState.AddModelError("ImageName", "تصویر را انتخاب کنید");
                    return View(slider);
                }

                slider.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(sliderImage.FileName);
                slider.Visit = 0;
                db.SliderRepository.InsertSlider(slider);
                db.Save();
                AddImageToServer(sliderImage,slider.ImageName,PathTools.OriginSliderImageFullPath,441,484, PathTools.ThumbSliderImageFullPath);
                
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        #endregion

        #region Edit

        [EshopPermission("EditSlider")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.SliderRepository.GetSliderById(id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Url,ImageName,Visit,IsActive,Text")] Slider slider, HttpPostedFileBase sliderImage)
        {
            if (ModelState.IsValid)
            {
                if (sliderImage != null)
                {
                    string imageName = Guid.NewGuid() + Path.GetExtension(sliderImage.FileName);
                    AddImageToServer(sliderImage, imageName, PathTools.OriginSliderImageFullPath,441,484,PathTools.ThumbSliderImageFullPath,slider.ImageName);
                    slider.ImageName = imageName;
                }

                db.SliderRepository.UpdateSlider(slider);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        #endregion

        #region Delete

        [EshopPermission("DeleteSlider")]
        public ActionResult DeleteSlider(int id)
        {
            var slider = db.SliderRepository.GetSliderById(id);
            if (slider != null)
            {
                if (System.IO.File.Exists(PathTools.OriginSliderImageFullPath + slider.ImageName))
                {
                    System.IO.File.Delete(PathTools.OriginSliderImageFullPath + slider.ImageName);
                }

                if (System.IO.File.Exists(PathTools.ThumbSliderImageFullPath + slider.ImageName))
                {
                    System.IO.File.Delete(PathTools.ThumbSliderImageFullPath + slider.ImageName);
                }
                
                db.SliderRepository.DeleteSlider(slider);
                db.Save();
            }

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #endregion
    }
}