using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;
using UnleashedTestProject.Pages;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UnleashedTestProject.Steps
{
    [Binding]
    public class FirstTaskSteps
    {
        private static IWebDriver driver;
        private static WebDriverWait wait;

        private static String salesNo;
        private static String prodStockOnHand;

        [Given(@"I login to the Unleashed website")]
        public void GivenILoginToTheUnleashedWebsite()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            // navigate to unleashed website
            driver.Navigate().GoToUrl("https://go.unleashedsoftware.com/v2/Account/LogOn");

            // login to the unleashed website
            LoginPage loginPage = new LoginPage(driver);
            loginPage.username.SendKeys("qa+umali@unl.sh");
            loginPage.password.SendKeys("M@rtyr4567");
            loginPage.loginBtn.Click();

            // skip 2-Step authentication
            LoginPage.SkipAuthentication();

            // skip tour
            LoginPage.SkipTour();

            // validate successful login
            Assert.True(wait.Until(ExpectedConditions.ElementIsVisible(loginPage.accountDropdown)).Displayed,
                "The account dropdown should be displayed");
        }

        [Given(@"I navigate to the Add Products page")]
        public void GivenINavigateToTheAddProductsPage()
        {
            LandingPage landingPage = new LandingPage(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(landingPage.shortcutMenuIcon));
            landingPage.menuIcon.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(LandingPage.addProduct));
            landingPage.addProductMenu.Click();

            AddProductsPage addProductsPage = new AddProductsPage(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(addProductsPage.detailsTab));
        }

        [When(@"I create a new product")]
        public void WhenICreateANewProduct()
        {
            AddProductsPage addProductsPage = new AddProductsPage(driver);
            addProductsPage.productCode.SendKeys("PROD" + new Random().Next(0, 100));
            addProductsPage.productDesc.SendKeys("Product Testing");
            addProductsPage.saveBtn.Click();
        }

        [Then(@"I see the product is successfully created")]
        public void ThenISeeTheProductIsSuccessfullyCreated()
        {
            AddProductsPage addProductsPage = new AddProductsPage(driver);
            Assert.True(wait.Until(ExpectedConditions.ElementIsVisible(addProductsPage.deleteButton)).Displayed,
                "The product delete button should be displayed");

            driver.Quit();
        }

        [Given(@"I retrieve the stock information of (.*)")]
        public void IRetrieveTheStockInformation(String product)
        {
            ProductStockOnHandEnquiry(product);

            StockOnHandEnquiryPage stockOnHandEnquiryPage = new StockOnHandEnquiryPage(driver);
            prodStockOnHand = stockOnHandEnquiryPage.stockOnHandDtl.Text;
        }

        [When(@"I navigate to the Add Sales Quote page")]
        public void INavigateToTheAddSalesQuotePage()
        {
            LandingPage landingPage = new LandingPage(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(landingPage.shortcutMenuIcon));
            landingPage.menuIcon.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(LandingPage.addQuote));
            landingPage.addQuoteMenu.Click();

            SalesQuotePage salesQuotePage = new SalesQuotePage(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(salesQuotePage.salesInvoiceTable));
        }

        [When(@"I accepted a new sales quote")]
        public void IAcceptedANewSalesQuote(Table salesTable)
        {
            SalesQuotePage salesQuotePage = new SalesQuotePage(driver);

            // sales quote details
            salesQuotePage.customerSearchBtn.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(salesQuotePage.custLocalSearchControl));
            salesQuotePage.custSearchCdInput.SendKeys("GBRO" + Keys.Enter);
            wait.Until(ExpectedConditions.ElementIsVisible(salesQuotePage.custLocalSearcFirstResult)).Click();

            // product details
            salesQuotePage.productSearchButton.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(salesQuotePage.prodLocalSearchControl));
            salesQuotePage.localProdSearchInput.SendKeys(salesTable.Rows[0][0] + Keys.Enter);
            wait.Until(ExpectedConditions.ElementIsVisible(salesQuotePage.prodLocalSearchFirstResult)).Click();
            salesQuotePage.prodQuantity.SendKeys(salesTable.Rows[0][1]);
            salesQuotePage.addProductBtn.Click();

            // verify product addition
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(salesQuotePage.prodSalesQuoteTable));
            Assert.True(driver.FindElements(salesQuotePage.prodSalesQuoteTable).Count > 0,
                "Sales Quote table should have more than zero records");

            // save quote
            salesQuotePage.saveSalesQuoteBtn.Click();

            // accept quote
            wait.Until(ExpectedConditions.ElementToBeClickable(salesQuotePage.acceptQuoteBtn)).Click(); ;
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(SalesQuotePage.acceptQuote));

            Assert.AreEqual(salesQuotePage.orderStatusDisplay.Text, "ACCEPTED", "Sales Quote status should be ACCEPTED");
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(SalesQuotePage.acceptQuote));
            salesNo = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("OrderNumberDisplay"))).Text;
        }

        [When(@"I complete a new sales order")]
        public void ICompleteANewSalesOrder()
        {
            SalesOrderPage salesOrderPage = new SalesOrderPage(driver);

            // create sales order
            wait.Until(ExpectedConditions.ElementToBeClickable(salesOrderPage.createOrderBtn));
            salesOrderPage.createOrderBtn.Click();

            // verify sales quote number
            wait.Until(ExpectedConditions.ElementIsVisible(salesOrderPage.orderMgmtDropdown));
            Assert.AreEqual(salesNo, salesOrderPage.salesQuoteNumberInput.GetAttribute("value"), "Sales Quote Number should be " + salesNo);

            // complete sales order 
            salesOrderPage.completeBtn.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(SalesOrderPage.confirmDialog));
            SalesOrderPage.confirmSalesOrderCompletion();

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(SalesOrderPage.btnComplete));
            Assert.AreEqual("COMPLETED", salesOrderPage.orderStatusDisplay.Text, "Sales Order status should be COMPLETED");
        }

        [Then(@"I see the stock information of (.*) is updated")]
        public void ThenISeeTheStockInformationOfCOUCHIsUpdated(String product)
        {
            ProductStockOnHandEnquiry(product);

            StockOnHandEnquiryPage stockOnHandEnquiryPage = new StockOnHandEnquiryPage(driver);
            Assert.Less(stockOnHandEnquiryPage.stockOnHandDtl.Text, prodStockOnHand,
                "The number of stocks on hand should not be greater than " + prodStockOnHand);

            driver.Quit();
        }

        public void ProductStockOnHandEnquiry(String product)
        {
            driver.Navigate().Refresh();

            // navigate to stock on hand enquiry page
            LandingPage landingPage = new LandingPage(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(landingPage.shortcutMenuIcon));
            landingPage.menuIcon.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(LandingPage.stockOnHandEnquiry));
            landingPage.stockOnHandEnquiryMenu.Click();

            StockOnHandEnquiryPage stockOnHandEnquiryPage = new StockOnHandEnquiryPage(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(stockOnHandEnquiryPage.enquiryForm));

            // search product
            stockOnHandEnquiryPage.prodCodeSearchBtn.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(stockOnHandEnquiryPage.prodSearchForm));

            stockOnHandEnquiryPage.productSearchInput.SendKeys(product + Keys.Enter);
            wait.Until(ExpectedConditions.ElementIsVisible(stockOnHandEnquiryPage.prodSearcFirstResult)).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(stockOnHandEnquiryPage.btnRun)).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(StockOnHandEnquiryPage.prodListFirstRow));
        }
    }
}
