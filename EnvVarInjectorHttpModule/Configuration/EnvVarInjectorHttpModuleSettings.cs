using System.Configuration;

namespace EnvVarInjectorHttpModule.Configuration
{
    public class EnvVarInjectorHttpModuleSettings : ConfigurationSection
    {
        private const string NamespaceKey = "namespace";
        private const string InterestingEnvVarPrefixKey = "interestingEnvVarPrefix";
        private const string SearchRegexKey = "searchRegex";

        [ConfigurationProperty(NamespaceKey, DefaultValue = "process.env")]
        public string Namespace
        {
            get => (string)this[NamespaceKey];
            set => this[NamespaceKey] = value;
        }

        [ConfigurationProperty(InterestingEnvVarPrefixKey, DefaultValue = "__")]
        public string InterestingEnvVarPrefix
        {
            get => (string)this[InterestingEnvVarPrefixKey];
            set => this[InterestingEnvVarPrefixKey] = value;
        }

        [ConfigurationProperty(SearchRegexKey, DefaultValue = @"main.*\.min\.js")]
        public string SearchRegex
        {
            get => (string)this[SearchRegexKey];
            set => this[SearchRegexKey] = value;
        }
    }
}
