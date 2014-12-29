using TechTalk.SpecFlow;

namespace Douglas.AcceptanceTests.Automation.Steps
{
    [Binding]
    public class EnvironmentSteps
    {
        [Given(@"the feature is available in ""(.*)""")]
        public void GivenTheFeatureIsAvailableIn(string p0)
        {
            TargetEnvironment.Set(p0);
        }
    }
}