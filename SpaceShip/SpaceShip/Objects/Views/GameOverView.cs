using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        bool enterPressed = false;
        bool enterReleased = false;

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


        /// <summary>
        /// Handles the keyboard input.
        /// </summary>
        public override void HandleKeyboardInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Enter))
            {
                enterPressed = true;
                enterReleased = false;
            }
            else if (keyState.IsKeyUp(Keys.Enter))
            {
                enterReleased = true;
                if (enterPressed && enterReleased)
                {
                    HandleSelectedMenuOption();
                    enterReleased = false;
                    enterPressed = false;
                }
            }

            base.HandleKeyboardInput();
        }


        /// <summary>
        /// Handles the selected menu option.
        /// </summary>
        private void HandleSelectedMenuOption()
        {
            //TODO: more options after game over
            game.ChangeGameState(GameState.MENU_MAIN);
        }
    }
}
