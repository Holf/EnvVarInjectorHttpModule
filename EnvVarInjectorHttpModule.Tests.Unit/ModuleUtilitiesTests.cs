using System;
using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;

namespace EnvVarInjectorHttpModule.Tests.Unit
{
    public class ModuleUtilitiesTests
    {
        [Test]
        public void GetJavaScript_should_return_the_correct_JavaScript()
        {
            var envVars = new Dictionary<string, string>
            {
                {"__InterestingEnvVar", "Value1"},
                {"__AnotherInterestingEnvVar", "Value2"},
                {"UninterestingEnvVar","Value3" }
            };

            var result = ModuleUtilities.GetJavaScript(envVars, "__", "process.env");

            result.Should()
                .Be(
                    "process={env:{}};process.env.InterestingEnvVar=\"Value1\";process.env.AnotherInterestingEnvVar=\"Value2\";");
        }

        [TestCase("http://www.webste.com/path1/path2/main-6eaf82c5a743187a8e95.min.js", true)]
        [TestCase("http://www.webste.com/path1/path2/man-6eaf82c5a743187a8e95.min.js", false)]
        [TestCase("http://www.webste.com/path1/path2/", false)]
        public void MatchesSearchRegex_should_match_Uri_correctly(string uriString, bool expectedResult)
        {
            var searchRegex = @"main.*\.min\.js";
            var uri = new Uri(uriString);

            var result = ModuleUtilities.MatchesSearchRegex(uri, searchRegex);

            result.Should().Be(expectedResult);
        }
    }
}
