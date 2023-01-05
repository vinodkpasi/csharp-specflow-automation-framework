using TechTalk.SpecFlow;

namespace UIAutomationTests.StepDefinitions
{
[Binding]
public abstract class BaseSteps: TechTalk.SpecFlow.Steps
    {
        protected TechTalk.SpecFlow.ScenarioContext scenarioContext;
        public BaseSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        public BaseSteps()
        {
        }
    }
}
