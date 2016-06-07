using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KitchenSink.Tests;

namespace KitchenSink.Test {

    [TestFixture("firefox")]
    [TestFixture("chrome")]
    [TestFixture("edge")]
    //[TestFixture("internet explorer")]
    public class CheckboxPageTest : BaseTest
    {
        private string canDrive = "You can drive";
        private string cantDrive = "You can't drive";

        public CheckboxPageTest(string browser) : base(browser) {}

        [Test]
        public void CheckboxPage_PageLoads() {
            driver.Navigate().GoToUrl(baseURL);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.LinkText("Checkbox")));
            driver.FindElement(By.LinkText("Checkbox")).ClickUsingMouse(driver);
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > input[type='checkbox']")));
            var element = driver.FindElement(By.XPath("(//input)[1]"));
            Assert.AreEqual(element.GetAttribute("type"), "checkbox");
        }

        [Test]
        public void CheckboxPage_CheckboxUnchecked()
        {
            driver.Navigate().GoToUrl(baseURL + "/Checkbox");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > input[type='checkbox']")));
            var element = driver.FindElement(By.XPath("(//input)[1]"));
            Assert.AreEqual(element.GetAttribute("type"), "checkbox");
            element.ClickUsingMouse(driver);
            wait.Until(x => !canDrive.Equals(driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text));
            var uncheckedText = driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text;
            Assert.AreEqual(cantDrive, uncheckedText);
        }

        [Test]
        public void CheckboxPage_CheckboxUncheckedAndCheckedAgain()
        {
            driver.Navigate().GoToUrl(baseURL + "/Checkbox");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > input[type='checkbox']")));
            var element = driver.FindElement(By.XPath("(//input)[1]"));
            Assert.AreEqual(element.GetAttribute("type"), "checkbox");
            element.ClickUsingMouse(driver);
            wait.Until(x => !canDrive.Equals(driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text));
            var uncheckedText = driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text;
            Assert.AreEqual(cantDrive, uncheckedText);
            element.ClickUsingMouse(driver);
            wait.Until(x => !cantDrive.Equals(driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text));
            var checkedText = driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text;
            Assert.AreEqual(canDrive, checkedText);
        }
    }
}
