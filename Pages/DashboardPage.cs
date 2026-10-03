using Microsoft.Playwright;

namespace CreateInvoiceSystem.E2E.Pages
{
    public class DashboardPage(IPage page)
    {
        public IPage Page => page;

        public ILocator WelcomeHeader => page.Locator("//h2[contains(text(), 'Witaj w systemie')]");
        public ILocator StatsSection => page.Locator("//div[@class='row g-4 mb-5']");
        public ILocator QuickActions => page.Locator("//div[@class='col-12']");
        public ILocator RecentInvoices => page.Locator("//div[@class='col-lg-8']");
        public ILocator LatestClients => page.Locator("//div[@class='col-lg-4']");
        public ILocator Spinner => page.Locator(".spinner-border");
        public ILocator QuickActionWystawFakture => page.GetByRole(AriaRole.Button, new() { Name = "Wystaw fakturę" });
        public ILocator QuickActionPrzegladajFaktury => page.GetByRole(AriaRole.Button, new() { Name = "Przeglądaj faktury" });
        public ILocator QuickActionKontrahenci => page.GetByRole(AriaRole.Button, new() { Name = "Kontrahenci" });


        public async Task<bool> IsLoadedAsync()
        {
            await Spinner.WaitForAsync(new()
            {
                State = WaitForSelectorState.Detached,
                Timeout = 5000
            });
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await WelcomeHeader.WaitForAsync(new() { Timeout = 5000 });
            return true;
        }
        
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
