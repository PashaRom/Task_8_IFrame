using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Aquality.Selenium.Browsers;
namespace Test.Utilities.Configuration
{
    public class ConfigurationGetter
    {         
        public ConfigurationGetter(string fileName)
        {
            var builder = new ConfigurationBuilder().AddJsonFile(fileName);
            GetConfiguration = builder.Build();            
        }
        public IConfiguration GetConfiguration { get; }
        public bool GetBooleanParam(string param)
        {
            string stringParam = null;
            bool booleanParam = false;
            AqualityServices.Logger.Info($"The param \"{param}\" is writing.");
            try
            {
                stringParam = GetConfiguration[param];
                if (String.IsNullOrEmpty(stringParam))
                    throw new FormatException($"The param \"{param}\" is empty or invalid format. \"{param}\" can have one value of false or true.");
                else
                    return booleanParam = Convert.ToBoolean(stringParam);
            }
            catch (FormatException ex)
            {
                AqualityServices.Logger.Fatal($"Invalid param \"{param}\" in {ConfigurationData.TestCofigurationFileName}. The param \"{param}\" can have value true or false", ex);
                return booleanParam;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal($"Unexpected error occurred during to write param {param} from {ConfigurationData.TestCofigurationFileName}.", ex);
                return booleanParam;
            }
        }
        public string GetStringParam(string param)
        {
            string errorMessage = $"The param \"{param}\" is empty or invalid format. \"{param}\" can have one value of false or true.";
            string stringParam = null;
            AqualityServices.Logger.Info($"The param \"{param}\" is writing.");
            try
            {
                stringParam = GetConfiguration[param];
                if (String.IsNullOrEmpty(stringParam))
                    throw new Exception(errorMessage);
                else
                    return stringParam;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal(errorMessage, ex);
                return stringParam;
            }
        }
        public int GetIntParam(string param)
        {
            string stringParam = null;
            int intParam = 0;
            AqualityServices.Logger.Info($"The param \"{param}\" is writing.");
            try
            {
                stringParam = GetConfiguration[param];
                if (String.IsNullOrEmpty(stringParam))
                    throw new FormatException($"The param \"{param}\" is empty or invalid format. \"{param}\" can have integer value.");
                else
                    return intParam = Convert.ToInt32(stringParam);
            }
            catch (FormatException ex)
            {
                AqualityServices.Logger.Fatal($"Invalid param \"{param}\" in {ConfigurationData.TestCofigurationFileName}. The param \"{param}\" can have integer value.", ex);
                return intParam;
            }
            catch (OverflowException ex)
            {
                AqualityServices.Logger.Fatal($"Invalid param \"{param}\" in {ConfigurationData.TestCofigurationFileName}. The param \"{param}\" can have integer value, but the current value has less than -2147483648 or greater than 2147483647.", ex);
                return intParam;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal($"Unexpected error occurred during to write param {param} from {ConfigurationData.TestCofigurationFileName}.", ex);
                return intParam;
            }
        }
        public Queue<T> GetSectionWithArray<T>(string nameSection)
        {
            AqualityServices.Logger.Info($"Get section \"{nameSection}\" for class \"{typeof(T).ToString()}\"");
            Queue<T> ts = new Queue<T>();
            try
            {
                var valuesSection = GetConfiguration.GetSection(nameSection);
                foreach (IConfigurationSection section in valuesSection.GetChildren())
                {
                    ts.Enqueue(section.Get<T>());
                }
                return ts;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal($"Unexpected error occurred during writing section \"{nameSection}\" for class \"{typeof(T).ToString()}\" from {ConfigurationData.TestCofigurationFileName} file.", ex);
            }
            return ts;
        }
        public T GetObjectParam<T>(string nameObject)
            where T: class, new()
        {
            T obj = new T();
            try
            {
                obj = GetConfiguration.GetSection(nameObject).Get<T>();
                return obj;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal($"Unexpected error occurred during writing section \"{nameObject}\" for class \"{typeof(T).ToString()}\" from {ConfigurationData.TestCofigurationFileName} file.", ex);
                return obj;
            }
        }
    }
}
