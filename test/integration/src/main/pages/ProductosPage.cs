using System;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Appium.Net.Integration.Tests.src.Main.Utils.Actions;

namespace Appium.Net.Integration.Tests.src.Main.Pages
{
    public class ProductosPage
    {
        private AndroidDriver _driver;
        private WebDriverWait wait;


        // Localizadores de elementos
        public By elements = By.Id("com.androidsample.generalstore:id/productName");
        public By addcarrito = By.Id("com.androidsample.generalstore:id/productAddCart");
        public By botoncarrito = By.Id("com.androidsample.generalstore:id/appbar_btn_cart");
        public By airjJordan1midse = By.XPath("//android.widget.TextView[@resource-id=\"com.androidsample.generalstore:id/productName\" and @text=\"Air Jordan 1 Mid SE\"]");

        // Constructor
        public ProductosPage(AndroidDriver _driver)
        {
            this._driver = _driver;
            this.wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));


        }

        public void BuscarTenis(string tenis)
        {
           
            Accions _accions = new Accions(_driver); // Agregar una instancia de la clase Accions
            _accions.ScrollAndClickElementWithText(tenis);

        }

        public void SeleccionarTenis(string tenis)
        {


            Accions _accions = new Accions(_driver); // Agregar una instancia de la clase Accions
            _accions.ScrollAndClickElementWithText(tenis);

            var elementos = _driver.FindElements(elements);
            int i = 0;

           
            foreach (var element in elementos)
            {

                // Realizar acciones con cada elemento, por ejemplo, imprimir el texto
                Console.WriteLine(element.Text);
                if (element.Text == tenis)
                {
                    
                    var adding = _driver.FindElements(addcarrito);
                    adding[i].Click();
                }
                i++;
            }

        }

        public void ClickBotonCarrito()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(botoncarrito));

            _driver.FindElement(botoncarrito).Click();
        }

        public void FullPage(string tenis) 
        {
            SeleccionarTenis(tenis);
            ClickBotonCarrito();

        }

        public string GetTenisSeleccionado(By selectortenis)
        {
           
            var elementoTenisSeleccionado = wait.Until(ExpectedConditions.ElementIsVisible(selectortenis));
            string tenisSeleccionado = elementoTenisSeleccionado.Text;
            return tenisSeleccionado;
        }
    }
}
