using System;
using System.Reflection;
using System.Resources;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Multilingual;

namespace FormsApp14.MarkupExtensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private static readonly string ResourceId = "FormsApp14.Resources.AppResources";
        private static readonly Lazy<ResourceManager> LocalResourceManager =
            new Lazy<ResourceManager>(() =>
                new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Text == null)
                return string.Empty;

            var ci = CrossMultilingual.Current.CurrentCultureInfo;
            string translateText = LocalResourceManager.Value.GetString(this.Text, ci);
            if (string.IsNullOrEmpty(translateText))
                translateText = this.Text;

            return translateText;
        }
    }
}
