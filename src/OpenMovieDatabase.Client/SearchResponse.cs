using System;
using OpenMovieDatabase.Client.Internal;
using System.Linq;
namespace OpenMovieDatabase.Client
{
    public class SearchResponse
    {
        public BasicResponse[] Results { get; }
        public int TotalResults { get; set; }

        internal SearchResponse(InternalSearchResponse searchResponse)
        {
            if (searchResponse == null)
                throw new ArgumentNullException(nameof(searchResponse));

            if (!searchResponse.Response)
                throw new ArgumentException("Should be a success response", nameof(searchResponse));

            Results = searchResponse.Search?.Select(i => new BasicResponse(i)).ToArray();
            TotalResults = searchResponse.TotalResults;
        }
    }
}
