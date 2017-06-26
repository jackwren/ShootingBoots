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
    [Activity(Label = "MainLeaderboard")]
    public class MainLeaderboard : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Leaderboard);

            //TABLE STILL NEEDS WORK (Default values)
            //SetContentView(Resource.Layout.Table_L);
            
            //Call Set High Score
            //SetHighScore();

        }

        public void SetHighScore()
        {
            //Template method to set SetHighscore
            //Connect to Main Game, get count etc
            //use Linq?
        }

    }
}