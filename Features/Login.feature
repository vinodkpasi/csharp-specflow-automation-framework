Feature: LoginFeature
@smoke
Scenario: Login functionality
Given the user navigates to the login page
When the user enters the correct credentials
And the user clicks on the login button
Then the user has successfully logged in