using System;
using Xunit;
namespace OpenMovieDatabase.Client.Test
{
    public class SearchRequestTest
    {
        private readonly SearchRequest _sut;

        public SearchRequestTest()
        {
            _sut = new SearchRequest();
        }

        [Fact]
        public void ShouldInitializeWithDefaultValues()
        {
            var parameters = _sut.GetParameters();

            Assert.NotNull(parameters);
            Assert.Equal(3, parameters.Count);
            Assert.False(string.IsNullOrEmpty(parameters["page"]));
            Assert.False(string.IsNullOrEmpty(parameters["r"]));
            Assert.False(string.IsNullOrEmpty(parameters["v"]));
        }
    }
}
