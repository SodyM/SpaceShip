using Microsoft.Xna.Framework;
using SpaceShip.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SpaceShip.Classes.XML
{

    [XmlType(TypeName = "EnemyObject")]
    public class EnemyObject
    {
        public EnemyInfo Info { get; set; }
        public EnemyTrigger Trigger { get; set; }
    }

    [XmlType(TypeName = "FileObject")]
    public class EnemyInfo
    {
        public EnemyType EnemyType { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
    }

    public enum EnemyTriggerType { Time, Action }

    [XmlType(TypeName = "EnemyTrigger")]
    public class EnemyTrigger
    {
        public EnemyTriggerType TriggerType { get; set; }
        public double Time { get; set; }
        public bool Active { get; set; }
    }
}
