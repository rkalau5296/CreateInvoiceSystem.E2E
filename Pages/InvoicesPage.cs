using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace CreateInvoiceSystem.E2E.Pages
{
    public record InvoiceData(string Title, string PaymentMethod, string ClientName, string Nip, string Email, string Street, string HouseNumber, string PostalCode, string City);
    public record InvoicePositions(string Product, string Quantity, string Price, string Description);

    public class InvoicesPage(IPage page)
    {
        private ILocator SearchInput => page.Locator("input[placeholder*='Numer faktury']");
        private ILocator TableRows => page.Locator("table tbody tr");
        public ILocator InvoicesNavLink => page.GetByRole(AriaRole.Link, new() { NameRegex = Helper.ToSafeRegex("Faktury") });
        private ILocator InputTitle => page.Locator("label:has-text('Tytuł faktury') + input");
        private ILocator PaymentMethod => page.Locator("label:has-text('Metoda płatności') + select");
        private ILocator InputClientName => page.Locator("label:has-text('Wybierz klienta lub wpisz nową nazwę') + input");
        private ILocator InputNip => page.Locator("label:has-text('NIP') + input");
        private ILocator InputEmail => page.Locator("label:has-text('Email klienta') + input");
        private ILocator InputStreet => page.Locator("label:has-text('Ulica') + input");
        private ILocator InputHouseNumber => page.Locator("label:has-text('Nr domu/lok.') + input");
        private ILocator InputPostalCode => page.Locator("label:has-text('Kod pocztowy') + input");
        private ILocator InputCity => page.Locator("label:has-text('Miasto') + input");
        private ILocator InputProduct => page.Locator("input[placeholder = 'Nazwa produktu...']");
        private ILocator InputQunatity => page.Locator("input[min='1']");
        private ILocator InputPrice => page.Locator("input[step='0.01']");
        private ILocator DeleteButtons => page.GetByRole(AriaRole.Button, new() { Name = "Usuń" });

        private ILocator EditPaymentMethodSelect => page.Locator("label:has-text('Metoda płatności') + select");

        private ILocator EditClientNameInput => page.GetByPlaceholder("Wpisz nazwę klienta...", new() { Exact = true });

        private ILocator EditClientAddressInput => page.Locator("label:has-text('Adres nabywcy') + textarea");

        private ILocator EditClientNipInput => page.GetByPlaceholder("NIP", new() { Exact = true });

        private ILocator EditClientEmailInput => page.GetByPlaceholder("Email", new() { Exact = true });
        public async Task NavigateAsync()
        {
            await InvoicesNavLink.ClickAsync();
        }
        
        public async Task ClickButtonAsync(string buttonName)
        {
            var safeRegex = Helper.ToSafeRegex(buttonName);
            await page.GetByRole(AriaRole.Button, new() { NameRegex = safeRegex }).ClickAsync();
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
                        
            page.Dialog += async (_, dialog) =>
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
            var row = page.GetByRole(AriaRole.Row)
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
            var modal = page.Locator(".modal-dialog, .modal-content").First;
            var confirmButton = modal.GetByRole(AriaRole.Button, new() { NameRegex = Helper.ToSafeRegex("Potwierdź|Tak|Usuń") });
            await confirmButton.ClickAsync();
        }
        public async Task DeleteAllInvoicesAsync()
        {
            await NavigateAsync();
            page.Dialog += async (_, dialog) =>
            {
                await dialog.AcceptAsync();
            };

            while (await DeleteButtons.CountAsync() > 0)
            {
                var deleteTask = DeleteButtons.First.ClickAsync();                
                await deleteTask;
            }
        }
        public async Task ClickEditForInvoiceAsync(string clientName)
        {
            var row = page.Locator("tr", new() { HasTextString = clientName });

            await row.GetByRole(AriaRole.Button, new() { Name = "Edytuj" }).ClickAsync();
        }

        public async Task FillEditInvoiceFormAsync(InvoiceData invoice)
        {
            await EditPaymentMethodSelect.SelectOptionAsync(invoice.PaymentMethod);
            await EditClientNameInput.FillAsync(invoice.ClientName);
            await EditClientAddressInput.FillAsync(
                $"{invoice.Street} {invoice.HouseNumber}, {invoice.PostalCode} {invoice.City}");
            await EditClientNipInput.FillAsync(invoice.Nip);
            await EditClientEmailInput.FillAsync(invoice.Email);
        }

        public async Task ReplaceInvoiceItemAsync(InvoicePositions data)
        {
            await AddItemAsync(data);
        }
    }
}