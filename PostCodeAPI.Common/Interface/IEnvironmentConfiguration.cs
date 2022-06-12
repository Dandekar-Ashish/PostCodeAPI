using System;
using System.Collections.Generic;
using System.Text;

namespace PostCodeAPI.Common.Interface
{
    public interface IEnvironmentConfiguration
    {
        string GetPostCodeBaseURI();
        string GetAutoCompleteRoute();
        string GetPostCodeLookupRoute();
    }
}
