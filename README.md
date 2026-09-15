# C# SpecFlow UI Automation Framework

A reusable **C# Selenium + SpecFlow BDD UI automation framework** for end-to-end web application testing.

Repository: https://github.com/vinodkpasi/csharp-specflow-automation-framework

## Overview

This framework combines **C#/.NET 6, Selenium WebDriver, SpecFlow/Gherkin, MSTest, Page Object Model, hooks, environment-specific test data, runtime parameters, cross-browser execution, sequential/parallel execution, Log4net, failure screenshots, and SpecFlow Living Documentation**.

The repository is organized into `Features`, `Hooks`, `Models`, `Pages`, `StepDefinitions`, `TestData`, and `Utils`, with `App.config`, `specflow.json`, `test.runsettings`, and `log4net.config` supporting the framework.

## Architecture

```text
Business Requirement
        ↓
Gherkin Feature
        ↓
SpecFlow Step Definition
        ↓
Page Object Model
        ↓
Selenium WebDriver
        ↓
Browser
        ↓
Application Under Test
        ↓
Assertions / Results
        ├── Screenshot on failure
        ├── Log4net log
        └── Living Documentation
```

## Technology Stack

| Technology | Version / Purpose |
|---|---|
| C# | Automation language |
| .NET | 6.0 target framework |
| Selenium.WebDriver | 4.7.0 |
| Selenium.Support | 4.7.0 |
| ChromeDriver | 108.0.5359.7100 |
| SpecFlow.MsTest | 3.9.74 |
| MSTest.TestFramework | 3.0.0 |
| MSTest.TestAdapter | 2.2.10 |
| Microsoft.NET.Test.Sdk | 17.4.0 |
| DotNetSeleniumExtras.WaitHelpers | 3.11.0 |
| Newtonsoft.Json | 13.0.2 |
| log4net | 2.0.15 |
| SpecFlow.Plus.LivingDocPlugin | 3.9.57 |

> Package versions above are from the repository `.csproj`; treat that file as the source of truth.

## Project Structure

```text
csharp-specflow-automation-framework/
│
├── Features/             # Gherkin feature files
├── Hooks/                # SpecFlow lifecycle hooks
├── Models/               # Test/application models
├── Pages/                # Selenium Page Objects
├── StepDefinitions/      # Given / When / Then bindings
├── TestData/
│   ├── dev/login.json
│   └── qa/login.json
├── Utils/                # Reusable utilities
├── App.config            # Environment/application settings
├── log4net.config        # Logging configuration
├── specflow.json         # SpecFlow/LivingDoc configuration
├── test.runsettings      # Runtime test parameters
├── test.Designer.cs      # Generated runsettings support
├── csharp-specflow-automation-framework.csproj
├── csharp-specflow-automation-framework.sln
└── ReadMe.md
```

## BDD / Gherkin

Example:

```gherkin
Feature: Login

  @smoke
  Scenario: Login with valid credentials
    Given I am on the login page
    When I enter valid credentials
    And I click the login button
    Then I should see the home page
```

Use `Feature`, `Scenario`, `Given`, `When`, `Then`, `And`, `But`, `Scenario Outline`, `Examples`, and tags to describe behavior. Keep feature files business-readable and avoid exposing Selenium locators.

## Step Definitions

Step definitions connect Gherkin to C#:

```csharp
[Given(@"I am on the login page")]
public void GivenIAmOnTheLoginPage()
{
    loginPage.Open();
}
```

Keep bindings thin:

```text
Gherkin → Step Definition → Page Object / Business Helper → Selenium
```

## Page Object Model

Page objects should encapsulate locators, UI actions, synchronization, and page-specific behavior.

```csharp
public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly By username = By.Id("username");
    private readonly By password = By.Id("password");
    private readonly By loginButton = By.Id("login");

    public LoginPage(IWebDriver driver) => this.driver = driver;

    public void Login(string user, string pwd)
    {
        driver.FindElement(username).SendKeys(user);
        driver.FindElement(password).SendKeys(pwd);
        driver.FindElement(loginButton).Click();
    }
}
```

