using System;
using System.Linq;
using System.Linq.Expressions;
using FubuCore;
using FubuCore.Dates;
using FubuCore.Reflection;
using FubuMVC.Authentication.Endpoints;
using OpenQA.Selenium;
using Serenity;
using Serenity.Fixtures;
using StoryTeller;
using StoryTeller.Assertions;
using StoryTeller.Engine;

namespace FubuMVC.Authentication.Serenity
{
    public class LoginScreenFixture : ScreenFixture<LoginRequest>
    {
        public static By LoginSubmitButton = By.Id("login-submit");
        public static By LoginMessageText = By.Id("login-message");

        public LoginScreenFixture()
        {
            Title = "Login Screen";
        }

        [FormatAs("Go to the login screen")]
        public void OpenLoginScreen()
        {
            Navigation.NavigateTo(new LoginRequest());
        }

        [FormatAs("Logout")]
        public void Logout()
        {
            Navigation.NavigateTo(new LogoutRequest());
        }

        [FormatAs("Login as {user}/{password}")]
        public void Login(string user, string password)
        {
            // TODO -- need to fix this in Serenity
            Wait.Until(() => SearchContext.FindElements(By.Name("UserName")).Any());

            enterValue(x => x.UserName, user);
            enterValue(x => x.Password, password);

            Driver.FindElement(LoginSubmitButton).Click();
        }

        // TODO (checked) -- this needs to be in ScreenFixture, Serenity
        protected IWebElement findElement(Expression<Func<LoginRequest, object>> property)
        {
            return SearchContext.FindElement(By.Name(property.ToAccessor().Name));
        }

        [FormatAs("Check the 'Remember me' checkbox")]
        public void CheckRememberMe()
        {
            IWebElement checkbox = findElement(x => x.RememberMe);
            if (!checkbox.Selected)
            {
                checkbox.Click();
            }
        }

        public IGrammar CheckUserName()
        {
            return CheckScreenValue(x => x.UserName);
        }

        [FormatAs("No message is shown")]
        public bool NoMessageIsShown()
        {
            return TheMessageShouldBe().IsEmpty();
        }

        [FormatAs("The message displayed should be {message}")]
        public string TheMessageShouldBe()
        {
            Wait.Until(() => Driver.FindElements(LoginMessageText).Any());

            return Driver.FindElement(LoginMessageText).Text;
        }

        [FormatAs("The user account locked out message should be displayed")]
        public bool TheLockedOutMessageShouldBeDisplayed()
        {
            string theMessage = TheMessageShouldBe().Trim();
            StoryTellerAssert.Fail(theMessage != LoginKeys.LockedOut.ToString(), () => "Was '{0}'".ToFormat(theMessage));

            return true;
        }

        [FormatAs("Should be on the login screen")]
        public bool IsOnTheLoginScreen()
        {
            string url = Application.Urls.UrlFor(new LoginRequest());
            return Driver.Url.StartsWith(url);
        }

        [FormatAs("Should have moved off the login screen")]
        public bool IsNotOnTheLoginScreen()
        {
            return !IsOnTheLoginScreen();
        }

        [FormatAs("The url should now be {url}")]
        public bool TheUrlIs(string url)
        {
            StoryTellerAssert.Fail(Driver.Url.Split('?').First().Contains(url), "The actual url is " + url);

            return true;
        }

        [FormatAs("Try to open the home page")]
        public void TryToGoHome()
        {
            Navigation.NavigateToHome();
        }

        [FormatAs("After {number} of minutes, reopen the login page")]
        public void ReopenTheLoginScreen(int number)
        {
            var clock = (Clock) Retrieve<IClock>();

            clock.RestartAtLocal(DateTime.Now.AddMinutes(number));

            OpenLoginScreen();
        }
    }
}