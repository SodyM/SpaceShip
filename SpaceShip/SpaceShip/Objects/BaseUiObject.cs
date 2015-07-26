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
        /// Gets the object rectangle.
        /// </summary>
        /// <value>
        /// The object rectangle.
        /// </value>
        public Rectangle ObjRectangle
        {
            get
            {
                return this.drawRectangle;
            }
        }

        /// <summary>
        /// Gets the location of UI object
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public Point Location
        {
            get { return drawRectangle.Center; }
        }

        /// <summary>
        /// Gets or sets the scale level of our ui object - use to change zoom level of ui object
        /// </summary>
        /// <value>
        /// The scale level.
        /// </value>
        public float ScaleLevel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUiObject"/> class.
        /// </summary>
        public BaseUiObject()
        {
            ScaleLevel = 1.0f;
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public virtual void Update(GameTime gameTime)
        {
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