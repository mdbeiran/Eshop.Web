using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Eshop.Services.Context;
using Eshop.Utilitiy.Security;
using Eshop.Utilitiy.Security.Contracts;
using Ninject;

namespace Eshop.Ioc
{

    public class NinjectController : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectController()
        {
            ninjectKernel = new StandardKernel();
            AddBinding();
        }

        private void AddBinding()
        {
            ninjectKernel.Bind<EshopUOW>().To<EshopUOW>();
            ninjectKernel.Bind<IPasswordHelper>().To<PasswordHelper>();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, "not found");
            }

            return (IController)ninjectKernel.Get(controllerType);
        }
    }
}
