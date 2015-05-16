using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;

namespace SpaceShip.Objects
{    
    /// <summary>
    /// Available types of enemies
    /// </summary>
    public enum EnemyType { Yellow, Red, Cyan, Blue, Green };

    /// <summary>
    /// Enemy
    /// </summary>
    class Enemy : AnimatedUiObject
    {
        EnemyType enemyType;
        const int START_FRAMERATE = 90;
        const int FRAMES_COUNT = 6;
        const int HEIGHT = 30;
        const int WIDTH = 40;

        // velocity information
        Vector2 velocity = new Vector2(0, 0);


        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// Sets the x location of the center of the teddy bear
        /// </summary>
        public int X
        {
            set { drawRectangle.X = value - drawRectangle.Width / 2; }
        }

        /// <summary>
        /// Sets the y location of the center of the teddy bear
        /// </summary>
        public int Y
        {
            set { drawRectangle.Y = value - drawRectangle.Height / 2; }
        }

        /// <summary>
        /// Get correct image file for given enemy type
        /// </summary>
        /// <param name="enemyType">EnemyType</param>
        /// <returns>Image filename</returns>
        string GetEnemyType(EnemyType enemyType)
        {
            if (enemyType.Equals(EnemyType.Yellow))
                return AssetsConstants.ENEMY_YELLOW;
            else if (enemyType.Equals(EnemyType.Red))
                return AssetsConstants.ENEMY_RED;
            else if (enemyType.Equals(EnemyType.Cyan))
                return AssetsConstants.ENEMY_CYAN;
            else if (enemyType.Equals(EnemyType.Blue))
                return AssetsConstants.ENEMY_BLUE;
            else if (enemyType.Equals(EnemyType.Green))
                return AssetsConstants.ENEMY_GREEN;
            else
                return AssetsConstants.ENEMY_YELLOW;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        /// <param name="position">Startposition</param>
        /// <param name="enemyType">EnemyType</param>
        public Enemy(ContentManager contentManager, GraphicsDevice device, Vector2 position, Vector2 velocity, EnemyType enemyType)
        {
            this.enemyType = enemyType;
            this.velocity = velocity;
            string textureSet = GetEnemyType(enemyType);
            sprite = contentManager.Load<Texture2D>(textureSet);
            
            base.Init(FRAMES_COUNT, WIDTH, HEIGHT, position);
            base.ChangeFrameRate(START_FRAMERATE);
        }


        public int GetScore()
        {
            switch (enemyType)
            {
                case EnemyType.Blue: return 10; break;
                case EnemyType.Cyan: return 20; break;
                case EnemyType.Green: return 30; break;
                case EnemyType.Yellow: return 40; break;
                case EnemyType.Red: return 40; break;
            }
            return 10;
        }


        public override void Update(GameTime gameTime)
        {
            if (!this.IsActive)
                return;

            int elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
            position.X += (int)(this.velocity.X * elapsedTime);
            position.Y += (int)(this.velocity.Y * elapsedTime);

           

            // keep sapceship in window           
            if (position.X < 0)
                this.IsActive = false;

            if (position.X > GameConstants.WINDOW_WIDTH - WIDTH)
                this.IsActive = false;

            //if (position.Y < 0)
            //    this.IsActive = false;

            //if (position.Y > GameConstants.WINDOW_HEIGHT - HEIGHT)
            //    this.IsActive = false;

            //if (position.Y < 10 + HEIGHT)
                //position.Y = 10 + HEIGHT;

            drawRectangle.X = (int)position.X;
            drawRectangle.Y = (int)position.Y;

            BounceTopBottom();

            base.Update(gameTime);
        }

        private void BounceTopBottom()
        {
            if (drawRectangle.Y <= 0)
            {
                // bounce off top
                drawRectangle.Y = 0;
                velocity.Y *= -1;
            }
            else if ((drawRectangle.Y + drawRectangle.Height) > GameConstants.WINDOW_HEIGHT)
            {
                // bounce off bottom
                drawRectangle.Y = GameConstants.WINDOW_HEIGHT - drawRectangle.Height;
                velocity.Y *= -1;
            }
        }
    }
}
