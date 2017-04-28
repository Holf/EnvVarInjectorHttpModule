using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EnvVarInjectorHttpModule
{
    public class ModuleUtilities
    {
        public static string GetJavaScript(IDictionary environmentVariables, string interestingEnvVarPrefix, string @namespace)
        {
            var interestingEnvVars = GetEnvironmentVariables(environmentVariables)
                .Where(x => x.Key.StartsWith(interestingEnvVarPrefix))
                .ToList();

            var javaScript = "process={env:{}};";

            interestingEnvVars.ForEach(x =>
            {
                var interestingEnvVarWithoutPrefix = x.Key.Replace(interestingEnvVarPrefix, string.Empty);
                var assignmentStatement = $"{@namespace}.{interestingEnvVarWithoutPrefix}=\"{x.Value}\";";
                javaScript += assignmentStatement;
            });

            return javaScript;
        }

        public static bool MatchesSearchRegex(Uri uri, string searchRegex)
        {
            var fileName = Path.GetFileName(uri.LocalPath);

            return Regex.IsMatch(fileName, searchRegex);
        }

        private static IDictionary<string, string> GetEnvironmentVariables(IDictionary environmentVariables)
        {
            return environmentVariables.Keys.Cast<object>().ToDictionary(k => k.ToString(), v => environmentVariables[v].ToString());
        }
    }
}