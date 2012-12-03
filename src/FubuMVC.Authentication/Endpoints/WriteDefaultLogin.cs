using System;
using System.Collections.Generic;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Endpoints
{
    public class WriteDefaultLogin : WriterNode
    {
        protected override ObjectDef toWriterDef()
        {
            return new ObjectDef(typeof(DefaultLoginRequestWriter));
        }

        protected override void createDescription(Description description)
        {
            description.ShortDescription = "Writes the default html view for the LoginRequest";
        }

        public override Type ResourceType
        {
            get { return typeof (LoginRequest); }
        }

        public override IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }
    }
}