Benefits include centralized locators, less duplication, reusable actions, cleaner steps, and easier UI maintenance.

## Hooks

Use hooks for test lifecycle activities:

```text
BeforeScenario
    ↓
Initialize configuration / WebDriver / pages
    ↓
Execute scenario
    ↓
AfterScenario
    ├── Screenshot if failed
    ├── Write logs
    └── Dispose browser
```

## Test Data

Environment-specific login data is stored under:

```text
TestData/
├── dev/login.json
└── qa/login.json
```

Conceptually:

```text
ENV=dev → TestData/dev/login.json
ENV=qa  → TestData/qa/login.json
```

Keep test data separate from page objects and step implementation.

## Environment Configuration

`App.config` contains an environment setting with a development default. Runtime `ENV` can also be supplied through test-run parameters.

Recommended structure:

```text
ENV
 ├── dev
 ├── qa
 ├── stage
 └── prod
```

Keep URLs and environment configuration outside feature files and page objects.

## Run Settings

`test.runsettings` supports runtime parameters such as:

```xml
<RunSettings>
  <TestRunParameters>
    <Parameter name="ENV" value="dev" />
    <Parameter name="UID" value="..." />
    <Parameter name="PWD" value="..." />
  </TestRunParameters>
</RunSettings>
```

**Security:** do not commit real credentials. The current repository file contains credential-like sample values; if they are real, rotate them and move secrets to GitHub Secrets, Azure DevOps variables, Jenkins Credentials, GitLab CI/CD variables, or a vault.

## Logging

The framework uses **log4net**. The configured rolling file output is:

```text
Reports/MyApp.log
```

Use logs for Selenium errors, setup failures, authentication issues, navigation problems, and CI troubleshooting.

## Screenshots

Failed tests automatically capture screenshots under:

```text
Reports/Screenshots/
```

Screenshots should have unique names and be attached/published with the corresponding test result where possible.

## Living Documentation

The project includes `SpecFlow.Plus.LivingDocPlugin`. `specflow.json` enables the Living Documentation generator and uses `TestExecution.json` as the execution-data file.

The repository documents the generated report as:

```text
Reports/Report.html
```

Install the CLI with:

```bash
dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
```

Use the activation process required by the applicable LivingDoc version.

## Prerequisites

- Git
- Visual Studio 2022
- .NET 6 SDK/runtime compatible with the project
- Chrome
- SpecFlow tooling compatible with the project
- LivingDoc CLI if Living Documentation is required

Verify:

```bash
dotnet --version
git --version
```

## Installation

```bash
git clone https://github.com/vinodkpasi/csharp-specflow-automation-framework.git
cd csharp-specflow-automation-framework
dotnet restore
dotnet build
```

## Build

```bash
dotnet clean
dotnet restore
dotnet build
```

Release build:

```bash
dotnet build --configuration Release
```

## Run Tests

Run all tests:

```bash
dotnet test
```

Run with runtime parameters:

```bash
dotnet test -- \
  TestRunParameters.Parameter(name="ENV", value="qa") \
  TestRunParameters.Parameter(name="UID", value="$TEST_UID") \
  TestRunParameters.Parameter(name="PWD", value="$TEST_PASSWORD")
```

Shell quoting differs between Bash, PowerShell, CMD, and CI systems.

## Run Tests by Tag

Example:

```gherkin
@smoke
Scenario: Verify login
```

The repository documents:

```bash
dotnet test --filter "testcategory=smoke" -- \
  TestRunParameters.Parameter(name="ENV", value="qa") \
  TestRunParameters.Parameter(name="UID", value="$TEST_UID") \
  TestRunParameters.Parameter(name="PWD", value="$TEST_PASSWORD")
```

Useful categories include `@smoke`, `@regression`, `@sanity`, `@critical`, and `@e2e`.

## Run Tests by Feature / Name

