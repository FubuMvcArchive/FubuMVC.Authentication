using FubuMVC.Core;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsSignInRequest
    {
        [QueryString]
        public string Url { get; set; }
    }
}