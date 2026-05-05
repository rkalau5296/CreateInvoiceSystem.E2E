Feature: Forgot Password
  As a user
  I want to reset my password
  So that I can regain access to my account

Scenario: Navigate to forgot password page
	Given I am on the login page
	When I click the Forgot Password link
	Then I should see the forgot password page

Scenario: Forgot password page UI elements
	Given I am on the forgot password page
	Then I should see the reset password header
	And I should see the email input field
	And I should see the reset button
	And I should see the back to login link

Scenario: Successful password reset request
	Given I am on the forgot password page
	When I enter my email "test@test.pl"
	And I click the reset password button
	Then I should see the reset confirmation page

Scenario: Confirmation page UI elements
	Given I am on the reset confirmation page
	Then I should see the success header
	And I should enter an email address
	And I should click reset button
	And I should see the success message
	And I should see the go to login button
	And I click the go to login button

Scenario: Validation when email field is empty
	Given I am on the reset confirmation page
	Then I should click reset button
	Then the email field should show a required validation message
	And a global validation alert should be displayed

