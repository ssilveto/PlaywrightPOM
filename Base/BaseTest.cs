using Microsoft.Playwright;

namespace PlaywrightPOM.Base;



public class BaseTest
{
    protected IBrowser Browser { get; private set; }
    protected IPage Page { get; private set; }
    protected IBrowserContext Context { get; private set; }
    protected IPlaywright PlaywrightInstance { get; private set; }

    // Инициализация с browserName
    public async Task InitializeAsync(string browserName)
    {
        PlaywrightInstance = await Playwright.CreateAsync();

        BrowserTypeLaunchOptions launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 100
        };

        Browser = browserName.ToLower() switch
        {
            "firefox" => await PlaywrightInstance.Firefox.LaunchAsync(launchOptions),
            "webkit" => await PlaywrightInstance.Webkit.LaunchAsync(launchOptions),
            _ => await PlaywrightInstance.Chromium.LaunchAsync(launchOptions),
        };

        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            RecordVideoDir = Path.Combine("videos", browserName),
            RecordVideoSize = new RecordVideoSize { Width = 1280, Height = 720 }
        });

        Page = await Context.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        await Context.CloseAsync();
        await Browser.CloseAsync();
        PlaywrightInstance.Dispose();
    }

    // Screenshot с timestamp
    protected async Task TakeScreenshotAsync(string testName, string browserName)
    {
        string folder = Path.Combine("screenshots", browserName);
        Directory.CreateDirectory(folder);

        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string filePath = Path.Combine(folder, $"{testName}_{timestamp}.png");

        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = filePath });
    }
}
