using Microsoft.Playwright;

namespace CreateInvoiceSystem.E2E.Pages
{
    public class ForgotPasswordPage
    {
        private readonly IPage _page;
        private readonly string _baseUrl = "https://createinvoicesystem-frontend-bfabepe5ekbbbec2.polandcentral-01.azurewebsites.net";

        public ForgotPasswordPage(IPage page)
        {
            _page = page;
        }

        public async Task GoToAsync()
        {
            await _page.GotoAsync($"{_baseUrl}/forgot-password");
        }

        public async Task<bool> IsLoadedAsync()
        {
            await _page.WaitForSelectorAsync("h3", new() { Timeout = 5000 });
            var text = await _page.Locator("h3").InnerTextAsync();
            return text.Contains("Resetowanie hasła");
        }

        public ILocator Header => _page.Locator("h3");
        public ILocator EmailInput => _page.Locator("input[placeholder='Twój e-mail']");
        public ILocator ResetButton => _page.Locator("button:has-text('Wyślij link resetujący')");
        public ILocator BackToLoginLink => _page.Locator("a[href='/login']");

        public async Task<bool> IsHeaderVisibleAsync()
        {
            await _page.WaitForSelectorAsync("h3");
            var text = await Header.InnerTextAsync();
            return text.Contains("Resetowanie hasła");
        }

        public async Task<bool> IsEmailInputVisibleAsync() => await EmailInput.IsVisibleAsync();
        public async Task<bool> IsResetButtonVisibleAsync() => await ResetButton.IsVisibleAsync();
        public async Task<bool> IsBackToLoginVisibleAsync() => await BackToLoginLink.IsVisibleAsync();

        public async Task EnterEmailAsync(string email)
        {
            await EmailInput.FillAsync(email);
        }

        public async Task ClickResetButtonAsync()
        {
            await ResetButton.ClickAsync();
        }        

        public async Task GoToConfirmationAsync()
        {
            await _page.GotoAsync($"{_baseUrl}/forgot-password/");
        }

        public ILocator SuccessHeader => _page.Locator("h3");
        public ILocator SuccessMessage => _page.Locator("//p[@class='mb-0 small']");
        public ILocator GoToLoginButton => _page.Locator("button:has-text('Przejdź do strony logowania')");

        public async Task<bool> IsSuccessHeaderVisibleAsync()
        {
            await _page.WaitForSelectorAsync("h3");
            var text = await SuccessHeader.InnerTextAsync();
            return text.Contains("Resetowanie hasła");
        }

        public async Task<bool> IsMessageVisibleAsync()
        {
            await _page.WaitForSelectorAsync("//p[@class='mb-0 small']", new() { Timeout = 5000 });            
            var text = await SuccessMessage.InnerTextAsync();
            return text.Contains("Jeśli podany adres e-mail znajduje się w naszej bazie, wysłaliśmy na niego instrukcję resetowania hasła. Prosimy o sprawdzenie skrzynki e-mail (oraz folderu Spam).");
        }
        
        public async Task<bool> IsGoToLoginButtonVisibleAsync() => await GoToLoginButton.IsVisibleAsync();

        public async Task ClickSendResetLinkAsync()
        {
            await _page.ClickAsync("button[type='submit']");
        }

        public async Task<bool> IsEmailFieldInvalidAsync()
        {
            var input = await _page.QuerySelectorAsync("input.form-control");
            var className = await input.GetAttributeAsync("class");
            return className.Contains("is-invalid");
        }

        public async Task<string> GetEmailFieldErrorMessageAsync()
        {
            var error = await _page.QuerySelectorAsync(".text-danger.small");
            return (await error.InnerTextAsync()).Trim();
        }

        public async Task<string> GetGlobalValidationAlertAsync()
        {
            var alert = await _page.QuerySelectorAsync(".alert.alert-danger");
            return (await alert.InnerTextAsync()).Trim();
        }

    }

}
