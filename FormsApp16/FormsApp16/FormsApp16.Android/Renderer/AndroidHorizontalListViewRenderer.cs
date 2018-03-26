using System.Collections;
using System.Linq;
using System.ComponentModel;

using Android.Content;
using Android.Content.Res;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using FormsApp16.Controls;
using FormsApp16.Droid.Renderer;

[assembly: ExportRenderer(typeof(HorizontalListView), typeof(AndroidHorizontalListViewRenderer))]
namespace FormsApp16.Droid.Renderer
{
    public class AndroidHorizontalListViewRenderer : ViewRenderer<HorizontalListView, RecyclerView>
    {
        public AndroidHorizontalListViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<HorizontalListView> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {
                var recyclerView = new RecyclerView(this.Context);
                recyclerView.SetLayoutManager(new LinearLayoutManager(this.Context, OrientationHelper.Horizontal, false));
                this.SetNativeControl(recyclerView);
                this.SetRecyclerViewAdapter(e.NewElement);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"PropertyName: {e.PropertyName}");
            if (e.PropertyName == nameof(this.Element.ItemsSource))
                this.SetRecyclerViewAdapter(this.Element);
        }

        private void SetRecyclerViewAdapter(HorizontalListView view)
        {
            var adapter = new RecyclerViewAdpater(view);
            this.Control.SetAdapter(adapter);
        }
    }

    public class RecyclerViewAdpater : RecyclerView.Adapter
    {
        #region == Fields & Properties ==
        private readonly HorizontalListView view;
        private readonly IList dataSource;

        public override int ItemCount
        {
            get { return dataSource?.Count ?? 0; }
        } 
        #endregion

        #region == Constructors ==
        public RecyclerViewAdpater(HorizontalListView view)
        {
            this.view = view;
            this.dataSource = view.ItemsSource?.Cast<object>()?.ToList();
            this.HasStableIds = true;
        }
        #endregion

        #region == Methods ==
        public override long GetItemId(int position)
        {
            return position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var contentFrame = new FrameLayout(parent.Context)
            {
                LayoutParameters = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent
                    , ViewGroup.LayoutParams.MatchParent)
                {
                    Width = (int)(view.ItemWidth * Resources.System.DisplayMetrics.Density),
                    Height = (int)(view.ItemHeight * Resources.System.DisplayMetrics.Density)
                },
                DescendantFocusability = DescendantFocusability.AfterDescendants
            };

            var viewHolder = new RecyclerViewHolder(contentFrame);
            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is RecyclerViewHolder item))
                return;

            var dataContext = dataSource[position];
            if (dataContext == null)
                return;

            var dataTemplate = view.ItemTemplate;
            ViewCell viewCell;
            if (dataTemplate is DataTemplateSelector selector)
            {
                var template = selector.SelectTemplate(dataSource[position], view.Parent);
                viewCell = template.CreateContent() as ViewCell;
            }
            else
            {
                viewCell = dataTemplate?.CreateContent() as ViewCell;
            }

            item.UpdateUi(viewCell, dataContext, view);
        } 
        #endregion
    }

    public class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        #region == Constructors ==
        public RecyclerViewHolder(Android.Views.View itemView) : base(itemView)
        {
            this.ItemView = ItemView;
        }
        #endregion

        #region == Methods ==
        public void UpdateUi(ViewCell viewCell, object dataContext, HorizontalListView view)
        {
            if (!(this.ItemView is FrameLayout frameLayout))
                return;

            viewCell.BindingContext = dataContext;
            viewCell.Parent = view;

            var metrics = Resources.System.DisplayMetrics;
            int width = (int)((view.ItemWidth + viewCell.View.Margin.Left + viewCell.View.Margin.Right)
                * metrics.Density);
            int height = (int)((view.ItemHeight + viewCell.View.Margin.Top + viewCell.View.Margin.Bottom)
                * metrics.Density);
            viewCell.View.Layout(new Rectangle(0, 0, view.ItemWidth, view.ItemHeight));

            var layoutParams = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent
                , ViewGroup.LayoutParams.MatchParent)
            {
                Width = width,
                Height = height
            };

            if (Platform.GetRenderer(viewCell.View) == null)
            {
                Platform.SetRenderer(viewCell.View
                    , Platform.CreateRendererWithContext(viewCell.View, this.ItemView.Context));
            }

            var renderer = Platform.GetRenderer(viewCell.View);
            var viewGroup = renderer.View;
            viewGroup.LayoutParameters = layoutParams;
            viewGroup.Layout(0, 0, width, height);

            frameLayout.RemoveAllViews();
            frameLayout.AddView(viewGroup);
        } 
        #endregion
    }
}
