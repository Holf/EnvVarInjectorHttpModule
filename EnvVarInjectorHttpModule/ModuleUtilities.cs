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

            if (interestingEnvVars.Any())
            {
                var javaScript = GetNamespaceObjectDeclaration(@namespace);

                interestingEnvVars.ForEach(x =>
                {
                    var interestingEnvVarWithoutPrefix = x.Key.Replace(interestingEnvVarPrefix, string.Empty);
                    var assignmentStatement = $"{@namespace}.{interestingEnvVarWithoutPrefix}=\"{x.Value}\";";
                    javaScript += assignmentStatement;
                });

                return javaScript;
            }

            return string.Empty;
        }

        private static string GetNamespaceObjectDeclaration(string @namespace)
        {
            const string prefix = "={";
            const string suffix = "};";
            var namespaceParts = @namespace.Split('.');

            if (namespaceParts.Length == 1)
            {
                return namespaceParts.First() + prefix + suffix;
            }

            var middlePart = string.Empty;

            foreach (var nameSpacePart in namespaceParts.Skip(1).Reverse())
            {
                middlePart = nameSpacePart + ":{" + middlePart + "}";
            }

            return namespaceParts.First() + prefix + middlePart + suffix;
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