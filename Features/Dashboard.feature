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

Scenario: Quick actions - Wystaw fakturê navigates correctly
	When I click the 'Wystaw fakture' quick action
	Then I should be on the 'invoice create' page

Scenario: Quick actions - Przegladaj faktury navigates correctly
	When I click the 'Przegladaj faktury' quick action
	Then I should be on the 'invoice list' page

Scenario: Quick actions - Kontrahenci navigates correctly
	When I click the 'Kontrahenci' quick action
	Then I should be on the 'clients' page
