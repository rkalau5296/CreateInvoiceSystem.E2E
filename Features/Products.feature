Feature: Products
  As a logged-in user
  I want to manage products
  So that I can add, edit, search and delete items

Background:
	Given I am on the login page
	When I log in with valid credentials
	And I navigate to the products page

Scenario: Products page loads correctly
	Then I should see the products header
	And I should see the search input
	And I should see the export button
	And I should see the add product button
	And I should see the products table

Scenario: Products table contains rows
	Then the products table should have at least 1 row

Scenario: Each product row has Edit and Delete buttons
	Then each product row should have Edit and Delete buttons

Scenario: Search filters products
    When I click the add product button
    And I fill the name with dynamic product name
    And I fill the description with 'Opis testowy'
    And I fill the price with '123.45'
    And I submit the add product form
    When I search for dynamic product name
    Then all visible products should contain dynamic product name

Scenario: Add product button opens modal
	When I click the add product button
	Then the add product modal should be visible

Scenario: Add product validation - empty fields
	When I click the add product button
	And I submit the add product form
	Then the name validation message should be visible
	And the price validation message should be visible

Scenario: Add product validation - missing name
	When I click the add product button
	And I fill the price with '10'
	And I submit the add product form
	Then the name validation message should be visible

Scenario: Add product validation - missing price
	When I click the add product button
	And I fill the name with 'Test Produkt'
	And I submit the add product form
	Then the price validation message should be visible

Scenario: Edit product navigates correctly
	When I click edit on the first product
	Then the url should contain '/products'

Scenario: Delete product successfully
    When I click the add product button
    And I fill the name with dynamic product name
    And I fill the description with 'Opis testowy'
    And I fill the price with '123.45'
    And I submit the add product form
    When I search for dynamic product name
    Then the products table should contain dynamic product name
    When I delete the product with dynamic product name
    Then the products table should not contain dynamic product name

Scenario: Pagination controls are visible
	Then I should see pagination controls

Scenario: Add product successfully
    When I click the add product button
    And I fill the name with dynamic product name
    And I fill the description with 'Opis testowy'
    And I fill the price with '123.45'
    And I submit the add product form
    Then the add product modal should close
    When I search for dynamic product name
    Then the products table should contain dynamic product name
