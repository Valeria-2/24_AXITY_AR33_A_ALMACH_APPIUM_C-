using System;
using System.Collections.Generic;
using Appium.Net.Integration.Tests.src.Main.Pages;
using Appium.Net.Integration.Tests.src.Main.Utils.Actions;
using Appium.Net.Integration.Tests.src.Test.TestReport.Models;
using Appium.Net.Integration.Tests.src.Test.TestReport.Utility;
using Appium.Net.Integration.Tests.src.Test.TestReport;
using Appium.Net.Integration.Tests.Src.Test.TestReport;
using NUnit.Framework;
using OpenQA.Selenium;
using Appium.Net.Integration.Tests.src.Main.Core;
using System.IO;

namespace Appium.Net.Integration.Tests.src.test.test_cases.tc_texto
{
    internal class TestSuiteText: BaseTest
    {
        [Test]
        public void TestEscribiendoEvidencia()
        {
            // arrange
            InfoPage infoPage = new(_driver);
            Accions accions = new(_driver);
            HomePage homePage = new(_driver);

            var paso1 = new ReportModel();
            paso1.NombreProyecto = "Oxxo HERE";
            paso1.InicioEjecuccion = DateTime.Now;
            paso1.StatusEjecuccion = "Pasado";
            paso1.Precondiciones = "Se debe de usar la version más actualizada de Android 13";
            paso1.NombreCasoPrueba = TestContext.CurrentContext.Test.MethodName;
            paso1.DescCasoPrueba = "Se debe de verificar que la entrada del campo sea visible para el usuario";
            paso1.NombreScript = Path.GetFileName(TestContext.CurrentContext.Test.ClassName);
            var isTestPassing = true;

            //ACT

            //PASO 1
            paso1.DescPaso = "Paso 1: Estar en la pantalla de HOME";
            paso1.DescResult = "Resultado 1: La pantalla de HOME se muestra correctamente";
            paso1.EstatusPaso = "Pasado";
            try
            {
                paso1.InputRoute = accions.TakeScreenshot();
                _driver.FindElement(homePage.homescreen);
                Assert.That(_driver.FindElement(homePage.homescreen).Displayed, Is.EqualTo(true));

            }
            catch (NoSuchElementException)
            {
                paso1.InputRoute = accions.TakeScreenshot();
                paso1.EstatusPaso = "Fallado";
                paso1.StatusEjecuccion = "Fallado";
                isTestPassing = false;
            }

            //Paso 2
            var paso2 = new ReportModel();
            paso2.DescPaso = "Paso 2: Abrir General Store App";
            paso2.DescResult = "Resultado 2: La General Store App se abre sin errores\"";
            paso2.EstatusPaso = "Pasado";
            paso2.InputRoute = string.Empty;

            try
            {
                paso2.EstatusPaso = isTestPassing ? "Pasado" : "No ejecutado";

                if (isTestPassing)
                {
                    _driver.StartActivity(GeneralStoreAppPackage, GeneralStoreMainActivity);
                    Assert.That(_driver.FindElement(infoPage.botonShop).Displayed, Is.EqualTo(true));
                    paso2.InputRoute = accions.TakeScreenshot();

                }
            }
            catch (NoSuchElementException)
            {
                paso2.EstatusPaso = "Fallado";
                paso1.StatusEjecuccion = "Fallado";
                paso2.InputRoute = accions.TakeScreenshot();
                isTestPassing = false;
            }

            //PASO 3
            var paso3 = new ReportModel();
            paso3.DescPaso = "Paso 3: Seleccionar la barra del nombre de usuario";
            paso3.DescResult = "Resultado 3: La barra del nombre de usuario se selecciona correctamente";
            paso3.EstatusPaso = "Pasado";
            paso3.InputRoute = string.Empty;

            try
            {
                paso3.EstatusPaso = isTestPassing ? "Pasado" : "No ejecutado";
                if (isTestPassing)
                {
                    _driver.FindElement(infoPage.name).Click();

                    if (_driver.IsKeyboardShown())
                    {
                        paso3.InputRoute = accions.TakeScreenshot();

                    }
                    else
                    {
                        throw new Exception("No se hizco click en la barra del nombre");
                    }
                }

            }
            catch (Exception ex)
            {
                paso3.InputRoute = accions.TakeScreenshot();
                paso3.EstatusPaso = "Fallado";
                paso1.StatusEjecuccion = "Fallado";
                isTestPassing = false;
            }

            //Bloque 4 - PASO 4
            var paso4 = new ReportModel();
            paso4.DescPaso = "Paso 4: Verificar que el nombre se haya escrito correctamente";
            paso4.DescResult = "Resultado 4: El nombre Eduardo Sánchez García se ha escrito correctamente";
            paso4.EstatusPaso = "Pasado";
            paso4.InputRoute = string.Empty;

            try
            {
                paso4.EstatusPaso = isTestPassing ? "Pasado" : "No ejecutado";
                if (isTestPassing)
                {
                    _driver.FindElement(infoPage.name).SendKeys("Eduardo  Garcia");

                    string textoIngresado = _driver.FindElement(infoPage.name).GetAttribute("text");
                    string textoEsperado = "Eduardo Sanchez Garcia";

                    Assert.That(textoIngresado, Is.EqualTo(textoEsperado), "El texto no es el mismo");

                    paso4.InputRoute = accions.TakeScreenshot();

                }
            }
            catch (Exception)
            {
                paso4.EstatusPaso = "Fallado";
                paso4.InputRoute = accions.TakeScreenshot();
                paso1.StatusEjecuccion = "Fallado";
            }

            // Mi ruta donde guardado mi archivo CSV
            string filePath = paso1.NombreCasoPrueba + "_datos.csv";

            paso1.FinEjecuccion = DateTime.Now;
            var tiempoEjecuccion = DateTime.Now - paso1.InicioEjecuccion;
            paso1.TiempoEjecuccion = $"{tiempoEjecuccion.Minutes:00}:{tiempoEjecuccion.Seconds:00}";


            var listPasos = new List<ReportModel> { paso1, paso2, paso3, paso4 };

            var csvWriter = new CreateCSV();
            csvWriter.CreatFile(filePath, listPasos);

            var report = new Createreport();
            var reportName = string.Format(ReportConstant.NombreReporte, paso1.NombreCasoPrueba, "-", DateTime.Now.ToString("ddMMyyyy"));
            var reportROute = report.CreateReport(filePath, reportName);
            TestContext.AddTestAttachment(reportROute);

        }
    }
}