```bash
dotnet test --filter login -- \
  TestRunParameters.Parameter(name="ENV", value="qa") \
  TestRunParameters.Parameter(name="UID", value="$TEST_UID") \
  TestRunParameters.Parameter(name="PWD", value="$TEST_PASSWORD")
```

Use a more specific filter if multiple tests match the same text.

## List Tests

```bash
dotnet test -t
```

Useful for checking discovery, generated SpecFlow scenarios, and filters.

## Visual Studio Execution

```text
Open solution
   ↓
Restore packages
   ↓
Build
   ↓
Open Test Explorer
   ↓
Configure test.runsettings
   ↓
Discover tests
   ↓
Run / Debug
```

## Parallel Execution

The framework supports sequential and parallel execution. Before enabling parallel execution, isolate WebDriver instances, browser sessions, test data, authentication, temporary files, and output artifacts.

Preferred model:

```text
Worker 1 → Driver 1 → Scenario 1
Worker 2 → Driver 2 → Scenario 2
Worker 3 → Driver 3 → Scenario 3
```

Avoid shared static WebDriver or mutable global test state.

## Cross-Browser Execution

The project documents cross-browser capability and currently includes ChromeDriver. A scalable driver factory can support:

```text
Chrome
Edge
Firefox
RemoteWebDriver
```

Example:

```csharp
public IWebDriver CreateDriver(string browser) => browser.ToLowerInvariant() switch
{
    "chrome"  => new ChromeDriver(),
    "edge"    => new EdgeDriver(),
    "firefox" => new FirefoxDriver(),
    _ => throw new ArgumentException($"Unsupported browser: {browser}")
};
```

The current project references `Selenium.WebDriver.ChromeDriver` 108.0.5359.7100. Review driver management when upgrading browsers/Selenium.

## CI/CD

Recommended pipeline:

```text
Checkout → Install .NET → Restore → Build → Inject Secrets
→ Select Environment → Run Tests → Collect Screenshots/Logs
→ Publish Results → Publish Report
```

Example:

```bash
dotnet restore
dotnet build --configuration Release --no-restore
dotnet test --configuration Release --no-build -- \
  TestRunParameters.Parameter(name="ENV", value="qa") \
  TestRunParameters.Parameter(name="UID", value="$TEST_UID") \
  TestRunParameters.Parameter(name="PWD", value="$TEST_PASSWORD")
```

Suitable CI platforms include GitHub Actions, Azure DevOps, Jenkins, and GitLab CI/CD.

## Security

Never commit passwords, API keys, access tokens, cloud credentials, or private certificates. Use CI/CD secret stores or a vault. If a real secret was committed, rotate it immediately and review repository history.

## Troubleshooting

### Tests not discovered

```bash
dotnet clean
dotnet restore
dotnet build
dotnet test -t
```

Check `Microsoft.NET.Test.Sdk`, MSTest adapter, `SpecFlow.MsTest`, generated feature code, and compilation errors.

### Step definition not found

Verify `[Binding]`, `[Given]`/`[When]`/`[Then]` attributes, exact Gherkin text, and successful project compilation.

### Browser does not start

Check Chrome installation, ChromeDriver compatibility, Selenium version, driver configuration, and CI permissions.

### Element not found

Check locator, page readiness, visibility, iframe/window context, explicit waits, and application state. Prefer condition-based waits over `Thread.Sleep`.

### Test data not loaded

Verify `ENV`, `TestData/<environment>`, JSON file names, output copying, and file paths.

### LivingDoc/report not generated

Check `specflow.json`, LivingDoc plugin/CLI, output permissions, and whether test execution completed successfully.

### Log not generated

Check `log4net.config` and `Reports/MyApp.log`.

## 🧹 Best Practices

### Feature files

- Business readable
- Behavior focused
- Concise and independent
- No Selenium locators

### Step definitions

- Thin and reusable
- No duplicated locators
- Delegate UI behavior to page objects

### Page objects

