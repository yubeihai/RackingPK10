using JobLibrary;
using JobLibrary.TimingTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RacingPK10
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //执行的任务
            JobManage.StartScheduleFromConfigAsync();
        }
        //当网站关闭时结束正在执行的工作
        protected void Application_End(object sender, EventArgs e)
        {
            //   在应用程序关闭时运行的代码
            JobManage.ShutDown();
        }
    }
}
