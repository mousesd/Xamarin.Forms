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
	public partial class CompletedTodoTemplate : ViewCell
	{
	    public static readonly BindableProperty BaseContextProperty
	        = BindableProperty.Create(nameof(BaseContext), typeof(object), typeof(CompletedTodoTemplate), null
	            , propertyChanged: OnParentContextPropertyChanged);
	    public object BaseContext
	    {
	        get { return this.GetValue(BaseContextProperty); }
	        set { this.SetValue(BaseContextProperty, value); }
	    }

		public CompletedTodoTemplate ()
		{
		    InitializeComponent();
		}

	    private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
            if (newValue == oldValue || newValue == null)
                return;

            if (!(bindable is CompletedTodoTemplate template))
                return;

            template.BaseContext = newValue;
	    }
	}
}
