using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App1
{
    public interface IPlatformAppearance
    {
        PlatformAppearanceMode GetPlatformAppearanceMode();
    }

    public enum PlatformAppearanceMode
    {
        Default,
        Dark
    }

    public static class PlatformAppearanceManager
    {
        private static readonly object _lock = new object();

        private static readonly Lazy<Tuple<ResourceDictionary, ResourceDictionary>> DarkTheme = new Lazy<Tuple<ResourceDictionary, ResourceDictionary>>(PreloadDarkTheme, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        private static readonly Lazy<Tuple<ResourceDictionary, ResourceDictionary>> LightTheme = new Lazy<Tuple<ResourceDictionary, ResourceDictionary>>(PreloadLightTheme, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        public static void SetTheme(PlatformAppearanceMode platformAppearanceMode)
        {
            lock (_lock)
            {
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                if (mergedDictionaries != null)
                {
                    if (platformAppearanceMode == PlatformAppearanceMode.Dark)
                    {
                        mergedDictionaries.Add(DarkTheme.Value.Item1);
                        mergedDictionaries.Remove(LightTheme.Value.Item1);
                    }
                    else if (platformAppearanceMode == PlatformAppearanceMode.Default)
                    {
                        mergedDictionaries.Add(LightTheme.Value.Item1);
                        mergedDictionaries.Remove(DarkTheme.Value.Item1);
                    }
                } 
            }
        }

        private static Tuple<ResourceDictionary, ResourceDictionary> PreloadDarkTheme() => PreloadTheme(new Themes.Dark());

        private static Tuple<ResourceDictionary, ResourceDictionary> PreloadLightTheme() => PreloadTheme(new Themes.Light());

        private static Tuple<ResourceDictionary, ResourceDictionary> PreloadTheme(ResourceDictionary theme) => Tuple.Create(theme, (ResourceDictionary)null);
    }
}
