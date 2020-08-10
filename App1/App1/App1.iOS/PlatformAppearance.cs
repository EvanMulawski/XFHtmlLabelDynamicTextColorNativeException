using System;
using System.Linq;
using UIKit;

namespace App1.iOS
{
    public class PlatformAppearance : IPlatformAppearance
    {
        public PlatformAppearanceMode GetPlatformAppearanceMode()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(12, 0))
            {
                var currentUIViewController = GetVisibleViewController();

                var userInterfaceStyle = currentUIViewController.TraitCollection.UserInterfaceStyle;

                switch (userInterfaceStyle)
                {
                    case UIUserInterfaceStyle.Light:
                        return PlatformAppearanceMode.Default;
                    case UIUserInterfaceStyle.Dark:
                        return PlatformAppearanceMode.Dark;
                    default:
                        throw new NotSupportedException($"UIUserInterfaceStyle {userInterfaceStyle} not supported");
                }
            }
            else
            {
                return PlatformAppearanceMode.Default;
            }
        }

        private static UIViewController GetVisibleViewController()
        {
            UIViewController viewController = null;

            var window = UIApplication.SharedApplication.KeyWindow;

            if (window.WindowLevel == UIWindowLevel.Normal)
                viewController = window.RootViewController;

            if (viewController is null)
            {
                window = UIApplication.SharedApplication
                    .Windows
                    .OrderByDescending(w => w.WindowLevel)
                    .FirstOrDefault(w => w.RootViewController != null && w.WindowLevel == UIWindowLevel.Normal);

                viewController = window?.RootViewController ?? throw new InvalidOperationException("Could not find current view controller.");
            }

            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;

            return viewController;
        }
    }
}