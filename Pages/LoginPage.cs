using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UnleashedTestProject.Pages
{
    class LoginPage
    {
        private static IWebDriver driver;
        private static WebDriverWait wait;

        public IWebElement username => driver.FindElement(By.Id("username"));
        public IWebElement password => driver.FindElement(By.Id("password"));
        public IWebElement loginBtn => driver.FindElement(By.Id("btnLogOn"));

        public By accountDropdown => By.ClassName("account-dropdown");

        public LoginPage(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public static void SkipAuthentication()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#mfa > div > a"))).Click();
        }

        public static void SkipTour()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'End tour')]"))).Click();
        }
    }
}
