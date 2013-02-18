using System;
using System.Diagnostics;

namespace FubuMVC.Authentication
{
    public class AuditMessage
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type
        {
            get { return GetType().Name; }
            set
            {
                // no-op;
            }
        }
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