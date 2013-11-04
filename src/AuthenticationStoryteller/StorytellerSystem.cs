using FubuCore.Binding;
using FubuMVC.Core;
using FubuMVC.PersistedMembership;
using FubuMVC.Spark;
using FubuMVC.StructureMap;
using Serenity;
using Spark;
using StructureMap;

namespace AuthenticationStoryteller
{
    public class AuthenticationStorytellerApplication : IApplicationSource
    {
        public FubuApplication BuildApplication()
        {
            return FubuApplication.For<StorytellerFubuRegistry>().StructureMap(new Container());
        }
    }

    public class StorytellerFubuRegistry : FubuRegistry
    {
        public StorytellerFubuRegistry()
        {
            Import<PersistedMembership<User>>();

            AlterSettings<SparkEngineSettings>(x => {
                x.PrecompileViews = false;
            });
        }
    }

    public class StorytellerSystem : FubuMvcSystem<AuthenticationStorytellerApplication>
    {
        protected override void configureApplication(IApplicationUnderTest application, BindingRegistry binding)
        {
            WebDriverSettings.Current.Browser = BrowserType.Chrome;
        }
    }
}