- Own locators and UI actions
- Encapsulate synchronization
- Expose business-level actions

### Test data

Keep environment data separate from automation code.

### Waits

Prefer `WebDriverWait` / condition-based waits over arbitrary sleeps.

### Assertions

Assert business outcomes rather than Selenium implementation details.

## 📈 Recommended Enhancements

For production-scale use, consider:

1. Driver Factory for local/remote browsers.
2. Central environment manager for dev/QA/stage/prod.
3. Secure secret management.
4. Allure/ExtentReports or CI-native reporting.
5. API-based test-data setup.
6. Controlled database utilities where appropriate.
7. Structured logging and richer diagnostics.
8. Carefully scoped retry strategy for transient infrastructure failures.
9. Selenium Grid/Docker for scalable execution.
10. BrowserStack/Sauce Labs/LambdaTest integration.
11. CI browser/environment matrix.
12. Unique test-data factories for parallel scenarios.

## 🔁 SpecFlow and Reqnroll

**SpecFlow reached end-of-life after December 31, 2024.** For new .NET BDD projects, consider **Reqnroll**, an open-source .NET BDD automation framework based on the SpecFlow codebase.

Modernization concept:

```text
Current:   C# + Selenium + SpecFlow + Gherkin + MSTest
                         ↓
Modern:    C# + Selenium + Reqnroll + Gherkin + MSTest/NUnit
```

Review feature files, bindings, hooks, generated code, configuration, adapters, reporting, CI/CD, and custom utilities before migrating.

## 🧭 Responsibility Matrix

| Component | Responsibility |
|---|---|
| `Features` | Business-readable Gherkin |
| `StepDefinitions` | Connect Gherkin to automation |
| `Pages` | UI interactions and locators |
| `Hooks` | Lifecycle/setup/cleanup |
| `Models` | Strongly typed data |
| `TestData` | Environment-specific data |
| `Utils` | Reusable utilities |
| `App.config` | Environment/application settings |
| `test.runsettings` | Runtime parameters |
| `log4net.config` | Logging |
| `specflow.json` | SpecFlow/LivingDoc configuration |
| `Reports` | Logs, screenshots, reports |

## 🧪 Example End-to-End Scenario

```gherkin
Feature: Gallery

  @smoke
  Scenario: User views gallery content
    Given I am on the gallery page
    When I select a gallery item
    Then the gallery details should be displayed
```

Execution:

```text
Feature → Step Definition → Page Object → Selenium → Browser → Application → Assertion
```

Failure artifacts:

```text
Failure → Screenshot + Log + LivingDoc result
```

## 🤝 Contributing

1. Create a branch:

```bash
git checkout -b feature/add-new-scenario
```

2. Implement and test the change.
3. Ensure no secrets are committed.
4. Update documentation when framework behavior changes.
5. Commit and push:

```bash
git add .
git commit -m "Add new BDD automation scenario"
git push origin feature/add-new-scenario
```

6. Open a Pull Request.

## 📄 License

No explicit open-source license is declared in the repository. Add a `LICENSE` file if public reuse rights are intended.

## 👤 Author

**Vinod Kumar — Lead SDET / QA Automation Leader**

Technical areas represented by this framework include C#, .NET, Selenium, SpecFlow/BDD, Gherkin, MSTest, Page Object Model, cross-browser testing, environment management, test data, logging, screenshots, Living Documentation, and CI/CD.

GitHub: https://github.com/vinodkpasi

Repository: https://github.com/vinodkpasi/csharp-specflow-automation-framework

## ⭐ Support

If this project is useful for learning or demonstrating C# BDD automation, consider starring the repository.

## 📚 References

- Selenium: https://www.selenium.dev/
- .NET: https://dotnet.microsoft.com/
- Gherkin: https://cucumber.io/docs/gherkin/
- SpecFlow: https://specflow.org/
- Reqnroll: https://reqnroll.net/
- MSTest: https://learn.microsoft.com/dotnet/core/testing/unit-testing-with-mstest
