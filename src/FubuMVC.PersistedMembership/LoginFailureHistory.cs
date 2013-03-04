using System;
using FubuPersistence;

namespace FubuMVC.PersistedMembership
{
    public class LoginFailureHistory : Entity
    {
        public string UserName { get; set; }
        public int Attempts { get; set; }
        public DateTime? LockedOutTime { get; set; }
    }
}