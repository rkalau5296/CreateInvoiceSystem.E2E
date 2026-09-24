using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace CreateInvoiceSystem.E2E.Pages;

public record ClientData(string FirmName, string NIP, string Email, string Street, string Number, string ZipCode, string City);

public class ClientsPage(IPage page)
{
    private readonly IPage _page = page;

    public ILocator Header => _page.GetByRole(AriaRole.Heading, new() { Name = "Zarządzanie Klientami" });
    private ILocator AddClientBtn => _page.GetByRole(AriaRole.Button, new() { Name = "Dodaj klienta" });
    private ILocator SearchInput => _page.GetByPlaceholder("Szukaj po nazwie lub NIP...");
    private ILocator TableRows => _page.Locator("table tbody tr");
    private ILocator FooterText => _page.Locator(".text-muted.small");
    private ILocator ValidationErrors => _page.Locator(".text-danger.small");
    private ILocator SaveBtn => _page.GetByRole(AriaRole.Button, new() { Name = "Zapisz" });
    public ILocator ClientsNavLink => _page.GetByRole(AriaRole.Link, new() { Name = "👥 Klienci" });                                

    private ILocator InputFirmName => _page.Locator("label:has-text('Nazwa firmy') + div input");
    private ILocator InputNIP => _page.Locator("label:has-text('NIP') + div input");
    private ILocator InputEmail => _page.Locator("label:has-text('E-mail') + div input");
    private ILocator InputStreet => _page.Locator("label:has-text('Ulica') + div input");
    private ILocator InputNumber => _page.Locator("label:has-text('Numer') + div input");
    private ILocator InputZipCode => _page.Locator("label:has-text('Kod pocztowy') + div input");
    private ILocator InputCity => _page.Locator("label:has-text('Miasto') + div input");

    public async Task NavigateAsync(string baseUrl) => await _page.GotoAsync($"{baseUrl}/clients");

    public async Task ClickAddClientAsync() => await AddClientBtn.ClickAsync();

    public async Task SearchAsync(string query) 
    {
        await SearchInput.FillAsync(query);
        await SearchInput.PressAsync("Enter");
    }

    public async Task FillClientFormAsync(ClientData data)
    {
        await InputFirmName.FillAsync(data.FirmName);
        await InputNIP.FillAsync(data.NIP);
        await InputEmail.FillAsync(data.Email);
        await InputStreet.FillAsync(data.Street);
        await InputNumber.FillAsync(data.Number);
        await InputZipCode.FillAsync(data.ZipCode);
        await InputCity.FillAsync(data.City);
    }

    public async Task ClickSaveAsync() => await SaveBtn.ClickAsync();

    public async Task ClickActionForClientAsync(string actionName, string clientName)
    {
        string safePattern = Regex.Replace(actionName, @"[^\x00-\x7F]", ".");
        var actionRegex = new Regex(safePattern, RegexOptions.IgnoreCase);

        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await TableRows
            .Filter(new() { HasText = clientName })
            .GetByRole(AriaRole.Button, new() { NameRegex = actionRegex })
            .ClickAsync();
    }

    public async Task<bool> IsClientVisibleAsync(string clientName) =>
        (await TableRows.AllInnerTextsAsync()).Any(t => t.Contains(clientName));

    public async Task<int> GetClientCountAsync() => await TableRows.CountAsync();

    public async Task<IEnumerable<string>> GetValidationMessagesAsync() =>
        await ValidationErrors.AllInnerTextsAsync();

    public async Task<string> GetClientRowTextAsync(string clientName) =>
        await TableRows.Filter(new() { HasText = clientName }).InnerTextAsync();

    public async Task<string> GetFooterTextAsync()
    {
        var footer = FooterText.First;
        var text = await footer.InnerTextAsync();
        return Regex.Replace(text, @"\s+", " ").Trim();
    }
}