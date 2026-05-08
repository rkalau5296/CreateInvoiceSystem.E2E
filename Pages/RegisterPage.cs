using Microsoft.Playwright;

namespace CreateInvoiceSystem.E2E.Pages
{
    public class RegisterPage
    {
        private readonly IPage _page;
        private readonly string _baseUrl;

        public RegisterPage(IPage page, AppSettings settings)
        {
            _page = page;
            _baseUrl = settings.BaseUrl;
        }

        public async Task GoToAsync()
        {
            await _page.GotoAsync($"{_baseUrl}register");
        }

        public async Task ClickRegisterButtonAsync()
        {
            await _page.ClickAsync("button[type='submit']");
        }

        public async Task EnterAsync(string placeholder, string value)
        {
            await _page.FillAsync($"input[placeholder='{placeholder}']", value);
        }

        public async Task<bool> IsInvalidAsync(string placeholder)
        {
            var input = await _page.QuerySelectorAsync($"input[placeholder='{placeholder}']");
            var cls = await input.GetAttributeAsync("class");
            return cls.Contains("is-invalid");
        }

        public async Task<bool> IsValidAsync(string placeholder)
        {
            var input = await _page.QuerySelectorAsync($"input[placeholder='{placeholder}']");
            var cls = await input.GetAttributeAsync("class");
            return cls.Contains("is-valid");
        }

        public async Task<string> GetErrorMessageAsync(string message)
        {
            var el = await _page.QuerySelectorAsync($".text-danger.small:has-text('{message}')");
            return (await el.InnerTextAsync()).Trim();
        }

        public async Task<string> GetGlobalAlertAsync()
        {
            var alert = await _page.QuerySelectorAsync(".alert.alert-danger");
            return (await alert.InnerTextAsync()).Trim();
        }

        public async Task ClickBackToLoginAsync()
        {
            await _page.ClickAsync("a[href='/login']");
        }

        public async Task<string> CurrentUrl()
        {
            return await Task.FromResult(_page.Url);
        }
    }
}
