using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShip.Objects
{
    /// <summary>
    /// StaticUiObject - base class for not moving object without an animation
    /// </summary>
    class StaticUiObject : BaseUiObject
    {
        Vector2 position;

        /// <summary>
        /// Initializes the specified width.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="position">The position.</param>
        public void Init(int width, int height, Vector2 position)
        {
            this.position = position;
            
            // center it
            this.position.X = position.X - sprite.Width / 2;
            this.position.Y = position.Y - sprite.Height / 2;

            // set draw rectangle
            drawRectangle = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), width, height);
        }
    }
}
