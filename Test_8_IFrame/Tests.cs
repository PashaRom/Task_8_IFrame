using NUnit.Framework;
using OpenQA.Selenium;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Test.Utilities;
using Test.Utilities.Configuration;
namespace Test_8_IFrame
{
    public class Tests
    {
        string serviceUrl = ConfigurationManager.Configuration.GetStringParam("url");
        string expectedPageHeader = ConfigurationManager.TestData.GetStringParam("pageHeader");
        string actualTextFieldFrame = string.Empty;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var browser = AqualityServices.Browser;
            browser.Maximize();
        }        

        [Test]
        [Order(1)]
        public void GoToWedResource()
        {
            StepLogger.Info(1,$"Go to url:{serviceUrl}");
            var browser = AqualityServices.Browser;           
            browser.GoTo(serviceUrl);
            browser.WaitForPageToLoad();
            StepLogger.Info(2, $"Get the page head.");
            var elementFactory = AqualityServices.Get<IElementFactory>();
            var actualPageHeader = elementFactory.GetLabel(By.XPath(".//div[@class='example']/h3"), "Page heder", ElementState.Displayed).GetText();
            string pageHeaderLogMessage = $"The expected page header is \"{expectedPageHeader}\". The actual page header was \"{actualPageHeader}\".";
            AqualityServices.Logger.Info(pageHeaderLogMessage);
            Assert.AreEqual(expectedPageHeader, actualPageHeader, pageHeaderLogMessage);           
        }

        [Test]
        [Order(2)]
        public void DeleteTextAndEnterGenerateText()
        {            
            var browser = AqualityServices.Browser;            
            StepLogger.Info(3, $"Switch to frame.");
            browser.Driver.SwitchTo().Frame("mce_0_ifr");
            var elementFactory = AqualityServices.Get<IElementFactory>();
            var textFieldFrame = elementFactory.GetTextBox(By.XPath(".//body[@id='tinymce']/p"),"Text field");
            StepLogger.Info(4, $"Delete the text inside the frame.");
            textFieldFrame.SetInnerHtml("<br>");
            StepLogger.Info(5, $"Generat text.");
            string expectedGenerateText = StringUtil.GeneraterText(50);
            StepLogger.Info(6, $"Write generated text inside the frame.");
            textFieldFrame.SetInnerHtml(expectedGenerateText);
            StepLogger.Info(7, $"Get the text has writen inside the frame.");
            actualTextFieldFrame = textFieldFrame.GetText();
            string textFieldFrameMessage = $"Generated text for the text field frame is \"{expectedGenerateText}\". The actual text is \"{actualTextFieldFrame}\".";
            AqualityServices.Logger.Info(textFieldFrameMessage);
            Assert.AreEqual(expectedGenerateText, actualTextFieldFrame, textFieldFrameMessage);            
        }

        [Test]
        [Order(3)]
        public void SelectTextAndMakeBold()
        {
            var browser = AqualityServices.Browser;
            var elementFactory = AqualityServices.Get<IElementFactory>();
            var textFieldFrame = elementFactory.GetTextBox(By.XPath(".//body[@id='tinymce']/p"), "Text field");
            textFieldFrame.SendKeys(Keys.Control + "a");
            browser.Driver.SwitchTo().DefaultContent();
            var buttonBold = elementFactory.GetButton(By.XPath(".//div[@id='mceu_3']/button"),"Bold");
            buttonBold.Click();
            browser.Driver.SwitchTo().Frame("mce_0_ifr");            
            var strongTextFrame = AqualityServices.ConditionalWait.WaitFor<IWebElement>(driver=> 
            {
                return driver.FindElement(By.XPath(".//body[@id='tinymce']/p/strong"));                
            });
            string actualStringStrongTextFrame = strongTextFrame.Text;
            string actualStringStrongTextFrameMessage = $"The bold text inside the text frame is \"{actualStringStrongTextFrame}\"";
            Assert.AreEqual(actualTextFieldFrame, actualStringStrongTextFrame, actualStringStrongTextFrame);
            Assert.Pass();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            AqualityServices.Browser.Quit();
        }
    }
}