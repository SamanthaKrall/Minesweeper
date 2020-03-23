using Unity;
using Unity.Lifetime;
using Minesweeper.Services.Utility;


namespace Minesweeper.App_Start
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILogger>(new ContainerControlledLifetimeManager());
        }
    }
}