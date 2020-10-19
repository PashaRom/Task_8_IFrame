using Aquality.Selenium.Browsers;
namespace Test.Utilities
{
    public static class StepLogger
    {
        public static void Info(int numberOfStep, string description)
        {
            string infoMessage = $"STEP: {numberOfStep}\tDescription: {description}";
            AqualityServices.Logger.Info(infoMessage);
        }
    }
}
