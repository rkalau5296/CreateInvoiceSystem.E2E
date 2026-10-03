using CreateInvoiceSystem.E2E.Pages;
using Reqnroll;

namespace CreateInvoiceSystem.E2E.Hooks
{
    [Binding]
    public sealed class ClientHooks(ClientsPage clientsPage)
    {
        [AfterScenario("clients", Order = 100)]
        public async Task CleanupInvoicesAsync()
        {
            await clientsPage.CleanupClientsAsync();
        }
    }
}