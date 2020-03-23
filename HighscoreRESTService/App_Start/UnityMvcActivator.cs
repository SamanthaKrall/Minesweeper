using System.Linq;
using System.Web.Mvc;
using Unity.AspNet.Mvc;

namespace HighscoreRESTService.App_Start
{
    public class UnityMvcActivator
    {
        public static void Start()
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));
        }

        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}