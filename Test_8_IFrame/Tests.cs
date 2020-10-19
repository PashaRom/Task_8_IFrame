using NUnit.Framework;
using Aquality.Selenium.Browsers;
using Test.Utilities;
using Test.Utilities.Configuration;
using Test_8_IFrame.PageObjects;
namespace Test_8_IFrame
{
    public class Tests
    { 
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            AqualityServices.Browser.Maximize();            
        }        

        [Test]        
        public void GoToWedResource()
        {
            StepLogger.Info(1,$"Go to url:{ConfigurationManager.Configuration.GetStringParam("url")}");
            AqualityServices.Browser.GoTo(ConfigurationManager.Configuration.GetStringParam("url"));
            AqualityServices.Browser.WaitForPageToLoad();
            FramePage framePage = new FramePage();           
            Assert.AreEqual(ConfigurationManager.TestData.GetStringParam("pageHeader"), framePage.PageHeder.Text,
                $"The expected page header was not equal with actual page header.");
            StepLogger.Info(2, $"Clear the editor's text box and enter the generic text there.");
            framePage.EditorForm.ClearTextBoxEditor();
            (string Expected, string Actual) textFromEditorTextBox = framePage.EditorForm.WriteText();
            Assert.AreEqual(textFromEditorTextBox.Expected, textFromEditorTextBox.Actual,
                $"Generated text for the text field frame and actual text there were different.");
            StepLogger.Info(3, $"Set text bold.");                      
            Assert.IsTrue(framePage.EditorForm.IsTextBold(), "The text in the text box has not been set bold.");
        }        

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            AqualityServices.Browser.Quit();
        }
    }
}