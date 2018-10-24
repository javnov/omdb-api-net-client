using System;
using MockHttpClient;
using Xunit;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace OpenMovieDatabase.Client.Test
{
    public static class OpenMovieDatabaseClientTest
    {
        public class Constructor
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void ShouldThrowWhenApiKeyIsNullOrEmptyString(string apiKey)
            {
                Assert.Throws<ArgumentNullException>(() => new OpenMovieDatabaseClient(apiKey, null));
            }

            [Fact]
            public void ShouldThrowWhenHttpClientIsNull()
            {
                Assert.Throws<ArgumentNullException>(() => new OpenMovieDatabaseClient("api-key", null));
            }
        }

        public class SearchAsync
        {
            private readonly MockHttpClient.MockHttpClient _mockHttpClient;
            private readonly OpenMovieDatabaseClient _sut;

            public SearchAsync()
            {
                _mockHttpClient = new MockHttpClient.MockHttpClient();
                _sut = new OpenMovieDatabaseClient("api-key", _mockHttpClient);
            }

            [Theory]
            [InlineData(HttpStatusCode.InternalServerError)]
            [InlineData(HttpStatusCode.NotFound)]
            [InlineData(HttpStatusCode.BadRequest)]
            public async Task ShouldThrowWhenResponseHasNotAnOkStatus(HttpStatusCode status)
            {
                var request = new SearchRequest
                {
                    Term = "test"
                };

                _mockHttpClient.When("http://www.omdbapi.com/*").Then((arg) => new HttpResponseMessage(status));

                await Assert.ThrowsAnyAsync<Exception>(() => _sut.SearchAsync(request));
            }

            [Fact]
            public async Task ShouldThrowWhenContentIsNull()
            {
                var request = new SearchRequest
                {
                    Term = "test"
                };

                _mockHttpClient.When("http://www.omdbapi.com/*").Then((arg) => new HttpResponseMessage(HttpStatusCode.OK).WithJsonContent(null));

                await Assert.ThrowsAnyAsync<Exception>(() => _sut.SearchAsync(request));
            }

            [Fact]
            public async Task ShouldThrowWhenContentIsNotNullAndResponseIsFalse()
            {
                var request = new SearchRequest
                {
                    Term = "test"
                };

                _mockHttpClient.When("http://www.omdbapi.com/*").Then((arg) => new HttpResponseMessage(HttpStatusCode.OK).WithJsonContent(new Internal.InternalSearchResponse { Response = false, Error = "Some Error" }));

                await Assert.ThrowsAnyAsync<Exception>(() => _sut.SearchAsync(request));
            }
        }
    }
}
