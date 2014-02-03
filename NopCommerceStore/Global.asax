<%@ Application Language="C#" %>
<%@ Import Namespace="NopSolutions.NopCommerce.BusinessLogic.Configuration" %>
<%@ Import Namespace="NopSolutions.NopCommerce.BusinessLogic" %>
<%@ Import Namespace="NopSolutions.NopCommerce.BusinessLogic.Infrastructure" %>
<%@ Import Namespace="NopSolutions.NopCommerce.BusinessLogic.Installation" %>
<%@ Import Namespace="NopSolutions.NopCommerce.BusinessLogic.Utils" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_BeginRequest(object sender, EventArgs e)
    {
        NopConfig.Init();
        if (!InstallerHelper.ConnectionStringIsSet())
        {
            InstallerHelper.RedirectToInstallationPage();
        }
    }


    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        NopConfig.Init();
        if (InstallerHelper.ConnectionStringIsSet())
        {
            //initialize IoC
            IoC.InitializeWith(new DependencyResolverFactory());
            
            //initialize task manager
            TaskManager.Instance.Initialize(NopConfig.ScheduleTasks);
            TaskManager.Instance.Start();
        }
        RegisterRoutes(RouteTable.Routes);
    }
    
    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
        if (InstallerHelper.ConnectionStringIsSet())
        {
            TaskManager.Instance.Stop();
        }
    }
    
    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex != null)
        {
            if (InstallerHelper.ConnectionStringIsSet())
            {
                IoC.Resolve<ILogService>().InsertLog(LogTypeEnum.Unknown, ex.Message, ex);
            }
        }
    }

    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        routes.MapPageRoute("seller", 
            "seller/{name}",
            "~/seller.aspx");
        
        routes.MapRoute(
            "Default", // Route name
            "{controller}/{action}/{id}", // URL with parameters
            new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
        );

    }
</script>

