using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceShip.Classes.XML;
using Microsoft.Xna.Framework;

namespace TestSpaceShip
{
    [TestClass]
    public class TestSpaceShip
    {
        [TestMethod]
        public void TestParser()
        {
            var loader = new LevelLoader();
            loader.LevelInfos = new LevelInformation();

            loader.LevelInfos.EnemyInfos = new System.Collections.Generic.List<EnemyObject>();
            var infoElement = new EnemyObject()
            {
                Info = new EnemyInfo()
                {
                    EnemyType = SpaceShip.Objects.EnemyType.Blue,
                    Position = new Vector2(0, 0),
                    Velocity = new Vector2(0, 0),
                }
            };

            loader.LevelInfos.EnemyInfos.Add(infoElement);

            loader.SaveToFile(@"c:\temp\loaderTest1.xml");
        }
    }
}
