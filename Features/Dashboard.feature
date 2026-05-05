Feature: Dashboard
  As a logged-in user
  I want to see the dashboard
  So that I can quickly access key information

  Background:
	Given I am on the login page
    When I log in with valid credentials

  Scenario: Dashboard loads correctly
    Then I should see the dashboard header
    And I should see the statistics section
    And I should see the quick actions
    And I should see the recent invoices section
    And I should see the latest clients section
