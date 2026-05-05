Feature: Login
  As a user
  I want to log into the system
  So that I can access the dashboard

Scenario: Successful login
	Given I am on the login page
	When I log in with valid credentials
	Then I should see the dashboard

Scenario: Remember me checkbox toggles
	Given I am on the login page
	When I toggle the Remember Me checkbox
	Then the Remember Me checkbox state should change

Scenario: Forgot password link works
	Given I am on the login page
	When I click the Forgot Password link
	Then I should be redirected to the forgot password page

Scenario: Register link works
	Given I am on the login page
	When I click the Register link
	Then I should be redirected to the register page
