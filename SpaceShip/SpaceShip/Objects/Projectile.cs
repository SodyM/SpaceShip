using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;
using System;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Projectile
    /// </summary>
    class Projectile : BaseUiObject
    {
        int windowHeight, windowWidth;
        Vector2 position;
        Vector2 spriteOrigin;
        const float SPEED = 10.0f;
        const int WIDTH = 46;
        const int HEIGHT = 16;       
    

        public int X_Position
        {
            get { return (int)position.X; }
        }

        public int TextureWidth
        {
            get { return sprite.Width; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        /// <param name="position">Start position of projectile</param>
        public Projectile(ContentManager contentManager, GraphicsDevice device, Vector2 position)
        {
            sprite = contentManager.Load<Texture2D>(AssetsConstants.LASER);
            this.position = position;

            // center it
            this.position.X = position.X - sprite.Width / 2;
            this.position.Y = position.Y - sprite.Height / 2;

            // set origin (center of sprite)
            spriteOrigin.X = (float)sprite.Width / 2;
            spriteOrigin.Y = (float)sprite.Height / 2;

            // set draw rectangle
            drawRectangle = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), WIDTH, HEIGHT);

            // set window dimensions
            windowHeight = device.Viewport.Height;
            windowWidth = device.Viewport.Width;
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            position.X += SPEED;
            drawRectangle.X = (int)position.X;
        }
    }
}