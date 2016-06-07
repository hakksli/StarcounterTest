using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KitchenSink.Tests;
using System.Threading;

namespace KitchenSink.Test
{
    [TestFixture("firefox")]
    [TestFixture("chrome")]
    [TestFixture("edge")]
    public class TextareaPageTest : BaseTest
    {
        public TextareaPageTest(string browser) : base(browser) { }

        /// <summary>
        /// TextareaPage_PageLoads is a which loads Textarea page from the Textarea-link
        /// </summary>
        [Test]
        public void TextareaPage_PageLoads()
        {
            driver.Navigate().GoToUrl(baseURL);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.LinkText("Textarea")));

            driver.FindElement(By.LinkText("Textarea")).ClickUsingMouse(driver);
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("textarea.form-control")));

            var element = driver.FindElement(By.XPath("(//textarea[@class='form-control'])[1]"));
            Assert.AreEqual(element.Text, "");
        }


        /// <summary>
        /// TextareaPage_WriteToTextArea tests if it possible to write/read to/from the textarea component
        /// </summary>
        [Test]
        public void TextareaPage_WriteToTextArea()
        {
            driver.Navigate().GoToUrl(baseURL + "/Textarea");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("textarea.form-control")));

            driver.FindElement(By.CssSelector("textarea.form-control")).Clear();

            string setString = "We all love princess cake!";
            driver.FindElement(By.CssSelector("textarea.form-control")).SendKeys(setString);

            string actualString = driver.FindElement(By.CssSelector("textarea.form-control")).GetAttribute("value");

            Assert.AreEqual(setString, actualString);
        }

        /// <summary>
        /// TextareaPage_CounterPropagationWhileTyping tests if the label continuously updates as the textarea changes
        /// </summary>
        [Test]
        public void TextareaPage_CounterPropagationWhileTyping()
        {
            driver.Navigate().GoToUrl(baseURL + "/Textarea");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > textarea[rows='3']")));

            var label = driver.FindElement(By.XPath("(//label[@class='control-label'])[1]"));
            var originalText = label.Text;

            driver.FindElement(By.XPath("(//textarea)[1]")).Clear();
            Assert.AreEqual("Length of your bio: 0 chars", originalText);

            driver.FindElement(By.XPath("(//textarea)[1]")).SendKeys("U");
            wait.Until(x => !label.Text.Equals(originalText));

            string actualString = driver.FindElement(By.XPath("(//label[@class='control-label'])[1]")).Text;
            Assert.AreEqual("Length of your bio: 1 chars", actualString);
        }

    }
}
