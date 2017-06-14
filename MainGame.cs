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
      
    }

    public class MyOvalShape : View
    {
        public ShapeDrawable _shape;

        public MyOvalShape(Context context) : base(context)
        {
            var paint = new Paint();
            paint.SetARGB(255, 200, 255, 0);
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 4;

            _shape = new ShapeDrawable(new OvalShape());
            _shape.Paint.Set(paint);

            _shape.SetBounds(200, 200, 300, 200);
           
        }

        protected override void OnDraw(Canvas canvas)
        {
            _shape.Draw(canvas);
        }
    }

}