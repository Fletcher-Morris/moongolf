using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallController_Script : NetworkBehaviour{

    public bool canBeDirected = true;
    public bool isBeingDirected = false;

    float startMouseX;
    public float relativeMouseX;
    float startMouseY;
    public float relativeMouseY;

    private void Update()
    {
        Debug.Log("Update");

        if (!isLocalPlayer)
            return;

        Debug.Log("Local Update");

        if (Input.GetMouseButton(0))
        {
            if (canBeDirected)
            {
                isBeingDirected = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canBeDirected)
            {
                isBeingDirected = true;
            }

            startMouseX = Input.mousePosition.x;
            startMouseY = Input.mousePosition.y;
        }

        if (isBeingDirected)
        {
            relativeMouseX = Input.mousePosition.x - startMouseX;
            relativeMouseY = Input.mousePosition.y - startMouseY;
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");
            if (isBeingDirected && canBeDirected && new Vector2(relativeMouseX, relativeMouseY).magnitude != 0)
            {
                CmdTakeShot(new Vector2(relativeMouseX, relativeMouseY));
                Debug.Log("LOCAL Take Shot"); 
            }

            isBeingDirected = false;
        }
    }

    [Command]
    public void CmdTakeShot(Vector2 shotVector)
    {
        RpcTakeShot(shotVector);
        Debug.Log("CMD Take Shot");
    }
    [ClientRpc]
    public void RpcTakeShot(Vector2 shotVector)
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(shotVector.y, 0, shotVector.x));
        Debug.Log("RPC Take Shot");
    }
}