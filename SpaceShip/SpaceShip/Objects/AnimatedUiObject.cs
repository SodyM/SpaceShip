using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceShip.Objects
{
    /// <summary>
    /// AnimatedUiObject - will be used for all animated objects
    /// </summary>
    class AnimatedUiObject : BaseUiObject
    {
        public Vector2 position;
        Vector2 spriteOrigin;

        // support for animation - settings
        int frames_count;               // count of frames of our animation
        int frame_rate = 75;            // animation speed
        int width;                      // width of single frame
        int height;                     // height of single frame

        int health;                     // object's health

        Rectangle sourceRectangle;      // source rectangle
        int currentFrame;               // index of current frame
        int elapsedFrameTime = 0;


        bool useAnimationTopDown = true;
        bool deactivateAfterAnimation = false;//deactivate object after animation was played once (example: explosion)


        /// <summary>
        /// Gets or sets the health.
        /// </summary>
        /// <value>
        /// The health.
        /// </value>
        public int Health
        { 
            get
            {
                return health;
            }
            set
            {
                health = value;
                if (health < 0)
                    health = 0;
            }
        }

        /// <summary>
        /// Sets a value indicating whether [animation direction from top to down].
        /// </summary>
        /// <value>
        /// <c>true</c> if [animation direction from top to down]; otherwise, <c>false</c>.
        /// </value>
        public bool AnimationDirectionFromTopToDown
        {
            set { useAnimationTopDown = value; }
        }
             
        /// <summary>
        /// Initialization of animated ui object
        /// </summary>
        /// <param name="numFrames">Numer of frames</param>
        /// <param name="width">Width of single frame</param>
        /// <param name="height">Height of single frame</param>
        /// <param name="position">Start position</param>
        /// <param name="sprite">Sprite</param>
        public void Init(int numFrames, int width, int height, Vector2 position, Texture2D sprite, int health = 100)
        {
            base.sprite = sprite;

            Init(numFrames, width, height, position, false, health);
        }
        
        /// <summary>
        /// Initialization of animated ui object
        /// </summary>
        /// <param name="numFrames">Numer of frames</param>
        /// <param name="width">Width of single frame</param>
        /// <param name="height">Height of single frame</param>
        /// <param name="position">Start position</param>
        public void Init(int numFrames, int width, int height, Vector2 position, bool deactiveAfterAnimation = false, int health = 100)
        {
            this.position = position;
            this.frames_count = numFrames;
            this.width = width;
            this.height = height;
            this.deactivateAfterAnimation = deactiveAfterAnimation;
            this.health = health;


            // center it
            this.position.X = position.X - sprite.Width / 2;
            this.position.Y = position.Y - sprite.Height / 2;

            // set origin (center of sprite)
            spriteOrigin.X = (float)sprite.Width / 2;
            spriteOrigin.Y = (float)sprite.Height / 2;

            // set draw rectangle
            drawRectangle = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), width, height);

            // set rectangle of start animation frame
            sourceRectangle.X = 0;
            sourceRectangle.Y = 0;
            sourceRectangle.Width = width;
            sourceRectangle.Height = height;
        }

        /// <summary>
        /// Change framerate
        /// </summary>
        /// <param name="new_frame_rate">New framerate to be set</param>
        public void ChangeFrameRate(int new_frame_rate)
        {
            this.frame_rate = new_frame_rate;
        }
        
        /// <summary>
        /// Set next frame
        /// </summary>
        /// <param name="frameNumber">frameNumber</param>
        private void SetSourceRectangleLocation(int frameNumber)
        {
            if (useAnimationTopDown)
                sourceRectangle.Y = frameNumber * height;
            else
                sourceRectangle.X = frameNumber * width;
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                // check for advancing animation frame
                elapsedFrameTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedFrameTime > frame_rate)
                {
                    elapsedFrameTime = 0;                       // reset frame timer                
                    if (currentFrame < frames_count - 1)        // advance the animation
                        currentFrame++;
                    else
                    {
                        currentFrame = 0;                       // reached the end of the animation
                        if (deactivateAfterAnimation)
                            IsActive = false;
                    }

                    SetSourceRectangleLocation(currentFrame);
                }
            }
        }

        /// <summary>
        /// Draw handler
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
