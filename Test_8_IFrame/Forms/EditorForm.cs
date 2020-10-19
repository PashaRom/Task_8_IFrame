using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Test.Utilities;
namespace Test_8_IFrame.Forms
{
    public class EditorForm
    {
        private ITextBox EditorsTextBox => AqualityServices.Get<IElementFactory>().GetTextBox(By.XPath(".//body[@id='tinymce']/p"), "Text field");
        private IButton BoldButton => AqualityServices.Get<IElementFactory>().GetButton(By.XPath(".//div[@id='mceu_3']/button"), "Bold");
        private string actualTextFieldFrame = string.Empty;
        private void MoveToTextBox()
        {
            AqualityServices.Browser.Driver.SwitchTo().Frame("mce_0_ifr");
        }        

        private void MoveToMenuEditor()
        {
            AqualityServices.Browser.Driver.SwitchTo().DefaultContent();
        }

        public void ClearTextBoxEditor()
        {
            AqualityServices.Logger.Info($"Move to the text box.");
            MoveToTextBox();
            AqualityServices.Logger.Info($"Delete the text inside the frame.");
            EditorsTextBox.SetInnerHtml("<br>");
        }

        public (string ExpectedText, string ActualText) WriteText()
        {           
            AqualityServices.Logger.Info($"Generat text.");
            string expectedGenerateText = StringUtil.GeneraterText(50);
            AqualityServices.Logger.Info($"Write generated text inside the frame.");
            EditorsTextBox.SetInnerHtml(expectedGenerateText);
            AqualityServices.Logger.Info($"Get the text has writen inside the frame.");
            actualTextFieldFrame = EditorsTextBox.Text;
            return (expectedGenerateText, actualTextFieldFrame);
        }

        public bool IsTextBold()
        {
            AqualityServices.Logger.Info($"Select all text inside the frame.");
            EditorsTextBox.SendKeys(Keys.Control + "a");
            AqualityServices.Logger.Info($"Move to the menu editor.");
            MoveToMenuEditor();            
            AqualityServices.Logger.Info($"Click the button \"Bold\".");
            BoldButton.Click();
            AqualityServices.Logger.Info($"Move to the text box.");
            MoveToTextBox();
            var strongTextFrame = AqualityServices.ConditionalWait.WaitFor<IWebElement>(driver =>
            {
                return driver.FindElement(By.XPath(".//body[@id='tinymce']/p/strong"));
            });
            string actualStringStrongTextFrame = strongTextFrame.Text;
            if (actualTextFieldFrame.Equals(actualStringStrongTextFrame))
                return true;
            else
                return false;
        }
    }
}
