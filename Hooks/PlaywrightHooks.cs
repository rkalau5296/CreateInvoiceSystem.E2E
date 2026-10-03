using CreateInvoiceSystem.E2E.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Reqnroll;
using Reqnroll.BoDi;

namespace CreateInvoiceSystem.E2E.Hooks
{
    [Binding]
    public class PlaywrightHooks(IObjectContainer container)
    {
        private readonly IObjectContainer _container = container;
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IPage? _page;

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    "appsettings.json",
                    optional: false,
                    reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var headless = config.GetValue<bool>("Playwright:Headless");
            var slowMo = config.GetValue<int>("Playwright:SlowMo");
            var baseUrl = config.GetValue<string>("App:BaseUrl") ?? string.Empty;
            var browser = config.GetValue<string>("Playwright:Browser") ?? "chromium";
            var login = config.GetValue<string>("LoggingData:login") ?? string.Empty;
            var password = config.GetValue<string>("LoggingData:password") ?? string.Empty;

            _playwright = await Playwright.CreateAsync();            

            _browser = browser switch
            {
                "firefox" => await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless,
                    SlowMo = slowMo
                }),
                "webkit" => await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless,
                    SlowMo = slowMo
                }),
                "msedge" => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless,
                    SlowMo = slowMo,
                    Channel = "msedge"
                }),
                "chrome" => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless,
                    SlowMo = slowMo,
                    Channel = "chrome"
                }),
                _ => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless,
                    SlowMo = slowMo
                })
            };


            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();

            _container.RegisterInstanceAs(_page);
            _container.RegisterInstanceAs(new AppSettings { BaseUrl = baseUrl, Login = login, Password = password});

            _container.RegisterTypeAs<LoginPage, LoginPage>();
            _container.RegisterTypeAs<DashboardPage, DashboardPage>();
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            if (_page != null)
            {
                await _page.CloseAsync();
            }

            if (_browser != null)
            {
                await _browser.CloseAsync();
            }

            _playwright?.Dispose();
        }
    }
}