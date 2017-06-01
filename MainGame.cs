using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ShootingBoots
{
    [Activity(Label = "MainGame")]
    public class MainGame : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Game);

            Button homeBtn = FindViewById<Button>(Resource.Id.button1);
            homeBtn.Click += goHome;

        }
        public void goHome(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

    }
}