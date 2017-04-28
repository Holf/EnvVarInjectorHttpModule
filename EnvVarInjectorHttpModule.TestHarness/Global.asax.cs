using System;
using System.Web;

namespace EnvVarInjectorHttpModule.TestHarness
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Environment.SetEnvironmentVariable("__MY_ENV_VAR", "THIS IS NOT A LOVE SONG");
        }
    }
}