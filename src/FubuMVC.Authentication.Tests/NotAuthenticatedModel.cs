using FubuMVC.Core.Ajax;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication.Tests
{
    [NotAuthenticated]
    public class NotAuthenticatedModel
    {
    }

    public class AuthenticatedModel
    {
        
    }

    [NotAuthenticated]
    public class NotAuthenticatedEndpoint
    {
        public AjaxContinuation get_something()
        {
            return AjaxContinuation.Successful();
        }
    }

    public class TestAuthenticationEndpoint<T>
    {
        public AjaxContinuation get_something(T input)
        {
            return AjaxContinuation.Successful();
        }
    }
}