using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField]
    private float travelDistance = 1f;

    float second = 0;

    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                this.gameObject.transform.position += new Vector3(travelDistance, 0, 0);
                break;
            case Direction.South:
                this.gameObject.transform.position += new Vector3(-travelDistance, 0, 0);
                break;
            case Direction.East:
                this.gameObject.transform.position += new Vector3(0, 0, -travelDistance);
                break;
            case Direction.West:
                this.gameObject.transform.position += new Vector3(0, 0, travelDistance);
                break;
            default:
                break;
        }
    }

}
