using System;
using System.Collections.Generic;
using System.Globalization;
namespace OpenMovieDatabase.Client
{
    public class SearchRequest : BaseRequest
    {
        public string Term
        {
            get { return _parameters["s"]; }
            set { _parameters["s"] = value; }
        }

        public ItemType Type
        {
            get { return (ItemType)Enum.Parse(typeof(ItemType), _parameters["type"], true); }
            set { _parameters["type"] = value.ToString().ToLowerInvariant(); }
        }

        public int Year
        {
            get { return int.Parse(_parameters["y"], CultureInfo.InvariantCulture); }
            set { _parameters["y"] = value.ToString(CultureInfo.InvariantCulture); }
        }

        public int Page
        {
            get { return int.Parse(_parameters["page"], CultureInfo.InvariantCulture); }
            set { _parameters["page"] = value.ToString(CultureInfo.InvariantCulture); }
        }

        public SearchRequest()
        {
            _parameters["page"] = "1";
        }
    }
}
