using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace KitchenSink.Tests.Tests {

    [TestFixture("firefox")]
    [TestFixture("chrome")]
    [TestFixture("edge")]
    public class RedirectPageTest : BaseTest
    {
        private WebDriverWait _wait;

        public RedirectPageTest(string browser) : base(browser) {
        }

        [SetUp]
        public void Setup() {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(baseURL + "/Redirect");
            _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//button[text()='Fruit']")));
        }

        [Test]
        public void ClickingOnFruitShouldChangeUrlAndText() {
            ClickButton("Fruit");

            // on edge juicy sometimes messes up the dom tree, so you can't be sure about its relative position to button
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[text()=\"You've got some tasty apple\"]")));
            Assert.That(driver.Url, Is.EqualTo($"{baseURL}/Redirect/apple"));
        }

        [Test]
        public void ClickingOnRedirectToAnotherPartialShouldChangeUrl() {
            ClickButton("Morph to another partial");

            // redirecting can take some time
            _wait.Until(ExpectedConditions.UrlContains(baseURL));
        }

        [Test]
        public void ClickingOnRedirectToDocsShouldChangeUrl() {
            ClickButton("Redirect to Starcounter.io");

            // see https://github.com/PuppetJs/puppet-redirect/issues/3
            if (browser == "firefox") {
                // depending on wheter or not Launcher will be present, the dialog will differ
                _wait.Until(d => WaitForNoConnectionAndDismiss(d) || d.FindElements(By.XPath("//h4[text()='Connection error']")).Count != 0);
            }

            // redirecting can take some time
            _wait.Until(ExpectedConditions.UrlContains("http://starcounter.io/"));
        }

        private bool WaitForNoConnectionAndDismiss(IWebDriver d) {
            try {
                d.SwitchTo().Alert().Dismiss();
                return true;
            } catch (NoAlertPresentException) {
                return false;
            }
        }

        private void ClickButton(string text) {
            driver.FindElement(By.XPath($"//button[text()='{text}']")).ClickUsingMouse(driver);
        }
    }
}
