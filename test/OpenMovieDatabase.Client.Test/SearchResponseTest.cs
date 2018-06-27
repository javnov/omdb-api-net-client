using System;
using OpenMovieDatabase.Client.Internal;
using Xunit;
namespace OpenMovieDatabase.Client.Test
{
    public class SearchResponseTest
    {
        [Fact]
        public void ShouldThrowWhenParamIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SearchResponse(null));
        }

        [Fact]
        public void ShouldThrowWhenResponseIsNotSuccess()
        {
            Assert.Throws<ArgumentException>(() => new SearchResponse(new InternalSearchResponse { Response = false }));
        }
    }
}
