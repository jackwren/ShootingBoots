using System.Collections.Generic;
using CocosSharp;
using System;
using System.Linq;

namespace ShootingBoots.Common
{
    public class GameLayer : CCLayerColor
    {
        CCSprite ballSprite;
        CCLabel scoreLabel;

        float ballXVelocity;
        float ballYVelocity;

        // How much to modify the ball's y velocity per second:
        const float gravity = 140;

        int score;
        bool doesUserTouchBall = false;

        public GameLayer() : base(CCColor4B.Black)
        {

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            
            ballSprite = new CCSprite("ball");
            ballSprite.PositionX = 320;
            ballSprite.PositionY = 600;
            AddChild(ballSprite);

            scoreLabel = new CCLabel("Score: 0", "Arial", 20, CCLabelFormat.SystemFont);
            scoreLabel.PositionX = 50;
            scoreLabel.PositionY = 1000;
            scoreLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
            AddChild(scoreLabel);

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
                ballYVelocity *= -1;
                // Then let's assign a random to the ball's x velocity:
                const float minXVelocity = -300;
                const float maxXVelocity = 300;
                ballXVelocity = CCRandom.GetRandomFloat(minXVelocity, maxXVelocity);

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
            var ballXMin = (ballSprite.PositionX - 25);
            var ballXMax = (ballSprite.PositionX + 25);
            //... same for y
            var ballYMin = (ballSprite.PositionY - 25);
            var ballYMax = (ballSprite.PositionY + 25);

            if (Enumerable.Range(Convert.ToInt32(ballXMin), Convert.ToInt32(ballXMax)).Contains(touchX) &&
                    Enumerable.Range(Convert.ToInt32(ballYMin), Convert.ToInt32(ballYMax)).Contains(touchY) &&
                    touches.Count > 0){

                        doesUserTouchBall = true;
            }
        }

        void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            // we only care about the first touch:
           
        }
    }
}

