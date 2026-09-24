using CreateInvoiceSystem.E2E.Hooks;
using CreateInvoiceSystem.E2E.Pages;
using FluentAssertions;
using Microsoft.Playwright;
using Reqnroll;
using static System.Net.Mime.MediaTypeNames;

namespace CreateInvoiceSystem.E2E.Steps;

[Binding]
public class ClientsSteps(IPage page)
{
    private readonly ClientsPage _clientsPage = new(page);
    

    [When(@"I navigate to the clients page")]
    [Given(@"The user is on the clients list page")]
    public async Task NavigateToClientsPage()
    {
        await _clientsPage.ClientsNavLink.ClickAsync();
        await Assertions.Expect(_clientsPage.Header)
                .ToBeVisibleAsync(new() { Timeout = 5000 });
    }

    [When(@"The user clicks the '(.*)' button")]
    public async Task WhenTheUserClicksButton(string buttonName)
    {
        if (buttonName == "Add client") await _clientsPage.ClickAddClientAsync();
        else if (buttonName == "Save") await _clientsPage.ClickSaveAsync();
    }

    [When(@"The user fills in the form with following data:")]
    public async Task WhenTheUserFillsForm(Table table) =>
        await _clientsPage.FillClientFormAsync(table.CreateInstance<ClientData>());

    [When(@"The user enters '(.*)' into the search bar")]
    public async Task WhenTheUserEntersSearch(string query) => await _clientsPage.SearchAsync(query);

    [When(@"The user clicks '(.*)' for client '(.*)'")]
    public async Task WhenTheUserClicksActionForClient(string action, string clientName) =>
        await _clientsPage.ClickActionForClientAsync(action, clientName);

    [Then(@"The new client '(.*)' should be visible in the list")]
    [Then(@"The client '(.*)' should be visible in the list")]
    public async Task ThenClientShouldBeVisible(string clientName)
    {
        var isVisible = await _clientsPage.IsClientVisibleAsync(clientName);
        isVisible.Should().BeTrue();
    }

    [Then(@"The client '(.*)' should no longer be visible in the list")]
    [Then(@"The client '(.*)' should no longer be visible")]
    public async Task ThenClientShouldNotBeVisible(string clientName)
    {
        await page.WaitForTimeoutAsync(1000);
        var isVisible = await _clientsPage.IsClientVisibleAsync(clientName);
        isVisible.Should().BeFalse();
    }

    [Then(@"Validation message '(.*)' should be visible")]
    public async Task ThenValidationMessageShouldBeVisible(string message)
    {
        var messages = await _clientsPage.GetValidationMessagesAsync();
        var safeRegex = Helper.ToSafeRegex(message);

        messages.Should().Contain(m => safeRegex.IsMatch(m));
    }

    [Then(@"The footer text should contain '(.*)'")]
    public async Task ThenTheFooterTextShouldContain(string expectedText)
    {        
        var actualText = await _clientsPage.GetFooterTextAsync();
        var regexExprected = Helper.FixPaginationText(expectedText);
        actualText.Should().Contain(regexExprected);
    }
}