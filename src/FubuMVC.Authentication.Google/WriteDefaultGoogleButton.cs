using System;
using System.Collections.Generic;
using FubuCore;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Google
{
    public class WriteDefaultGoogleButton : WriterNode
    {
        public override Type ResourceType
        {
            get { return typeof (GoogleLoginRequest); }
        }

        public override IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }

        protected override ObjectDef toWriterDef()
        {
            return new ObjectDef(typeof (DefaultGoogleLoginRequestWriter));
        }

        protected override void createDescription(Description description)
        {
            description.ShortDescription = "Writes the default html tag for the {0}".ToFormat(typeof (GoogleLoginRequest).Name);
        }
    }
}