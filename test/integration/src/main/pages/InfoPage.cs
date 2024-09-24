using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Appium.Net.Integration.Tests.src.Main.Utils.Actions;

namespace Appium.Net.Integration.Tests.src.Main.Pages

{
    public class InfoPage
    {
        
        private AndroidDriver _driver;
        private WebDriverWait wait;

       

        // Localizadores de elementos
        public By name = By.Id("com.androidsample.generalstore:id/nameField");
        public By femenino = By.Id("com.androidsample.generalstore:id/radioFemale");
        public By botonShop = MobileBy.Id("com.androidsample.generalstore:id/btnLetsShop");
        public By listapais = By.Id("android:id/text1");
        public By algeria = By.XPath("//android.widget.TextView[@resource-id=\"android:id/text1\" and @text=\"Algeria\"]");

        // Constructor
        public InfoPage(AndroidDriver _driver)
        {
            this._driver = _driver;
            this.wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
          

        }

        public void EnterUsername(string username)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(name));

            _driver.FindElement(name).SendKeys(username);

        }

        public void EnterGenderFemenino()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(femenino));

            _driver.FindElement(femenino).Click();

        }

        public void ClickBotonShop()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(botonShop));

            _driver.FindElement(botonShop).Click();
        }

        public void ClickListaPais()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(listapais));

            _driver.FindElement(listapais).Click();
        }

        public void SeleccionarPais(string pais)
        {
            ClickListaPais();

            Accions _accions = new Accions(_driver); // Agregar una instancia de la clase Accions
            _accions.ScrollAndClickElementWithText(pais);

            //_driver.FindElement(MobileBy.AndroidUIAutomator($"new UiScrollable(new UiSelector()).scrollTextIntoView(\"{pais}\")")).Click();
        }

        public string TextoPais(string pais)
        {
            var k =_driver.FindElement(MobileBy.AndroidUIAutomator($"new UiScrollable(new UiSelector()).scrollTextIntoView(\"{pais}\")"));
            string paistexto = k.Text;

            Console.WriteLine(paistexto);

            return paistexto;


        }
        public void FullPage(string pais, string name) 
        {
            SeleccionarPais(pais);
            EnterUsername(name);
            _driver.HideKeyboard();
            EnterGenderFemenino();
            ClickBotonShop();
        }
        public bool IsGenderFemeninoSelected()
        {
            return _driver.FindElement(femenino).Selected;
        }

    }
}
