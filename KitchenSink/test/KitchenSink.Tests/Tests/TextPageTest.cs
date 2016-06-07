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
    public class TextPageTest : BaseTest
    {
        public TextPageTest(string browser) : base(browser) {}

        [Test]
        public void TextPage_PageLoads() {
            driver.Navigate().GoToUrl(baseURL + "/Text");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("html body puppet-client")));
        }

        [Test]
        public void TextPage_TextPropagationOnUnfocus() {
            driver.Navigate().GoToUrl(baseURL + "/Text");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > input[type='text']")));
            var label = driver.FindElement(By.XPath("(//label[@class='control-label'])[1]"));
            var originalText = label.Text;
            driver.FindElement(By.XPath("(//input)[1]")).Clear();
            driver.FindElement(By.XPath("(//input)[1]")).SendKeys("Marcin");
            driver.FindElement(By.XPath("//body")).ClickUsingMouse(driver);
            wait.Until(x => !label.Text.Equals(originalText));
            Assert.AreEqual("Hi, Marcin!", driver.FindElement(By.XPath("(//label[@class='control-label'])[1]")).Text);
        }

        [Test]
        public void TextPage_TextPropagationWhileTyping() {
            driver.Navigate().GoToUrl(baseURL + "/Text");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > input[type='text']")));
            var label = driver.FindElement(By.XPath("(//label[@class='control-label'])[2]"));
            var originalText = label.Text;
            driver.FindElement(By.XPath("(//input)[2]")).Clear();
            driver.FindElement(By.XPath("(//input)[2]")).SendKeys("M");
            wait.Until(x => !label.Text.Equals(originalText));
            Assert.AreEqual("Hi, M!", driver.FindElement(By.XPath("(//label[@class='control-label'])[2]")).Text);
        }
    }
}
