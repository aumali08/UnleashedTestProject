using OpenQA.Selenium;

namespace UnleashedTestProject.Pages
{
    class SalesQuotePage
    {
        private static IWebDriver driver;
        public IWebElement orderStatusDisplay => driver.FindElement(By.Id("OrderStatusDisplay"));
        public IWebElement customerSearchBtn => driver.FindElement(By.Id("CustomerSearchButton"));
        public IWebElement custSearchCdInput => driver.FindElement(By.Id("CustomerSearchCode"));
        public IWebElement productSearchButton => driver.FindElement(By.Id("ProductSearchButton"));
        public IWebElement localProdSearchInput => driver.FindElement(By.Id("LocalProductSearch"));
        public IWebElement prodQuantity => driver.FindElement(By.Id("QtyAddLine"));
        public IWebElement addProductBtn => driver.FindElement(By.Id("btnAddOrderLine"));
        public IWebElement saveSalesQuoteBtn => driver.FindElement(By.Id("ddbSave"));
        public IWebElement acceptQuoteBtn => driver.FindElement(acceptQuote);

        public By salesInvoiceTable => By.Id("SalesInvoiceTable");
        public By custLocalSearchControl => By.Id("CustomerLocalSearchControl");
        public By custLocalSearcFirstResult => By.CssSelector("table#CustomerLocalSearch *> td#CustomerLocalSearch_tccell0_0 > a");
        public By prodLocalSearchControl => By.Id("ProductLocalSearchControl");
        public By prodLocalSearchFirstResult => By.CssSelector("table#ProductLocalSearch *> td#ProductLocalSearch_tccell0_0 > div > a");
        public By prodSalesQuoteTable => By.CssSelector("table#SalesQuoteLinesList_DXMainTable *> tr.dxgvDataRow");
        public static By acceptQuote => By.Id("btnAcceptQuote");

        public SalesQuotePage(IWebDriver webDriver)
        {
            driver = webDriver;
        }
    }
}
