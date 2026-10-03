using CreateInvoiceSystem.E2E.Pages;
using Reqnroll;

namespace CreateInvoiceSystem.E2E.Steps
{
    [Binding]
    public class InvoicesSteps(InvoicesPage invoicesPage)
    {
        [Given(@"I navigate to the invoices page")]
        [When(@"I navigate to the invoices page")]
        public async Task WhenINavigateToInvoicesPage()
        {
            await invoicesPage.NavigateAsync();
        }

        [When(@"The user fills in the invoice form with following data:")]
        public async Task WhenTheUserFillsInTheInvoiceFormWithFollowingData(Table table)
        {
            await invoicesPage.FillFormAsync(table.CreateInstance<InvoiceData>());
        }

        [When(@"The user adds an invoice item with following data:")]
        public async Task WhenTheUserAddsAnInvoiceItemWithFollowingData(Table table)
        {
            await invoicesPage.AddItemAsync(table.CreateInstance<InvoicePositions>());
        }

        [When(@"The user clicks '(.*)' for invoice '(.*)'")]
        public async Task WhenTheUserClicksActionForInvoice(string actionName, string invoiceNumber)
        {
            if(actionName == "Usuń")
                await invoicesPage.ClickDeleteInvoiceAsync(actionName, invoiceNumber);
            else
                await invoicesPage.ClickActionForInvoiceAsync(actionName, invoiceNumber);
        }

        [When(@"The user confirms the action")]
        public async Task WhenTheUserConfirmsTheAction()
        {
            await invoicesPage.ConfirmActionAsync();
        }

        [Then(@"The invoice '(.*)' should be visible in the list")]
        public async Task ThenInvoiceShouldBeVisible(string invoiceIdentifier)
        {
            await invoicesPage.AssertInvoiceIsVisibleAsync(invoiceIdentifier);
        }

        [Then(@"The invoice '(.*)' should no longer be visible")]
        [Then(@"The invoice '(.*)' should no longer be visible in the list")]
        public async Task ThenInvoiceShouldNotBeVisible(string invoiceNumber)
        {
            await invoicesPage.AssertInvoiceIsNotVisibleAsync(invoiceNumber);
        }

        [When(@"The user clicks the '(.*)' button to issue an invoice")]
        public async Task WhenTheUserClicksButton(string buttonName)
        {            
            await invoicesPage.ClickButtonAsync(buttonName);           
        }

        [When(@"The user enters '(.*)' into the invoice search bar")]
        public async Task WhenTheUserEntersSearch(string query) => await invoicesPage.SearchAsync(query);

        [When(@"The user edits invoice '(.*)' with following data:")]
        public async Task WhenTheUserEditsInvoiceWithFollowingData(string clientName, DataTable table)
        {
            await invoicesPage.ClickEditForInvoiceAsync(clientName);
            await invoicesPage.FillEditInvoiceFormAsync(table.CreateInstance<InvoiceData>());
        }

        [When(@"The user replaces invoice items with following items:")]
        public async Task WhenTheUserReplacesInvoiceItemsWithFollowingItems(Table table)
        {
            var row = table.Rows[0];

            var item = new InvoicePositions(
                row["Product"],
                row["Quantity"],
                row["Price"],
                row.ContainsKey("Description") ? row["Description"] : string.Empty);

            await invoicesPage.ReplaceInvoiceItemAsync(item);
        }
    }
}