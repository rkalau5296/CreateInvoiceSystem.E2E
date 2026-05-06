using Microsoft.Playwright;

namespace CreateInvoiceSystem.E2E.Pages
{
    public class ProductsPage
    {
        private readonly IPage _page;

        public ProductsPage(IPage page)
        {
            _page = page;
        }

        public IPage Page => _page;
                
        public ILocator Header => _page.GetByRole(AriaRole.Heading, new() { Name = "Zarządzanie Produktami" });                
        public ILocator SearchInput => _page.Locator("input[placeholder='Wpisz nazwę lub opis...']");                
        public ILocator ExportCsvButton => _page.GetByRole(AriaRole.Button, new() { Name = "Eksportuj CSV" });
        public ILocator AddProductButton => _page.GetByRole(AriaRole.Button, new() { Name = "Dodaj produkt" });                
        public ILocator ProductsTable => _page.Locator("table.table");
        public ILocator ProductRows => _page.Locator("table.table tbody tr");                
        public ILocator EditButtons => _page.GetByRole(AriaRole.Button, new() { Name = "Edytuj" });
        public ILocator DeleteButtons => _page.GetByRole(AriaRole.Button, new() { Name = "Usuń" });                
        public ILocator Pagination => _page.Locator(".btn-group");                
        public ILocator AddProductModal => _page.Locator(".modal-dialog");
        public ILocator ModalNameInput => _page.Locator("input[placeholder='np. Kawa']");
        public ILocator ModalDescriptionInput => _page.Locator("input[placeholder='Opcjonalny opis...']");
        public ILocator ModalPriceInput => _page.Locator("input[inputmode='decimal']");
        public ILocator NameValidation => _page.Locator("text=Nazwa jest wymagana.");
        public ILocator PriceValidation => _page.Locator("text=Podaj prawidłową cenę (maksymalnie 2 miejsca po przecinku)");
        public ILocator ModalSaveButton => _page.GetByRole(AriaRole.Button, new() { Name = "Zapisz produkt" });
        public ILocator DeleteModal => _page.Locator(".modal.show");
        public ILocator ProductsNavLink => _page.GetByRole(AriaRole.Link, new() { Name = "📦 Produkty" });

        public async Task ClickAddProduct() =>  await AddProductButton.ClickAsync();
        public async Task ClickEditFirstProduct() => await EditButtons.First.ClickAsync();
        public async Task ClickDeleteFirstProduct() => await DeleteButtons.First.ClickAsync();

        public async Task Search(string text)
        {
            await SearchInput.FillAsync(text);
            await _page.Keyboard.PressAsync("Enter");
        }

        public async Task SubmitAddProductForm()
        {
            await ModalSaveButton.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public async Task FillDescription(string text) =>
            await ModalDescriptionInput.FillAsync(text);

        
        public async Task<bool> IsModalClosed()
        {
            return !(await AddProductModal.IsVisibleAsync());
        }
        
        public async Task<bool> TableContainsProduct(string name)
        {
            var rows = await ProductRows.AllAsync();
            foreach (var row in rows)
            {
                var text = await row.InnerTextAsync();
                if (text.Contains(name, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        public async Task<int> GetProductCount() => await ProductRows.CountAsync();

        public async Task DeleteProductByName(string name)
        {
            var rows = await ProductRows.AllAsync();
            foreach (var row in rows)
            {
                var text = await row.InnerTextAsync();
                if (text.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    await row.GetByRole(AriaRole.Button, new() { Name = "Usuń" }).ClickAsync();
                    await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                    return;
                }
            }
        }

    }
}
