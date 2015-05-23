using Microsoft.Xna.Framework;
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
    /// SettingsView
    /// Will display all settings for resultion etc.
    /// </summary>
    class SettingsView : BaseView
    {
        int MENU_FRAMERATE = 200;
        int MENU_WIDTH = 120;
        int MENU_HEIGHT = 20;
        int FRAMECOUNT = 2;

        public SettingsView(ContentManager contentManager, GraphicsDevice device, SpaceShipGame game, SoundBank soundBank)
            : base(contentManager, device, game, soundBank)
        {
            this.soundBank = soundBank;

            int left = device.Viewport.Width / 2 - MENU_WIDTH / 2;
            int top = device.Viewport.Height / 2 - MENU_HEIGHT / 2;

            //menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top),
            //            contentManager.Load<Texture2D>(AssetsConstants.GAME_OVER), MENU_FRAMERATE));
        }
    }
}
