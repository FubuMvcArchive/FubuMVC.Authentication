using System;
using System.Collections.Generic;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using FubuMVC.Authentication.OAuth;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Twitter
{
    // TODO -- Harden this. It's a nightmare to test thanks to DotNetOpenAuth's internal and/or sealed types
    public class TwitterProxy : ITwitterProxy
    {
        private readonly WebConsumer _consumer;
        private readonly IFubuRequest _request;
        private readonly ISystemUrls _urls;

        public TwitterProxy(IConsumerTokenManager manager, ISignInEndpointBuilder endpointBuilder,
            IFubuRequest request, ISystemUrls urls)
        {
            _request = request;
            _urls = urls;

            var endpoints = endpointBuilder.Build();
            _consumer = new WebConsumer(endpoints, manager);
        }

        public void SignIn()
        {
            var parameters = new Dictionary<string, string>();
            var loginCallback = _request.Get<TwitterLoginCallback>();
            var url = _urls.FullUrlFor(loginCallback);

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
}