using CreateInvoiceSystem.E2E.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CreateInvoiceSystem.E2E.Steps
{
    [Binding]
    public class ForgotPasswordSteps
    {
        private readonly LoginPage _loginPage;
        private readonly ForgotPasswordPage _forgotPasswordPage;

        public ForgotPasswordSteps(LoginPage loginPage, ForgotPasswordPage forgotPasswordPage)
        {
            _loginPage = loginPage;
            _forgotPasswordPage = forgotPasswordPage;
        }

        [Given(@"I am on the forgot password page")]
        public async Task GivenIAmOnTheForgotPasswordPage()
        {
            await _forgotPasswordPage.GoToAsync();
        }

        [Then(@"I should see the forgot password page")]
        public async Task ThenIShouldSeeTheForgotPasswordPage()
        {
            (await _forgotPasswordPage.IsLoadedAsync()).Should().BeTrue();
        }

        [Then(@"I should see the reset password header")]
        public async Task ThenIShouldSeeTheResetPasswordHeader()
        {
            (await _forgotPasswordPage.IsHeaderVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should see the email input field")]
        public async Task ThenIShouldSeeTheEmailInputField()
        {
            (await _forgotPasswordPage.IsEmailInputVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should see the reset button")]
        public async Task ThenIShouldSeeTheResetButton()
        {
            (await _forgotPasswordPage.IsResetButtonVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should see the back to login link")]
        public async Task ThenIShouldSeeTheBackToLoginLink()
        {
            (await _forgotPasswordPage.IsBackToLoginVisibleAsync()).Should().BeTrue();
        }

        [When(@"I enter my email ""(.*)""")]
        public async Task WhenIEnterMyEmail(string email)
        {
            await _forgotPasswordPage.EnterEmailAsync(email);
        }

        [When(@"I click the reset password button")]
        public async Task WhenIClickTheResetPasswordButton()
        {
            await _forgotPasswordPage.ClickResetButtonAsync();
        }

        [Then(@"I should see the reset confirmation page")]
        public async Task ThenIShouldSeeTheResetConfirmationPage()
        {
            (await _forgotPasswordPage.IsMessageVisibleAsync()).Should().BeTrue();
        }

        [Given(@"I am on the reset confirmation page")]
        public async Task GivenIAmOnTheResetConfirmationPage()
        {
            await _forgotPasswordPage.GoToConfirmationAsync();
        }

        [Then(@"I should see the success header")]
        public async Task ThenIShouldSeeTheSuccessHeader()
        {
            (await _forgotPasswordPage.IsSuccessHeaderVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should see the success message")]
        public async Task ThenIShouldSeeTheSuccessMessage()
        {
            (await _forgotPasswordPage.IsMessageVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should enter an email address")]
        public async Task ThenIShouldEnterAnEmailAddress()
        {
            await _forgotPasswordPage.EnterEmailAsync("rafal.kalata@icloud.com");
        }

        [Then(@"I should click reset button")]
        public async Task ThenIShouldClickResetButton()
        {
            await _forgotPasswordPage.ClickResetButtonAsync();
        }

        [Then(@"I should see the go to login button")]
        public async Task ThenIShouldSeeTheGoToLoginButton()
        {
            (await _forgotPasswordPage.IsGoToLoginButtonVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I click the go to login button")]
        public async Task ThenIShouldClickTheGoToLoginButton()
        {
            await _forgotPasswordPage.GoToLoginButton.ClickAsync();
        }
    }
}
