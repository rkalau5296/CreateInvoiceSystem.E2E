Feature: Register
  As a new user
  I want to see proper validation on the registration form
  So that I know what data is required

Background:
	Given I am on the Register page

Scenario: Validation when submitting empty registration form
	When I click the Register button without filling any fields
	Then all required fields should show validation errors
	And a global validation alert should be displayed

Scenario: Validation when entering invalid email
	When I enter 'invalid-email' into the Email field
	And I click the Register button without filling any fields
	Then the email field should show an invalid email message

Scenario: Validation when password is too short
	When I enter '123' into the Password field
	And I click the Register button without filling any fields
	Then the password field should show a minimum length validation message

Scenario: Validation when passwords do not match
	When I enter 'Password123!' into the Password field
	And I enter 'Different123!' into the Confirm Password field
	And I click the Register button without filling any fields
	Then the confirm password field should show a mismatch validation message

Scenario: Validation when company name is empty
	When I fill all fields except Company Name
	And I click the Register button without filling any fields
	Then the Company Name field should show a required validation message

Scenario: Validation when NIP is invalid
	When I enter '123' into the NIP field
	And I click the Register button without filling any fields
	Then the NIP field should show an invalid NIP message

Scenario: Validation when bank account number is invalid
	When I enter '123' into the Bank Account field
	And I click the Register button without filling any fields
	Then the Bank Account field should show an invalid account number message

Scenario: Validation when postal code is invalid
	When I enter 'ABC' into the Postal Code field
	And I click the Register button without filling any fields
	Then the Postal Code field should show an invalid postal code message

Scenario: Validation disappears after entering correct data
	When I click the Register button without filling any fields
	And I enter 'test@example.com' into the Email field
	Then the Email field should no longer be invalid

Scenario: All fields valid and user returns to login page without submitting
	When I fill all fields with valid data
	And I click the Back to Login link
	Then I should be on the Login page
