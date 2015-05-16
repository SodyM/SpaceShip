using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Simple Infowwindow with "Respect"
    /// - show funny picture for few secs.
    /// - play respect sound
    /// </summary>
    class InfoWindow : StaticUiObject
    {
        int timer = 0;
        int mode = 0;
        Cue cue;
        SoundBank soundBank;
        bool soundIsActive = false;
        Vector2 position;

        string sound = AssetsConstants.RESPECT_SOUND;
        string image = AssetsConstants.RESPECT;


        public void SetDisplayContent(ContentManager contentManager, string image, string sound)
        {
            if (this.image == image)
                return;

            this.image = image;
            this.sound = sound;
            

            sprite = contentManager.Load<Texture2D>(this.image);
            base.Init(sprite.Width / 2, sprite.Height / 2, position);
            cue = soundBank.GetCue(this.sound);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoWindow"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="device">The device.</param>
        /// <param name="soundBank">The sound bank.</param>
        /// <param name="position">The position.</param>
        public InfoWindow(ContentManager contentManager, GraphicsDevice device, SoundBank soundBank, Vector2 position, string image, string sound)
        {
            this.soundBank = soundBank;
            this.position = position;
            this.image = image;
            this.sound = sound;

            IsActive = false;
            sprite = contentManager.Load<Texture2D>(this.image);
            base.Init(sprite.Width / 2, sprite.Height / 2, position);
            cue = soundBank.GetCue(this.sound);
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                PlaySound();

                // start countdown and display this window only for short time
                timer += gameTime.ElapsedGameTime.Milliseconds;
                if (timer > GameConstants.SHOW_INFOWINDOW_DELAY)
                {
                    IsActive = false;
                    timer = 0;
                    soundIsActive = false;
                    cue.Stop(AudioStopOptions.AsAuthored);   
                }
            }
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        void PlaySound()
        {
            if (!soundIsActive)
            {
                soundIsActive = true;
                cue = soundBank.GetCue(this.sound);
                cue.Play();
            }                
        }
    }
}
