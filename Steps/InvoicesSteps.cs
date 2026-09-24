using CreateInvoiceSystem.E2E.Pages;
using Reqnroll;

namespace CreateInvoiceSystem.E2E.Steps
{
    [Binding]
    public class InvoicesSteps
    {
        private readonly InvoicesPage _invoicesPage;

        public InvoicesSteps(InvoicesPage invoicesPage)
        {
            _invoicesPage = invoicesPage;
        }

        [Given(@"I navigate to the invoices page")]
        [When(@"I navigate to the invoices page")]
        public async Task WhenINavigateToInvoicesPage()
        {
            await _invoicesPage.NavigateAsync();
        }

        [When(@"The user fills in the invoice form with following data:")]
        public async Task WhenTheUserFillsInTheInvoiceFormWithFollowingData(Table table)
        {
            await _invoicesPage.FillFormAsync(table.CreateInstance<InvoiceData>());
        }

        [When(@"The user adds an invoice item with following data:")]
        public async Task WhenTheUserAddsAnInvoiceItemWithFollowingData(Table table)
        {
            await _invoicesPage.AddItemAsync(table.CreateInstance<InvoicePositions>());
        }

        [When(@"The user clicks '(.*)' for invoice '(.*)'")]
        public async Task WhenTheUserClicksActionForInvoice(string actionName, string invoiceNumber)
        {
            if(actionName == "Usuń")
                await _invoicesPage.ClickDeleteInvoiceAsync(actionName, invoiceNumber);
            else
                await _invoicesPage.ClickActionForInvoiceAsync(actionName, invoiceNumber);
        }

        [When(@"The user confirms the action")]
        public async Task WhenTheUserConfirmsTheAction()
        {
            await _invoicesPage.ConfirmActionAsync();
        }

        [Then(@"The invoice '(.*)' should be visible in the list")]
        public async Task ThenInvoiceShouldBeVisible(string invoiceIdentifier)
        {
            await _invoicesPage.AssertInvoiceIsVisibleAsync(invoiceIdentifier);
        }

        [Then(@"The invoice '(.*)' should no longer be visible")]
        [Then(@"The invoice '(.*)' should no longer be visible in the list")]
        public async Task ThenInvoiceShouldNotBeVisible(string invoiceNumber)
        {
            await _invoicesPage.AssertInvoiceIsNotVisibleAsync(invoiceNumber);
        }

        [When(@"The user clicks the '(.*)' button to issue an invoice")]
        public async Task WhenTheUserClicksButton(string buttonName)
        {            
            await _invoicesPage.ClickButtonAsync(buttonName);           
        }

        [When(@"The user enters '(.*)' into the invoice search bar")]
        public async Task WhenTheUserEntersSearch(string query) => await _invoicesPage.SearchAsync(query);
    }
}