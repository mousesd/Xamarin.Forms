using System;

using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace FormsApp17.Droid
{
    [Application(Debuggable = true)]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }

        #region == Override members of the Applciation class ==
        public override void OnCreate()
        {
            base.OnCreate();
            this.RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            this.UnregisterActivityLifecycleCallbacks(this);
        } 
        #endregion

        #region == Implement members of the Application.IActivityLifecycleCallbacks interface ==
        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {

        }

        public void OnActivityDestroyed(Activity activity)
        {

        }

        public void OnActivityPaused(Activity activity)
        {

        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {

        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {

        } 
        #endregion
    }
}
