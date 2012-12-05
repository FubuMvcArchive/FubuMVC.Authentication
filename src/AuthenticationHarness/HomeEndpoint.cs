using System.Threading;

namespace AuthenticationHarness
{
    public class HomeEndpoint
    {
        public string Index()
        {
            var principal = Thread.CurrentPrincipal;
            return "You are authenticated as " + principal.Identity.Name;
        }
    }
}