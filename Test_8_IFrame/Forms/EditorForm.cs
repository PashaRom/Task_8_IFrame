using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
namespace Test_8_IFrame.Forms
{
    public class EditorForm
    {
        private Browser browser = AqualityServices.Browser;
        private IElementFactory elementFactory = AqualityServices.Get<IElementFactory>();
        public ITextBox TextBox;
        public IButton BoldButton;

        public void MoveToTextBox()
        {
            browser.Driver.SwitchTo().Frame("mce_0_ifr");
        }
        public void FindTextBox()
        {
            TextBox = elementFactory.GetTextBox(By.XPath(".//body[@id='tinymce']/p"), "Text field");
        }

        public void MoveToMenuEditor()
        {
            browser.Driver.SwitchTo().DefaultContent();
        }

        public void FindBoldButton()
        {
            BoldButton = elementFactory.GetButton(By.XPath(".//div[@id='mceu_3']/button"), "Bold");
        }

        public bool IsTextBold(string expectedTextFrame)
        {
            var strongTextFrame = AqualityServices.ConditionalWait.WaitFor<IWebElement>(driver =>
            {
                return driver.FindElement(By.XPath(".//body[@id='tinymce']/p/strong"));
            });
            string actualStringStrongTextFrame = strongTextFrame.Text;
            if (expectedTextFrame.Equals(actualStringStrongTextFrame))
                return true;
            else
                return false;
        }
    }
}
