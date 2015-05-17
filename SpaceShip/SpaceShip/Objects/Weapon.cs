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

        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponInfo"/> class.
        /// </summary>
        /// <param name="spriteName">Name of the sprite.</param>
        /// <param name="weaponType">Type of the weapon.</param>
        public WeaponInfo(string spriteName, WeaponType weaponType)
        {
            this.spriteName = spriteName;
            this.weaponType = weaponType;
        }
    }

    /// <summary>
    /// Weapon
    /// </summary>
    public class Weapon
    {
    }
}
