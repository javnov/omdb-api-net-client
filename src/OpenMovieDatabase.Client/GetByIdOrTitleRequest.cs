using System;
using System.Globalization;

namespace OpenMovieDatabase.Client
{
    public class GetByIdOrTitleRequest : BaseRequest
    {
        public string ImdbId
        {
            get { return _parameters.ContainsKey("i") ? _parameters["i"] : null; }
            set { _parameters["i"] = value; }
        }

        public string Title
        {
            get { return _parameters.ContainsKey("t") ? _parameters["t"] : null; }
            set { _parameters["t"] = value; }
        }

        public ItemType Type
        {
            get { return (ItemType)Enum.Parse(typeof(ItemType), _parameters["type"], true); }
            set { _parameters["type"] = value.ToString(); }
        }

        public int Year
        {
            get { return int.Parse(_parameters["y"], CultureInfo.InvariantCulture); }
            set { _parameters["y"] = value.ToString(CultureInfo.InvariantCulture); }
        }

        public PlotType Plot
        {
            get { return (PlotType)Enum.Parse(typeof(PlotType), _parameters["plot"], true); }
            set { _parameters["plot"] = value.ToString().ToLowerInvariant(); }
        }

        public GetByIdOrTitleRequest()
        {
            Plot = PlotType.Short;
        }
    }
}
