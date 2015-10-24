using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShip.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        bool upPressed = false;
        bool upReleased = false;

        bool downPressed = false;
        bool downReleased = false;

        bool enterPressed = false;
        bool enterReleased = false;

        bool leftPressed = false;
        bool leftReleased = false;

        bool rightPressed = false;
        bool rightReleased = false;

        int MENU_FRAMERATE = 200;
        int MENU_WIDTH = 160;
        int MENU_HEIGHT = 20;
        int FRAMECOUNT = 2;
        int STEP = 50;
        int selectedItemIndex = 0;

        int MENU_ITEM_VALUE_RIGHT_PADDING = 150;


        int soundVolume = 50;
        int musicVolume = 50;

        List<Text> optionSettings;
        List<Number> numberSettings;


        Resolution selectedResolution;

        bool fullScreenOn = false;

        AudioCategory musicCategory;
        AudioEngine engine;

        ScreenResultions currentResolution = ScreenResultions.R800x600;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsView"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="device">The device.</param>
        /// <param name="game">The game.</param>
        /// <param name="soundBank">The sound bank.</param>
        public SettingsView(ContentManager contentManager, GraphicsDevice device, SpaceShipGame game, SoundBank soundBank)
            : base(contentManager, device, game, soundBank)
        {

            // Get the category.
            engine = new AudioEngine("Content\\SpaceShip.xgs");
            musicCategory = engine.GetCategory("Music");


            optionSettings = new List<Text>();
            numberSettings = new List<Number>();
            
            this.soundBank = soundBank;


            InitSettingsPage(contentManager, device);

        }

        private void InitSettingsPage(ContentManager contentManager, GraphicsDevice device)
        {
            Trace.WriteLine("device.Viewport: " + device.Viewport);

            int left = device.Viewport.Width / 2 - MENU_WIDTH / 2;
            int top = device.Viewport.Height / 2 - (MENU_HEIGHT + STEP * 4) / 2;

            
            menuItems.Clear();
            optionSettings.Clear();
            numberSettings.Clear();

            // load text + texture for RESOLUTION option menu
            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top),
                contentManager.Load<Texture2D>(AssetsConstants.MENU_RESOLUTION), MENU_FRAMERATE));

            // load text + textures for selectec resolution
            selectedResolution = new Resolution(contentManager, device, left + MENU_ITEM_VALUE_RIGHT_PADDING, top);

            //optionSettings.Add(new Text(contentManager, device, "michal", left + MENU_ITEM_VALUE_RIGHT_PADDING, top));
            //optionSettings.Add(new Text(contentManager, device, "michal", left + MENU_ITEM_VALUE_RIGHT_PADDING, top));
            

            // load text + texture for FULL SCREEN option menu
            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top + STEP),
                contentManager.Load<Texture2D>(AssetsConstants.MENU_FULLSCREEN), MENU_FRAMERATE));

            optionSettings.Add(new Text(contentManager, device, "OFF", left + MENU_ITEM_VALUE_RIGHT_PADDING, top + STEP + 3));

            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top + 2 * STEP),
                contentManager.Load<Texture2D>(AssetsConstants.MENU_SOUND), MENU_FRAMERATE));

            // number for sound volume
            numberSettings.Add(new Number(contentManager, device, soundVolume, left + MENU_ITEM_VALUE_RIGHT_PADDING, top + 2 * STEP + 3));

            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top + 3 * STEP),
                contentManager.Load<Texture2D>(AssetsConstants.MENU_MUSIC), MENU_FRAMERATE));

            // number for music volume
            numberSettings.Add(new Number(contentManager, device, musicVolume, left + MENU_ITEM_VALUE_RIGHT_PADDING, top + 3 * STEP + 3));

            menuItems.Add(new AnimatedUiObject(FRAMECOUNT, MENU_WIDTH, MENU_HEIGHT, new Vector2(left, top + 4 * STEP),
                contentManager.Load<Texture2D>(AssetsConstants.MENU_BACK), MENU_FRAMERATE));
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
        /// Draw handler
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var item in optionSettings)
            {                
               item.DrawText(spriteBatch, TextColor.Yellow);
            }

            foreach (var item in numberSettings)
            {
                item.DrawText(spriteBatch);
            }

            selectedResolution.DrawResolution(spriteBatch, currentResolution);

            base.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Handles the keyboard input. Pressing ESC key will change game state to MENU_MAIN
        /// </summary>
        public override void HandleKeyboardInput()
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
                    if (selectedItemIndex == 4)
                        game.ChangeGameState(GameState.MENU_MAIN);

                    enterReleased = false;
                    enterPressed = false;
                }
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                leftPressed = true;
                leftReleased = false;
            }
            else if (keyState.IsKeyUp(Keys.Left))
            {
                leftReleased = true;
                if (leftPressed && leftReleased)
                {
                    Left();
                    leftReleased = false;
                    leftPressed = false;
                }
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                rightPressed = true;
                rightReleased = false;
            }
            else if (keyState.IsKeyUp(Keys.Right))
            {
                rightReleased = true;
                if (rightPressed && rightReleased)
                {
                    Right();
                    rightReleased = false;
                    rightPressed = false;
                }
            }

            base.HandleKeyboardInput();
        }

        /// <summary>
        /// Updates the selected option.
        /// </summary>
        /// <param name="pressedRight">if set to <c>true</c> [pressed right].</param>
        void UpdateSelectedOption(bool pressedRight)
        {
            if (selectedItemIndex == 0)
            {
                UpdateTextValueOfActualMenuItem(optionSettings[selectedItemIndex], pressedRight);
                InitSettingsPage(contentManager, device);
            }
            else if (selectedItemIndex == 1)
            {
                UpdateTextValueOfActualMenuItem(optionSettings[selectedItemIndex - 1]);
            }
            else if (selectedItemIndex == 2)
            {                
                UpdateNumberValueOfActualMenuItem(numberSettings[0], pressedRight);
            }
            else if (selectedItemIndex == 3)
            {
                UpdateNumberValueOfActualMenuItem(numberSettings[1], pressedRight);
            }
        }

        private void UpdateTextValueOfActualMenuItem(Text text)
        {
            UpdateTextValueOfActualMenuItem(text, true);
        }

        /// <summary>
        /// Updates the resolution text - change ui resolution and change resolution text
        /// </summary>
        void UpdateResolutionText()
        {
            if (currentResolution == ScreenResultions.R800x600)
            {
                game.ChangeScreenResolution(800, 600);
            }
            else if (currentResolution == ScreenResultions.R1024x768)
            {
                game.ChangeScreenResolution(1024, 768);
            }
            else if (currentResolution == ScreenResultions.R1280x720)
            {
                game.ChangeScreenResolution(1280, 720);
            }
            else if (currentResolution == ScreenResultions.R1366x768)
            {
                game.ChangeScreenResolution(1366, 768);
            }

            // TODO: ! set correct text from RESOLUTIONS.PNG
        }

        /// <summary>
        /// Updates the text value of actual menu item.
        /// </summary>
        /// <param name="text">The text.</param>
        private void UpdateTextValueOfActualMenuItem(Text text, bool pressedRight)
        {
            if (selectedItemIndex == 0)
            {
                if (pressedRight)
                {
                    if (currentResolution < ScreenResultions.R1366x768)
                        currentResolution += 1;
                }
                else
                {
                    if (currentResolution > ScreenResultions.R800x600)
                        currentResolution -= 1;
                }
                UpdateResolutionText();
            }
            else if (selectedItemIndex == 1)
            {                
                if (fullScreenOn)
                    text.ChangeText("OFF");
                else
                    text.ChangeText("ON");

                game.SetFullScreen();
                fullScreenOn = !fullScreenOn;
            }
        }

        /// <summary>
        /// Updates the number value of actual menu item.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="pressedRight">if set to <c>true</c> [pressed right].</param>
        private void UpdateNumberValueOfActualMenuItem(Number number, bool pressedRight)
        {
            if (selectedItemIndex == 2)
            {                
                if (pressedRight)
                    ++this.soundVolume;                    
                else
                    --this.soundVolume;

                number.ChangeNumberValue(this.soundVolume);                
            }
            else if (selectedItemIndex == 3)
            {
                if (pressedRight)
                    ++this.musicVolume;
                else
                    --this.musicVolume;

                number.ChangeNumberValue(this.musicVolume); 
            }
        }

        /// <summary>
        /// Handler for RIGHT button click
        /// </summary>
        private void Right()
        {
            UpdateSelectedOption(true);
            PlayClick();
        }

        /// <summary>
        /// Handler for LEFT button click
        /// </summary>
        private void Left()
        {
            UpdateSelectedOption(false);
            PlayClick();
        }

        /// <summary>
        /// Handler for DOWN button click
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
        /// Handler for UP button click
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
    }
}
