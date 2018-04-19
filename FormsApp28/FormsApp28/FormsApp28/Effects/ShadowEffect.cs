using System.Linq;
using Xamarin.Forms;

namespace FormsApp28.Effects
{
    public static class ShadowEffect
    {
        #region == Attached properties ==
        public static readonly BindableProperty HasShadowProperty
            = BindableProperty.CreateAttached("HasShadow", typeof(bool), typeof(ShadowEffect), default(bool)
                , propertyChanged: OnHasShadowChanged);

        public static readonly BindableProperty ColorProperty
            = BindableProperty.CreateAttached("Color", typeof(Color), typeof(ShadowEffect), Color.Default);

        public static readonly BindableProperty RadiusProperty
            = BindableProperty.CreateAttached("Radius", typeof(float), typeof(ShadowEffect), 1.0F);

        public static readonly BindableProperty DistanceXProperty
            = BindableProperty.CreateAttached("DistanceX", typeof(float), typeof(ShadowEffect), default(float));

        public static readonly BindableProperty DistanceYProperty
            = BindableProperty.CreateAttached("DistanceY", typeof(float), typeof(ShadowEffect), default(float));
        #endregion

        #region == Get/Set methods for attached properties ==
        public static bool GetHasShadow(BindableObject bindable)
        {
            return (bool)bindable.GetValue(HasShadowProperty);
        }

        public static void SetHasShadow(BindableObject bindable, bool value)
        {
            bindable.SetValue(HasShadowProperty, value);
        }

        public static Color GetColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(ColorProperty);
        }

        public static void SetColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(ColorProperty, value);
        }

        public static float GetRadius(BindableObject bindable)
        {
            return (float)bindable.GetValue(RadiusProperty);
        }

        public static void SetRadius(BindableObject bindable, float value)
        {
            bindable.SetValue(RadiusProperty, value);
        }

        public static float GetDistanceX(BindableObject bindable)
        {
            return (float)bindable.GetValue(DistanceXProperty);
        }

        public static void SetDistanceX(BindableObject bindable, float value)
        {
            bindable.SetValue(DistanceXProperty, value);
        }


        public static float GetDistanceY(BindableObject bindable)
        {
            return (float)bindable.GetValue(DistanceYProperty);
        }

        public static void SetDistanceY(BindableObject bindable, float value)
        {
            bindable.SetValue(DistanceYProperty, value);
        } 
        #endregion

        private static void OnHasShadowChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is View view))
                return;

            bool hasShadow = (bool)newvalue;
            if (hasShadow)
                view.Effects.Add(new LabelShadowEffect());
            else
            {
                var effect = view.Effects.FirstOrDefault(e => e is LabelShadowEffect);
                if (effect != null)
                    view.Effects.Remove(effect);
            }
        }

        private class LabelShadowEffect : RoutingEffect
        {
            public LabelShadowEffect() : base("HomeNet.LabelShadowEffect") { }
        }
    }
}
