using Microsoft.Playwright;

namespace CreateInvoiceSystem.E2E.Pages
{
    public class ProductsPage(IPage page)
    {
        public IPage Page => page;
                
        public ILocator Header => page.GetByRole(AriaRole.Heading, new() { Name = "Zarządzanie Produktami" });                
        public ILocator SearchInput => page.Locator("input[placeholder='Wpisz nazwę lub opis...']");                
        public ILocator ExportCsvButton => page.GetByRole(AriaRole.Button, new() { Name = "Eksportuj CSV" });
        public ILocator AddProductButton => page.GetByRole(AriaRole.Button, new() { Name = "Dodaj produkt" });                
        public ILocator ProductsTable => page.Locator("table.table");
        public ILocator ProductRows => page.Locator("table.table tbody tr");                
        public ILocator EditButtons => page.GetByRole(AriaRole.Button, new() { Name = "Edytuj" });
        public ILocator DeleteButtons => page.GetByRole(AriaRole.Button, new() { Name = "Usuń" });                
        public ILocator Pagination => page.Locator(".btn-group");                
        public ILocator AddProductModal => page.Locator(".modal-dialog");
        public ILocator ModalNameInput => page.Locator("input[placeholder='np. Kawa']");
        public ILocator ModalDescriptionInput => page.Locator("input[placeholder='Opcjonalny opis...']");
        public ILocator ModalPriceInput => page.Locator("input[inputmode='decimal']");
        public ILocator NameValidation => page.Locator("text=Nazwa jest wymagana.");
        public ILocator PriceValidation => page.Locator("text=Podaj prawidłową cenę (maksymalnie 2 miejsca po przecinku)");
        public ILocator ModalSaveButton => page.GetByRole(AriaRole.Button, new() { Name = "Zapisz produkt" });
        public ILocator DeleteModal => page.Locator(".modal.show");
        public ILocator ProductsNavLink => page.GetByRole(AriaRole.Link, new() { Name = "📦 Produkty" });

        public async Task GotoProductsAsync() => await ProductsNavLink.ClickAsync();
        public async Task ClickAddProduct() =>  await AddProductButton.ClickAsync();
        public async Task ClickEditFirstProduct() => await EditButtons.First.ClickAsync();
        public async Task ClickDeleteFirstProduct() => await DeleteButtons.First.ClickAsync();

        public async Task Search(string text)
        {
            await SearchInput.FillAsync(text);
            await page.Keyboard.PressAsync("Enter");
        }

        public async Task SubmitAddProductForm()
        {
            await ModalSaveButton.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
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
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                    return;
                }
            }
        }
        public async Task DeleteAllProductsAsync()
        {
            await GotoProductsAsync();

            await SearchInput.FillAsync(string.Empty);
            while (await DeleteButtons.CountAsync() > 0)
            {
                await DeleteButtons.First.ClickAsync();                
            }
        }
    }
}
