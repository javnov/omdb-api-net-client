using Newtonsoft.Json;

namespace OpenMovieDatabase.Client.Internal
{
    internal class InternalSearchResponse : IInternalBaseResponse
    {
        [JsonProperty]
        public bool Response { get; set; }

        [JsonProperty]
        public string Error { get; set; }

        [JsonProperty]
        public InternalSearchItem[] Search { get; set; }

        [JsonProperty]
        public int TotalResults { get; set; }
    }
}
