using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;
using SpaceShip.Classes.XML;
using System;
using System.Collections.Generic;

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
        SoundBank soundBank;
        SpaceShipGame thisGame;
        EnemyType enemyType;
        const int START_FRAMERATE = 90;
        const int FRAMES_COUNT = 6;
        const int HEIGHT = 30;
        const int WIDTH = 40;
        int firingDelay = 5000;
        int elapsedShotTime = 0;
        int refreshTargetDelay = 1500;
        int elapsedRefreshTime = 0;
        // velocity information
        Vector2 velocity = new Vector2(0, 0);
        List<Vector2> waypoints = new List<Vector2>();
        AnimatedUiObject target;

        Weapon weapon;

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
        public Enemy(ContentManager contentManager, GraphicsDevice device, Vector2 position, Vector2 velocity, EnemyType enemyType, SpaceShipGame game, SoundBank soundBank)
        {
            this.soundBank = soundBank;
            thisGame = game;
            this.enemyType = enemyType;
            this.velocity = velocity;
            string textureSet = GetEnemyType(enemyType);            
            sprite = contentManager.Load<Texture2D>(textureSet);

            base.Init(FRAMES_COUNT, WIDTH, HEIGHT, position);
            this.Health = 50; //TODO: Health depends on enemy type
            base.ChangeFrameRate(START_FRAMERATE);

            var weaponInfo = new WeaponInfo(AssetsConstants.ENEMY_LASER, GameConstants.PLAYER_LASER_SPEED, 0, WeaponType.Laser);
            var laserTexture = thisGame.GetTextureForName(AssetsConstants.ENEMY_LASER);
            weapon = new Weapon(laserTexture, weaponInfo, game, this, ProjectileSource.Enemy);

            firingDelay = GetRandomFiringDelay();
        }

        /// <summary>
        /// Makes the enemy fly towards the target
        /// </summary>
        /// <param name="target"></param>
        public void SetTargetLocation(Vector2 target)
        {
            //position.X = drawRectangle.Center.X;
            //position.Y = drawRectangle.Center.Y;
            velocity = Vector2.Normalize(target - position) * 0.15f;//BaseSpeed
        }

        public void SetTarget(AnimatedUiObject target)
        {
            this.target = target;
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

            //Todo: use flag to follow target
            //elapsedRefreshTime += elapsedTime;
            //if ((target != null) && (target.IsActive) & (elapsedRefreshTime > refreshTargetDelay))
            //{
            //    SetTargetLocation(new Vector2(target.Location.X, target.Location.Y));
            //    elapsedRefreshTime = 0;
            //} 
            
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

                   

                    //var projectileSprite = AssetsConstants.ENEMY_LASER;

                    //var projectileVelocity = new Vector2()
                    //{
                    //    X = -GameConstants.Enemy_LASER_SPEED - velocity.X,
                    //    Y = 0
                    //};
                    weapon.SetSpeed(-GameConstants.Enemy_LASER_SPEED + velocity.X);
                    weapon.Fire(position);

                    //thisGame.AddProjectile(position, projectileVelocity, projectileSprite, 0, ProjectileSource.Enemy);
                    soundBank.PlayCue(AssetsConstants.ENEMY_LASER_FIRE);
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
