using System;
using FubuCore.Dates;
using FubuMVC.Authentication;
using FubuPersistence;

namespace FubuMVC.PersistedMembership
{
    public class Audit : Entity
    {
        public AuditMessage Message { get; set; }
        public DateTime Timestamp { get; set; }

        public string Type
        {
            get { return Message.GetType().Name; }
            set
            {
                // no-op;
            }
        }

        public string Username
        {
            get { return Message.UserName; }
            set
            {
                // do nothing
            }
        }
    }

    public class PersistedLoginAuditor : ILoginAuditor
    {
        private readonly ISystemTime _systemTime;
        private readonly ITransaction _transaction;

        public PersistedLoginAuditor(ISystemTime systemTime, ITransaction transaction)
        {
            _systemTime = systemTime;
            _transaction = transaction;
        }

        public void Audit(LoginRequest request)
        {
            AuditMessage message = request.Status == LoginStatus.Succeeded
                                       ? (AuditMessage) new LoginSuccess()
                                       : new LoginFailure();

            message.UserName = request.UserName;

            persistAudit(message);
        }

        public void Audit<T>(T log) where T : AuditMessage
        {
            persistAudit(log);
        }

        private void persistAudit(AuditMessage message)
        {
            var audit = new Audit
            {
                Message = message,
                Timestamp = _systemTime.UtcNow()
            };

            _transaction.WithRepository(repo => repo.Update(audit));
        }
    }
}