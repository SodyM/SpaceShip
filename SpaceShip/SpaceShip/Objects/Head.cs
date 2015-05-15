using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Head - our boss enemy
    /// 
    /// After his destruction:
    /// - give player 10 coins with value of 100 points for each one
    /// - playe "So ein Tag"
    /// </summary>
    class Head : AnimatedUiObject
    {
        const int START_FRAMERATE = 150;
        const int FRAMES_COUNT = 6;
        const int HEIGHT = 128;
        const int WIDTH = 128;

        /// <summary>
        /// Head
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        /// <param name="position">Start position</param>
        public Head(ContentManager contentManager, GraphicsDevice device, Vector2 position)
        {
            sprite = contentManager.Load<Texture2D>(AssetsConstants.HEAD);
            base.Init(FRAMES_COUNT, WIDTH, HEIGHT, position);
            base.ChangeFrameRate(START_FRAMERATE);
        }
    }
}
