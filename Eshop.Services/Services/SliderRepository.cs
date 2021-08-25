using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.DataLayer.Contexts;
using Eshop.DomainClass.Public;
using Eshop.Services.Repositories;

namespace Eshop.Services.Services
{
    class SliderRepository : ISliderRepository
    {
        #region Constructor

        private EshopDbContext db;
        public SliderRepository(EshopDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Sliders

        public List<Slider> GetAllSliders()
        {
            return db.Sliders.ToList();
        }

        public Slider GetSliderById(int sliderId)
        {
            return db.Sliders.SingleOrDefault(s => s.ID == sliderId);
        }

        public void InsertSlider(Slider slider)
        {
            db.Sliders.Add(slider);
        }

        public void UpdateSlider(Slider slider)
        {
            db.Entry(slider).State = EntityState.Modified;
        }

        public void DeleteSlider(Slider slider)
        {
            db.Entry(slider).State = EntityState.Deleted;
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
