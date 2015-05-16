using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShip.Classes;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Musicmanager
    /// - takes care about playing main theme
    /// - can be switched on and off. Status is displayed in info line on the display
    /// </summary>
    class MusicManager
    {
        bool keyMPressed;
        bool keyMReleased;
        bool musicIsActiv;
        Text textHelper;

        Cue mainCue;
        SoundBank soundBank;
        GraphicsDevice graphics;

        public MusicManager(ContentManager contentManager, SoundBank soundBank, GraphicsDevice device)
        {
            this.soundBank = soundBank;
            graphics = device;
            PlayMainTheme();
            textHelper = new Text(contentManager, device);
        }

        /// <summary>
        /// Updates the music.
        /// </summary>
        /// <param name="keyboardState">State of the keyboard.</param>
        public void Update(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.M))
            {
                keyMPressed = true;
                keyMReleased = false;
            }
            else if (keyboardState.IsKeyUp(Keys.M))
            {
                keyMReleased = true;
                if (keyMPressed && keyMReleased)
                {
                    ChangeMusicState();
                    keyMPressed = false;
                    keyMReleased = false;
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string status = GameConstants.MUSIC_OFF;
            if (musicIsActiv)
                status = GameConstants.MUSIC_ON;

            textHelper.DrawText(spriteBatch, status, TextColor.Yellow, 
                GameConstants.WINDOW_WIDTH - GameConstants.MUSIC_STATUS_LEFT_MINUS_STEP, GameConstants.MUSIC_STATUS_TOP);
        }

        /// <summary>
        /// Changes the state of the music.
        /// </summary>
        private void ChangeMusicState()
        {
            if (musicIsActiv)
            {
                musicIsActiv = false;
                StopMainTheme();
            }
            else
            {
                musicIsActiv = true;
                if (mainCue.IsStopped)
                    PlayMainTheme();
            }            
        }

        /// <summary>
        /// Plays the main theme.
        /// </summary>
        public void PlayMainTheme()
        {
            musicIsActiv = true;
            mainCue = soundBank.GetCue(AssetsConstants.BACKGROUND_MUSIC);
            mainCue.Play();            
        }

        /// <summary>
        /// Stops the main theme.
        /// </summary>
        private void StopMainTheme()
        {
            if (mainCue.IsPlaying)
                mainCue.Stop(AudioStopOptions.AsAuthored);
        }
    }
}
