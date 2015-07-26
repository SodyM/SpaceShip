using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShip.Classes
{
    /// <summary>
    /// AssetsConstants
    /// It's the one and only place where all asset names are defined. Please define all graphics and soundnames here
    /// </summary>
    public static class AssetsConstants
    {
        // graphics
        public const string PLAYER                              = "ship_64x29";
        public const string ENEMY_YELLOW                        = "ship1_40x30";
        public const string ENEMY_RED                           = "ship2_40x30";
        public const string ENEMY_CYAN                          = "ship3_40x30";
        public const string ENEMY_BLUE                          = "ship4_40x30";
        public const string ENEMY_GREEN                         = "ship5_40x30";
        public const string EXPLOSION                           = "explosion";
        public const string HATCH                               = "hatch_sheet";
        public const string HEAD                                = "head_sheet";
        public const string ENEMY_LASER                         = "enemy_laser";
        public const string LASER                               = "laser";
        public const string FONTS                               = "fonts";
        public const string STARTFIELD                          = "starfield";
        public const string FARBACK                             = "farback";
        public const string RESPECT                             = "respect";
        public const string NUMBERS                             = "numbers";
        public const string COLLISION_INFO_PIC                  = "heino_scream";
        public const string GAME_OVER                           = "GameOver";

        // menu
        public const string MENU_NEW_GAME                       = "new_game";
        public const string MENU_SETTINGS                       = "settings";
        public const string MENU_CREDITS                        = "credits";
        public const string MENU_QUIT                           = "quit";
        public const string MENU_BACK                           = "back";
        public const string MENU_MUSIC                          = "music";
        public const string MENU_RESOLUTION                     = "resolution";
        public const string MENU_FULLSCREEN                     = "fullscreen";
        public const string MENU_SOUND                          = "sound";
        

        // sounds
        public const string AUDIO_ENGINE                        = @"Content\SpaceShip.xgs";
        public const string WAVE_BANK                           = @"Content\Wave Bank.xwb";
        public const string SOUND_BANK                          = @"Content\Sound Bank.xsb";

        public const string LASER_FIRE                          = "laserFire";
        public const string ENEMY_LASER_FIRE                    = "_laserFire";
        public const string RESPECT_SOUND                       = "cool";
        public const string COLLISION_SOUND                     = "scream";
        public const string MENU_CLICK                          = "click";
        //public const string EXPLOSION_PLAYER                  = "explosion_player";
        public const string EXPLOSION_PLAYER                    = "so_ein_tag_short";
        public const string BIG_BOSS_WARNING                    = "explosion_player_alt";

        // musics
        public const string BACKGROUND_MUSIC                    = "main_theme";
        public const string WINNER_MUSIC                        = "winner";
        public const string CREDITS                             = "so_ein_tag_credits";
        public const string SO_EIN_TAG_SHORT                    = "so_ein_tag_short";

        // settings
        public const string GOD = "god";
        public const string PRAY = "pray";
    }
}