﻿using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace Appium.Integration.Tests.Android
{
    public class AndroidSettingTest
    {
        private AndroidDriver<AppiumWebElement> driver;

        [SetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Env.isSauce() ?
                Caps.getAndroid501Caps(Apps.get("androidApiDemos")) :
                Caps.getAndroid19Caps(Apps.get("androidApiDemos"));
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
            driver.CloseApp();
        }

        [Test]
        [Category("Android")]
        public void IgnoreUnimportantViewsTest()
        {
            driver.IgnoreUnimportantViews(true);
            bool ignoreViews =
                    (bool) driver.Settings[AutomatorSetting.IgnoreUnimportantViews];
            Assert.True(ignoreViews);
            driver.IgnoreUnimportantViews(false);
            ignoreViews = (bool)driver.Settings[AutomatorSetting.IgnoreUnimportantViews];
            Assert.False(ignoreViews);
        }

        [Test]
        [Category("Android")]
        public void ConfiguratorTest()
        {
            driver.ConfiguratorSetActionAcknowledgmentTimeout(500);
            driver.ConfiguratorSetKeyInjectionDelay(400);
            driver.ConfiguratorSetScrollAcknowledgmentTimeout(300);
            driver.ConfiguratorSetWaitForIdleTimeout(600);
            driver.ConfiguratorSetWaitForSelectorTimeout(1000);

            Dictionary<string, object> settings = driver.Settings;
            Assert.AreEqual(settings[AutomatorSetting.KeyInjectionDelay], 400);
            Assert.AreEqual(settings[AutomatorSetting.WaitActionAcknowledgmentTimeout], 500);
            Assert.AreEqual(settings[AutomatorSetting.WaitForIDLETimeout], 600);
            Assert.AreEqual(settings[AutomatorSetting.WaitForSelectorTimeout], 1000);
            Assert.AreEqual(settings[AutomatorSetting.WaitScrollAcknowledgmentTimeout], 300);
        }

        [Test]
        [Category("Android")]
        public void ConfiguratorPropertyTest()
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {[AutomatorSetting.KeyInjectionDelay] = 1500,
                [AutomatorSetting.WaitActionAcknowledgmentTimeout] = 2500,
                [AutomatorSetting.WaitForIDLETimeout] = 3500,
                [AutomatorSetting.WaitForSelectorTimeout] = 5000,
                [AutomatorSetting.WaitScrollAcknowledgmentTimeout] = 7000
            };

            driver.Settings = data;
            Dictionary<string, object> settings = driver.Settings;
            Assert.AreEqual(settings[AutomatorSetting.KeyInjectionDelay], 1500);
            Assert.AreEqual(settings[AutomatorSetting.WaitActionAcknowledgmentTimeout], 2500);
            Assert.AreEqual(settings[AutomatorSetting.WaitForIDLETimeout], 3500);
            Assert.AreEqual(settings[AutomatorSetting.WaitForSelectorTimeout], 5000);
            Assert.AreEqual(settings[AutomatorSetting.WaitScrollAcknowledgmentTimeout], 7000);
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
