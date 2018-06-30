using Newtonsoft.Json;

namespace OpenMovieDatabase.Client.Internal
{
    internal class InternalGetByIdOrTitleResponse : InternalSearchItem, IInternalBaseResponse
    {
        [JsonProperty]
        public bool Response { get; set; }

        [JsonProperty]
        public string Error { get; set; }
    }
}
