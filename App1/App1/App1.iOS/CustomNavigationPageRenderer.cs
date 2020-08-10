using App1.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRenderer))]
namespace App1.iOS
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (TraitCollection.UserInterfaceStyle != previousTraitCollection.UserInterfaceStyle)
            {
                PlatformAppearanceManager.SetTheme(TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark ? PlatformAppearanceMode.Dark : PlatformAppearanceMode.Default);
            }
        }
    }
}