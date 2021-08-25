using System;
using System.Collections.Generic;
using Eshop.DomainClass.Public;

namespace Eshop.Services.Repositories
{
    public interface ISliderRepository:IDisposable
    {
        List<Slider> GetAllSliders();
        Slider GetSliderById(int sliderId);
        void InsertSlider(Slider slider);
        void UpdateSlider(Slider slider);
        void DeleteSlider(Slider slider);

    }
}