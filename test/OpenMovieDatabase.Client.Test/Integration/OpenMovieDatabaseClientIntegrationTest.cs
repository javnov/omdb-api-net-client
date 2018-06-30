using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace OpenMovieDatabase.Client.Test.Integration
{
    public class OpenMovieDatabaseClientIntegrationTest
    {
        private readonly OpenMovieDatabaseClient _sut;

        public OpenMovieDatabaseClientIntegrationTest()
        {
            var handler = new HttpClientHandler
            {
                UseCookies = false,
                AllowAutoRedirect = true,
                ServerCertificateCustomValidationCallback = null
            };

             var httpClient = new HttpClient(handler);

            //I'm using the same api key that http://www.omdbapi.com this could be stop working any moment
            _sut = new OpenMovieDatabaseClient("PlzBanMe", httpClient);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task SearchAsync()
        {
            var request = new SearchRequest
            {
                Term = "avengers",
                Type = ItemType.Movie
            };


            var response = await _sut.SearchAsync(request);

            Assert.NotNull(response);
            Assert.Equal(75, response.TotalResults);
            Assert.NotNull(response.Results);
            Assert.NotEmpty(response.Results);
            Assert.True(response.Results.All(r => r.Title.IndexOf(request.Term, StringComparison.OrdinalIgnoreCase) > -1));
        }

        [Fact()]
        [Trait("Category", "Integration")]
        public async Task SearchAsyncThrowsError()
        {
            var request = new SearchRequest
            {
                Term = "the",
                Type = ItemType.Movie
            };

            await Assert.ThrowsAnyAsync<OpenMovieDatabaseException>(() => _sut.SearchAsync(request));
        }

        [Fact]
        //[Fact(Skip = "Integration test")]
        public async Task GetByIdOrTitleAsync()
        {
            var request = new GetByIdOrTitleRequest
            {
                ImdbId = "tt0499549",
                Type = ItemType.Movie
            };


            var response = await _sut.GetByIdOrTitleAsync(request);

            Assert.NotNull(response);
            Assert.Equal("tt0499549", response.ImdbID);
            Assert.Equal("Avatar", response.Title);

            Assert.NotNull(response.Released);
            Assert.Equal(18, response.Released.Value.Day);
            Assert.Equal(12, response.Released.Value.Month);
            Assert.Equal(2009, response.Released.Value.Year);

            Assert.NotNull(response.Runtime);
            Assert.Equal(162, response.Runtime.Value.TotalMinutes);
        }

        [Fact]
        //[Fact(Skip = "Integration test")]
        public async Task GetByIdOrTitleAsyncThrowsError()
        {
            var request = new GetByIdOrTitleRequest
            {
                ImdbId = "tt0000000",
                Type = ItemType.Movie
            };

            await Assert.ThrowsAnyAsync<OpenMovieDatabaseException>(() => _sut.GetByIdOrTitleAsync(request));
        }
    }
}
