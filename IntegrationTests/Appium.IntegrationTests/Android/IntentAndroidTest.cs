﻿using Appium.IntegrationTests.Shared.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.IntegrationTests.Android
{
    class IntentAndroidTest
    {
        private AndroidDriver<AppiumWebElement> driver;

        [SetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Env.isSauce() ?
                Caps.getAndroid501Caps(Apps.get("intentApp")) :
                Caps.getAndroid19Caps(Apps.get("intentApp"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "android - complex");
                capabilities.SetCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
        }

        [Test] 
        [Category("Android")]
        public void StartActivityWithDefaultIntentAndDefaultCategoryWithOptionalArgs()
        {
            driver.StartActivityWithIntent("com.prgguru.android", ".GreetingActivity", "android.intent.action.MAIN", null, null,
                "android.intent.category.DEFAULT", "0x4000000",
                "--es \"USERNAME\" \"AppiumIntentTest\" -t \"text/plain\"");
            Assert.AreEqual(driver.FindElementById("com.prgguru.android:id/textView1").Text,
                "Welcome AppiumIntentTest");
        }

        [TearDown]
        public void AfterAll()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

    }
}
