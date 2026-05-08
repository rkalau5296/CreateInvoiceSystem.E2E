using BoDi;
using CreateInvoiceSystem.E2E.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace CreateInvoiceSystem.E2E.Hooks
{
    [Binding]
    public class PlaywrightHooks
    {
        private readonly IObjectContainer _container;
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        public PlaywrightHooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var headless = config.GetValue<bool>("Playwright:Headless");
            var slowMo = config.GetValue<int>("Playwright:SlowMo");
            var baseUrl = config.GetValue<string>("App:BaseUrl") ?? string.Empty;

            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless,
                SlowMo = slowMo
            });

            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();

            _container.RegisterInstanceAs(_page);
            _container.RegisterInstanceAs(new AppSettings { BaseUrl = baseUrl });

            _container.RegisterTypeAs<LoginPage, LoginPage>();
            _container.RegisterTypeAs<DashboardPage, DashboardPage>();
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _page.CloseAsync();
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}