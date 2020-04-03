using Unity;
using Unity.Lifetime;
using Minesweeper.Services.Utility;
using System;

namespace Minesweeper
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });
        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILogger, MyLogger2>(new ContainerControlledLifetimeManager());
        }
    }
}