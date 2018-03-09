using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using FormsApp13.DependencyServices;
using FormsApp13.Utilities;
using Java.Util;

[assembly:Dependency(typeof(FormsApp13.Droid.DependencyServices.LocalizeOnAndroid))]
namespace FormsApp13.Droid.DependencyServices
{
    public class LocalizeOnAndroid : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var nativeLocale = Locale.Default;
            string dotNetLanguage = this.NativeToDotNetLanguage(nativeLocale.ToString().Replace('_', '-'));

            //# TODO: Android의 Language, Locale코드로 .NET의 CultureInfo를 생성하는 더 좋은 방법에 대해 검토
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

        private string NativeToDotNetLanguage(string nativeLanguage)
        {
            string dotNetLanguage = nativeLanguage;
            switch (nativeLanguage)
            {
                case "ms-BN":   // 'Malaysian (Brunei)' not supported .NET culture
                case "ms-MY":   // 'Malaysian (Malaysia)' not supported .NET culture
                case "ms-SG":   // 'Malaysian (Singapore)' not supported .NET culture
                    dotNetLanguage = "ms";
                    break;
                case "in-ID":  // 'Indonesian (Indonesia)' has different code in .NET
                    dotNetLanguage = "id-ID";
                    break;
                case "gsw-CH":  // 'Schwiizertüütsch (Swiss German)' not supported .NET culture
                    dotNetLanguage = "de-CH"; // closest supported
                    break;
            }
            return dotNetLanguage;
        }

        private string ToDotNetFallbackLanguage(PlatformCulture platformCulture)
        {
            string dotNetLanguage = platformCulture.LanguageCode;
            switch (platformCulture.LanguageCode)
            {
                case "gsw":
                    dotNetLanguage = "de-CH";
                    break;
            }
            return dotNetLanguage;
        }
    }
}