using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace WCFService
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Container => container.Value;
            #endregion

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}