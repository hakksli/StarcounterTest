﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace KitchenSink.Tests {
    class WebDriverFactory {
        private static readonly Uri RemoteWebDriverUri = new Uri("http://127.0.0.1:4444/wd/hub");

        public static IWebDriver Create(string browser) {
            DesiredCapabilities capabilities;
            IWebDriver driver;

            switch (browser) {
                case "chrome":
                    capabilities = DesiredCapabilities.Chrome();
                    driver = new RemoteWebDriver(RemoteWebDriverUri, capabilities);
                    break;
                case "internet explorer":
                    InternetExplorerOptions options = new InternetExplorerOptions();
                    options.IgnoreZoomLevel = true;
                    capabilities = (DesiredCapabilities)options.ToCapabilities();
                    driver = new RemoteWebDriver(RemoteWebDriverUri, capabilities, TimeSpan.FromSeconds(10));
                    break;
                case "edge":
                    capabilities = DesiredCapabilities.Edge();
                    driver = new RemoteWebDriver(RemoteWebDriverUri, capabilities);
                    break;
                default:
                    capabilities = DesiredCapabilities.Firefox();
                    driver = new RemoteWebDriver(RemoteWebDriverUri, capabilities);
                    break;
            }

            return driver;
        }
    }
}
