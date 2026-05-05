using Microsoft.Playwright;

namespace CreateInvoiceSystem.E2E.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task GoToAsync()
        {
            await _page.GotoAsync("https://createinvoicesystem-frontend-bfabepe5ekbbbec2.polandcentral-01.azurewebsites.net/login");
        }

        public async Task LoginAsync(string email, string password)
        {
            await _page.FillAsync("input[type='email']", email);
            await _page.FillAsync("input[type='password']", password);
            await _page.ClickAsync("button:has-text('Zaloguj')");
        }
    }
}
