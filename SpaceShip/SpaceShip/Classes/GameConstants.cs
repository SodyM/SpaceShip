using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShip.Classes
{
    /// <summary>
    /// GameConstants
    /// Place all game constants here
    /// </summary>
    public static class GameConstants
    {
        // UI & system
        public const int WINDOW_WIDTH                       = 800;
        public const int WINDOW_HEIGHT                      = 600;
        public const int SPAWN_BORDER_SIZE                  = 100;
        
        // infoline
        public const int MUSIC_STATUS_TOP                   = 10;
        public const int MUSIC_STATUS_LEFT_MINUS_STEP       = 220;
        public const string MUSIC_ON                        = "MUSIC ON";
        public const string MUSIC_OFF                       = "MUSIC OFF";
        public const string SCORE                           = "SCORE";

        // game standards
        public const int ENEMY_MAX_COUNT                    = 5;
        public const string SCORE_PREFIX                    = "Score";
    }
}
