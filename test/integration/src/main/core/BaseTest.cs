using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using Appium.Net.Integration.Tests.src.Main.Config;

namespace Appium.Net.Integration.Tests.src.Main.Core
{
    public class BaseTest:AppConfig
    {
        
        public AndroidDriver _driver;

        [SetUp]
        public void SetUp()
        {

            // Obtener la URI del servidor de Appium desde la clase ServerConfig
            var appiumServerUri = Config.ServerConfig.GetAppiumServerUri();

            // Iniciar el servidor de Appium desde la clase ServerConfig
            Config.ServerConfig.StartAppiumServer();

            //Capabilities de la Clase AppConfig
            var capabilities = AppConfig.GetAndroidCapabilities();

            // Inicialización del controlador de Android con las opciones configuradas

            _driver = new AndroidDriver (appiumServerUri, capabilities, TimeSpan.FromSeconds(180));
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    

        [TearDown]
        public void TearDown()
        {
            //Finalizando mis pruebas
            //Metodo para cerrar la app
            _driver.TerminateApp(GeneralStoreAppPackage);
            _driver.Dispose();

        }
    }
}
