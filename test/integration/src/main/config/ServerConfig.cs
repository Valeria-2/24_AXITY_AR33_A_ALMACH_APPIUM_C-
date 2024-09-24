using OpenQA.Selenium.Appium.Service;
using System;

namespace Appium.Net.Integration.Tests.src.Main.Config
{
    public class ServerConfig
    {
        public static Uri GetAppiumServerUri()
        {
            // Obtener la dirección del servidor de Appium desde la variable de entorno "APPIUM_HOST" o utilizar la dirección predeterminada "http://127.0.0.1:4723/"
            var appiumHost = Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/";
            return new Uri(appiumHost);
        }

        public static AppiumLocalService StartAppiumServer(int port = 4723, string ipAddress = "127.0.0.1")
        {
            // Configurar y comenzar un servicio local de Appium en el puerto 4723 y en la dirección IP local "127.0.0.1"
            var service = new AppiumServiceBuilder().UsingPort(port).WithIPAddress(ipAddress).Build();
            service.Start();
            return service;
        }
    }
}