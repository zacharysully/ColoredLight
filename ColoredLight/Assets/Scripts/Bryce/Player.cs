using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eDirections
{
    up,
    down,
    left,
    right
}

public class Player : MonoBehaviour
{
    //Variable declaration
    public static Player p;
    private float blockSize;
    public float raycastHeight = .5f;

    // Start is called before the first frame update
    void Start()
    {

        p = this;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            Move(eDirections.up);
        }
        else if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            Move(eDirections.down);
        }
        else if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            Move(eDirections.left);
        }
        else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            Move(eDirections.right);
        }
    }

    private void Move(eDirections dir)
    {

        Debug.Log("Moving" + dir);
        //Insert calls to Kyle's code here

    }

}
