using System;
using System.Configuration;
using System.Web;
using EnvVarInjectorHttpModule.Configuration;

namespace EnvVarInjectorHttpModule
{
    public class Module : IHttpModule
    {
        private string _javaScript;
        private static string _searchRegex = @"main.*\.min\.js";

        public void Init(HttpApplication context)
        {
            var config = (EnvVarInjectorHttpModuleSettings)ConfigurationManager.GetSection(typeof(EnvVarInjectorHttpModuleSettings).Name);

            _javaScript = ModuleUtilities.GetJavaScript(
                Environment.GetEnvironmentVariables(),
                config.InterestingEnvVarPrefix,
                config.Namespace);

            _searchRegex = config.SearchRegex;

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

