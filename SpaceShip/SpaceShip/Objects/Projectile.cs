using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;
using System;

namespace SpaceShip.Objects
{

    /// <summary>
    /// ProjectileSource - helper for determination who fired our projectile
    /// </summary>
    public enum ProjectileSource
    { 
        Player, Enemy
    }

    /// <summary>
    /// ProjectileType - helper for type of projectile (blue - fired from player, red - fired from enemy)
    /// </summary>
    public enum ProjectileType
    {
        Player, Enemy
    }

    /// <summary>
    /// ProjectileInfos - helper for detailed definition of projectile extra infos -> future use
    /// </summary>
    public class ProjectileInfos
    { 
    }

    /// <summary>
    /// Projectile
    /// </summary>
    class Projectile : BaseUiObject
    {
        int windowHeight, windowWidth;
        Vector2 position;
        Vector2 velocity;
        Vector2 spriteOrigin;
        const float SPEED = 10.0f;
        const int WIDTH = 46;
        const int HEIGHT = 16;

        int damage = 0;
        double lifespan = 0;

        
        ProjectileSource projectileSource;
        public ProjectileSource SourceOfProjectile
        {
            get
            {
                return projectileSource;
            }
        }

        public int X_Position
        {
            get { return (int)position.X; }
        }

        public int TextureWidth
        {
            get { return sprite.Width; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        /// <param name="position">Start position of projectile</param>
        public Projectile(Texture2D sprite, GraphicsDevice device, Vector2 position, Vector2 velocity, double lifespan, ProjectileSource projectileSource)
        {
            this.sprite = sprite;//old: contentManager.Load<Texture2D>(AssetsConstants.LASER);
            this.position = position;
            this.velocity = velocity;
            this.projectileSource = projectileSource;
            this.lifespan = lifespan;

            // center it
            this.position.X = position.X - sprite.Width / 2;
            this.position.Y = position.Y - sprite.Height / 2;

            // set origin (center of sprite)
            spriteOrigin.X = (float)sprite.Width / 2;
            spriteOrigin.Y = (float)sprite.Height / 2;

            // set draw rectangle
            drawRectangle = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), WIDTH, HEIGHT);

            // set window dimensions
            windowHeight = device.Viewport.Height;
            windowWidth = device.Viewport.Width;
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            var elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X += velocity.X;// *elapsedGameTime;
            drawRectangle.X = (int)position.X;

            if (lifespan > 0)
            {
                lifespan -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if (lifespan <= 0)
                    this.IsActive = false;
            }
        }
    }
}