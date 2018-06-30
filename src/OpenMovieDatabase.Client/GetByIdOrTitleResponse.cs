using System;
using OpenMovieDatabase.Client.Internal;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

namespace OpenMovieDatabase.Client
{
    public class GetByIdOrTitleResponse : BasicResponse
    {
        private static Regex _timeRegex = new Regex(@"^(\d{1,})+((\s{1})+min{1})$", RegexOptions.IgnoreCase);

        public string Rated { get; set; }
        public DateTime? Released { get; set; }
        public TimeSpan? Runtime { get; set; }
        public string[] Genre { get; set; }
        public string[] Director { get; set; }
        public string[] Writer { get; set; }
        public string[] Actors { get; set; }
        public string Plot { get; set; }
        public string[] Language { get; set; }
        public string[] Country { get; set; }
        public string Awards { get; set; }
        public Rating[] Ratings { get; set; }
        public int Metascore { get; set; }
        public float ImdbRating { get; set; }
        public long? ImdbVotes { get; set; }
        public DateTime? Dvd { get; set; }
        public decimal? BoxOffice { get; set; }
        public string Production { get; set; }
        public Uri Website { get; set; }

        internal GetByIdOrTitleResponse(InternalGetByIdOrTitleResponse item) : base(item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Rated = IsNullOrEmptyOrNA(item.Rated) ? null : item.Rated;
            Released = ParseOmdbDate(item.Released);
            Runtime = ParseOmdbTime(item.Runtime);
            Genre = SplitByColon(item.Genre);
            Director = SplitByColon(item.Director);
            Writer = SplitByColon(item.Writer);
            Actors = SplitByColon(item.Actors);
            Plot = item.Plot;
            Language = SplitByColon(item.Language);
            Country = SplitByColon(item.Country);
            Awards = item.Awards;
            Ratings = item.Ratings?.Select(r => new Rating(r)).ToArray();
            Metascore = item.Metascore;
            ImdbRating = item.imdbRating;
            ImdbVotes = ParseOmdbNumber(item.imdbVotes);
            Dvd = ParseOmdbDate(item.DVD);
            BoxOffice = ParseOmdbNumber(item.BoxOffice);
            Production = item.Production;
            Website = IsNullOrEmptyOrNA(item.Website) ? null : new Uri(item.Website);
        }

        private static DateTime? ParseOmdbDate(string value)
        {
            if (IsNullOrEmptyOrNA(value))
                return null;

            if (DateTime.TryParseExact(value, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime parsedValue))
                return parsedValue;

            return null;
        }

        private static TimeSpan? ParseOmdbTime(string value)
        {
            if (IsNullOrEmptyOrNA(value))
                return null;

            if (!_timeRegex.IsMatch(value))
                return null;

            if (int.TryParse(value.Replace(" min", ""), out int minutes))
                return TimeSpan.FromMinutes(minutes);

            return null;
        }

        private static string ParseOmdbString(string value)
        {
            if (IsNullOrEmptyOrNA(value))
                return null;

            return value;
        }

        private static string[] SplitByColon(string value)
        {
            if (IsNullOrEmptyOrNA(value))
                return null;

            return value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
        }

        private static long? ParseOmdbNumber(string value)
        {
            if (IsNullOrEmptyOrNA(value))
                return null;

            string normalizedNumber = value.Replace(",", "").Replace("$", "");
            if (long.TryParse(normalizedNumber, out long number))
                return number;

            return null;
        }
    }

    public class Rating
    {
        public string Source { get; }
        public string Value { get; }

        internal Rating(InternalRating rating)
        {
            Source = rating.Source;
            Value = rating.Value;
        }
    }
}
