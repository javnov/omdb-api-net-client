using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace OpenMovieDatabase.Client
{
    public class OpenMovieDatabaseClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenMovieDatabaseClient(HttpClient httpClient, string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = apiKey;
        }


    }
}
