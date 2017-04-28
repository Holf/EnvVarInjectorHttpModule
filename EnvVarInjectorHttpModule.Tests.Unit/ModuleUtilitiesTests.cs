using System;
using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;

namespace EnvVarInjectorHttpModule.Tests.Unit
{
    public class ModuleUtilitiesTests
    {
        [TestCase("process", "process={};process.InterestingEnvVar=\"Value1\";process.AnotherInterestingEnvVar=\"Value2\";")]
        [TestCase("process.env", "process={env:{}};process.env.InterestingEnvVar=\"Value1\";process.env.AnotherInterestingEnvVar=\"Value2\";")]
        [TestCase("process.env.vars", "process={env:{vars:{}}};process.env.vars.InterestingEnvVar=\"Value1\";process.env.vars.AnotherInterestingEnvVar=\"Value2\";")]
        public void GetJavaScript_should_return_the_correct_JavaScript(string @namespace, string expectedResult)
        {
            var envVars = new Dictionary<string, string>
            {
                {"__InterestingEnvVar", "Value1"},
                {"__AnotherInterestingEnvVar", "Value2"},
                {"UninterestingEnvVar","Value3" }
            };

            var result = ModuleUtilities.GetJavaScript(envVars, "__", @namespace);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void GetJavaScript_should_return_empty_string_if_there_are_no_Interesting_EnvVars()
        {
            var envVars = new Dictionary<string, string>();

            var result = ModuleUtilities.GetJavaScript(envVars, "__", "process.env");

            result.Should().BeEmpty();
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
