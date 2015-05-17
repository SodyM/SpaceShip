﻿using Microsoft.Xna.Framework;
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
        SpaceShipGame thisGame;
        EnemyType enemyType;
        const int START_FRAMERATE = 90;
        const int FRAMES_COUNT = 6;
        const int HEIGHT = 30;
        const int WIDTH = 40;
        int firingDelay = 5000;
        int elapsedShotTime = 0;
        // velocity information
        Vector2 velocity = new Vector2(0, 0);


        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
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
        public Enemy(ContentManager contentManager, GraphicsDevice device, Vector2 position, Vector2 velocity, EnemyType enemyType, SpaceShipGame game)
        {
            thisGame = game;
            this.enemyType = enemyType;
            this.velocity = velocity;
            string textureSet = GetEnemyType(enemyType);
            sprite = contentManager.Load<Texture2D>(textureSet);

            base.Init(FRAMES_COUNT, WIDTH, HEIGHT, position);
            this.Health = 50; //TODO: Health depends on enemy type
            base.ChangeFrameRate(START_FRAMERATE);

            firingDelay = GetRandomFiringDelay();
        }


        /// <summary>
        /// Gets the score of current enemy
        /// </summary>
        /// <returns>Score for given enemy</returns>
        public int GetScore()
        {
            switch (enemyType)
            {
                case EnemyType.Blue: return 10;
                case EnemyType.Cyan: return 20;
                case EnemyType.Green: return 30;
                case EnemyType.Yellow: return 40;
                case EnemyType.Red: return 40;
            }
            return 10;
        }

        /// <summary>
        /// Helpermethod to avoid small velocities that get truncated to 0
        /// </summary>
        /// <param name="elapsedTime"></param>
        /// <param name="velocityUpdate"></param>
        /// <returns></returns>
        private float GetLocationChange(int elapsedTime, float velocityUpdate)
        {
            var change = (velocityUpdate * elapsedTime);
            if ((change < 0) && (change > -1))
                change = -1;
            if ((change > 0) && (change < 1))
                change = 1;

            return change;
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            if (!this.IsActive)
                return;

            int elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
            var xChange = GetLocationChange(elapsedTime, this.velocity.X);
            var yChange = GetLocationChange(elapsedTime, this.velocity.Y);

            position.X += (int)xChange;
            position.Y += (int)yChange;// (this.velocity.Y * elapsedTime);

            //TODO: flag to signal that enemy was visible; Do not deactivate when enemy is spawn in an invisible area!

            if (position.X < 0)
                this.IsActive = false;

            if (position.Y < 10 + HEIGHT)
                position.Y = 10 + HEIGHT;

            //Enemy spawns to the right of the visible area
            //if (position.X > GameConstants.WINDOW_WIDTH - WIDTH)
            //    this.IsActive = false;


            drawRectangle.X = (int)position.X;
            drawRectangle.Y = (int)position.Y;

            BounceTopBottom();
            if (GameConstants.ENEMIES_SHOOT)
            {
                elapsedShotTime += elapsedTime;
                if (elapsedShotTime > firingDelay)
                {
                    elapsedShotTime = 0;
                    firingDelay = GetRandomFiringDelay();
                    var projectileSprite = AssetsConstants.LASER;

                    var projectileVelocity = new Vector2()
                    {
                        X = -GameConstants.Enemy_LASER_SPEED - velocity.X,
                        Y = 0
                    };

                    thisGame.AddProjectile(position, projectileVelocity, projectileSprite, ProjectileSource.Enemy);
                    //this.shootSound.Play();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Will make the enemy change its direction whenever the upper or lower boundary is hit
        /// </summary>
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

        /// <summary>
        /// Gets the random firing delay.
        /// </summary>
        /// <returns></returns>
        private int GetRandomFiringDelay()
        {
            return GameConstants.ENEMY_MIN_FIRE_DELAY +
                RandomNumberGenerator.Next(GameConstants.ENEMY_FIRE_DELAY_RANGE);
        }

        /// <summary>
        /// Gets the projectile velocity.
        /// </summary>
        /// <returns></returns>
        private float GetProjectileVelocity()
        {
            if (velocity.Y > 0)
            {
                return velocity.Y + GameConstants.ENEMY_PROJECTILE_SPEED;
            }
            else
            {
                return GameConstants.ENEMY_PROJECTILE_SPEED;
            }
        }
    }
}
