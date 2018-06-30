using System;
namespace OpenMovieDatabase.Client
{
    public class EmptyResponseException : Exception
    {
        public EmptyResponseException() : base("There is not content present on response")
        {
        }
    }
}
