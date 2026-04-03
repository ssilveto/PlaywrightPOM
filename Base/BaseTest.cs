using Microsoft.Playwright;

namespace PlaywrightPOM.Base;



    public abstract class BaseTest : IAsyncLifetime
    {
        protected IPlaywright Playwright = null!;
        protected IBrowser Browser = null!;
        protected IBrowserContext Context = null!;
        protected IPage Page = null!;

        protected virtual string BrowserType => "chromium";
        protected string Timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        protected string BaseDir =>
            Path.Combine(Directory.GetCurrentDirectory(), "videos");

        public async Task InitializeAsync()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            Browser = BrowserType switch
            {
                "chromium" => await Playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 200 }),
                "firefox" => await Playwright.Firefox.LaunchAsync(new() { Headless = false, SlowMo = 200 }),
                "webkit" => await Playwright.Webkit.LaunchAsync(new() { Headless = false, SlowMo = 200 }),
                _ => throw new Exception("Invalid browser")
            };

            Context = await Browser.NewContextAsync(new()
            {
                RecordVideoDir = Path.Combine(BaseDir, GetType().Name, BrowserType, Timestamp),
                RecordVideoSize = new() { Width = 1280, Height = 720 }
            });

            Page = await Context.NewPageAsync();
        }

        public async Task DisposeAsync()
        {
            await Page.WaitForTimeoutAsync(1000);

            await Context.CloseAsync();
            await Browser.CloseAsync();
            Playwright.Dispose();
        }

        protected async Task TakeScreenshotAsync(string name)
        {
            var dir = Path.Combine(BaseDir, GetType().Name, BrowserType, Timestamp, "screenshots");
            Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, $"{name}.png");

            await Page.ScreenshotAsync(new() { Path = path });
        }
    }


