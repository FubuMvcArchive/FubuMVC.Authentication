using System.Diagnostics;

namespace FubuMVC.Authentication
{
    public interface ILoginAuditor
    {
        void Audit(LoginRequest request);
    }

    public class NulloLoginAuditor : ILoginAuditor
    {
        public void Audit(LoginRequest request)
        {
            Debug.WriteLine(request);
        }
    }
}