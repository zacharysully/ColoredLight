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
    public int[] coords;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            SendMove(eDirections.up);
        }
        else if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            SendMove(eDirections.down);
        }
        else if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            SendMove(eDirections.left);
        }
        else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            SendMove(eDirections.right);
        }
    }

    //Tell the GridHandler where we want to move
    private void SendMove(eDirections dir)
    {

        //GridHandler.AttemptMovePlayer(coords[0], coords[1], dir) ? DidMove() : DidNotMove();


    }

    private void DidMove() { Debug.Log("Moved!"); }
    private void DidNotMove() { Debug.Log("Didn't move!"); }
}
