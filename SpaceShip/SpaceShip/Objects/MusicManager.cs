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
        Cue creditsCue;
        SoundBank soundBank;
        GraphicsDevice graphics;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicManager"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="soundBank">The sound bank.</param>
        /// <param name="device">The device.</param>
        public MusicManager(ContentManager contentManager, SoundBank soundBank, GraphicsDevice device)
        {
            this.soundBank = soundBank;
            graphics = device;            
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
                StopMusic();
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
        public void StopMusic()
        {
            if (mainCue.IsPlaying)
                mainCue.Stop(AudioStopOptions.AsAuthored);

            musicIsActiv = false;
        }

        /// <summary>
        /// Plays the credits theme.
        /// </summary>
        public void PlayCreditsTheme()
        {
            creditsCue = soundBank.GetCue(AssetsConstants.CREDITS);
            if (!musicIsActiv)
            {
                creditsCue.Play();
            }

            musicIsActiv = true;
        }   
     
        public void StopCreditsTheme()
        {
            creditsCue.Stop(AudioStopOptions.Immediate);

            musicIsActiv = false;
        }
    }
}