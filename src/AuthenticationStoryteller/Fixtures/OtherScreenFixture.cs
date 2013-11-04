using FubuCore;
using FubuMVC.Authentication.Serenity;
using StoryTeller.Engine;

namespace AuthenticationStoryteller.Fixtures
{
    public class OtherScreenFixture : LoginScreenFixture
    {
        [FormatAs("Go to a different page for {name}")]
        public void GoToDifferentPage(string name)
        {
            Navigation.NavigateTo(new DifferentInput{Name = name});
        }

        [FormatAs("Should be on the different page for {name}")]
        public bool ShouldBeOnTheDifferentPage(string name)
        {
            var url = Application.Urls.UrlFor(new DifferentInput {Name = name});
            return Application.Browser.Driver.Url.EqualsIgnoreCase(url);
        }
    }
}