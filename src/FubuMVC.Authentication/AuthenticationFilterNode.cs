using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication
{
    public class AuthenticationFilterNode : Process
    {
        public AuthenticationFilterNode() : base(typeof(AuthenticationBehavior))
        {
        }
    }
}