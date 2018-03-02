using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FormsApp12
{
    public class ResourceDictionaryEx : ResourceDictionary
    {
        public ResourceDictionaryEx()
        {
            var buttonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                    new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.CenterAndExpand },
                    new Setter { Property = Button.BorderColorProperty, Value = Color.Lime },
                    new Setter { Property = Button.BorderWidthProperty, Value = 5 },
                    new Setter { Property = VisualElement.WidthRequestProperty, Value = 200 },
                    new Setter { Property = Button.TextColorProperty, Value = Color.Teal }
                }
            };
            this.Add(nameof(buttonStyle), buttonStyle);
        }
    }
}
