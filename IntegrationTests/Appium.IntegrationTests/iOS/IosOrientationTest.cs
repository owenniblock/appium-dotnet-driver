﻿using Appium.IntegrationTests.Shared.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.IntegrationTests.iOS
{
 	[TestFixture ()]
    public class iOSOrientationTest
	{
		private IWebDriver driver;

		[SetUp]
		public void beforeAll(){
			DesiredCapabilities capabilities = Caps.getIos92Caps (Apps.get("iosTestApp")); 
			if (Env.isSauce ()) {
				capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME")); 
				capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
				capabilities.SetCapability("name", "ios - complex");
				capabilities.SetCapability("tags", new string[]{"sample"});
			}
			Uri serverUri = Env.isSauce () ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);	
			driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
		}

		[TearDown]
		public void afterAll(){
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void OrientationTest()
        {
            IRotatable rotatable =  ((IRotatable) driver);
            rotatable.Orientation = ScreenOrientation.Landscape;
            Assert.AreEqual(ScreenOrientation.Landscape, rotatable.Orientation);
        }
    }
}
