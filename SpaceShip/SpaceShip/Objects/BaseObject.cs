using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShip.Objects
{
    /// <summary>
    /// BaseUiObject for all objects in game
    /// </summary>
    class BaseUiObject
    {
        /// <summary>
        /// IsActive flag
        /// </summary>
        bool isActive = true;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }      

        /// <summary>
        /// Sprite for our UI object
        /// </summary>
        protected Texture2D sprite;

        /// <summary>
        /// Drawrectangle
        /// </summary>
        protected Rectangle drawRectangle;

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public virtual void Update(GameTime gameTime)
        {
            /*
            if (isActive)
            {
            }
            */
        }

        /// <summary>
        /// Draw handler
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="gameTime">GameTime</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isActive)
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.White);
            }            
        }
    }
}