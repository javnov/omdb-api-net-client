using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using OpenMovieDatabase.Client.Internal;
using Newtonsoft.Json;

namespace OpenMovieDatabase.Client
{
    public class OpenMovieDatabaseClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenMovieDatabaseClient(string apiKey, HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            _apiKey = apiKey;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.Term))
                throw new ArgumentException("The term should not be empty", nameof(request));

            var uri = GetUrlFromRequest(request);
            var internalResponse = await HandleResponse<InternalSearchResponse>(_httpClient.GetAsync(uri));

            return new SearchResponse(internalResponse);
        }

        public async Task<GetByIdOrTitleResponse> GetByIdOrTitleAsync(GetByIdOrTitleRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.Title) && string.IsNullOrEmpty(request.ImdbId))
                throw new ArgumentException("Should set imdb id or tilte", nameof(request));

            var uri = GetUrlFromRequest(request);
            var internalResponse = await HandleResponse<InternalGetByIdOrTitleResponse>(_httpClient.GetAsync(uri));

            return new GetByIdOrTitleResponse(internalResponse);
        }

        private Uri GetUrlFromRequest(BaseRequest request)
        {
            IDictionary<string, string> parameters = request.GetParameters();
            parameters["apikey"] = _apiKey;

            string queryString = string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}").ToArray());
            return new Uri($"http://www.omdbapi.com/?{queryString}");
        }

        private static async Task<T> HandleResponse<T>(Task<HttpResponseMessage> responseTask) where T : class, IInternalBaseResponse
        {
            HttpResponseMessage httpResponse = await responseTask;

            httpResponse.EnsureSuccessStatusCode();

            string content = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(content);

            if (result == null)
                throw new EmptyResponseException();

            if (!result.Response)
                throw new OpenMovieDatabaseException(result);

            return result;
        }


    }
}
