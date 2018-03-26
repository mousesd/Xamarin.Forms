using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

using CoreGraphics;
using Foundation;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using FormsApp16.Controls;
using FormsApp16.iOS.Renderer;

[assembly: ExportRenderer(typeof(HorizontalListView), typeof(iOSHorizontalListViewRenderer))]
namespace FormsApp16.iOS.Renderer
{
    public class iOSHorizontalListViewRenderer : ViewRenderer<HorizontalListView, UICollectionView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<HorizontalListView> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {
                var layout = new UICollectionViewFlowLayout
                {
                    ScrollDirection = UICollectionViewScrollDirection.Horizontal
                };

                if (e.NewElement != null)
                {
                    layout.ItemSize = new CGSize(e.NewElement.ItemWidth, e.NewElement.ItemHeight);
                    layout.MinimumLineSpacing = 0;
                    layout.MinimumInteritemSpacing = 0;

                    var rect = new CGRect(0, 0, 100, 100);
                    this.SetNativeControl(new UICollectionView(rect, layout));
                    this.Control.BackgroundColor = e.NewElement.BackgroundColor.ToUIColor();
                }
                this.SetDataSource();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(this.Element.ItemsSource))
                this.SetDataSource();
        }

        private void SetDataSource()
        {
            if (this.Control == null)
                return;

            this.Control.DataSource = new iOSViewSource(this.Element);
            this.Control.RegisterClassForCell(typeof(iOSViewCell), nameof(iOSViewCell));
        }
    }

    public class iOSViewSource : UICollectionViewDataSource
    {
        #region == Fields & Constructors ==
        private readonly HorizontalListView view;
        private readonly IList dataSource;
        public iOSViewSource(HorizontalListView view)
        {
            this.view = view;
            this.dataSource = view.ItemsSource?.Cast<object>().ToList();
        }
        #endregion

        #region == Methods ==
        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return dataSource?.Count ?? 0;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (iOSViewCell)collectionView.DequeueReusableCell(nameof(iOSViewCell), indexPath);
            var dataContext = dataSource[indexPath.Row];
            if (dataContext != null)
            {
                ViewCell viewCell;
                var dataTemplate = view.ItemTemplate;
                if (dataTemplate is DataTemplateSelector selector)
                {
                    var template = selector.SelectTemplate(dataSource[indexPath.Row], view.Parent);
                    viewCell = template.CreateContent() as ViewCell;
                }
                else
                {
                    viewCell = dataTemplate?.CreateContent() as ViewCell;
                }
                cell.UpdateUi(viewCell, dataContext, view);
            }
            return cell;
        } 
        #endregion
    }

    public class iOSViewCell : UICollectionViewCell
    {
        public iOSViewCell(IntPtr handle) : base(handle) { }

        public void UpdateUi(ViewCell viewCell, object dataContext, HorizontalListView view)
        {
            viewCell.BindingContext = dataContext;
            viewCell.Parent = view;

            int height = (int)(view.ItemHeight + viewCell.View.Margin.Top + viewCell.View.Margin.Bottom);
            int width = (int)(view.ItemWidth + viewCell.View.Margin.Left + viewCell.View.Margin.Right);
            viewCell.View.Layout(new Rectangle(0, 0, width, height));

            if (Platform.GetRenderer(viewCell.View) == null)
                Platform.SetRenderer(viewCell.View, Platform.CreateRenderer(viewCell.View));

            var nativeView = Platform.GetRenderer(viewCell.View).NativeView;
            foreach (var subview in this.ContentView.Subviews)
                subview.RemoveFromSuperview();

            nativeView.ContentMode = UIViewContentMode.ScaleAspectFit;
            this.ContentView.AddSubview(nativeView);
        }
    }
}
