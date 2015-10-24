using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShip.Objects
{
    class Resolution : BaseUiObject
    {
        const int WIDTH = 680;
        const int HEIGHT = 90;
        int x = 0;
        int y = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resolution"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="device">The device.</param>
        /// <param name="left">The left position of selected resolution</param>
        /// <param name="top">The top position of selected resolution</param>
        public Resolution(Microsoft.Xna.Framework.Content.ContentManager contentManager, GraphicsDevice device, int left, int top)
        {
            sprite = contentManager.Load<Texture2D>(AssetsConstants.MENU_OPTIONS_RESOLUTIONS);
            x = left;
            y = top;
        }

        /// <summary>
        /// Draws the selected resolution.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="resolution">The resolution.</param>
        public void DrawResolution(SpriteBatch spriteBatch, ScreenResultions resolution)
        {            
            drawRectangle = new Rectangle(x, y, WIDTH, HEIGHT);
            Rectangle sourceRectangle = new Rectangle(0, (int)resolution * HEIGHT, WIDTH, HEIGHT);
            
            spriteBatch.Draw(sprite, new Vector2(drawRectangle.X, drawRectangle.Y), sourceRectangle, Color.White, 0.0f, Vector2.Zero, 0.15f, SpriteEffects.None, 0);
        }
    }
}
