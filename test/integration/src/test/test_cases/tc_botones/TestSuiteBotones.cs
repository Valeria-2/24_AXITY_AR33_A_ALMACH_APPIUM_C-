using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Appium.Net.Integration.Tests.src.Main.Core;
using Appium.Net.Integration.Tests.src.Main.Pages;
using iText.Licensing.Base.Reporting.Volume;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Appium.Net.Integration.Tests.src.test.test_cases.tc_botones
{
    internal class TestSuiteBotones : BaseTest
    {

        [Test]
        public void TestBotonGeneroFemenino()
        {
            //ARRANGE
            _driver.StartActivity(GeneralStoreAppPackage, GeneralStoreMainActivity);
            InfoPage infoPage = new(_driver);

            //ACT
            infoPage.EnterGenderFemenino();
           
            //ASSERT
            bool generoFemeninoSeleccionado = infoPage.IsGenderFemeninoSelected();
            Assert.That(infoPage.IsGenderFemeninoSelected(), Is.True, "El género femenino no está seleccionado correctamente.");

        }

        [Test]
        public void TestBotonShop()
        {
            //ARRANGE
            _driver.StartActivity(GeneralStoreAppPackage, GeneralStoreMainActivity);
            InfoPage infoPage = new(_driver);
            ProductosPage productosPage = new(_driver);

            //ACT
            infoPage.EnterUsername("User1");
            infoPage.ClickBotonShop();

            //ASSERT
            Assert.That(_driver.FindElement(productosPage.botoncarrito).Displayed, Is.EqualTo(true));

        }
        [Test]
        public void TestListaPaisDisplayed()
        {
            //ARRRANGE
            _driver.StartActivity(GeneralStoreAppPackage, GeneralStoreMainActivity);
            InfoPage infoPage = new(_driver);

            //ACT
            infoPage.ClickListaPais();

            //ASSERT
            Assert.That(_driver.FindElement(infoPage.algeria).Displayed, Is.EqualTo(true));

        }
       
    }
}
