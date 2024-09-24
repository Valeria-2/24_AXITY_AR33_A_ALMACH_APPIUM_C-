using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appium.Net.Integration.Tests.src.Main.Core;
using Appium.Net.Integration.Tests.src.Main.Pages;
using Appium.Net.Integration.Tests.src.Main.Utils.Actions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Appium.Net.Integration.Tests.src.test.test_cases.tc_end_to_end
{
    internal class TestSuiteE2E : BaseTest
    {
        [Test]
        public void TestE2E()
        {
            //ARRANGE
            _driver.StartActivity(GeneralStoreAppPackage, GeneralStoreMainActivity);
            InfoPage infoPage = new(_driver);
            ProductosPage productosPage = new(_driver);
            ConfirmacionPage confirmacionPage = new(_driver);
            Accions accions = new(_driver);

            //ACT
            infoPage.FullPage("Argentina", "Mau");
            productosPage.FullPage("Air Jordan 1 Mid SE");
            confirmacionPage.ClickCheckBox();
            confirmacionPage.ClickCompleteboton();
            accions.TakeScreenshot();

            //ASSERT
            Assert.That(_driver.FindElement(By.Id("android:id/content")).Displayed, Is.EqualTo(true));
        }

       
    }
}
