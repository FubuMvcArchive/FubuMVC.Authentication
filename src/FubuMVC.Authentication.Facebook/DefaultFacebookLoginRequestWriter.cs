using System.Collections.Generic;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using HtmlTags;

namespace FubuMVC.Authentication.Facebook
{
    public class DefaultFacebookLoginRequestWriter : IMediaWriter<FacebookLoginRequest>
    {
        private readonly IFubuRequest _request;
        private readonly IUrlRegistry _urls;
        private readonly IOutputWriter _writer;

        public DefaultFacebookLoginRequestWriter(IOutputWriter writer, IUrlRegistry urls, IFubuRequest request)
        {
            _writer = writer;
            _urls = urls;
            _request = request;
        }

        public void Write(string mimeType, FacebookLoginRequest resource)
        {
            var request = _request.Get<FacebookSignIn>();
            HtmlTag tag = new HtmlTag("a").Attr("href", _urls.UrlFor(request)).Text(FacebookLoginKeys.LoginWithFacebook);
            _writer.WriteHtml(tag.ToString());
        }

        public IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }
    }
}