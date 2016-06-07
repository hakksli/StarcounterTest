using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace KitchenSink.Tests {
    public static class SeleniumExtensions {
        /// <summary>
        /// Click on the element using Actions API. 'Click' method is known to not work in same strange cases on Edge,
        /// Use this method instead
        /// </summary>
        public static void ClickUsingMouse(this IWebElement element, IWebDriver driver) {
            new Actions(driver).Click(element).Build().Perform();
        }
    }
}