using System.Collections.Generic;
using CocosSharp;
using System;
using System.Linq;
using Android.Widget;
using Android.App;

namespace ShootingBoots.Common
{
    public class GameLayer : CCLayerColor
    {
        CCSprite ballSprite;
        CCLabel scoreLabel;

        float ballXVelocity;
        float ballYVelocity;
        float ballYMin;
        float ballXMin;
        float ballXMax;
        float ballYMax;

        // How much to modify the ball's y velocity per second:
        const float gravity = 180;

        int score;
        bool doesUserTouchBall = false;
        bool sendBallLeft = false;
        bool sendBallRight = false;
        bool ballGoesBelow = false;

        public GameLayer() : base(CCColor4B.White)
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;

            Random rnd = new Random();
            int rndX = rnd.Next(250, 400);
            int rndY = rnd.Next(550, 700);

            ballSprite = new CCSprite("ball");
            ballSprite.PositionX = rndX;
            ballSprite.PositionY = rndY;
            AddChild(ballSprite);

            //If ball goes below bottom window, game over reset
            if (ballGoesBelow == true)
            {
                //Finish the game
                scoreLabel = new CCLabel("GAME OVER", "Arial", 45, CCLabelFormat.SystemFont);
                scoreLabel.Color = CCColor3B.Black;
                scoreLabel.PositionX = 500;
                scoreLabel.PositionY = 500;
                scoreLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
                AddChild(scoreLabel);

            }
            else
            {
                scoreLabel = new CCLabel("Score: 0", "Arial", 20, CCLabelFormat.SystemFont);
                scoreLabel.Color = CCColor3B.Black;
                scoreLabel.PositionX = 50;
                scoreLabel.PositionY = 1000;
                scoreLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
                AddChild(scoreLabel);
            }

            Schedule(RunGameLogic);

        }

        void RunGameLogic(float frameTimeInSeconds)
        {
            // This is a linear approximation, so not 100% accurate
            ballYVelocity += frameTimeInSeconds * -gravity;
            ballSprite.PositionX += ballXVelocity * frameTimeInSeconds;
            ballSprite.PositionY += ballYVelocity * frameTimeInSeconds;
             
            // ... and if the ball is moving downward.
            bool isMovingDownward = ballYVelocity < 0;

            //REWORK THIS *
            if (doesUserTouchBall && isMovingDownward)
            {
                // First let's invert the velocity:
                var fallSpeed = 1;

                ballYVelocity *= - Convert.ToSingle(fallSpeed);

                //Rework this so depends where you touch ball goes in x direction
                if (sendBallLeft == true)
                {
                    //Send ball left
                    const float minXVelocity_Left = -300;
                    const float maxXVelocity_Left = -100;
                    ballXVelocity = CCRandom.GetRandomFloat(minXVelocity_Left, maxXVelocity_Left);

                    sendBallLeft = false;

                }
                else if (sendBallRight == true)
                {
                    //Send ball right
                    const float minXVelocity_Right = 100;
                    const float maxXVelocity_Right = 300;
                    ballXVelocity = CCRandom.GetRandomFloat(minXVelocity_Right, maxXVelocity_Right);

                    sendBallRight = false;
                }
                else
                {
                    //Send the ball directly up
                    const float minXVelocity_Up = -25;
                    const float maxXVelocity_Up = 25;
                    ballXVelocity = CCRandom.GetRandomFloat(minXVelocity_Up, maxXVelocity_Up);
                }

                // DisplayScore:
                score++;
                scoreLabel.Text = "Score: " + score;

                //Set the touch back to default
                doesUserTouchBall = false;
            }

            // First let’s get the ball position:   
            float ballRight = ballSprite.BoundingBoxTransformedToParent.MaxX;
            float ballLeft = ballSprite.BoundingBoxTransformedToParent.MinX;
            // Then let’s get the screen edges
            float screenRight = VisibleBoundsWorldspace.MaxX;
            float screenLeft = VisibleBoundsWorldspace.MinX;

            // Check if the ball is either too far to the right or left:    
            bool shouldReflectXVelocity =
                (ballRight > screenRight && ballXVelocity > 0) ||
                (ballLeft < screenLeft && ballXVelocity < 0);

            if (shouldReflectXVelocity)
            {
                ballXVelocity *= -1;
            }

            if (ballSprite.PositionY > 1050)
            {
                ballGoesBelow = true;
            }
            

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            CCRect bounds = VisibleBoundsWorldspace;

            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            touchListener.OnTouchesMoved = HandleTouchesMoved;
            AddEventListener(touchListener, this);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            //get location of touch
            var location = touches[0].LocationOnScreen;
            location = WorldToScreenspace(location);
            
            //Put touch X&Y into variables
            int touchX = Convert.ToInt32(location.X);
            int touchY = Convert.ToInt32(location.Y);

            //Ball limits for touch, max above x and below
            ballXMin = (ballSprite.PositionX - 25);
            ballXMax = (ballSprite.PositionX + 25);
            //... same for y
            ballYMin = (ballSprite.PositionY - 25);
            ballYMax = (ballSprite.PositionY + 25);


            //If user touches the ball within the range of the ball object make it jump
            if (Enumerable.Range(Convert.ToInt32(ballXMin), Convert.ToInt32(ballXMax)).Contains(touchX) &&
                    Enumerable.Range(Convert.ToInt32(ballYMin), Convert.ToInt32(ballYMax)).Contains(touchY) &&
                    touches.Count > 0){

                        doesUserTouchBall = true;
            }

            //If the ball is less than the max X and more than the median X
            //So we touch the ball on the right hand side
            if (touchX > (Convert.ToInt32(ballXMin) + 15) && touchX < Convert.ToInt32(ballXMax))
            {
                //Send the ball in left x direction
                sendBallLeft = true;
            }

            //If the ball is greater than the min X and less than the median X
            //So we touch the ball on the left hand side
            if (touchX < (Convert.ToInt32(ballXMin) + 15) && touchX > Convert.ToInt32(ballXMin))
            {
                //Send the ball in a right x direction
                sendBallRight = true;
            }

        }

        void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            // we only care about the first touch:
           
        }
    }
}

