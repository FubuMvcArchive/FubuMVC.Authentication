using System;
using FubuCore;
using FubuMVC.Core.Registration.ObjectGraph;

namespace FubuMVC.Authentication.Membership
{
    public class MembershipNode : AuthenticationNode
    {
        private readonly Type _membershipType;

        public MembershipNode(Type membershipType) : base(typeof (MembershipAuthentication))
        {
            if (!membershipType.CanBeCastTo<IMembershipRepository>())
            {
                throw new ArgumentOutOfRangeException("membershipType",
                                                      "membershipType has to be assignable to IMembershipRepository");
            }

            _membershipType = membershipType;
        }

        public Type MembershipType
        {
            get { return _membershipType; }
        }

        protected override void configure(ObjectDef def)
        {
            def.DependencyByType<IMembershipRepository>(new ObjectDef(_membershipType));
        }

        public static MembershipNode For<T>() where T : IMembershipRepository
        {
            return new MembershipNode(typeof (T));
        }
    }
}