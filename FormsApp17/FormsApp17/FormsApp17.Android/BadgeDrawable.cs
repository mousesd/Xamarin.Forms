using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;

namespace FormsApp17.Droid
{
    public class BadgeDrawable : Drawable
    {
        #region == Fields ==
        private static readonly string BadgeValueOverflow = "+";

        private readonly Context context;
        private readonly Paint badgeBackground;
        private readonly Paint badgeText;
        private readonly Rect textRect = new Rect();

        private bool shouldDraw = true;
        private string badgeValue; 
        #endregion

        #region == Constructors ==
        public BadgeDrawable(Context context, Color backgroundColor, Color textColor)
        {
            this.context = context;

            badgeBackground = new Paint
            {
                Color = backgroundColor,
                AntiAlias = true,
            };
            badgeBackground.SetStyle(Paint.Style.Fill);

            float textSize = context.Resources.GetDimension(Resource.Dimension.textsize_badge_count);
            badgeText = new Paint
            {
                Color = textColor,
                TextSize = textSize,
                AntiAlias = true,
                TextAlign = Paint.Align.Center
            };
            badgeText.SetTypeface(Typeface.Default);
        } 
        #endregion

        #region == Implement members of the Drawable class ==
        public override void Draw(Canvas canvas)
        {
            if (!shouldDraw)
                return;

            var bounds = this.Bounds;
            float width = bounds.Right - bounds.Left;
            float height = bounds.Bottom - bounds.Top;
            float oneDp = 1 * context.Resources.DisplayMetrics.Density;

            //# Position the badge the top-right quadrant of the icon
            float radius = Java.Lang.Math.Max(width, height) / 2 / 2;
            float centerX = width - radius - 1 + oneDp * 2;
            float centerY = radius - 2 * oneDp;
            canvas.DrawCircle(centerX, centerY, (int)(radius + oneDp * 5), badgeBackground);

            //# Draw badge count message inside the circle
            badgeText.GetTextBounds(badgeValue, 0, badgeValue.Length, textRect);
            float textHeight = textRect.Bottom - textRect.Top;
            float textY = centerY + textHeight / 2f;
            canvas.DrawText(badgeValue.Length > 2 ? BadgeValueOverflow : badgeValue, centerX, textY, badgeText);
        }

        public override void SetAlpha(int alpha)
        {
            
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
            
        }

        public override int Opacity
        {
            get { return (int)Format.Unknown; }
        }
        #endregion

        #region == Methods ==
        public static void SetBadgeCount(Context context, IMenuItem item, int count, Color backgroundColor, Color textColor)
        {
            SetBadgeText(context, item, $"{count}", backgroundColor, textColor);
        }

        public static void SetBadgeText(Context context, IMenuItem item, string text, Color backgroundColor, Color textColor)
        {
            if (item.Icon == null)
                return;

            BadgeDrawable badge = null;
            var icon = item.Icon;

            if (icon is LayerDrawable layerDrawable)
            {
                if (string.IsNullOrEmpty(text) || text == "0")
                {
                    icon = layerDrawable.GetDrawable(0);
                    layerDrawable.Dispose();
                }
                else
                {
                    for (int index = 0; index < layerDrawable.NumberOfLayers; index++)
                    {
                        if (layerDrawable.GetDrawable(index) is BadgeDrawable)
                        {
                            badge = layerDrawable.GetDrawable(index) as BadgeDrawable;
                            break;
                        }
                    }

                    if (badge == null)
                    {
                        badge = new BadgeDrawable(context, backgroundColor, textColor);
                        icon = new LayerDrawable(new[] { item.Icon, badge });
                    }
                }
            }
            else
            {
                badge = new BadgeDrawable(context, backgroundColor, textColor);
                icon = new LayerDrawable(new[] { item.Icon, badge });
            }

            badge?.SetBadgeText(text);
            item.SetIcon(icon);
            icon.Dispose();
        }

        private void SetBadgeText(string text)
        {
            badgeValue = text;
            shouldDraw = text != "0";
            this.InvalidateSelf();
        } 
        #endregion
    }
}
