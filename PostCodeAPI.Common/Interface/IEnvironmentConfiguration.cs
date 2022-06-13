namespace PostCodeAPI.Common.Interface
{
    public interface IEnvironmentConfiguration
    {
        string GetPostCodeBaseURI();
        string GetAutoCompleteRoute();
        string GetPostCodeLookupRoute();
    }
}
