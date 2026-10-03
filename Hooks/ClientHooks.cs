using CreateInvoiceSystem.E2E.Pages;
using Reqnroll;

namespace CreateInvoiceSystem.E2E.Hooks
{
    [Binding]
    public sealed class ClientHooks(ClientsPage clientsPage)
    {
        [AfterScenario("clients", "invoices", Order = 200)]
        public async Task CleanupInvoicesAsync()
        {            
            await clientsPage.CleanupClientsAsync();
        }
    }
}