<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        //Environment.SetEnvironmentVariable();
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://limitedinformation.net"))
        {
            //Response.RedirectPermanent("http://www.limitedinformation.net");
            HttpContext.Current.Response.RedirectPermanent("http://www.limitedinformation.net/", false);
        }
        if (HttpContext.Current.Request.Url.ToString().ToLower().Equals("http://www.limitedinformation.net"))
        {
            //Response.RedirectPermanent("http://www.limitedinformation.net");
            HttpContext.Current.Response.RedirectPermanent("http://www.limitedinformation.net/", false);
        }
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>