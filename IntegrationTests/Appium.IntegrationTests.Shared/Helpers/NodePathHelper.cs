using System;
using OpenQA.Selenium;

namespace Appium.IntegrationTests.Shared.Helpers
{
    public class NodePathHelper
    {
        public NodePathHelper()
        {
        }

        public string GetNodePath()
        {
            bool isWindows = Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows);
            bool isMacOS = Platform.CurrentPlatform.IsPlatformType(PlatformType.Mac);
            bool isLinux = Platform.CurrentPlatform.IsPlatformType(PlatformType.Linux);
            var bytes = new byte[] { };
            var path = string.Empty;

            if (isWindows)
            {
                bytes = Properties.Resources.PathToWindowsNode;
            }

            if (isMacOS)
            {
                bytes = Properties.Resources.PathToMacOSNode;
            }

            if (isLinux)
            {
                bytes = Properties.Resources.PathToLinuxNode;
            }

            path = System.Text.Encoding.UTF8.GetString(bytes);

            return path;
        }
    }
}
