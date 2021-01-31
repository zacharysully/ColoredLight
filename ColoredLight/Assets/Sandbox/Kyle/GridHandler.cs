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
        _levelGrid = new DataSpace[0, 0];
        _objectsGrid = new System.Object[0, 0];
        _objectsGrid[0, 0] = null;
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
                DataSpace newspace = null;
                switch (data[i,j])
                {
                    case "P":
                        newspace = new DataSpace("O");
                        break;
                    case "B":
                    case "O":
                        newspace = new DataSpace(data[i, j]);
                        break;
                    case "L":
                        newspace = new DataSpace("O");
                        break;
                    case "W":
                        newspace = new DataSpace("X");
                        break;
                    default:
                        newspace = null;
                        break;
                }

                _objectsGrid[i, j] = null;
                _levelGrid[i, j] = newspace;
                _worldRef.Populate3DLevel(i, j, data[i, j]);
            }
        }
    }

    public static void AddObjectToGrid(int x, int z, System.Object script)
    {
        _objectsGrid[x, z] = script;
    }

    //updates the data that the something has moved to a new position
    private static void UpdateGrid<T>(int xpos1, int zpos1, int xpos2, int zpos2, T thing)
    {
        T temp = thing;
        _objectsGrid[xpos1, zpos1] = null;
        _objectsGrid[xpos2, zpos2] = temp;

        _worldRef.Update3DArea(xpos1, zpos1, xpos2, xpos2);
    }

    //called to move the player in any directions that are passed
    //data grids are changed and then reflected into the 3D objects
    public static bool AttemptMovePlayer(int row, int column, eDirections dir)
    {

        Debug.Log("Position: " + row + "," + column);
        int tempRow = row;
        int tempColumn = column;

        switch (dir)
        {
            case eDirections.up:
                tempColumn -= 1;
                break;
            case eDirections.down:
                tempColumn += 1;
                break;
            case eDirections.left:
                tempRow -= 1;
                break;
            case eDirections.right:
                tempRow += 1;
                break;
            default:
                break;
        }

        Debug.Log("Moving to: " + tempRow + "," + tempColumn);
        if (IsTileWithinBounds(tempRow, tempColumn))
        {
            if (_objectsGrid[tempColumn, tempRow] != null)
            {
                return false;
            }
            else
            {
                Debug.Log("No object detected at " + tempColumn + "," + tempRow);
                _worldRef.MoveObject(row,column,dir);
                
                return true;
            }
        }
        else
        {
            return false;
        }

    }

    //-B
    private static bool IsTileWithinBounds(int row, int column)
    {
        if (row < 0 || column < 0 || row > _levelGrid.GetLength(1) - 1 || column > _levelGrid.GetLength(0) - 1 || _levelGrid[row,column].GetID == "X")
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    //Resets array data for level layouts to make way for the next one
    public static void UnloadLevel()
    {
        for (int i = 0; i < _levelGrid.GetLength(0); i++)
        {
            for (int j = 0; j < _levelGrid.GetLength(1); j++)
            {
                _levelGrid[i, j] = null;
                _objectsGrid[i, j] = null;
            }
        }
    }


    //Various Getters and Setters
    public static DataSpace[,] CheckFloorLayout { get { return _levelGrid; } }
    public static System.Object[,] CheckObjectLayout { get { return _objectsGrid; } }

    public static WorldHandler SetWorldRef { set { _worldRef = value; } }
}
