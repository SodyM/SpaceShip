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

        bool enterPressed = false;
        bool enterReleased = false;

        //int MENU_FRAMERATE = 200;
        //int MENU_WIDTH = 120;
        //int MENU_HEIGHT = 20;
        //int FRAMECOUNT = 2;

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
            int top = device.Viewport.Height / 2;

            credits.Add(new Text(contentManager, device, GameConstants.LINE1, left - line1_padding, top));
            credits.Add(new Text(contentManager, device, GameConstants.LINE2, left - line2_padding, top + line2_top_padding));


            //menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top),
            //            contentManager.Load<Texture2D>(AssetsConstants.GAME_OVER), MENU_FRAMERATE));
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
            base.Draw(spriteBatch, gameTime);
        }        
    }
}