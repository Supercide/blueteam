﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            MvcHandler.DisableMvcResponseHeader = true;
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("VisitStart", DateTime.UtcNow.ToString()));

            // secure the ASP.NET Session ID only if using SSL
            // if you don't check for the issecureconnection, it will not work.
            if (Request.IsSecureConnection == true)
            {
                Response.Cookies["ASP.NET_SessionID"].Secure = true;
                Response.Cookies["ASP.NET_SessionID"].HttpOnly = true;
            }

        }
    }
}