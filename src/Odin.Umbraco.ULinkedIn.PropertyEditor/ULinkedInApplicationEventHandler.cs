using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.IO;

namespace website.Odin.Umbraco.ULinkedIn.PropertyEditor
{
    public class ULinkedInApplicationEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // Should use 'umbraco.GlobalSettings.UmbracoMvcArea', but not available. See:
            //https://our.umbraco.org/forum/developing-packages/84508-globalsettings-does-not-contain-a-definition-for-umbracomvcarea
            
            var umbracoPath = System.Configuration.ConfigurationManager.AppSettings["umbracoPath"];
            if (umbracoPath.StartsWith(SystemDirectories.Root)) // beware of TrimStart, see U4-2518
                umbracoPath = umbracoPath.Substring(SystemDirectories.Root.Length);
            umbracoPath = umbracoPath.TrimStart('~').TrimStart('/').Replace('/', '-').Trim().ToLower();

            System.Web.Routing.RouteTable.Routes.MapRoute(
                "uLinkedIn oAuth authorize",
                umbracoPath + "/backoffice/auth/ULinkedIn/{action}",
                new
                {
                    controller = "ULinkedIn",
                    action = "RequestAnAuthorizationCode"
                }
            );

            //System.Web.Routing.RouteTable.Routes.MapRoute(
            //    "uLinkedIn oAuth authorizexxxx",
            //    "ULinkedInRefreshToken/{action}",
            //    new
            //    {
            //        controller = "ULinkedInRefreshToken",
            //        action = "GetRefreshedToken"
            //    }
            //);

        }
    }
}