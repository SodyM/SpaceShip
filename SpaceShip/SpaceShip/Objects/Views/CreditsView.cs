using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public CreditsView(ContentManager contentManager, GraphicsDevice device, SpaceShipGame game, SoundBank soundBank)
            : base(contentManager, device, game, soundBank)
        {
            this.soundBank = soundBank;
            credits = new List<Text>();
            
            int left = device.Viewport.Width / 2;
            int top = device.Viewport.Height / 2;

            credits.Add(new Text(contentManager, device, GameConstants.LINE1, left - line1_padding, top));
            credits.Add(new Text(contentManager, device, GameConstants.LINE2, left - line2_padding, top + line2_top_padding));            
        }

        public override void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (var item in credits)
            {
                item.DrawText(spriteBatch, TextColor.Blue);    
            }
            

            base.Draw(spriteBatch, gameTime);
        }
    }
}
