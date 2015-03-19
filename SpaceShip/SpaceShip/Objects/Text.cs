using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Possible colors of text
    /// </summary>
    enum TextColor{ Blue, Red, Yellow}

    /// <summary>
    /// Textrender class
    /// </summary>
    class Text : BaseUiObject
    {
        const int CHARACTER_COUNT       = 26;
        const int WIDTH                 = 16;
        const int HEIGHT                = 16;
        const int BLUE_TYPE             = 0;
        const int RED_TYPE              = 20;
        const int YELLOW_TYPE           = 40;
        const int CHARACTER_SPACE_WIDTH = 20;

        const string CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string ASSET_NAME = "fonts";

        Dictionary<string, List<Rectangle>> textDictionary;
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        public Text(ContentManager contentManager, GraphicsDevice device)
        {
            sprite = contentManager.Load<Texture2D>(ASSET_NAME);
            textDictionary = new Dictionary<string, List<Rectangle>>();
        }

        /// <summary>
        /// Gets text from fonts.png image file
        /// </summary>
        /// <param name="text">Text to be created</param>
        /// <param name="textColor">Color of text</param>
        /// <returns>Coordinates of final text from image file</returns>
        List<Rectangle> GetTextFromTexture(string text, TextColor textColor)
        {
            int y_coordinate = BLUE_TYPE;                      // blue is default color
            if (textColor == TextColor.Red)
                y_coordinate = RED_TYPE;
            else if (textColor == TextColor.Yellow)
                y_coordinate = YELLOW_TYPE;

            List<Rectangle> result = new List<Rectangle>();
            foreach(var character in text)
            {
                int index = CHARACTERS.IndexOf(character);
                Rectangle rect = new Rectangle(index * WIDTH, y_coordinate, WIDTH, HEIGHT);
                result.Add(rect);
            }
            textDictionary.Add(text, result);

            return result;
        }

        /// <summary>
        /// Drawtext on ui
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="text">Text to be draw</param>
        /// <param name="textColor">Selected color</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public void DrawText(SpriteBatch spriteBatch, string text, TextColor textColor, int x, int y)
        {
            List<Rectangle> sourceRectangles = new List<Rectangle>();
            if (!textDictionary.ContainsKey(text))
                sourceRectangles = GetTextFromTexture(text, textColor);
            else
                textDictionary.TryGetValue(text, out sourceRectangles);

            drawRectangle = new Rectangle(x, y, WIDTH, HEIGHT);            

            foreach (var sourceRectangle in sourceRectangles)
            {
                drawRectangle.X += CHARACTER_SPACE_WIDTH;
                spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
            }        
        }
    }
}
