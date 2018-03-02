using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FormsApp12
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //# 1.
		    //var buttonStyle = new Style(typeof(Button))
		    //{
		    //    Setters =
		    //    {
      //              new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Center },
      //              new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.CenterAndExpand },
      //              new Setter { Property = Button.BorderColorProperty, Value = Color.Lime },
      //              new Setter { Property = Button.BorderWidthProperty, Value = 5 },
      //              new Setter { Property = VisualElement.WidthRequestProperty, Value = 200 },
      //              new Setter { Property = Button.TextColorProperty, Value = Color.Teal }
		    //    }
		    //};
		    //this.Resources = new ResourceDictionary { { nameof(buttonStyle), buttonStyle } };

            //# 2.Custom ResourceDictionary를 이용해서 설정
		    this.Resources = new ResourceDictionaryEx();

		    MainPage = new MainPage();
		}
	}
}
