using CreateInvoiceSystem.E2E.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CreateInvoiceSystem.E2E.Steps
{
    [Binding]
    public class RegisterSteps
    {
        private readonly RegisterPage _page;

        public RegisterSteps(RegisterPage page)
        {
            _page = page;
        }

        [Given(@"I am on the Register page")]
        public async Task GivenIAmOnTheRegisterPage()
        {
            await _page.GoToAsync();
        }

        [When(@"I click the Register button without filling any fields")]
        public async Task WhenIClickTheRegisterButtonWithoutFillingAnyFields()
        {
            await _page.ClickRegisterButtonAsync();
        }

        [Then(@"all required fields should show validation errors")]
        public async Task ThenAllRequiredFieldsShouldShowValidationErrors()
        {
            var fields = new[]
            {
                "Email / Login",
                "Hasło",
                "Potwierdź Hasło",
                "Imię i Nazwisko",
                "Nazwa firmy",
                "NIP",
                "Konto bankowe",
                "Ulica",
                "Numer budynku",
                "Kod pocztowy",
                "Miasto"
            };

            foreach (var f in fields)
                (await _page.IsInvalidAsync(f)).Should().BeTrue();
        }

        [When(@"I enter '(.*)' into the Email field")]
        public async Task WhenIEnterIntoTheEmailField(string value)
        {
            await _page.EnterAsync("Email / Login", value);
        }

        [Then(@"the email field should show an invalid email message")]
        public async Task ThenTheEmailFieldShouldShowAnInvalidEmailMessage()
        {
            (await _page.IsInvalidAsync("Email / Login")).Should().BeTrue();
        }

        [When(@"I enter '(.*)' into the Password field")]
        public async Task WhenIEnterIntoThePasswordField(string value)
        {
            await _page.EnterAsync("Hasło", value);
        }

        [When(@"I enter '(.*)' into the Confirm Password field")]
        public async Task WhenIEnterIntoTheConfirmPasswordField(string value)
        {
            await _page.EnterAsync("Potwierdź Hasło", value);
        }

        [Then(@"the password field should show a minimum length validation message")]
        public async Task ThenThePasswordFieldShouldShowAMinimumLengthValidationMessage()
        {
            (await _page.IsInvalidAsync("Hasło")).Should().BeTrue();

            var error = await _page.GetErrorMessageAsync("co najmniej 6 znaków");
            error.Should().NotBeNullOrEmpty();
        }

        [Then(@"the confirm password field should show a mismatch validation message")]
        public async Task ThenTheConfirmPasswordFieldShouldShowAMismatchValidationMessage()
        {
            (await _page.IsInvalidAsync("Potwierdź Hasło")).Should().BeTrue();
        }

        [When(@"I fill all fields except Company Name")]
        public async Task WhenIFillAllFieldsExceptCompanyName()
        {
            await _page.EnterAsync("Email / Login", "test@example.com");
            await _page.EnterAsync("Hasło", "Password123!");
            await _page.EnterAsync("Potwierdź Hasło", "Password123!");
            await _page.EnterAsync("Imię i Nazwisko", "Jan Kowalski");
            await _page.EnterAsync("NIP", "1234567890");
            await _page.EnterAsync("Konto bankowe", "12345678901234567890123456");
            await _page.EnterAsync("Ulica", "Testowa");
            await _page.EnterAsync("Numer budynku", "10");
            await _page.EnterAsync("Kod pocztowy", "01-234");
            await _page.EnterAsync("Miasto", "Warszawa");
        }

        [Then(@"the Company Name field should show a required validation message")]
        public async Task ThenTheCompanyNameFieldShouldShowARequiredValidationMessage()
        {
            (await _page.IsInvalidAsync("Nazwa firmy")).Should().BeTrue();
        }

        [When(@"I enter '(.*)' into the NIP field")]
        public async Task WhenIEnterIntoTheNIPField(string value)
        {
            await _page.EnterAsync("NIP", value);
        }

        [Then(@"the NIP field should show an invalid NIP message")]
        public async Task ThenTheNIPFieldShouldShowAnInvalidNIPMessage()
        {
            (await _page.IsInvalidAsync("NIP")).Should().BeTrue();
        }

        [When(@"I enter '(.*)' into the Bank Account field")]
        public async Task WhenIEnterIntoTheBankAccountField(string value)
        {
            await _page.EnterAsync("Konto bankowe", value);
        }

        [Then(@"the Bank Account field should show an invalid account number message")]
        public async Task ThenTheBankAccountFieldShouldShowAnInvalidAccountNumberMessage()
        {
            (await _page.IsInvalidAsync("Konto bankowe")).Should().BeTrue();
        }

        [When(@"I enter '(.*)' into the Postal Code field")]
        public async Task WhenIEnterIntoThePostalCodeField(string value)
        {
            await _page.EnterAsync("Kod pocztowy", value);
        }

        [Then(@"the Postal Code field should show an invalid postal code message")]
        public async Task ThenThePostalCodeFieldShouldShowAnInvalidPostalCodeMessage()
        {
            (await _page.IsInvalidAsync("Kod pocztowy")).Should().BeTrue();
        }

        [Then(@"the Email field should no longer be invalid")]
        public async Task ThenTheEmailFieldShouldNoLongerBeInvalid()
        {
            (await _page.IsInvalidAsync("Email / Login")).Should().BeFalse();
        }

        [When(@"I fill all fields with valid data")]
        public async Task WhenIFillAllFieldsWithValidData()
        {
            await _page.EnterAsync("Email / Login", "test@example.com");
            await _page.EnterAsync("Hasło", "Password123!");
            await _page.EnterAsync("Potwierdź Hasło", "Password123!");
            await _page.EnterAsync("Imię i Nazwisko", "Jan Kowalski");
            await _page.EnterAsync("Nazwa firmy", "Test Sp. z o.o.");
            await _page.EnterAsync("NIP", "1234563218"); 
            await _page.EnterAsync("Konto bankowe", "PL12345678901234567890123456");
            await _page.EnterAsync("Ulica", "Testowa");
            await _page.EnterAsync("Numer budynku", "10");
            await _page.EnterAsync("Kod pocztowy", "01-234");
            await _page.EnterAsync("Miasto", "Warszawa");
                        
            var fields = new[]
                {
                    "Email / Login",
                    "Hasło",
                    "Potwierdź Hasło",
                    "Imię i Nazwisko",
                    "Nazwa firmy",
                    "NIP",
                    "Konto bankowe",
                    "Ulica",
                    "Numer budynku",
                    "Kod pocztowy",
                    "Miasto"
                };

            foreach (var f in fields)
                (await _page.IsInvalidAsync(f)).Should().BeFalse($"Field '{f}' should not be invalid when filled correctly");
        }

        [When(@"I click the Back to Login link")]
        public async Task WhenIClickTheBackToLoginLink()
        {
            await _page.ClickBackToLoginAsync();
        }

        [Then(@"I should be on the Login page")]
        public async Task ThenIShouldBeOnTheLoginPage()
        {
            var url = await _page.CurrentUrl();
            url.Should().NotContain("/login");
        }
    }
}
