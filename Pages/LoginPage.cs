using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Playwright;

namespace PlaywrightPOM.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        private ILocator UsernameField => _page.Locator("#user-name");
        private ILocator PasswordField => _page.Locator("#password");
        private ILocator LoginButton => _page.Locator("#login-button");

        public async Task GoToAsync() => await _page.GotoAsync("https://www.saucedemo.com/");

        public async Task LoginAsync(string username, string password)
        {
            await UsernameField.FillAsync(username);
            await PasswordField.FillAsync(password);
            await LoginButton.ClickAsync();
        }
    }

}