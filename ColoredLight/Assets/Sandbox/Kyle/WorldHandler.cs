using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
    public string LevelName;

    GameObject _playerOBJ;
    GameObject _floorOBJ;
    GameObject _lightHouseOBJ;
    GameObject _bridgeOBJ;
    GameObject _wallOBJ;

    GameObject[,] _floorTracker;
    GameObject[,] _objectTracker;
    private void Awake()
    {
        _playerOBJ = Resources.Load<GameObject>("Prefabs/Player");
        _floorOBJ = Resources.Load<GameObject>("Prefabs/Tile");
        _lightHouseOBJ = Resources.Load<GameObject>("Prefabs/Lighthouse");
        _bridgeOBJ = Resources.Load<GameObject>("Prefabs/Bridge");
        _wallOBJ = Resources.Load<GameObject>("Prefabs/Wall");

        GridHandler.SetWorldRef = this;
        GridHandler.LoadLevel(LevelName);
    }

    public void Initialize3DArea(int width, int length)
    {
        _floorTracker = new GameObject[width, length];
        _objectTracker = new GameObject[width, length];
    }

    public void Populate3DLevel(int xPos, int zPos, string id)
    {
        Vector3 spawnPos = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z + zPos);

        GameObject newfloor = null;
        GameObject newobject = null;

        switch (id)
        {
            case "P":
                Debug.Log("player made");
                newobject = Instantiate<GameObject>(_playerOBJ, spawnPos, _playerOBJ.transform.rotation, transform);
                newobject.AddComponent<Player>();
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<Player>());
                newfloor = Instantiate<GameObject>(_floorOBJ, spawnPos, _floorOBJ.transform.rotation, transform);
                break;
            case "O":
                Debug.Log("tile made");
                newobject = null;
                newfloor = Instantiate<GameObject>(_floorOBJ, spawnPos, _floorOBJ.transform.rotation, transform);
                break;
            case "L":
                Debug.Log("light source made");
                newobject = Instantiate<GameObject>(_lightHouseOBJ, spawnPos, _lightHouseOBJ.transform.rotation, transform);
                newobject.AddComponent<TestObjectScript>(); 
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<TestObjectScript>());
                newfloor = Instantiate<GameObject>(_floorOBJ, spawnPos, _floorOBJ.transform.rotation, transform);
                break;
            case "B":
                Debug.Log("bridge made");
                newobject = null;
                newfloor = Instantiate<GameObject>(_bridgeOBJ, spawnPos, _bridgeOBJ.transform.rotation, transform);
                //add script to brdige to do cool color stuff
                break;
            case "W":
                Debug.Log("tile made");
                newobject = Instantiate<GameObject>(_wallOBJ, spawnPos, _wallOBJ.transform.rotation, transform);
                newfloor = null;
                break;
            default:
                Debug.Log("nothing made");
                newobject = null;
                newfloor = null;
                break;
        }

        _objectTracker[xPos, zPos] = newobject;
        _floorTracker[xPos, zPos] = newfloor;
    }

    public void Update3DArea(int thingPosx, int thingPosz, int newposx, int newposz)
    {
        //move 3d data aroound in _objectsTracker
        //the lerp object trying to move to applicable tile position
        //Player.transform.poition -> newtile.transform.position

    }

    public void MoveObject(int xPos, int zPos, eDirections direction)
    {
        
    }

}
