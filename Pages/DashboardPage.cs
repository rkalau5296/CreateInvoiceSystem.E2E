using Microsoft.Playwright;
using System.Runtime.CompilerServices;

namespace CreateInvoiceSystem.E2E.Pages
{
    public class DashboardPage
    {
        private readonly IPage _page;

        public DashboardPage(IPage page)
        {
            _page = page;
        }

        public IPage Page => _page;

        public ILocator WelcomeHeader => _page.Locator("//h2[contains(text(), 'Witaj w systemie')]");
        public ILocator StatsSection => _page.Locator("//div[@class='row g-4 mb-5']");
        public ILocator QuickActions => _page.Locator("//div[@class='col-12']");
        public ILocator RecentInvoices => _page.Locator("//div[@class='col-lg-8']");
        public ILocator LatestClients => _page.Locator("//div[@class='col-lg-4']");
        public ILocator Spinner => _page.Locator(".spinner-border");

        public async Task<bool> IsLoadedAsync()
        {
            await Spinner.WaitForAsync(new()
            {
                State = WaitForSelectorState.Detached,
                Timeout = 5000
            });
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await WelcomeHeader.WaitForAsync(new() { Timeout = 5000 });
            return true;
        }

        public ILocator QuickActionWystawFakture =>  _page.GetByRole(AriaRole.Button, new() { Name = "Wystaw fakturę" });

        public ILocator QuickActionPrzegladajFaktury => _page.GetByRole(AriaRole.Button, new() { Name = "Przeglądaj faktury" });

        public ILocator QuickActionKontrahenci => _page.GetByRole(AriaRole.Button, new() { Name = "Kontrahenci" });

        public async Task ClickWystawFakture()
        {
            await QuickActionWystawFakture.ClickAsync();
        }

        public async Task ClickPrzegladajFaktury()
        {
            await QuickActionPrzegladajFaktury.ClickAsync();
        }

        public async Task ClickKontrahenci()
        {
            await QuickActionKontrahenci.ClickAsync();
        }
    }
}
