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

        public ILocator EmailInput => _page.Locator("input[type='email']");
        public ILocator PasswordInput => _page.Locator("input[type='password']");
        public ILocator RememberMeCheckbox => _page.Locator("#rememberMe");
        public ILocator ForgotPasswordLink => _page.Locator("a[href='/forgot-password']");
        public ILocator RegisterLink => _page.Locator("a[href='/register']");
        public ILocator LoginButton => _page.Locator("button[type='submit']");

        private bool _rememberMeInitialState;

        public async Task ToggleRememberMeAsync()
        {
            _rememberMeInitialState = await RememberMeCheckbox.IsCheckedAsync();
            await RememberMeCheckbox.ClickAsync();
        }

        public async Task<bool> WasRememberMeToggledAsync()
        {
            var newState = await RememberMeCheckbox.IsCheckedAsync();
            return newState != _rememberMeInitialState;
        }

        public async Task ClickForgotPasswordAsync()
        {
            await ForgotPasswordLink.ClickAsync();
        }

        public async Task<bool> IsForgotPasswordPageAsync()
        {
            return _page.Url.Contains("forgot-password");
        }

        public async Task ClickRegisterAsync()
        {
            await RegisterLink.ClickAsync();
        }

        public async Task<bool> IsRegisterPageAsync()
        {
            return _page.Url.Contains("register");
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
