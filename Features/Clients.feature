Feature: Clients
    As a system user
    I want to manage the client database
    To ensure data integrity for the invoicing process

    Background:
        Given I am on the login page
        When I log in with valid credentials
        And I navigate to the clients page

    Scenario: Successfully add a new client with all fields and cleanup
        When The user clicks the 'Add client' button
        And The user fills in the form with following data:
          | FirmName     | NIP        | Email            | Street | Number | ZipCode | City   |
          | New Corp LLC | 1210236761 | contact@corp.com | Wall St| 10     | 00-001  | Warsaw |
        And The user clicks the 'Save' button
        And The user enters 'New Corp LLC' into the search bar
        Then The new client 'New Corp LLC' should be visible in the list
        When The user clicks 'Usuñ' for client 'New Corp LLC'
        Then The client 'New Corp LLC' should no longer be visible in the list

    Scenario: Verify mandatory field validations
        When The user clicks the 'Add client' button
        And The user clicks the 'Save' button
        Then Validation message 'Musisz podaæ nazwê klienta' should be visible
        And Validation message 'NIP jest obowi¹zkowy' should be visible
        And Validation message 'Ulica jest wymagana' should be visible
        And Validation message 'Numer domu/lokalu jest wymagany' should be visible
        And Validation message 'Kod pocztowy jest wymagany' should be visible
        And Validation message 'Miasto jest wymagane' should be visible

    Scenario: Update newly added client information and cleanup
        When The user clicks the 'Add client' button
        And The user fills in the form with following data:
          | FirmName    | NIP        | Email          | Street | Number | ZipCode | City   |
          | Temp Client | 9876543210 | temp@test.com  | Old St | 1      | 11-111  | Berlin |
        And The user clicks the 'Save' button
        And The user enters 'Temp Client' into the search bar
        And The user clicks 'Edytuj' for client 'Temp Client'
        And The user fills in the form with following data:
          | FirmName     | NIP        | Email            | Street | Number | ZipCode | City   |
          | Client Final | 5268399053 | updated@final.pl | New St | 99     | 22-222  | Gdansk |
        And The user clicks the 'Save' button
        And The user enters 'Client Final' into the search bar
        Then The client 'Client Final' should be visible in the list
        And The client 'Temp Client' should no longer be visible
        When The user clicks 'Usuñ' for client 'Client Final'
        Then The client 'Client Final' should no longer be visible in the list

    Scenario: Remove a newly added client from the system
        When The user clicks the 'Add client' button
        And The user fills in the form with following data:
          | FirmName    | NIP        | Email         | Street | Number | ZipCode | City   |
          | DeleteMe Co | 1210236761 | delete@me.com | Hidden | 99     | 00-999  | Warsaw |
        And The user clicks the 'Save' button
        And The user enters 'DeleteMe Co' into the search bar
        And The user clicks 'Usuñ' for client 'DeleteMe Co'
        Then The client 'DeleteMe Co' should no longer be visible in the list
        And The footer text should contain 'Pokazujê 0 z 0 klientów | Strona 1 z 0'