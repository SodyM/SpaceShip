using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Hatch - Simple coin
    /// </summary>
    class Hatch : AnimatedUiObject
    {
        // constants for our object
        const int START_FRAMERATE = 90;
        const int FRAMES_COUNT = 4;
        const int HEIGHT = 16;
        const int WIDTH = 16;

        // velocity information
        Vector2 velocity = new Vector2(0, 0);
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        int value = 0;

        /// <summary>
        /// Gets the value of hatch
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        /// <param name="position">Start position</param>
        public Hatch(ContentManager contentManager, GraphicsDevice device, Vector2 position, Vector2 velocity, int value)
        {
            sprite = contentManager.Load<Texture2D>(AssetsConstants.HATCH);
            this.velocity = velocity;
            this.value = value;

            base.Init(FRAMES_COUNT, WIDTH, HEIGHT, position);
            base.ChangeFrameRate(START_FRAMERATE);

        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            if (!this.IsActive)
                return;

            int elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
            position.X += (int)(this.velocity.X * elapsedTime);
            position.Y += (int)(this.velocity.Y * elapsedTime);

            if ((position.X < 0) || (position.X > GameConstants.WINDOW_WIDTH - WIDTH))
                this.IsActive = false;

            if ((position.Y < 0) || (position.Y > GameConstants.WINDOW_HEIGHT - HEIGHT))
                this.IsActive = false;

            drawRectangle.X = (int)position.X;
            drawRectangle.Y = (int)position.Y;

            base.Update(gameTime);
        }
    }
}
