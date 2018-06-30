using System;
using Newtonsoft.Json;

namespace OpenMovieDatabase.Client.Internal
{
    internal class InternalSearchItem
    {
        [JsonProperty]
        internal string Title { get; set; }

        [JsonProperty]
        internal int Year { get; set; }

        [JsonProperty]
        internal string Rated { get; set; }

        [JsonProperty]
        internal string Released { get; set; }

        [JsonProperty]
        internal string Runtime { get; set; }

        [JsonProperty]
        internal string Genre { get; set; }

        [JsonProperty]
        internal string Director { get; set; }

        [JsonProperty]
        internal string Writer { get; set; }

        [JsonProperty]
        internal string Actors { get; set; }

        [JsonProperty]
        internal string Plot { get; set; }

        [JsonProperty]
        internal string Language { get; set; }

        [JsonProperty]
        internal string Country { get; set; }

        [JsonProperty]
        internal string Awards { get; set; }

        [JsonProperty]
        internal string Poster { get; set; }

        [JsonProperty]
        internal InternalRating[] Ratings { get; set; }

        [JsonProperty]
        internal int Metascore { get; set; }

        [JsonProperty]
        internal float imdbRating { get; set; }

        [JsonProperty]
        internal string imdbVotes { get; set; }

        [JsonProperty]
        internal string imdbID { get; set; }

        [JsonProperty]
        internal string Type { get; set; }

        [JsonProperty]
        internal string DVD { get; set; }

        [JsonProperty]
        internal string BoxOffice { get; set; }

        [JsonProperty]
        internal string Production { get; set; }

        [JsonProperty]
        internal string Website { get; set; }
    }

    internal class InternalRating
    {
        [JsonProperty]
        internal string Source { get; set; }

        [JsonProperty]
        internal string Value { get; set; }
    }
}
