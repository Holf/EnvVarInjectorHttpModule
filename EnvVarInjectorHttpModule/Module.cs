using System;
using System.Web;
using System.Web.Configuration;

namespace EnvVarInjectorHttpModule
{
    public class Module : IHttpModule
    {
        private static string _namespace = "process.env";
        private static string _interestingEnvVarPrefix = "__";
        private string _javaScript;
        private static string _searchRegex = @"main.*\.min\.js";

        public void Init(HttpApplication context)
        {
            _javaScript = ModuleUtilities.GetJavaScript(
                Environment.GetEnvironmentVariables(),
                _interestingEnvVarPrefix,
                _namespace);

            context.BeginRequest += Context_BeginRequest;
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var context = application.Context;
            var requestUri = context.Request.Url;

            if (ModuleUtilities.MatchesSearchRegex(requestUri, _searchRegex))
            {
                context.Response.Write(_javaScript);
            }
        }

        public void Dispose()
        {
 
        }
    }
}

