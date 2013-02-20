using System;
using System.Linq;
using System.Linq.Expressions;
using FubuCore;
using FubuCore.Reflection;
using OpenQA.Selenium;
using Serenity;
using Serenity.Fixtures;
using Serenity.Fixtures.Handlers;
using StoryTeller;
using StoryTeller.Assertions;
using StoryTeller.Engine;

namespace FubuMVC.Authentication.Serenity
{
    public class LoginScreenFixture : ScreenFixture<LoginRequest>
    {
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
            enterValue(x => x.UserName, user);
            enterValue(x => x.Password, password);
        }

        // TODO (checked) -- this needs to be in ScreenFixture, Serenity
        protected IWebElement findElement(Expression<Func<LoginRequest, object>> property)
        {
            return SearchContext.FindElement(By.Name(property.ToAccessor().Name));
        }

        [FormatAs("Check the 'Remember me' checkbox")]
        public void CheckRememberMe()
        {
            var checkbox = findElement(x => x.RememberMe);
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
            return readValue(x => x.Message).IsEmpty();
        }

        [FormatAs("The message displayed should be {message}")]
        public string TheMessageShouldBe()
        {
            return readValue(x => x.Message);
        }

        [FormatAs("Should be on the login screen")]
        public bool IsOnTheLoginScreen()
        {
            var url = Application.Urls.UrlFor(new LoginRequest());
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
    }

}