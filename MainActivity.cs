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

            //Set Content to Main page
            SetContentView(Resource.Layout.Main);

            //Create button press instance
            Button playButton = FindViewById<Button>(Resource.Id.button1);
            playButton.Click += PlaybuttonClick;

            Button leaderboardButton = FindViewById<Button>(Resource.Id.button2);
            leaderboardButton.Click += LeaderboardbuttonClick;
           
        }

        //Creates methods for opening new pages
        public void PlaybuttonClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainGame));
            StartActivity(intent);
        }
        public void LeaderboardbuttonClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainLeaderboard));
            StartActivity(intent);
        }
       
    }
}

