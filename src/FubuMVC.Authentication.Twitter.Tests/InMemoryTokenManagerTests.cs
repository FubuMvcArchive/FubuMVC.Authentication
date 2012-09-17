using System;
using DotNetOpenAuth.OAuth.Messages;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class InMemoryTokenManagerTests : InteractionContext<InMemoryTokenManager>
    {
        private OAuthSettings theSettings;

        protected override void beforeEach()
        {
            theSettings = new OAuthSettings
                              {
                                  ConsumerKey = Guid.NewGuid().ToString(),
                                  ConsumerSecret = Guid.NewGuid().ToString()
                              };

            Services.Inject(theSettings);
        }

        [Test]
        public void uses_the_consumer_key_setting()
        {
            ClassUnderTest.ConsumerKey.ShouldEqual(theSettings.ConsumerKey);
        }

        [Test]
        public void uses_the_consumer_secret_setting()
        {
            ClassUnderTest.ConsumerSecret.ShouldEqual(theSettings.ConsumerSecret);
        }

        [Test]
        public void stores_the_token_secret()
        {
            var response = new StubTokenSecretContainingMessage {Token = "Test Token", TokenSecret = "Test Secret"};
            ClassUnderTest.StoreNewRequestToken(null, response);
            ClassUnderTest.GetTokenSecret(response.Token).ShouldEqual(response.TokenSecret);
        }

        [Test]
        public void removes_and_stores_the_new_secret()
        {
            var oldToken = "Old Token";
            var token = "New Token";
            var secret = "Test Secret";

            ClassUnderTest.ExpireRequestTokenAndStoreNewAccessToken(null, oldToken, token, secret);
            ClassUnderTest.GetTokenSecret(oldToken).ShouldBeNull();
            ClassUnderTest.GetTokenSecret(token).ShouldEqual(secret);
        }
    }

    public class StubTokenSecretContainingMessage : ITokenSecretContainingMessage
    {
        public string Token { get; set; }
        public string TokenSecret { get; set; }
    }
}