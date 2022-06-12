using PostCodeAPI.Common.Interface;
using PostCodeAPI.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostCodeAPI.Common.Impl
{
    public class EnvironmentConfiguration : IEnvironmentConfiguration
    {
        private EnvironmentConfigurationSection _configSection;
        public EnvironmentConfiguration(EnvironmentConfigurationSection configSection)
        {
            _configSection = configSection;
        }
        public string GetAutoCompleteRoute() => _configSection.Routes.AutoComplete;

        public string GetPostCodeBaseURI() => _configSection.PostCodeBaseURI;

        public string GetPostCodeLookupRoute() => _configSection.Routes.PostCodeLookup;
    }
}
