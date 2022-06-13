namespace PostCodeAPI.Common.Model
{
    public class EnvironmentConfigurationSection
    {
        public string PostCodeBaseURI { get; set; }
        public EnvironmentConfigurationSectionRoutes Routes { get; set; }
    }
    public class EnvironmentConfigurationSectionRoutes
    {
        public string AutoComplete { get; set; }
        public string PostCodeLookup { get; set; }
    }
}
