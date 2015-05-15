using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShip.Classes;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Player
    /// </summary>
    class Player : AnimatedUiObject
    {        
        const float VELOCITY_STEP = 10.0f;
        const float VELOCITY_SPEED = 0.98f;
        const int WIDTH = 64;
        const int HEIGHT = 29;
        const int COUNT_OF_FRAMES = 4;

        SoundBank soundBank;

        SpaceShipGame thisGame;
        Vector2 velocity;
        int windowHeight, windowWidth;        
        bool spacePressed;
        bool spaceReleased;        
        int score = 0;

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        /// <param name="position">Startposition of player</param>
        public Player(ContentManager contentManager, GraphicsDevice device, Vector2 position, SpaceShipGame game, SoundBank soundBank)
        {
            thisGame = game;
            sprite = contentManager.Load<Texture2D>(AssetsConstants.PLAYER);
            base.Init(COUNT_OF_FRAMES, WIDTH, HEIGHT, position);
            
            // set window dimensions
            windowHeight = device.Viewport.Height;
            windowWidth = device.Viewport.Width;
            this.soundBank = soundBank;         
        }
               
        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {            
            KeyboardState keyState = Keyboard.GetState();
            MoveHandler(keyState);
            FireHandler(keyState);

            position.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X += velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity *= VELOCITY_SPEED;

            // keep sapceship in window           
            if (position.X < 0)
                position.X = 0;

            if (position.X > windowWidth - WIDTH)
                position.X = windowWidth - WIDTH;

            if (position.Y < 0)
                position.Y = 0;
            
            if (position.Y > windowHeight - HEIGHT)
                position.Y = windowHeight - HEIGHT;

            if (position.Y < 10 + HEIGHT)
                position.Y = 10 + HEIGHT;

            drawRectangle.X = (int)position.X;
            drawRectangle.Y = (int)position.Y;

            base.Update(gameTime);
        }

        /// <summary>
        /// Handle movement
        /// </summary>
        /// <param name="keyState">KeyboardState</param>
        void MoveHandler(KeyboardState keyState)
        {
            // stick to this simple if statements...player should have a possibility to move up+left, up+right...etc
            if (keyState.IsKeyDown(Keys.Up))
                MoveUp();

            if (keyState.IsKeyDown(Keys.Down))
                MoveDown();

            if (keyState.IsKeyDown(Keys.Left))
                MoveLeft();

            if (keyState.IsKeyDown(Keys.Right))
                MoveRight();
        }

        /// <summary>
        /// Handle fire
        /// </summary>
        /// <param name="keyState">KeyboardState</param>
        void FireHandler(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Space))
            {
                spacePressed = true;
                spaceReleased = false;
            }
            else if (keyState.IsKeyUp(Keys.Space))
            {
                spaceReleased = true;
                if (spacePressed && spaceReleased)
                {
                    Fire();
                    spaceReleased = false;
                    spacePressed = false;
                }
            }
        }

        // <summary>
        /// Fire laser
        /// </summary>
        void Fire()
        {
            Vector2 position = new Vector2();
            position.X = this.position.X + WIDTH + 18;
            position.Y = this.position.Y + 7;

            thisGame.AddProjectile(position);
            soundBank.PlayCue(AssetsConstants.LASER_FIRE);
        }

        /// <summary>
        /// Move up ship
        /// </summary>
        void MoveUp()
        {
            velocity.Y -= VELOCITY_STEP;
        }

        /// <summary>
        /// Move down ship
        /// </summary>
        void MoveDown()
        {
            velocity.Y += VELOCITY_STEP;
        }

        /// <summary>
        /// Move right ship
        /// </summary>
        void MoveRight()
        {
            velocity.X += VELOCITY_STEP;
        }

        /// <summary>
        /// Move left sh
        /// </summary>
        void MoveLeft()
        {
            velocity.X -= VELOCITY_STEP;
        }
    }
}