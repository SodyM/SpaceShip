using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Hatch
    /// </summary>
    class Hatch : AnimatedUiObject
    {
        // constants for our object
        const int START_FRAMERATE = 90;
        const int FRAMES_COUNT = 4;
        const int HEIGHT = 16;
        const int WIDTH = 16;
        const string ASSET_NAME = "hatch_sheet";

        int value = 0;

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
        public Hatch(ContentManager contentManager, GraphicsDevice device, Vector2 position, int value)
        {
            sprite = contentManager.Load<Texture2D>(ASSET_NAME);
            this.value = value;

            base.Init(FRAMES_COUNT, WIDTH, HEIGHT, position);
            base.ChangeFrameRate(START_FRAMERATE);

        }       
    }
}
