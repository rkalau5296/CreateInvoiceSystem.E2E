using Microsoft.Playwright;

namespace CreateInvoiceSystem.E2E.Pages;

public class RegisterPage(IPage page, AppSettings settings)
{
    private readonly string _baseUrl = settings.BaseUrl;

    private ILocator EmailInput => page.GetByPlaceholder("Email");
    private ILocator PasswordInput => page.GetByPlaceholder("Password");
    private ILocator ConfirmPasswordInput => page.GetByPlaceholder("Confirm password");
    private ILocator RegisterButton => page.Locator("//div/button");
    private ILocator BackToLoginLink => page.Locator("a[href='/login']");
    private ILocator GlobalAlert => page.Locator(".alert.alert-danger");

    public async Task GoToAsync()
    {
        await page.GotoAsync($"{_baseUrl}/register");
    }

    public async Task RegisterAsync(
        string email,
        string password,
        string confirmPassword)
    {
        await EmailInput.FillAsync(email);
        await PasswordInput.FillAsync(password);
        await ConfirmPasswordInput.FillAsync(confirmPassword);
        await RegisterButton.ClickAsync();
    }

    public async Task EnterAsync(string placeholder, string value)
    {
        await InputByPlaceholder(placeholder)
            .FillAsync(value);
    }

    public async Task ClickRegisterButtonAsync()
    {
        await RegisterButton.ClickAsync();
    }

    public async Task<bool> IsInvalidAsync(string placeholder)
    {
        var className = await InputByPlaceholder(placeholder)
            .GetAttributeAsync("class");

        return className?.Contains("is-invalid") == true;
    }

    public async Task<bool> IsValidAsync(string placeholder)
    {
        var className = await InputByPlaceholder(placeholder)
            .GetAttributeAsync("class");

        return className?.Contains("is-valid") == true;
    }

    public async Task<string> GetErrorMessageAsync(string message)
    {
        var element = ErrorMessage(message);

        return (await element.InnerTextAsync()).Trim();
    }

    public async Task<string> GetGlobalAlertAsync()
    {
        return (await GlobalAlert.InnerTextAsync()).Trim();
    }

    public async Task ClickBackToLoginAsync()
    {
        await BackToLoginLink.ClickAsync();
    }

    public string CurrentUrl()
    {
        return page.Url;
    }

    private ILocator InputByPlaceholder(string placeholder)
    {
        return page.GetByPlaceholder(
            placeholder,
            new PageGetByPlaceholderOptions
            {
                Exact = true
            });
    }

    private ILocator ErrorMessage(string message)
    {
        return page.Locator(
            ".text-danger.small",
            new PageLocatorOptions
            {
                HasTextString = message
            });
    }
}