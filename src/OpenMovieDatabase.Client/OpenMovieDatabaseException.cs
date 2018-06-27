using System;
using OpenMovieDatabase.Client.Internal;

namespace OpenMovieDatabase.Client
{
    public class OpenMovieDatabaseException : Exception
    {
        internal OpenMovieDatabaseException(IInternalBaseResponse baseResponse) : base(baseResponse?.Error)
        {
            if (baseResponse == null)
                throw new ArgumentNullException(nameof(baseResponse));

            if (baseResponse.Response)
                throw new ArgumentException("Response should not be a succeed one", nameof(baseResponse));

        }
    }
}
