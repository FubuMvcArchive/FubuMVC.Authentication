using FubuCore;
using FubuMVC.Core;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsSignInRequest
    {
        private string _url;

        [QueryString]
        public string Url
        {
            get
            {
                if (_url.IsEmpty())
                {
                    _url = "~/";
                }

                return _url;
            }
            set { _url = value; }
        }
    }
}