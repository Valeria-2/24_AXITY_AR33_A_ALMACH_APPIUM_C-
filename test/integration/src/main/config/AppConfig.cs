using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium;


namespace Appium.Net.Integration.Tests.src.Main.Config
{
    public class AppConfig
    {
        //adb shell pm list packages | findstr "generalstore"
        public const string GeneralStoreAppPackage = "com.androidsample.generalstore";

        //adb shell dumpsys package com.androidsample.generalstore | findstr /i "activity"
        public const string GeneralStoreMainActivity = ".SplashActivity";

        public const string OS = "Android";
        public const string NameDevice = "emulator-5554";

        public static AppiumOptions GetAndroidCapabilities()
        {
            var appiumOptions = new AppiumOptions()
            {
                AutomationName = AutomationName.AndroidUIAutomator2,
                //Tipo de OS
                PlatformName = OS,
                //Nombre del emulador o dispostivo
                DeviceName = NameDevice
            };
            appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.NoReset, true);

            return appiumOptions;
        }
    }
}

