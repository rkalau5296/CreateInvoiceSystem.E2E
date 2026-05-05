Feature: Login
  As a user
  I want to log into the system
  So that I can access the dashboard

  Scenario: Successful login
    Given I am on the login page
    When I log in with valid credentials
    Then I should see the dashboard
