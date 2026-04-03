using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using PlaywrightPOM.Base;
using PlaywrightPOM.Pages;

namespace PlaywrightPOM.Tests
{
    public class LoginTest : BaseTest, IAsyncLifetime
    {


        [Theory]
        [InlineData("chromium")]
        [InlineData("firefox")]
        [InlineData("webkit")]
        public async Task Login_Should_Work(string browserName)
        {
            await InitializeAsync(browserName);

            var loginPage = new LoginPage(Page);
            await loginPage.GoToAsync();
            await loginPage.LoginAsync("standard_user", "secret_sauce");

            Assert.Contains("inventory.html", Page.Url);

            await TakeScreenshotAsync("LoginTest", browserName);

            await DisposeAsync();
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public Task DisposeAsync() => Task.CompletedTask;

    }
}