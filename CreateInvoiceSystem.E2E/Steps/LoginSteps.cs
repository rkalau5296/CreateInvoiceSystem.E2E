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
        [When(@"I toggle the Remember Me checkbox")]
        public async Task WhenIToggleTheRememberMeCheckbox()
        {
            await _loginPage.ToggleRememberMeAsync();
        }

        [Then(@"the Remember Me checkbox state should change")]
        public async Task ThenTheRememberMeCheckboxStateShouldChange()
        {
            (await _loginPage.WasRememberMeToggledAsync()).Should().BeTrue();
        }

        [When(@"I click the Forgot Password link")]
        public async Task WhenIClickTheForgotPasswordLink()
        {
            await _loginPage.ClickForgotPasswordAsync();
        }

        [Then(@"I should be redirected to the forgot password page")]
        public async Task ThenIShouldBeRedirectedToTheForgotPasswordPage()
        {
            (await _loginPage.IsForgotPasswordPageAsync()).Should().BeTrue();
        }

        [When(@"I click the Register link")]
        public async Task WhenIClickTheRegisterLink()
        {
            await _loginPage.ClickRegisterAsync();
        }

        [Then(@"I should be redirected to the register page")]
        public async Task ThenIShouldBeRedirectedToTheRegisterPage()
        {
            (await _loginPage.IsRegisterPageAsync()).Should().BeTrue();
        }

    }
}
