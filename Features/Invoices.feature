Feature: Invoices

    Background:
        Given I am on the login page
        When I log in with valid credentials
        And I navigate to the invoices page

    @invoices
    Scenario: Create a new invoice successfully
        When The user clicks the 'Wystaw nową fakturę' button to issue an invoice
        And The user fills in the invoice form with following data:
            | Title      | PaymentMethod | ClientName    | Nip        | Email                            | Street    | HouseNumber | PostalCode | City     |
            | 2/05/2026  | Przelew       | Simple Client | 1234567890 | rafal.kalata.itservice@gmail.com | Dłutowa 5 | 10A         | 00-001     | Warszawa |
        And The user adds an invoice item with following data:
            | Product    | Quantity | Price  |
            | Usługa IT  | 1        | 500.00 |
        And The user clicks the 'Zapisz fakturę' button to issue an invoice
        And The user enters 'Simple Client' into the invoice search bar
        Then The invoice 'Simple Client' should be visible in the list
    
    @invoices
    Scenario: Edit an existing invoice successfully
        When The user clicks the 'Wystaw nową fakturę' button to issue an invoice
        And The user fills in the invoice form with following data:
            | Title      | PaymentMethod | ClientName    | Nip        | Email                            | Street    | HouseNumber | PostalCode | City     |
            | 2/05/2026  | Przelew       | Simple Client | 1234567890 | rafal.kalata.itservice@gmail.com | Dłutowa 5 | 10A         | 00-001     | Warszawa |
        And The user adds an invoice item with following data:
            | Product   | Quantity | Price  |
            | Usługa IT | 1        | 500.00 |
        And The user clicks the 'Zapisz fakturę' button to issue an invoice
        And The user edits invoice 'Simple Client' with following data:
            | Title      | PaymentMethod | ClientName     | Nip        | Email                      | Street     | HouseNumber | PostalCode | City  |
            | 2/05/2026  | Gotówka       | Updated Client | 9876543210 | updated.client@example.com | Kwiatowa 7 | 12B         | 00-002     | Radom |
        And The user replaces invoice items with following items:
            | Product         | Quantity | Price  |
            | Konsultacja IT  | 2        | 300.00 |           
        And The user clicks the 'Zapisz zmiany' button to issue an invoice
        And The user enters 'Updated Client' into the invoice search bar
        Then The invoice 'Updated Client' should be visible in the list
   
   @invoices
    Scenario: Delete an existing invoice successfully
         When The user clicks the 'Wystaw nową fakturę' button to issue an invoice
        And The user fills in the invoice form with following data:
            | Title      | PaymentMethod | ClientName    | Nip        | Email                            | Street    | HouseNumber | PostalCode | City     |
            | 2/05/2026  | Przelew       | Simple Client | 1234567890 | rafal.kalata.itservice@gmail.com | Dłutowa 5 | 10A         | 00-001     | Warszawa |
        And The user adds an invoice item with following data:
            | Product   | Quantity | Price  |
            | Usługa IT | 1        | 500.00 |
        And The user clicks the 'Zapisz fakturę' button to issue an invoice
        When The user clicks 'Usuń' for invoice 'Simple Client'
        Then The invoice 'Simple Client' should no longer be visible in the list
        
    Scenario: Validation error when trying to save empty invoice form
        When The user clicks the 'Wystaw nową fakturę' button to issue an invoice
        And The user clicks the 'Zapisz fakturę' button to issue an invoice
        Then Validation message 'Tytuł faktury jest wymagany' should be visible
        And Validation message 'Metoda płatności jest wymagana' should be visible
        And Validation message 'Nazwa klienta jest wymagana' should be visible
        And Validation message 'NIP klienta jest wymagany' should be visible
        And Validation message 'Ulica jest wymagana' should be visible
        And Validation message 'Numer domu/lokalu jest wymagany' should be visible
        And Validation message 'Kod pocztowy jest wymagany' should be visible
        And Validation message 'Miasto jest wymagane' should be visible
        And Validation message 'Nazwa produktu jest wymagana' should be visible
        And Validation message 'Cena produktu jest wymagana' should be visible