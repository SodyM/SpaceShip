using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShip.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShip.Objects.Views
{
    /// <summary>
    /// BaseView - basic window for menu pages
    /// Contains main functions like Update, Draw and HandleKeyInput
    /// Pressing ESC key will change game state to MENU_MAIN
    /// </summary>
    class BaseView : BaseUiObject
    {
        protected ContentManager contentManager;
        protected GraphicsDevice device;
        protected SpaceShipGame game;
        protected SoundBank soundBank;
        protected List<AnimatedUiObject> menuItems;

        bool escPressed = false;
        bool escReleased = false;


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseView"/> class. It's a base menu page
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="device">The device.</param>
        /// <param name="game">The game.</param>
        /// <param name="soundBank">The sound bank.</param>
        public BaseView(ContentManager contentManager, GraphicsDevice device, SpaceShipGame game, SoundBank soundBank)
        {
            this.contentManager = contentManager;
            this.device = device;
            this.game = game;
            this.soundBank = soundBank;

            menuItems = new List<AnimatedUiObject>();
        }

        /// <summary>
        /// Plays the click.
        /// </summary>
        protected void PlayClick()
        {
            soundBank.PlayCue(AssetsConstants.MENU_CLICK);
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            HandleKeyboardInput();

            foreach (var item in menuItems)
            {
                item.Update(gameTime);
            }
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

        /// <summary>
        /// Handles the keyboard input. Pressing ESC key will change game state to MENU_MAIN
        /// </summary>
        public virtual void HandleKeyboardInput()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
            {
                escPressed = true;
                escReleased = false;
            }
            else if (keyState.IsKeyUp(Keys.Escape))
            {
                escReleased = true;
                if (escPressed && escReleased)
                {
                    escPressed = false;
                    escReleased = false;

                    GameState currentGameState = game.GetCurrentGameState();
                    if (currentGameState == GameState.MENU_MAIN)
                    {
                        this.game.ChangeGameState(GameState.QUIT);
                    }                    
                    else
                    {
                        this.game.ChangeGameState(GameState.MENU_MAIN);
                    }
                }
            }
        }
    }
}
