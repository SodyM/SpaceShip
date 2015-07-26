using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShip.Classes;
using System.Collections.Generic;

namespace SpaceShip.Objects.Views
{
    /// <summary>
    /// CreditsView
    /// Will display credits
    /// </summary>
    class CreditsView : BaseView
    {
        List<Text> credits;

        int line1_padding = 250;
        int line2_padding = 270;
        int line2_top_padding = 30;
        int line3_padding = 180;

        int image_left_padding = 100;
        int image_size = 200;

        int top = 10;
        int bottom = 390;
        

        Texture2D sprite_god, sprite_pray;


        /// <summary>
        /// Initializes a new instance of the <see cref="CreditsView"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="device">The device.</param>
        /// <param name="game">The game.</param>
        /// <param name="soundBank">The sound bank.</param>
        public CreditsView(ContentManager contentManager, GraphicsDevice device, SpaceShipGame game, SoundBank soundBank)
            : base(contentManager, device, game, soundBank)
        {
            this.soundBank = soundBank;
            credits = new List<Text>();
            
            int left = device.Viewport.Width / 2;
            int top = device.Viewport.Height / 2 - 30;

            credits.Add(new Text(contentManager, device, GameConstants.LINE1, left - line1_padding, top));
            credits.Add(new Text(contentManager, device, GameConstants.LINE2, left - line2_padding, top + line2_top_padding));
            credits.Add(new Text(contentManager, device, GameConstants.LINE3, left - line3_padding, top + 2 *(line2_top_padding)));

            sprite_god = contentManager.Load<Texture2D>(AssetsConstants.GOD);
            sprite_pray = contentManager.Load<Texture2D>(AssetsConstants.PRAY);
        }

        /// <summary>
        /// Draw handler
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (var item in credits)
            {
                item.DrawText(spriteBatch, TextColor.Blue);    
            }

            spriteBatch.Draw(sprite_god, new Rectangle(device.Viewport.Width / 2 - image_left_padding, top, image_size, image_size), Color.White);
            spriteBatch.Draw(sprite_pray, new Rectangle(device.Viewport.Width / 2 - image_left_padding, bottom, image_size, image_size), Color.White);

            base.Draw(spriteBatch, gameTime);
        }        
    }
}