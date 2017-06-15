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
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

namespace ShootingBoots
{
    [Activity(Label = "MainGame")]
    public class MainGame : Activity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Draw Ball
            SetContentView(new MyOvalShape(this));
      
        }

        //public boolean onTouch(View v, MotionEvent event) {

             //Create method to take user touch on screen
             //Need to find if they have touched the area of the ball
             //If so increase top and bottom position by 200 etc
          
        //}

      
    }

    public class MyOvalShape : View
    {
        public ShapeDrawable _shape;
        public int top = 900;
        public int bottom = 1000;
        public int right = 450;
        public int left = 350;


        public MyOvalShape(Context context) : base(context)
        {
            var paint = new Paint();
            paint.SetARGB(255, 200, 255, 200);
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 4;

            _shape = new ShapeDrawable(new OvalShape());
            _shape.Paint.Set(paint);

            _shape.SetBounds(left, top, right, bottom);
            
           
        }

        protected override void OnDraw(Canvas canvas)
        {
            _shape.Draw(canvas);
        }
    }

}