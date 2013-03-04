using System.Collections.Generic;
using FubuMVC.PersistedMembership;
using FubuPersistence;
using StoryTeller;
using StoryTeller.Engine;
using System.Linq;

namespace AuthenticationStoryteller.Fixtures
{
    public class LoginAuditingFixture : Fixture
    {
        public LoginAuditingFixture()
        {
            Title = "Login Auditing";
        }

        public IGrammar TheAuditsAre()
         {
             return VerifySetOf(allAudits).Titled("All of the audit messages should be")
                 .Ordered()                         
                 .MatchOn(x => x.Username, x => x.Type);
         }

        private IEnumerable<Audit> allAudits()
        {
            return Retrieve<IEntityRepository>().All<Audit>()
                                                .OrderBy(x => x.Timestamp);
        }
    }
}