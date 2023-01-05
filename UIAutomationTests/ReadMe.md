# Gallery UI Automation 
Project developed in selenium, specflow and dot net technology. this project handles end to end Gallery UI testing.

# Prerequisite 
Install the below dependencies.
- Git
- Visual Studio 2022 
- Specflow for Visual Studio 2022- install extension in your visual studio and restart your machine.
- Specflow Living doc Installation & Activation- Install Living doc CLI tool and activate by personal/professional microsoft account(See more details below)
- How to install living doc CLI
```sh
      dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI 
```
- Chrome Browser

# Features
- Page Object Model
- BDD Approch
- Living doc reports
- Logger file
- Cross Browser
- Screenshots
- Sequencial/Paraller Execution
- Cross Environment
- Credentials set via command line/run-settings

# Test Execution

***Specflow Account Activation Process-***
  Sign in with your Microsoft account. It can be a personal or corporate/enterprise account.
  If you are already signed in to your personal Microsoft account, this should happen automatically.
  You might need additional permissions from your Active Directory admin for professional MS account.

  Click below links for more details about specflow account/living doc activation process.
  [here](https://docs.specflow.org/en/latest/specflowaccount.html)  Official Page
  [here](https://www.youtube.com/watch?v=eX8JCtZKPfk)  Official Video
  
  ***Execution- By Visual Studio Test Explorer*** 
  Build the project solution. Using Test-->MS Test Explorer menu you can view and click on configure run settings and click on GalleryUIAutomationTest folder then You can see test.runsettings xml file. Please click on test.runsettings file, this is one time setting operation.

***Execution- By Command line Interface(CLI)*** 
- Set the runtime paramter(env,credentials) by CLI
```sh
- New QA-- old QA-please pass correct value of keys like -(name=\"ENV\", value=\"dev\")
dotnet test --filter login -- TestRunParameters.Parameter(name=\"UID\", value=\"value\") TestRunParameters.Parameter(name=\"PWD\", value=\"value\") TestRunParameters.Parameter(name=\"ENV\", value=\"dev\")
```

- Run all test cases based on projectrootpath
```sh
      dotnet test -- TestRunParameters.Parameter(name=\"UID\", value=\"value\") TestRunParameters.Parameter(name=\"PWD\", value=\"value\") TestRunParameters.Parameter(name=\"ENV\", value=\"value\") 
```
- To list all the tests in the project you can use the -t flag: 
```sh
      dotnet test -t
```
- Run Specfic tag(ex-smoke) based test cases:
```sh
     dotnet test --filter "testcategory=smoke" -- TestRunParameters.Parameter(name=\"UID\", value=\"value\") TestRunParameters.Parameter(name=\"PWD\", value=\"value\") TestRunParameters.Parameter(name=\"ENV\", value=\"value\")
```
- Run Specfic feature file test cases --filter
```sh
   dotnet test --filter login -- TestRunParameters.Parameter(name=\"UID\", value=\"value\") TestRunParameters.Parameter(name=\"PWD\", value=\"value\") TestRunParameters.Parameter(name=\"ENV\", value=\"value\")
```
# Driver
- Currently Chrome driver manager configured in the project (bin/debug/net) will support
  chrome browser latest version.Please confirm your local chrome browser version with chrome driver exe version. 

# Report
- After execution test cases-report is auto generated in the Reports folder.  directory(\UIAutomationTests\Reports).
- You will find test living docreport html file by Report.html name.

# Screenshots
- When test case is failed,by default screenshot is genrated in the Reports folder. folder(\Reports\Screenshots).
- All error images have unique name by Error/featurefilename/ScenarioName/Timestamp.png
- You can also see attached screenshots link by click on Show TestOutput button in living doc report.

# Logger
- Logs file-MyApp.log is genrated in  Reports folder(\UIAutomationTests\Reports\MyApp.log).