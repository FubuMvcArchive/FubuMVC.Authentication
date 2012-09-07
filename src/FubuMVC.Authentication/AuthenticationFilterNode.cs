using System;
using System.Collections.Generic;
using FubuCore;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;

namespace FubuMVC.Authentication
{
    public class AuthenticationFilterNode : Process
    {
        private readonly IList<Action<ObjectDef>> _alterations = new List<Action<ObjectDef>>(); 

        public AuthenticationFilterNode() : base(typeof(AuthenticationBehavior))
        {
        }

        public void Modify(Action<ObjectDef> action)
        {
            _alterations.Add(action);
        }

        public ObjectDef ReplaceService<TDependency, TConcrete>() 
            where TConcrete : TDependency
        {
            return ReplaceService(typeof (TDependency), typeof (TConcrete));
        }

        public ObjectDef ReplaceService(Type dependencyType, Type actualType)
        {
            ObjectDef target = null;

            Modify(def =>
            {
                var existing = def.DependencyFor(dependencyType);
                if (existing != null)
                {
                    target = existing.As<ConfiguredDependency>().Definition;
                    target.Type = actualType;
                    return;
                }

                def.DependencyByType(dependencyType, actualType);
            });

            return target;
        }

        protected override ObjectDef buildObjectDef()
        {
            var def = base.buildObjectDef();
            _alterations.Each(x => x(def));

            return def;
        }
    }
}