using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using FormsApp13.DependencyServices;
using Xamarin.Forms;
using FormsApp13.Resources;

namespace FormsApp13
{
	[SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //MainPage = new FormsApp13.MainPage();

            //# Loading embedded resources
            var assembly = typeof(AppResources).GetTypeInfo().Assembly;
            foreach (var resource in assembly.GetManifestResourceNames())
                Debug.WriteLine($"Found resource: {resource}");

            //# 명시적으로 리소스의 CultureInfo를 설정
            //AppResources.Culture = new CultureInfo("ko-kr");

            //# NOTE: 디바이스의 언어설정으로 리소스의 CultureInfo를 설정
            //# - Notice that we don't need to manually set this value for Windows Phone and UWP, since
            //# - the resource framework automatically recognizes the selected language on those platforms.
            //# NOTE: 다국어를 지원하기 위한 각 플랫폼별 설정
            //# 1.iOS
            //# - Info.plist 파일에 지원하는 언어목록(CFBundleLocalizations), 개발지역(CFBundleDevelopmentRegion) 설정
            //# 2.Android
            //# - 프로젝트 속성 > Android Options > 'Use Fast Deployment (debug mode only)' uncheck!
            //# - If the translated strings are working in your RELEASE Android builds but not while debugging,
            //# - right-click on the Android Project and select Options > Build > Android Build and ensure that the
            //# - 'Fast assembly deployment' is NOT ticked. This option causes problems with loading resources and
            //# - should not be used if your are testing localized apps.
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
		    {
		        var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.Culture = ci;
                DependencyService.Get<ILocalize>().SetLocale(ci);
		    }

            //# 코드로 실행
		    //this.MainPage = new MainCodePage();

            //# XAML 실행
		    this.MainPage = new MainPage();
		}
	}
}
