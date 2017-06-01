using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace ShootingBoots
{
    [Activity(Label = "ShootingBoots",
        MainLauncher = true,
        Icon = "@drawable/icon"
        )]
    public class MainActivity : Activity
    {
  
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += playbuttonClick;

        }

        public void playbuttonClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainGame));
            StartActivity(intent);
        }

    }
}

