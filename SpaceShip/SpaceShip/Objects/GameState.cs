using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShip.Objects
{
    /// <summary>
    /// Simple enum with Gamestates
    /// </summary>
    public enum GameState
    {
        MENU_MAIN,
        MENU_DISPLAY_SETTINGS,
        MENU_CREDITS,
        START_NEW_GAME,
        PLAY,
        PLAYER_DIED,
        GAME_OVER,
        QUIT
    };
}
