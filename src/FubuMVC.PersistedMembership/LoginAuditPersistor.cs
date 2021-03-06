﻿using FubuMVC.Authentication;
using FubuPersistence;
using Raven.Client;

namespace FubuMVC.PersistedMembership
{
    public class LoginAuditPersistor
    {
        private readonly IEntityRepository _repository;
        private readonly IDocumentSession _session;

        public LoginAuditPersistor(IEntityRepository repository, IDocumentSession session)
        {
            _repository = repository;
            _session = session;
        }

        public void LogFailure(LoginRequest request, Audit audit)
        {
            _repository.Update(audit);

            var history = _session.Load<LoginFailureHistory>(request.UserName) ?? new LoginFailureHistory
            {
                Id = request.UserName
            };

            history.Attempts = request.NumberOfTries;
            history.LockedOutTime = request.LockedOutUntil;

            _session.Store(history);
        }

        public void LogSuccess(LoginRequest request, Audit audit)
        {
            _repository.Update(audit);

            var history = _session.Load<LoginFailureHistory>(request.UserName);
            if (history != null)
            {
                _session.Delete(history);
            }
        }

        public void ApplyHistory(LoginRequest request)
        {
            var history = _session.Load<LoginFailureHistory>(request.UserName);
            if (history == null) return;

            request.NumberOfTries = history.Attempts;
            request.LockedOutUntil = history.LockedOutTime;
        }

    }
}