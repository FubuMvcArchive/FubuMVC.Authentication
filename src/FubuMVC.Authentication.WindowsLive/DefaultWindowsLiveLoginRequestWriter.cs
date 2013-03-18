using System.Collections.Generic;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using HtmlTags;

namespace FubuMVC.Authentication.WindowsLive
{
    public class DefaultWindowsLiveLoginRequestWriter : IMediaWriter<WindowsLiveLoginRequest>
    {
        private readonly IFubuRequest _request;
        private readonly IUrlRegistry _urls;
        private readonly IOutputWriter _writer;

        public DefaultWindowsLiveLoginRequestWriter(IOutputWriter writer, IUrlRegistry urls, IFubuRequest request)
        {
            _writer = writer;
            _urls = urls;
            _request = request;
        }

        public void Write(string mimeType, WindowsLiveLoginRequest resource)
        {
            var request = _request.Get<WindowsLiveSignIn>();
            HtmlTag tag = new HtmlTag("a").Attr("href", _urls.UrlFor(request)).Text(WindowsLiveLoginKeys.LoginWithWindowsLive);
            _writer.WriteHtml(tag.ToString());
        }

        public IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }
    }
}