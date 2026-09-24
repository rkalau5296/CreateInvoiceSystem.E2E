using Microsoft.Playwright;
using Reqnroll;
using static Microsoft.Playwright.Assertions;

namespace CreateInvoiceSystem.E2E.Pages
{
    public record InvoiceData(string Title, string PaymentMethod, string ClientName, string Nip, string Email, string Street, string HouseNumber, string PostalCode, string City);
    public record InvoicePositions(string Product, string Quantity, string Price, string Desription);

    public class InvoicesPage
    {
        private readonly IPage _page;

        public InvoicesPage(IPage page)
        {
            _page = page;
        }

        private ILocator SearchInput => _page.Locator("input[placeholder*='Numer faktury']");
        private ILocator TableRows => _page.Locator("table tbody tr");
        public ILocator InvoicesNavLink => _page.GetByRole(AriaRole.Link, new() { NameRegex = Helper.ToSafeRegex("Faktury") });
        private ILocator InputTitle => _page.Locator("label:has-text('Tytuł faktury') + input");
        private ILocator PaymentMethod => _page.Locator("label:has-text('Metoda płatności') + select");
        private ILocator InputClientName => _page.Locator("label:has-text('Wybierz klienta lub wpisz nową nazwę') + input");
        private ILocator InputNip => _page.Locator("label:has-text('NIP') + input");
        private ILocator InputEmail => _page.Locator("label:has-text('Email klienta') + input");
        private ILocator InputStreet => _page.Locator("label:has-text('Ulica') + input");
        private ILocator InputHouseNumber => _page.Locator("label:has-text('Nr domu/lok.') + input");
        private ILocator InputPostalCode => _page.Locator("label:has-text('Kod pocztowy') + input");
        private ILocator InputCity => _page.Locator("label:has-text('Miasto') + input");
        private ILocator InputProduct => _page.Locator("input[placeholder = 'Nazwa produktu...']");
        private ILocator InputQunatity => _page.Locator("input[min='1']");
        private ILocator InputPrice => _page.Locator("input[step='0.01']");        

        public async Task NavigateAsync()
        {
            await InvoicesNavLink.ClickAsync();
        }        

        public async Task ClickButtonAsync(string buttonName)
        {
            var safeRegex = Helper.ToSafeRegex(buttonName);
            await _page.GetByRole(AriaRole.Button, new() { NameRegex = safeRegex }).ClickAsync();
        }

        public async Task SearchAsync(string query)
        {
            await SearchInput.FillAsync(query);
            await SearchInput.PressAsync("Enter");
            await SearchInput.BlurAsync();
        }

        public async Task ClickActionForInvoiceAsync(string actionName, string invoiceNumber)
        {
            var safeActionRegex = Helper.ToSafeRegex(actionName);
            var safeInvoiceRegex = Helper.ToSafeRegex(invoiceNumber);

            await TableRows
                .Filter(new() { HasTextRegex = safeInvoiceRegex })
                .GetByRole(AriaRole.Button, new() { NameRegex = safeActionRegex })
                .ClickAsync();            
        }

        public async Task ClickDeleteInvoiceAsync(string actionName, string invoiceNumber)
        {
            var safeActionRegex = Helper.ToSafeRegex(actionName);
            var safeInvoiceRegex = Helper.ToSafeRegex(invoiceNumber);
                        
            _page.Dialog += async (_, dialog) =>
            {
                await dialog.AcceptAsync();
            };

            await TableRows
                .Filter(new() { HasTextRegex = safeInvoiceRegex })
                .GetByRole(AriaRole.Button, new() { NameRegex = safeActionRegex })
                .ClickAsync();
        }

        public async Task AssertInvoiceIsVisibleAsync(string invoiceIdentifier)
        {
            var row = _page.GetByRole(AriaRole.Row)
                .Filter(new() { HasText = invoiceIdentifier })
                .Nth(0);

            await Expect(row).ToBeVisibleAsync();
        }

        public async Task AssertInvoiceIsNotVisibleAsync(string invoiceNumber)
        {
            var safeRegex = Helper.ToSafeRegex(invoiceNumber);
            await Expect(TableRows.Filter(new() { HasTextRegex = safeRegex })).ToBeHiddenAsync();
        }

        public async Task FillFormAsync(InvoiceData data)
        {            
            await InputTitle.FillAsync(data.Title);
            await PaymentMethod.SelectOptionAsync(data.PaymentMethod);
            await InputClientName.FillAsync(data.ClientName);
            await InputNip.FillAsync(data.Nip);
            await InputEmail.FillAsync(data.Email);
            await InputStreet.FillAsync(data.Street);
            await InputHouseNumber.FillAsync(data.HouseNumber);
            await InputPostalCode.FillAsync(data.PostalCode);
            await InputCity.FillAsync(data.City);
        }

        public async Task AddItemAsync(InvoicePositions data)
        {
            await InputProduct.FillAsync(data.Product);
            await InputQunatity.FillAsync(data.Quantity);
            await InputPrice.FillAsync(data.Price);
        }

        public async Task ConfirmActionAsync()
        {
            var modal = _page.Locator(".modal-dialog, .modal-content").First;
            var confirmButton = modal.GetByRole(AriaRole.Button, new() { NameRegex = Helper.ToSafeRegex("Potwierdź|Tak|Usuń") });
            await confirmButton.ClickAsync();
        }
    }
}