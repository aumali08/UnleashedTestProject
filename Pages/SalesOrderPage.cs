using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UnleashedTestProject.Pages
{
    class SalesOrderPage
    {
        private static IWebDriver driver;
        private static WebDriverWait wait;

        public IWebElement createOrderBtn => driver.FindElement(By.Id("btnCreateOrder"));
        public IWebElement salesQuoteNumberInput => driver.FindElement(By.Id("SalesQuoteNumber"));
        public IWebElement completeBtn => driver.FindElement(btnComplete);
        public IWebElement orderStatusDisplay => driver.FindElement(By.Id("OrderStatusDisplay"));

        public By mainContentPanel => By.Id("main-content-panel");
        public By orderMgmtDropdown => By.Id("ddbOrderManagement");
        public static By btnComplete => By.Id("btnComplete");
        public static By confirmDialog => By.Id("generic-confirm-dialog");

        public static void confirmSalesOrderCompletion()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("generic-confirm-modal-yes"))).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(confirmDialog));
        }

        public SalesOrderPage(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }
    }
}
