using System;
using System.Collections.Generic;
namespace OpenMovieDatabase.Client
{
    public abstract class BaseRequest
    {
        protected readonly Dictionary<string, string> _parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["r"] = "json",
            ["v"] = "1"
        };

        internal IDictionary<string, string> GetParameters()
        {
            return _parameters;
        }
    }
}
