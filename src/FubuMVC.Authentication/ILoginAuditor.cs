using System;
using System.Diagnostics;

namespace FubuMVC.Authentication
{
    public class AuditMessage
    {
        public string UserName { get; set; }
    }

    public class LoginSuccess : AuditMessage
    {
        // TODO -- add IP address & probably other stuff
    }

    public class LoginFailure : AuditMessage
    {
        // TODO -- add IP address & probably other stuff
    }

    public interface ILoginAuditor
    {
        void Audit(LoginRequest request);
        void Audit<T>(T log) where T : AuditMessage;
    }

    public class NulloLoginAuditor : ILoginAuditor
    {
        public void Audit(LoginRequest request)
        {
            Debug.WriteLine(request);
        }

        public void Audit<T>(T log) where T : AuditMessage
        {
            Debug.WriteLine(log);
        }
    }
}