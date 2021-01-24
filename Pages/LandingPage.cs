using OpenQA.Selenium;

namespace UnleashedTestProject.Pages
{
    class LandingPage
    {
        private static IWebDriver driver;

        public IWebElement menuIcon => driver.FindElement(By.CssSelector("#shortcuts > div.shortcut-menu-icon"));
        public IWebElement addProductMenu => driver.FindElement(addProduct);
        public IWebElement stockOnHandEnquiryMenu => driver.FindElement(stockOnHandEnquiry);
        public IWebElement addQuoteMenu => driver.FindElement(addQuote);

        public By shortcutMenuIcon => By.Id("shortcuts");
        public static By addProduct => By.LinkText("Add Product");
        public static By stockOnHandEnquiry => By.LinkText("Stock On Hand Enquiry");
        public static By addQuote => By.LinkText("Add Quote");

        public LandingPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }
    }
}
