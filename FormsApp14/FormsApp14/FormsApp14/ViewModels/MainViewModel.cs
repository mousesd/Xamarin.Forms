using System.Globalization;
using MvvmHelpers;
using Plugin.Multilingual;
using FormsApp14.Models;
using FormsApp14.Resources;

namespace FormsApp14.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Language> Languages { get; }

        public string translateText;
        public string TranslateText
        {
            get { return translateText; }
            set
            {
                this.SetProperty(ref translateText, value);
            }
        }

        private Language selectedLanguage;
        public Language SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                this.SetProperty(ref selectedLanguage, value, onChanged: this.OnSelectedLanguageChanged);
            }
        }

        public MainViewModel()
        {
            this.Title = "Let's Translate!";
            this.TranslateText = AppResources.HelloWorld;
            this.Languages = new ObservableRangeCollection<Language>
            {
                new Language { DisplayName = "한국어 - Korean", ShortName = "ko" },
                new Language { DisplayName = "日本語 - Japanese", ShortName = "ja" }
            };
        }

        private void OnSelectedLanguageChanged()
        {
            var ci = new CultureInfo(selectedLanguage.ShortName);
            AppResources.Culture = ci;
            CrossMultilingual.Current.CurrentCultureInfo = ci;

            this.TranslateText = AppResources.HelloWorld;
        }
    }
}
