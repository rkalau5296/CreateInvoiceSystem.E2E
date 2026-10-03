using CreateInvoiceSystem.E2E.Pages;
using Reqnroll;

namespace CreateInvoiceSystem.E2E.Hooks
{
    [Binding]

    public sealed class ProductHooks(ProductsPage productsPage)
    {
        [AfterScenario("products", "invoices", Order = 300)]
        public async Task CleanupInvoicesAsync()
        {            
            await productsPage.DeleteAllProductsAsync();
        }
    }
}
