using NUnit.Framework;
using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Test.Utilities;
using Test.Utilities.Configuration;
using Test_8_IFrame.PageObjects;
namespace Test_8_IFrame
{
    public class Tests
    {
        private string serviceUrl = ConfigurationManager.Configuration.GetStringParam("url");
        private string expectedPageHeader = ConfigurationManager.TestData.GetStringParam("pageHeader");
        private string actualTextFieldFrame = string.Empty;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var browser = AqualityServices.Browser;
            browser.Maximize();
        }        

        [Test]        
        public void GoToWedResource()
        {
            StepLogger.Info(1,$"Go to url:{serviceUrl}");
            FramePage framePage = new FramePage();
            framePage.LoadPage(serviceUrl);            
            StepLogger.Info(2, $"Get the page head.");            
            framePage.FindPageHeader();            
            var actualPageHeader = framePage.PageHeder.Text;            
            string pageHeaderLogMessage = $"The expected page header is \"{expectedPageHeader}\". The actual page header was \"{actualPageHeader}\".";
            AqualityServices.Logger.Info(pageHeaderLogMessage);
            Assert.AreEqual(expectedPageHeader, actualPageHeader, pageHeaderLogMessage);
            StepLogger.Info(3, $"Move to the text box.");
            framePage.EditorForm.MoveToTextBox();
            StepLogger.Info(4, $"Find the text box.");
            framePage.EditorForm.FindTextBox();
            StepLogger.Info(5, $"Delete the text inside the frame.");
            framePage.EditorForm.TextBox.SetInnerHtml("<br>");
            StepLogger.Info(6, $"Generat text.");
            string expectedGenerateText = StringUtil.GeneraterText(50);
            StepLogger.Info(7, $"Write generated text inside the frame.");
            framePage.EditorForm.TextBox.SetInnerHtml(expectedGenerateText);
            StepLogger.Info(8, $"Get the text has writen inside the frame.");
            actualTextFieldFrame = framePage.EditorForm.TextBox.Text;
            string textFieldFrameMessage = $"Generated text for the text field frame is \"{expectedGenerateText}\". The actual text is \"{actualTextFieldFrame}\".";
            AqualityServices.Logger.Info(textFieldFrameMessage);
            Assert.AreEqual(expectedGenerateText, actualTextFieldFrame, textFieldFrameMessage);
            StepLogger.Info(9, $"Select all text inside the frame.");
            framePage.EditorForm.TextBox.SendKeys(Keys.Control + "a");
            StepLogger.Info(10, $"Move to the menu editor.");
            framePage.EditorForm.MoveToMenuEditor();
            StepLogger.Info(11, $"Find the button \"Bold\".");
            framePage.EditorForm.FindBoldButton();
            StepLogger.Info(12, $"Click the button \"Bold\".");
            framePage.EditorForm.BoldButton.Click();
            StepLogger.Info(13, $"Move to the text box.");
            framePage.EditorForm.MoveToTextBox();            
            Assert.IsTrue(framePage.EditorForm.IsTextBold(actualTextFieldFrame), "The text in the text box has not been made bold");
        }        

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            AqualityServices.Browser.Quit();
        }
    }
}