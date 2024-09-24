using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace Appium.Net.Integration.Tests.src.Main.Pages
{
    public class HomePage
    {
        private AndroidDriver _driver;
        private WebDriverWait wait;

        // Localizadores de elementos
        public By homescreen = MobileBy.AccessibilityId("Home");
   



        public HomePage(AndroidDriver _driver)
        {
            this._driver = _driver;
            this.wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        }



    }
}
