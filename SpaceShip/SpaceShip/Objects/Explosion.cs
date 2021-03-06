﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Explosion
    /// </summary>
    class Explosion : AnimatedUiObject
    {
        const int START_FRAMERATE = 45;
        const int FRAMES_COUNT = 12;
        const int HEIGHT = 134;
        const int WIDTH = 134;
        const string ASSET_NAME = "explosion";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        /// <param name="position">Start position</param>
        public Explosion(ContentManager contentManager, GraphicsDevice device, Vector2 position)
        {
            sprite = contentManager.Load<Texture2D>(ASSET_NAME);
            base.Init(FRAMES_COUNT, WIDTH, HEIGHT, position);
            base.ChangeFrameRate(START_FRAMERATE);
            AnimationDirectionFromTopToDown = false;
        }
    }
}
