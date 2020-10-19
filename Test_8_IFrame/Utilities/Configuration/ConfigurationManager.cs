using System;
using Aquality.Selenium.Browsers;
namespace Test.Utilities.Configuration
{
    public class ConfigurationManager
    {
        public static ConfigurationGetter Configuration;
        public static ConfigurationGetter TestData;
        static ConfigurationManager() 
        {
            try { 
                Configuration = new ConfigurationGetter(ConfigurationData.TestCofigurationFileName);            
                TestData = new ConfigurationGetter(ConfigurationData.TestDataFileName);
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Fatal("Unexpected error occurred during creating ConfigurationManager.", ex);
            }
        }        
    }   
}
