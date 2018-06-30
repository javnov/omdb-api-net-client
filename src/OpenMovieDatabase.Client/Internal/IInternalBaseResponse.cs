namespace OpenMovieDatabase.Client.Internal
{
    internal interface IInternalBaseResponse
    {
        bool Response { get; set; }
        string Error { get; set; }
    }
}
