using System;
using Xamarin.Forms;
using FormsApp28.Effects;

namespace FormsApp28
{
	public partial class MainPage : ContentPage
	{
	    private bool isLabelColorTeal;

		public MainPage()
		{
			InitializeComponent();
		}

	    private void OnChangeShadowColor(object sender, EventArgs e)
	    {
            ShadowEffect.SetColor(label, isLabelColorTeal ? Color.Teal : Color.Red);
            isLabelColorTeal = !isLabelColorTeal;
	    }
	}
}
