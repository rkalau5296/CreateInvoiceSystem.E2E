using CreateInvoiceSystem.E2E.Pages;
using Reqnroll;

namespace CreateInvoiceSystem.E2E.Hooks;

[Binding]
public sealed class InvoiceHooks(InvoicesPage invoicesPage)
{
    [AfterScenario("invoices", Order = 100)]
    public async Task CleanupInvoicesAsync()
    {
        await invoicesPage.DeleteAllInvoicesAsync();
    }
}