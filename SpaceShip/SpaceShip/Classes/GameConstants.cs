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

        public const int SCORE_TEXT_LEFT                    = 20;
        public const int SCORE_VALUE_LEFT                   = 140;
        public const int INFOLINE_TOP                       = 10;

        // life bar
        public const int LIFEBAR_LEFT                       = 250;
        public const int LIFEBAR_TOP                        = MUSIC_STATUS_TOP;
        public const int MAXVALUE                           = 100;              // maximal value in oxymeter
        public const int HEIGHT                             = 15;               // height of oxymeter

        // game standards

        /* 
         * minimum score ans further step for super cool
         * Example:
         * SUPERCOOL_SCORE = 1000
         * - first message when player has reached score 1000
         * - next message when player has reached score 2000
         */
        public static int SUPERCOOL_SCORE                   = 1000;      // change super cool score if you like here

        public const int PLAYER_LIVES_START                 = 3;
        public const int PLAYER_DEFAULT_HEALTH              = 100;
        public const int PLAYER_DEFAULT_DAMAGE              = 100;
        public const float PLAYER_LASER_SPEED               = 10.0f;    //change to a property of the weapon type
        public const float Enemy_LASER_SPEED                = 4.0f;     //change to a property of the weapon type
        public const int ENEMY_MAX_COUNT                    = 5;
        public const int ENEMY_COLLISION_DAMAGE             = 15;
        public const string SCORE_PREFIX                    = "Score";
        public const int EXPLOSION_OFFSET                   = -50;

        public const bool ENEMIES_SHOOT                     = true; //for Tests
        public const bool ENEMIES_TARGET_PLAYER             = false; //for Tests

        public const float ENEMY_SPEED_RANGE                = 0.2f;
        public const float MIN_ENEMY_SPEED                  = 0.1f;
        public const float HATCH_SPEED                      = -0.1f;

        //Default enemy firing characteristics; will later depend on the type of the enemy
        public const int ENEMY_MIN_FIRE_DELAY               = 1000;
        public const int ENEMY_FIRE_DELAY_RANGE             = 3000;
        public const int ENEMY_PROJECTILE_DAMAGE            = 20;//will later be a property of the weapon type
        public const float ENEMY_PROJECTILE_SPEED           = 0.3f;

        public const int SHOW_INFOWINDOW_DELAY              = 1500;

        public const string GAME_OVER                       = "GAME OVER";

        // Credits
        public const string LINE1 = "Programmed by Sody and Oli";
        public const string LINE2 = "We would like to thank Heino";
    }
}
