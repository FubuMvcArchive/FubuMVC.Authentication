using System.Collections.Generic;
using DotNetOpenAuth.OAuth.ChannelElements;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.UI.Extensibility;
using FubuMVC.Core.View;
using HtmlTags;

namespace FubuMVC.Authentication.Twitter
{
    public class TwitterFubuExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<TwitterEndpoints>();
            registry.Services<TwitterServices>();

            registry.Extensions().For(new TwitterContentExtension());
        }
    }

    public class TwitterServices : ServiceRegistry
    {
        public TwitterServices()
        {
            SetServiceIfNone<ITwitterProxy, TwitterProxy>();
            SetServiceIfNone<ITwitterCallback, TwitterCallback>();
            SetServiceIfNone<ITwitterResponseHandler, TwitterResponseHandler>();
            SetServiceIfNone<ISignInEndpointBuilder, SignInEndpointBuilder>();
            SetServiceIfNone<IConsumerTokenManager, InMemoryTokenManager>();
        }
    }

    public class TwitterEndpoints : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            yield return ActionCall.For<TwitterController>(x => x.Login(null));
            yield return ActionCall.For<TwitterController>(x => x.Callback(null));
        }
    }

    public class TwitterContentExtension : IContentExtension<LoginRequest>
    {
        public IEnumerable<object> GetExtensions(IFubuPage<LoginRequest> page)
        {
            var partial = page.Get<IPartialFactory>().BuildPartial(typeof (TwitterLoginRequest));
            var output = page.Get<IOutputWriter>().Record(() => partial.InvokePartial());

            yield return output.GetText();
        }
    }
}