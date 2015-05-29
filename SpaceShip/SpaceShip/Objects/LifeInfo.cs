using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Lifeinfo
    /// Shows livebar together with actual count of lifes
    /// </summary>
    class LifeInfo : StaticUiObject
    {
        Texture2D life;
        int actualValue = 0;

        private ContentManager Content;
        private GraphicsDevice GraphicsDevice;
        private SpaceShipGame spaceShipGame;

        /// <summary>
        /// Initializes a new instance of the <see cref="LifeInfo"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="spaceShipGame">The space ship game.</param>
        public LifeInfo(ContentManager content, GraphicsDevice graphicsDevice, SpaceShipGame spaceShipGame)
        {
            this.Content = content;
            this.GraphicsDevice = graphicsDevice;
            this.spaceShipGame = spaceShipGame;            

            life = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            life.SetData<Color>(new Color[] { Color.Red });
            
            actualValue = GameConstants.MAXVALUE;
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            actualValue = spaceShipGame.GetPlayerHealth();
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw handler
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {                        
            spriteBatch.Draw(life, new Rectangle(GameConstants.LIFEBAR_LEFT, GameConstants.LIFEBAR_TOP, actualValue, GameConstants.HEIGHT), Color.White);
        }
    }
}