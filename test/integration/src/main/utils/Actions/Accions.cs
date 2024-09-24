using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace Appium.Net.Integration.Tests.src.Main.Utils.Actions
{
    public class Accions
    {
        private AndroidDriver _driver;
        private WebDriverWait wait;

        public Accions(AndroidDriver _driver)
        {
            this._driver = _driver;
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // Puedes ajustar el tiempo de espera según sea necesario

        }
        public void ScrollAndClickElementWithText(string text)
        {
            // Utiliza la función de desplazamiento de AndroidUIAutomator para desplazarte hasta el elemento con el texto especificado y hace click
            _driver.FindElement(MobileBy.AndroidUIAutomator($"new UiScrollable(new UiSelector()).scrollTextIntoView(\"{text}\")")).Click();
        }

        public void ScrollIntoViewElement(string text)
        {
            // Utiliza la función de desplazamiento de AndroidUIAutomator para desplazarte hasta el elemento con el texto especificado
            _driver.FindElement(MobileBy.AndroidUIAutomator($"new UiScrollable(new UiSelector()).scrollTextIntoView(\"{text}\")"));

        }
        public string TakeScreenshot()
        {
            // Obtener la ruta del directorio del proyecto
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            // Combinar la ruta del directorio del proyecto con la estructura de carpetas donde deseas guardar las capturas de pantalla
            string screenshotsDirectory = Path.Combine(projectDirectory, "Src", "Test", "Screenshots");

            // Crear el directorio si no existe
            if (!Directory.Exists(screenshotsDirectory))
            {
                Directory.CreateDirectory(screenshotsDirectory);
            }

            // Obtener la marca de tiempo actual
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");

            // Concatenar la marca de tiempo al nombre del archivo
            string filePath = Path.Combine(screenshotsDirectory, $"screenshot-{timestamp}.png");

            // Capturar la captura de pantalla
            Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();

            // Guardar la captura de pantalla en un archivo
            screenshot.SaveAsFile(filePath);

            Console.WriteLine(filePath);

            return filePath;
        }
        public bool IsElementPresent(By locator)
        {
            try
            {
                _driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }
}