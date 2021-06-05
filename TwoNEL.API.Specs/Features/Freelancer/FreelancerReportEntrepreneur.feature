Feature: Freelancer Reports a Entrepreneur
    As a freelancer
    I want to report the entrepreneur who do not follow the contract
    In order to prevent them from continuing to make negligent contracts 
    
Scenario: 
    Given I am in an entrepreneur's profile
    When I click on the "Report" button
    And I fill the report reasons
    Then The entrepreneur will be reported