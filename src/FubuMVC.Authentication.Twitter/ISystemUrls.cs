using System.Web;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Urls;

namespace FubuMVC.Authentication.Twitter
{
    public interface ISystemUrls
    {
        string FullUrlFor(object model);
    }
    
    // TODO -- This obviously only works w/ ASP.NET as does all of this Bottle
    // Need a good way to do this in FubuMVC.Core
    public class SystemUrls : ISystemUrls
    {
        private readonly IUrlRegistry _urls;
        private readonly HttpRequestBase _request;

        public SystemUrls(IUrlRegistry urls)
        {
            _urls = urls;
            _request = new HttpRequestWrapper(HttpContext.Current.Request);
        }

        public string FullUrlFor(object model)
        {
            var url = _urls.UrlFor(model);
            var fullUri = _request.Url;
            var qualified = string.Format("{0}://{1}", fullUri.Scheme, fullUri.Host);

            if (!fullUri.IsDefaultPort)
            {
                qualified += ":{0}".ToFormat(fullUri.Port);
            }


            return url.ToServerQualifiedUrl(qualified);
        }
    }
}