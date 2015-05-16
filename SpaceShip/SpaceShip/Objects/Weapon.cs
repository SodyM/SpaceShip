using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShip.Objects
{
    public enum WeaponType
    {
        Laser, Rocket
    }

    public class WeaponInfo
    {
        string spriteName;
        WeaponType weaponType;

        public WeaponInfo(string spriteName, WeaponType weaponType)
        {
            this.spriteName = spriteName;
            this.weaponType = weaponType;
        }
    }



    public class Weapon
    {
    }
}
