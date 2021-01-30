using System;
using System.Collections;
using System.Collections.Generic;

public static class GridHandler 
{
    static char[,] _levelGrid;
    static Object[,] _objectsGrid;
    //static Player _playerRef;

    static WorldHandler _worldRef;
    public static void Init()
    {
        _levelGrid = new char[0, 0];
        _objectsGrid = new Object[0, 0];
        _objectsGrid[0,0] = null;
    }

    public static void LoadLevel(string Level)
    {
        
    }

    /*public static MovePlayer(eDirections dir)
    {

    }*/

    public static void UnloadLevel()
    {
        _levelGrid = new char[0, 0];
    }

    public static WorldHandler SetWorldRef { set { _worldRef = value; } }
}
