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
    public class MainGame : Activity, GestureDetector.IOnGestureListener
    {
        private GestureDetector _gestureDetector;
        private MyOvalShape myView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Draw Ball
            SetContentView(new MyOvalShape(this));
            _gestureDetector = new GestureDetector(this);

        }

        public override bool OnTouchEvent(MotionEvent e) {

            _gestureDetector.OnTouchEvent(e);
            return false;
          
        }

        public bool OnDown(MotionEvent e)
        {
            return false;
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
           
            myView.top =+ 100;
            myView.bottom =+ 100;

            return true;
        }

        public void OnLongPress(MotionEvent e)
        {
            
        }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return false;
        }

        public void OnShowPress(MotionEvent e)
        {
            
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            return false;
        }

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