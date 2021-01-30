using System.Collections;
using System.Collections.Generic;
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
        
        switch (id)
        {
            case "P":
                Debug.Log("player made");
                _objectTracker[xPos,zPos] = Instantiate<GameObject>(_playerOBJ, spawnPos, _playerOBJ.transform.rotation, transform);
                _floorTracker[xPos,zPos] = Instantiate<GameObject>(_floorOBJ, spawnPos, _floorOBJ.transform.rotation, transform);
                break;
            case "O":
                Debug.Log("tile made");
                _objectTracker[xPos,zPos] = null;
                _floorTracker[xPos,zPos] = Instantiate<GameObject>(_floorOBJ, spawnPos, _floorOBJ.transform.rotation, transform);
                break;
            case "L":
                Debug.Log("light source made");
                _objectTracker[xPos, zPos] = Instantiate<GameObject>(_lightHouseOBJ, spawnPos, _lightHouseOBJ.transform.rotation, transform);
                _floorTracker[xPos, zPos] = Instantiate<GameObject>(_floorOBJ, spawnPos, _floorOBJ.transform.rotation, transform);
                break;
            case "B":
                Debug.Log("bridge made");
                _objectTracker[xPos, zPos] = null;
                _floorTracker[xPos, zPos] = Instantiate<GameObject>(_bridgeOBJ, spawnPos, _bridgeOBJ.transform.rotation, transform);
                break;
            case "W":
                Debug.Log("tile made");
                _objectTracker[xPos, zPos] = Instantiate<GameObject>(_wallOBJ, spawnPos, _wallOBJ.transform.rotation, transform);
                _floorTracker[xPos, zPos] = null;
                break;
            default:
                Debug.Log("nothing made");
                _objectTracker[xPos,zPos] = null;
                _floorTracker[xPos,zPos] = null;
                break;
        }
    }
}
