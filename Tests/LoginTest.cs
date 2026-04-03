using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using PlaywrightPOM.Base;
using PlaywrightPOM.Pages;

namespace PlaywrightPOM.Tests
{
    public class LoginTest : BaseTest
    {
        [Fact]
        public async Task Login_Should_Work()
        {
            var loginPage = new LoginPage(Page);

            await loginPage.Navigate();
            await loginPage.Login("standard_user", "secret_sauce");

            await Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");

            await TakeScreenshotAsync("login_success");
        }
    }

}