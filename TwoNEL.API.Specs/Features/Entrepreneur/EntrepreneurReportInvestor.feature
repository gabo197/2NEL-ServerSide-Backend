Feature: Entrepreneur Reports an Investor
    As an entrepreneur
    I want to report the investor who do poor work
    In order to prevent them from continuing to do negligent work 
    
Scenario: 
    Given I am in an investor's profile
    When I click on the "Report" button
    And I fill the report reasons
    Then The investor will be reported