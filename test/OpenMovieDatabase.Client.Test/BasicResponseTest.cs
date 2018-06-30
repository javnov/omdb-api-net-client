using System;
using Xunit;
namespace OpenMovieDatabase.Client.Test
{
    public class BasicResponseTest
    {
        [Fact]
        public void ShouldThrowWhenParamIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BasicResponse(null));
        }
    }
}
