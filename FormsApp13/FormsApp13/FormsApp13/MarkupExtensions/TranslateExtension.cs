using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using FormsApp13.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsApp13.MarkupExtensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private static readonly string ResourceId = "FormsApp13.Resources.AppResources";

        private static readonly Lazy<ResourceManager> LocalResourceManager
            = new Lazy<ResourceManager>(() =>
                new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        private readonly CultureInfo cultureInfo;
        public string Text { get; set; }

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(this.Text))
                return string.Empty;

            string translateText = LocalResourceManager.Value.GetString(this.Text, cultureInfo);
            if (string.IsNullOrEmpty(translateText))
            {
                Debug.WriteLine(
                    $"Key '{this.Text}' was not found in resources '{ResourceId}' for culture '{cultureInfo.Name}'");
                translateText = this.Text;
            }
            return translateText;
        }
    }
}
