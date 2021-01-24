using OpenQA.Selenium;

namespace UnleashedTestProject.Pages
{
    class AddProductsPage
    {
        private static IWebDriver driver;

        public IWebElement productCode => driver.FindElement(By.Id("Product_ProductCode"));
        public IWebElement productDesc => driver.FindElement(By.Id("Product_ProductDescription"));
        public IWebElement saveBtn => driver.FindElement(By.Id("btnSave"));

        public By detailsTab => By.Id("tabsDetailsLink");
        public By deleteButton => By.Id("DeleteButton");

        public AddProductsPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }
    }
}
