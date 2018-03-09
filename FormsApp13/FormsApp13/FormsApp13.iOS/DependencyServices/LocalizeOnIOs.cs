using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using UIKit;

using FormsApp13.DependencyServices;
using FormsApp13.Utilities;
using Xamarin.Forms;

[assembly:Dependency(typeof(FormsApp13.iOS.DependencyServices.LocalizeOnIOs))]
namespace FormsApp13.iOS.DependencyServices
{
    public class LocalizeOnIOs : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            string dotNetLanguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                string preferredLanguage = NSLocale.PreferredLanguages[0];
                dotNetLanguage = this.IOsToDotNetLanguage(preferredLanguage);
            }

            //# 좋은방법은 아닌거 같다...
            //# TODO: iOS의 Language, Locale코드로 .NET의 CultureInfo를 생성하는 더 좋은 방법에 대해 검토
            CultureInfo ci;
            try
            {
                ci = new CultureInfo(dotNetLanguage);
            }
            catch (CultureNotFoundException)
            {
                try
                {
                    string fallbackLanguage = this.ToDotNetFallbackLanguage(new PlatformCulture(dotNetLanguage));
                    ci = new CultureInfo(fallbackLanguage);
                }
                catch (CultureNotFoundException)
                {
                    ci = new CultureInfo("en");
                }
            }
            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        private string IOsToDotNetLanguage(string iOSLanguage)
        {
            string dotNetLanguage = iOSLanguage;
            switch (iOSLanguage)
            {
                case "ms-MY":   //# 'Malaysian (Malaysia)' not supported .NET culture
                case "ms-SG":   //# 'Malaysian (Singapore)' not supported .NET culture
                    dotNetLanguage = "ms";
                    break;
                case "gsw-CH":  //# 'Schwiizertüütsch (Swiss German)' not supported .NET culture
                    dotNetLanguage = "de-CH";
                    break;
            }
            return dotNetLanguage;
        }

        private string ToDotNetFallbackLanguage(PlatformCulture platformCulture)
        {
            string dotNetLanguage = platformCulture.LanguageCode;
            switch (platformCulture.LanguageCode)
            {
                case "pt":
                    dotNetLanguage = "pt-PT";
                    break;
                case "gsw":
                    dotNetLanguage = "de-CH";
                    break;
            }
            return dotNetLanguage;
        }
    }
}