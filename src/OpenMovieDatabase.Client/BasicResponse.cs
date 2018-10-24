using System;
using OpenMovieDatabase.Client.Internal;
namespace OpenMovieDatabase.Client
{
    public class BasicResponse
    {
        public string Title { get; }
        public int?[] Year { get; }
        public string ImdbID { get; }
        public ItemType Type { get; }
        public Uri Poster { get; }

        internal BasicResponse(InternalSearchItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Title = item.Title;
            Year = item.Year;
            ImdbID = item.imdbID;
            Type = (ItemType)Enum.Parse(typeof(ItemType), item.Type, true);
            Poster = IsNullOrEmptyOrNA(item.Poster) ? null : new Uri(item.Poster);
        }

        protected static bool IsNullOrEmptyOrNA(string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;

            return string.Equals("N/A", value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
