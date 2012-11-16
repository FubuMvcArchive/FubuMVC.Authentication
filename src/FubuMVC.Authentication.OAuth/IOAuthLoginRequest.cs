using FubuMVC.Core;

namespace FubuMVC.Authentication.OAuth
{
    public interface IOAuthLoginRequest
    {
        [QueryString]
        string Url { get; set; }
    }
}