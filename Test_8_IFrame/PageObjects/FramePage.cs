using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Test_8_IFrame.Forms;
namespace Test_8_IFrame.PageObjects
{
    public class FramePage
    {
        private Browser browser = AqualityServices.Browser;
        private IElementFactory elementFactory = AqualityServices.Get<IElementFactory>();
        public EditorForm EditorForm = new EditorForm();
        public ILabel PageHeder;

        public void LoadPage(string url)
        {           
            browser.GoTo(url);
            browser.WaitForPageToLoad();
        }

        public void FindPageHeader()
        {
            PageHeder = elementFactory.GetLabel(By.XPath(".//div[@class='example']/h3"), "Page heder", ElementState.Displayed);
        }         
    }
}
