using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsApp30.DataTemplates
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CurrentTodoTemplate : ViewCell
	{
	    public static readonly BindableProperty BaseContextProperty
	        = BindableProperty.Create(nameof(BaseContext), typeof(object), typeof(CurrentTodoTemplate), null
	            , propertyChanged: OnParentContextPropertyChanged);
	    public object BaseContext
	    {
	        get { return this.GetValue(BaseContextProperty); }
	        set { this.SetValue(BaseContextProperty, value); }
	    }

	    public CurrentTodoTemplate ()
	    {
	        InitializeComponent();
	    }

	    private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
            if (newValue == oldValue || newValue == null)
                return;

            if (!(bindable is CurrentTodoTemplate template))
                return;

            template.BaseContext = newValue;
	    }
	}
}