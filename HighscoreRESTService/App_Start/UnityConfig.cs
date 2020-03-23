using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace HighscoreRESTService.App_Start
{
    public class UnityConfig
    {
        public static object Container { get; internal set; }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}