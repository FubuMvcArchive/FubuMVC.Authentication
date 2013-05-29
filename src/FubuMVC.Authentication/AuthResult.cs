using FubuMVC.Core.Continuations;

namespace FubuMVC.Authentication
{
    public class AuthResult
    {
        public bool Success;
        public FubuContinuation Continuation;

        public bool IsDeterministic()
        {
            return Success || Continuation != null;
        }

        public static AuthResult Failed()
        {
            return new AuthResult{Success = false};
        }

        public static AuthResult Successful()
        {
            return new AuthResult{Success = true};
        }
    }
}