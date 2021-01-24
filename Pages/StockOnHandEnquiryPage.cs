using OpenQA.Selenium;

namespace UnleashedTestProject.Pages
{
    class StockOnHandEnquiryPage
    {
        private static IWebDriver driver;

        public IWebElement prodCodeSearchBtn => driver.FindElement(By.Id("btnProductCodeSearch"));
        public IWebElement productSearchInput => driver.FindElement(By.Id("LocalProductSearch"));
        public IWebElement runBtn => driver.FindElement(btnRun);
        public IWebElement stockOnHandDtl => driver.FindElement(By.CssSelector("#SOHList_DXDataRow0 > td:nth-child(7)"));

        public By btnRun => By.Id("btnRun");
        public By enquiryForm => By.Id("formtop");
        public By prodSearchForm => By.Id("modal-content-product");
        public static By prodListFirstRow => By.Id("SOHList_DXDataRow0");
        public By prodSearcFirstResult => By.CssSelector("td#ProductLocalSearch_tccell0_0 *> a");

        public StockOnHandEnquiryPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }
    }
}
