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

        // Locators
        private ILocator Username => _page.GetByPlaceholder("Username");
        private ILocator Password => _page.GetByPlaceholder("Password");
        private ILocator LoginBtn => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });

        // Actions
        public async Task Navigate()
        {
            await _page.GotoAsync("https://www.saucedemo.com/");
        }

        public async Task Login(string user, string pass)
        {
            await Username.FillAsync(user);
            await Password.FillAsync(pass);
            await LoginBtn.ClickAsync();
        }
    }

}
