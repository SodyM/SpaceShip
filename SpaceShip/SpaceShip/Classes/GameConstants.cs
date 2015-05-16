using SpaceShip.Objects;
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

        public const int SCORE_TEXT_LEFT = 20;
        public const int SCORE_VALUE_LEFT = 140;
        public const int INFOLINE_TOP = 10;

        // game standards
        public const int PLAYER_DEFAULT_HEALTH              = 100;
        public const int ENEMY_MAX_COUNT                    = 5;
        public const int ENEMY_COLLISION_DAMAGE             = 15;
        public const string SCORE_PREFIX                    = "Score";

        public const float ENEMY_SPEED_RANGE                = 0.2f;
        public const float MIN_ENEMY_SPEED                  = 0.1f;

        public const int SHOW_INFOWINDOW_DELAY              = 1500;
    }
}
