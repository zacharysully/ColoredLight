using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;



public static class GridHandler 
{
    static DataSpace[,] _levelGrid;
    static System.Object[,] _objectsGrid;
    //static Player _playerRef;
   
    static WorldHandler _worldRef;

    //Creates basic info for varibales to hold
    public static void Init()
    {
        _levelGrid = new DataSpace[0,0];
        _objectsGrid = new System.Object[0,0];
        _objectsGrid[0,0] = null;
    }

    //Takes in a List of strings and decodes them
    //to create the player space in the data 
    //which will then populate and create the 3D area
    public static void LoadLevel(string LevelName)
    {
        string[,] organizedLevelText = OrganizeLevelText(LevelName);
        PopulateGridData(organizedLevelText);
    }

    //takes whatever Text name is passed
    //finds the corresponding Text File in the StreamingAssets Folder
    //splits text lines into seperate strings
    //then splits again at every "/" to find the seperate objects and tiles
    //passes and 2d array of all the ids of things to spawn
    private static string[,] OrganizeLevelText(string LevelName)
    {
        string path = "Assets/StreamingAssets/LevelTexts/" + LevelName + ".txt";

        string text = System.IO.File.ReadAllText(path);
        string[] lines = Regex.Split(text, "\r\n|\r|\n");
        int rows = lines.Length;


        string[][] levelset = new string[rows][];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] stringsOfLine = Regex.Split(lines[i], "/");
            levelset[i] = stringsOfLine;
        }

        string[,] levelBase = new string[rows, levelset[0].Length];
        for (int i = 0; i < levelBase.GetLength(0); i++)
        {
            for (int j = 0; j < levelBase.GetLength(1); j++)
            {
                levelBase[i, j] = levelset[i][j];
            }
        }

        return levelBase;
    }

    //after the text has been split up and placed into a 2s array we have to fill up our data arrays
    //after this, positions and data is passed to the WorldHandler
    //so that it can begin to populate the 3D scene
    private static void PopulateGridData(string[,] data)
    {
        int rows = data.GetLength(0);
        int columns = data.GetLength(1);
        _levelGrid = new DataSpace[rows, columns];
        _objectsGrid = new System.Object[rows, columns];
        _worldRef.Initialize3DArea(rows, columns);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (data[i,j] == "P")
                {
                    _objectsGrid[i,j] = new object(); //Player.p;
                    _levelGrid[i,j] = new DataSpace("O");
                }
                else
                {
                    _objectsGrid[i, j] = null;
                    _levelGrid[i,j] = new DataSpace(data[i,j]);
                }

                _worldRef.Populate3DLevel(i, j, data[i,j]);
            }
        }
    }

    //called to move the player in any directions that are passed
    //data grids are changed and then reflected into the 3D objects
    public static void MovePlayer(eDirections dir)
    {
        switch (dir)
        {
            case eDirections.up:
                break;
            case eDirections.down:
                break;
            case eDirections.left:
                break;
            case eDirections.right:
                break;
            default:
                break;
        }
    }

    //Resets array data for level layouts to make way for the next one
    public static void UnloadLevel()
    {
        for (int i = 0; i < _levelGrid.GetLength(0); i++)
        {
            for (int j = 0; j < _levelGrid.GetLength(1); j++)
            {
                _levelGrid[i,j] = null;
                _objectsGrid[i,j] = null;
            }
        }
    }


    //Various Getters and Setters
    public static DataSpace[,] CheckFloorLayout { get { return _levelGrid; } }
    public static System.Object[,] CheckObjectLayout { get { return _objectsGrid; } }

    public static WorldHandler SetWorldRef { set { _worldRef = value; } }
}
