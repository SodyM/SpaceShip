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
    class Number : BaseUiObject
    {
        const int NUMBER_COUNT = 10;
        const int WIDTH = 90;
        const int HEIGHT = 90;
        const int CHARACTER_SPACE_WIDTH = 15;

        Dictionary<string, List<Rectangle>> numbersDictionary;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        public Number(ContentManager contentManager, GraphicsDevice device)
        {
            sprite = contentManager.Load<Texture2D>(AssetsConstants.NUMBERS);
        }

        public void DrawNumber(SpriteBatch spriteBatch, int score, int x, int y)
        {
            List<Rectangle> sourceRectangles = new List<Rectangle>();
            string text = score.ToString();

            //if (!numbersDictionary.ContainsKey(text))
                sourceRectangles = GetNumberFromScore(score.ToString());
            //else
              //  numbersDictionary.TryGetValue(text, out sourceRectangles);

            drawRectangle = new Rectangle(x, y, WIDTH, HEIGHT);

            foreach (var sourceRectangle in sourceRectangles)
            {
                drawRectangle.X += CHARACTER_SPACE_WIDTH;
                drawRectangle.Height = 15;
                drawRectangle.Width = 15;
                spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
            }
        }

        private List<Rectangle> GetNumberFromScore(string text)
        {
            List<Rectangle> result = new List<Rectangle>();
            for (int i = 0; i < text.Length; i++ )
            {
                string character = text[i].ToString();
                int index = Convert.ToInt16(character);
                Rectangle rect = new Rectangle(index * WIDTH, 0, WIDTH, HEIGHT);
                result.Add(rect);
            }
            //numbersDictionary.Add(text, result);

            return result;
        }
    }
}
