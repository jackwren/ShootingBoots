using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

            //Set Content to Game Page
            SetContentView(Resource.Layout.Game);

            Ball newBall = new Ball();
            //newBall.DrawEllipseInt(e);

            //Home Button instance
            Button homeBtn = FindViewById<Button>(Resource.Id.button1);
            homeBtn.Click += goHome;

        }

        //Home button method
        public void goHome(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
        
    }

    public class Ball {

        int xPosition = 100;
        int yPosition = 300;
        int width = 200;
        int height = 100;


        //Need to create function for displaying ball
        //private void DrawEllipseInt(PaintEventArgs e)
        //{
        //    // Create pen.
        //    Pen blackPen = new Pen(Color.Black, 3);

        //    // Draw ellipse to screen.
        //    e.Graphics.DrawEllipse(blackPen, xPosition, yPosition, width, height);
        //}


    }

}