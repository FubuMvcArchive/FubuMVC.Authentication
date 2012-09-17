using DotNetOpenAuth.OAuth;

namespace FubuMVC.Authentication.Twitter
{
    public interface ISignInEndpointBuilder
    {
        ServiceProviderDescription Build();
    }
}