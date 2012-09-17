using System;
using System.Collections.Generic;
using System.Web;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;

namespace FubuMVC.Authentication.Twitter
{
    // TODO -- Harden this
    public class TwitterProxy : ITwitterProxy
    {
        private readonly WebConsumer _consumer;
        private readonly IFubuRequest _request;
        private readonly IUrlRegistry _urls;
        private readonly ICurrentHttpRequest _httpRequest;

        public TwitterProxy(IConsumerTokenManager manager, ISignInEndpointBuilder endpointBuilder, 
            IFubuRequest request, IUrlRegistry urls, ICurrentHttpRequest httpRequest)
        {
            _request = request;
            _urls = urls;
            _httpRequest = httpRequest;

            var endpoints = endpointBuilder.Build();
            _consumer = new WebConsumer(endpoints, manager);
        }

        public void SignIn()
        {
            var parameters = new Dictionary<string, string>();
            var loginCallback = _request.Get<TwitterLoginCallback>();
            var url = _urls.SystemUrl(loginCallback, _httpRequest);

            var request = _consumer.PrepareRequestUserAuthorization(new Uri(url), null, parameters);
            _consumer.Channel.PrepareResponse(request).Send();
        }

        public void Process(Action<TwitterAuthResponse> success)
        {
            var tokenResponse = _consumer.ProcessUserAuthorization();
            if (tokenResponse == null) return;
            
            var screenName = tokenResponse.ExtraData["screen_name"];
            var userId = int.Parse(tokenResponse.ExtraData["user_id"]);

            var authResponse = new TwitterAuthResponse(userId, screenName, new TokenResponse(tokenResponse));
            success(authResponse);
        }
    }

    public static class UrlExtensions
    {
        public static string SystemUrl(this IUrlRegistry urls, object model, ICurrentHttpRequest request)
        {
            var url = urls.UrlFor(model).ToAbsoluteUrl();

            // TODO -- Figure out what's going on in Fubu
            if (url.StartsWith("/"))
            {
                var fullUri = new Uri(request.FullUrl());
                var qualified = string.Format("{0}://{1}", fullUri.Scheme, fullUri.Host);
                if (!HttpContext.Current.Request.Url.IsDefaultPort)
                {
                    qualified += ":{0}".ToFormat(HttpContext.Current.Request.Url.Port);
                }


                url = url.ToServerQualifiedUrl(qualified);
            }

            return url;
        }
    }
}