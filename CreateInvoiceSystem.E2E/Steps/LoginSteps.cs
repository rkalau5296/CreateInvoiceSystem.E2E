using CreateInvoiceSystem.E2E.Hooks;
using CreateInvoiceSystem.E2E.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CreateInvoiceSystem.E2E.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;
        private readonly DashboardPage _dashboardPage;

        public LoginSteps(LoginPage loginPage, DashboardPage dashboardPage)
        {
            _loginPage = loginPage;
            _dashboardPage = dashboardPage;
        }

        [Given(@"I am on the login page")]
        public async Task GivenIAmOnTheLoginPage()
        {
            await _loginPage.GoToAsync();
        }

        [When(@"I log in with valid credentials")]
        public async Task WhenILogInWithValidCredentials()
        {
            await _loginPage.LoginAsync("rafal.kalata@icloud.com", "LampaAlladyna1830!!");
        }

        [Then(@"I should see the dashboard")]
        public async Task ThenIShouldSeeTheDashboard()
        {
            (await _dashboardPage.IsLoadedAsync()).Should().BeTrue();
        }
    }
}
