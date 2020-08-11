using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Task.DataAccess.Abstract;
using Task.DataAccess.Concrete.Ef;

namespace Task.WebUI.Ninject
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public NinjectControllerFactory()
        {
            kernel = new StandardKernel(new NinjectBindingModule());
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)kernel.Get(controllerType);
        }
    }

    public class NinjectBindingModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IProject>().To<ProjectDal>();
        }
    }
}