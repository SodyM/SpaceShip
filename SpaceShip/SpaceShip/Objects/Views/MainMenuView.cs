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
    /// MainMenuView
    /// - display main menu view with standard menu options
    /// </summary>
    class MainMenuView : StaticUiObject
    {
        bool upPressed = false;
        bool upReleased = false;

        bool downPressed = false;
        bool downReleased = false;

        bool enterPressed = false;
        bool enterReleased = false;
        
        int selectedItemIndex = 0;

        List<AnimatedUiObject> menuItems;
        SpaceShipGame thisGame;
        SoundBank soundBank;

        int MENU_FRAMERATE = 200;
        int MENU_WIDTH = 120;
        int MENU_HEIGHT = 20;
        int STEP = 50;
        int FRAMECOUNT = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuView"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="device">The device.</param>
        /// <param name="game">The game.</param>
        public MainMenuView(ContentManager contentManager, GraphicsDevice device, SpaceShipGame game, SoundBank soundBank)
        {
            thisGame = game;
            this.soundBank = soundBank;

            menuItems = new List<AnimatedUiObject>();

            int left = device.Viewport.Width / 2 - MENU_WIDTH / 2;
            int top = device.Viewport.Height / 2 - (MENU_HEIGHT + STEP * 4) / 2;

            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top), 
                contentManager.Load<Texture2D>(AssetsConstants.MENU_NEW_GAME), MENU_FRAMERATE));

            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top + STEP), 
                contentManager.Load<Texture2D>(AssetsConstants.MENU_SETTINGS), MENU_FRAMERATE));

            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top + 2 * STEP), 
                contentManager.Load<Texture2D>(AssetsConstants.MENU_CREDITS), MENU_FRAMERATE));

            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top + 3 * STEP), 
                contentManager.Load<Texture2D>(AssetsConstants.MENU_QUIT), MENU_FRAMERATE));
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            HandleKeyboardInput();

            for (int i = 0; i < menuItems.Count; i++)
            {
                var item = menuItems[i];
                item.IsActive = true;
                if (i == selectedItemIndex)
                {                    
                    item.RestartAnimationAfterStop();
                }
                else
                {
                    item.StopAnimationAndReset();
                }
                item.Update(gameTime);
            }
        }

        /// <summary>
        /// Plays the click.
        /// </summary>
        void PlayClick()
        {
            soundBank.PlayCue(AssetsConstants.MENU_CLICK);
        }

        /// <summary>
        /// Handles the keyboard input.
        /// </summary>
        private void HandleKeyboardInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
            {
                upPressed = true;
                upReleased = false;
            }
            else if (keyState.IsKeyUp(Keys.Up))
            {
                upReleased = true;
                if (upPressed && upReleased)
                {
                    Up();
                    upReleased = false;
                    upPressed = false;
                }
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                downPressed = true;
                downReleased = false;
            }
            else if (keyState.IsKeyUp(Keys.Down))
            {
                downReleased = true;
                if (downPressed && downReleased)
                {
                    Down();
                    downReleased = false;
                    downPressed = false;
                }
            }

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
        }

        /// <summary>
        /// Handles the selected menu option.
        /// </summary>
        private void HandleSelectedMenuOption()
        {
            if (selectedItemIndex == 0)
            {
                thisGame.ChangeGameState(GameState.START_NEW_GAME);
            }
            else if (selectedItemIndex == 1)
            {
                thisGame.ChangeGameState(GameState.MENU_DISPLAY_SETTINGS);
            }
            else if (selectedItemIndex == 2)
            {
                thisGame.ChangeGameState(GameState.MENU_CREDITS);
            }
            else if (selectedItemIndex == 3)
            {
                thisGame.ChangeGameState(GameState.QUIT);
            }
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        private void Down()
        {
            if (selectedItemIndex == this.menuItems.Count - 1)
            {
                selectedItemIndex = 0;
            }
            else
            {
                ++selectedItemIndex;
            }
            PlayClick();
        }

        /// <summary>
        /// Ups this instance.
        /// </summary>
        private void Up()
        {
            if (selectedItemIndex == 0)
            {
                selectedItemIndex = this.menuItems.Count - 1;
            }
            else
            {
                --selectedItemIndex;
            }
            PlayClick();
        }

        /// <summary>
        /// Draw handler
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (AnimatedUiObject item in menuItems)
            {
                item.Draw(spriteBatch, gameTime);
            }
        }
    }
}