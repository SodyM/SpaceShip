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
        int lifespan = 0;
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
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponInfo"/> class.
        /// </summary>
        /// <param name="spriteName">Name of the sprite.</param>
        /// <param name="weaponType">Type of the weapon.</param>
        public WeaponInfo(string spriteName, float speed, WeaponType weaponType)
        {
            this.spriteName = spriteName;
            this.weaponType = weaponType;
            this.speed = speed;
        }
    }

    /// <summary>
    /// Weapon
    /// </summary>
    class Weapon : BaseUiObject
    {
        SpaceShipGame thisGame;
        WeaponInfo weaponInfo;

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
        public Weapon(Texture2D sprite, WeaponInfo weaponInfo, SpaceShipGame game, AnimatedUiObject owner)
        {
            thisGame = game;
            this.sprite = sprite;
            this.weaponInfo = weaponInfo;
            this.owner = owner;
        }

        /// <summary>
        /// Update handler
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
        }
        //Vector2 position

        /// <summary>
        /// Firing of the weapon
        /// </summary>
        public void Fire()
        {
            Vector2 position = new Vector2();
            position.X = owner.position.X + WIDTH + 18;//Todo: dynmic offset 
            position.Y = owner.position.Y + 7;

            var velocity = new Vector2()
            {
                X = this.weaponInfo.Speed,
                Y = 0
            };

            string projectileSprite = AssetsConstants.LASER;
            //Todo: Lifespan und Damage übergeben + Weapoinfos
            thisGame.AddProjectile(position, velocity, projectileSprite, ProjectileSource.Player);
            //soundBank.PlayCue(AssetsConstants.LASER_FIRE);
        }
    }
}
