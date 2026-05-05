using Microsoft.Playwright;

namespace CreateInvoiceSystem.E2E.Pages
{
    public class DashboardPage
    {
        private readonly IPage _page;

        public DashboardPage(IPage page)
        {
            _page = page;
        }
               
        public ILocator WelcomeHeader => _page.Locator("//h2[contains(text(), 'Witaj w systemie')]");
        public ILocator StatsSection => _page.Locator("//div[@class='row g-4 mb-5']");
        public ILocator QuickActions => _page.Locator("//div[@class='col-12']");
        public ILocator RecentInvoices => _page.Locator("//div[@class='col-lg-8']");
        public ILocator LatestClients => _page.Locator("//div[@class='col-lg-4']");

        public async Task<bool> IsLoadedAsync()
        {            
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
         
            await WelcomeHeader.WaitForAsync(new() { Timeout = 5000 });

            return true;
        }
    }
}
