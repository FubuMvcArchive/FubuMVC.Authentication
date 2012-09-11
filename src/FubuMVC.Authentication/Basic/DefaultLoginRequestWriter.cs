using System.Collections.Generic;
using FubuCore;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.UI;
using HtmlTags;

namespace FubuMVC.Authentication.Basic
{
    public class DefaultLoginRequestWriter : IMediaWriter<LoginRequest>
    {
        private readonly IServiceLocator _services;
        private readonly IOutputWriter _writer;

        public DefaultLoginRequestWriter(IServiceLocator services, IOutputWriter writer)
        {
            _services = services;
            _writer = writer;
        }

        public void Write(string mimeType, LoginRequest resource)
        {
            var document = BuildView(resource);
            _writer.WriteHtml(document.ToString());
        }

        public virtual HtmlDocument BuildView(LoginRequest request)
        {
            // TODO -- Revisit all of this when we get HTML conventions everywhere
            var view = new FubuHtmlDocument<LoginRequest>(_services);
            var form = view.FormFor<LoginRequest>();
            form.Append(new HtmlTag("legend").Text(LoginKeys.Login));

            form.Append(view.Edit(x => x.UserName));
            form.Append(view.Edit(x => x.Password));
            form.Append(view.Edit(x => x.RememberMe));

            form.Append(new HtmlTag("input").Attr("type", "submit").Attr("value", LoginKeys.Login));

            view.Add(form);

            return view;
        }

        public IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }
    }
}