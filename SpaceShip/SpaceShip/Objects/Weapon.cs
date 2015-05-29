using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShip.Objects
{
    /// <summary>
    /// WeaponType - simple enum for types of weapons -> future use
    /// </summary>
    public enum WeaponType
    {
        Laser, Rocket
    }

    /// <summary>
    /// WeaponInfo - will store all detailed informations about actual weapon
    /// </summary>
    public class WeaponInfo
    {
        string spriteName;
        WeaponType weaponType;
        double lifespan = 0;
        float speed;

        /// <summary>
        /// Property for the speed of this weapon
        /// </summary>
        public float Speed
        {
            get 
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        /// <summary>
        /// Property for the spriteName of this weapon
        /// </summary>
        public string SpriteName
        {
            get
            {
                return spriteName;
            }
        }

        /// <summary>
        /// Property for the lifespan of this weapon
        /// </summary>
        public double Lifespan
        {
            get
            {
                return lifespan;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponInfo"/> class.
        /// </summary>
        /// <param name="spriteName">Name of the sprite.</param>
        /// <param name="weaponType">Type of the weapon.</param>
        public WeaponInfo(string spriteName, float speed, double lifespan, WeaponType weaponType)
        {
            this.spriteName = spriteName;
            this.weaponType = weaponType;
            this.speed = speed;
            this.lifespan = lifespan;
        }
    }

    /// <summary>
    /// Weapon
    /// </summary>
    class Weapon : BaseUiObject
    {
        SpaceShipGame thisGame;
        WeaponInfo weaponInfo;
        ProjectileSource source;

        //Key to fire weapon

        AnimatedUiObject owner;

        int windowHeight, windowWidth;
        //Vector2 position;
        const float SPEED = 10.0f;
        const int WIDTH = 46;
        const int HEIGHT = 16;

        
        /// <summary>
        /// Constructor for weapon
        /// </summary>
        /// <param name="sprite">Texture of the weapon</param>
        /// <param name="weaponInfo">information for the specific weapon</param>
        /// <param name="game">reference to game class</param>
        /// <param name="owner">The owner: for example the player</param>
        public Weapon(Texture2D sprite, WeaponInfo weaponInfo, SpaceShipGame game, AnimatedUiObject owner, ProjectileSource source)
        {
            thisGame = game;
            this.sprite = sprite;
            this.weaponInfo = weaponInfo;
            this.owner = owner;
            this.source = source;
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {

        }
        //Vector2 position

        public void SetSpeed(float newSpeed)
        {
            this.weaponInfo.Speed = newSpeed;
        }

        /// <summary>
        /// Firing of the weapon
        /// </summary>
        public void Fire(Vector2 position)
        {
            if (position == null)
            {
                position = new Vector2();
                position.X = owner.position.X + WIDTH + 18;//Todo: dynmic offset 
                position.Y = owner.position.Y + 7;
            }
            var velocity = new Vector2()
            {
                X = this.weaponInfo.Speed,
                Y = 0
            };

            string projectileSprite = AssetsConstants.LASER;
            //Todo: Lifespan und Damage übergeben + Weapoinfos
            thisGame.AddProjectile(position, velocity, this.weaponInfo.SpriteName, this.weaponInfo.Lifespan, source);
            //soundBank.PlayCue(AssetsConstants.LASER_FIRE);
        }
    }
}
