using FubuCore;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.Utilities
{
    public static class ActionCallExtensions
    {
        public static bool InputIs<TInputModel>(this ActionCall call)
        {
            return call.HasInput && call.InputType().CanBeCastTo<TInputModel>();
        }
    }
}