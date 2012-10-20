using System;
using System.Collections.Generic;
using FubuCore;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Facebook
{
    public class WriteDefaultFacebookButton : WriterNode
    {
        public override Type ResourceType
        {
            get { return typeof (FacebookLoginRequest); }
        }

        public override IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }

        protected override ObjectDef toWriterDef()
        {
            return new ObjectDef(typeof (DefaultFacebookLoginRequestWriter));
        }

        protected override void createDescription(Description description)
        {
            description.ShortDescription = "Writes the default html tag for the {0}".ToFormat(typeof (FacebookLoginRequest).Name);
        }
    }
}