using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        const string ASSET_NAME_YELLOW = "ship1_40x30";
        const string ASSET_NAME_RED = "ship2_40x30";
        const string ASSET_NAME_CYAN = "ship3_40x30";
        const string ASSET_NAME_BLUE = "ship4_40x30";
        const string ASSET_NAME_GREEN = "ship5_40x30";

        /// <summary>
        /// Get correct image file for given enemy type
        /// </summary>
        /// <param name="enemyType">EnemyType</param>
        /// <returns>Image filename</returns>
        string GetEnemyType(EnemyType enemyType)
        {
            if (enemyType.Equals(EnemyType.Yellow))
                return ASSET_NAME_YELLOW;
            else if (enemyType.Equals(EnemyType.Red))
                return ASSET_NAME_RED;
            else if (enemyType.Equals(EnemyType.Cyan))
                return ASSET_NAME_CYAN;
            else if (enemyType.Equals(EnemyType.Blue))
                return ASSET_NAME_BLUE;
            else if (enemyType.Equals(EnemyType.Green))
                return ASSET_NAME_GREEN;
            else
                return ASSET_NAME_YELLOW;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">ContentManager</param>
        /// <param name="device">GraphicsDevice</param>
        /// <param name="position">Startposition</param>
        /// <param name="enemyType">EnemyType</param>
        public Enemy(ContentManager contentManager, GraphicsDevice device, Vector2 position, EnemyType enemyType)
        {
            this.enemyType = enemyType;
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
    }
}
