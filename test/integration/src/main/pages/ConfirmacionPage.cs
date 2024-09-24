using System;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Appium.Net.Integration.Tests.src.Main.Pages
{
    public class ConfirmacionPage
    {
        private AndroidDriver _driver;
        private WebDriverWait wait;



        // Localizadores de elementos
        private By checkbox = By.ClassName("android.widget.CheckBox");
        private By completeboton = By.Id("com.androidsample.generalstore:id/btnProceed");


        // Constructor
        public ConfirmacionPage(AndroidDriver _driver)
        {
            this._driver = _driver;
            this.wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        }

        public void ClickCheckBox()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(checkbox));

            _driver.FindElement(checkbox).Click();

        }

        public void ClickCompleteboton() 
        {
            wait.Until(ExpectedConditions.ElementIsVisible(completeboton));

            _driver.FindElement(completeboton).Click();

        }
       
    }
}
