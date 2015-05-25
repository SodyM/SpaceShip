using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;

namespace SpaceShip.Objects.Views
{
    /// <summary>
    /// GameOverView
    /// Will display game over
    /// </summary>
    class GameOverView : BaseView
    {        
        int MENU_FRAMERATE = 200;
        int MENU_WIDTH = 120;
        int MENU_HEIGHT = 20;
        int FRAMECOUNT = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverView"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="device">The device.</param>
        /// <param name="game">The game.</param>
        /// <param name="soundBank">The sound bank.</param>
        public GameOverView(ContentManager contentManager, GraphicsDevice device, SpaceShipGame game, SoundBank soundBank)
            : base(contentManager, device, game, soundBank)
        {
            this.soundBank = soundBank;

            int left = device.Viewport.Width / 2 - MENU_WIDTH / 2;
            int top = device.Viewport.Height / 2 - MENU_HEIGHT / 2;

            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top),
                        contentManager.Load<Texture2D>(AssetsConstants.GAME_OVER), MENU_FRAMERATE));
        }
    }
}
