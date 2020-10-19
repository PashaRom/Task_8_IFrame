using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Test_8_IFrame.Forms;
namespace Test_8_IFrame.PageObjects
{
    public class FramePage
    {
        public EditorForm EditorForm = new EditorForm();
        public ILabel PageHeder => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath(".//div[@class='example']/h3"), "Page heder", ElementState.Displayed);        
    }
}
