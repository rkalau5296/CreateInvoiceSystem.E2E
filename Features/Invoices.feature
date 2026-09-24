Feature: Invoices

  Background:
    Given I am on the login page
    When I log in with valid credentials
    And I navigate to the invoices page

  Scenario: Create a new invoice successfully
    When The user clicks the 'Wystaw nową fakturę' button to issue an invoice
    And The user fills in the invoice form with following data:

      | Title     | PaymentMethod | ClientName  | Nip        | Email                            | Street    | HouseNumber | PostalCode | City     |
      | 2/05/2026 | Przelew       | Temp Client | 1234567890 | rafal.kalata.itservice@gmail.com | Dłutowa 5 | 10A         | 00-001     | Warszawa |
    And The user adds an invoice item with following data:
      | Product     | Quantity | Price        |
      | Usługa IT   | 1        | 500.00       |
    And The user clicks the 'Zapisz fakturę' button to issue an invoice
    And The user enters 'Temp Client' into the invoice search bar
    Then The invoice 'Temp Client' should be visible in the list
    When The user clicks 'Usuń' for invoice 'Temp Client'

  Scenario: Edit an existing invoice
    When The user enters '1/05/2026' into the search bar
    And The user clicks 'Edytuj' for invoice '1/05/2026'
    And The user fills in the invoice form with following data:
      | InvoiceNumber | ClientName   | IssueDate  | DueDate    |
      | 1/05/2026-EDIT| Client Final | 2026-05-08 | 2026-05-22 |
    And The user clicks the 'Zapisz' button
    And The user enters '1/05/2026-EDIT' into the search bar
    Then The invoice '1/05/2026-EDIT' should be visible in the list
    And The invoice '1/05/2026' should no longer be visible    

  Scenario: Delete an invoice
    When The user enters 'DeleteMe Co' into the search bar
    And The user clicks 'Usuń' for invoice '1/05/2026'
    And The user confirms the action
    And The user enters '1/05/2026' into the search bar
    Then The invoice '1/05/2026' should no longer be visible in the list

  Scenario: Validation error when trying to save empty invoice form
    When The user clicks the 'Wystaw nową fakturę' button to issue an invoice
    And The user clicks the 'Zapisz fakturę' button to issue an invoice
    Then Validation message 'Tytuł faktury jest wymagany' should be visible
    Then Validation message 'Metoda płatności jest wymagana' should be visible
    Then Validation message 'Nazwa klienta jest wymagana' should be visible
    Then Validation message 'NIP klienta jest wymagany' should be visible
    Then Validation message 'Ulica jest wymagana' should be visible
    Then Validation message 'Numer domu/lokalu jest wymagany' should be visible
    Then Validation message 'Kod pocztowy jest wymagany' should be visible
    Then Validation message 'Miasto jest wymagane' should be visible
    Then Validation message 'Nazwa produktu jest wymagana' should be visible
    Then Validation message 'Cena produktu jest wymagana' should be visible