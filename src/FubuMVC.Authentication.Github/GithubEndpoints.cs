using System.Collections.Generic;
using System.Reflection;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Authentication.Github
{
    public class GithubEndpoints : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(Assembly assembly)
        {
            yield return ActionCall.For<GithubController>(x => x.Login(null));
            yield return ActionCall.For<GithubController>(x => x.Button(null));
            yield return ActionCall.For<GithubController>(x => x.Callback(null));
        }
    }
}