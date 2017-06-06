using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallController_Script : NetworkBehaviour{

    public bool canBeDirected = true;
    public bool isBeingDirected = false;
    public bool isTakingShot = false;
    public float velocityStopPoint = 1f;
    public float minimumShotTime = 1f;

    float startMouseX;
    public float relativeMouseX;
    float startMouseY;
    public float relativeMouseY;

    public float waitTime;

    private void Start()
    {
        if (!isLocalPlayer)
            return;
    }

    private void Update()
    {
        Debug.Log("Update");

        if (!isLocalPlayer)
            return;

        waitTime -= Time.deltaTime;

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
            if (canBeDirected && !isTakingShot)
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

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, relativeMouseX, transform.localEulerAngles.z);
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");
            if (isBeingDirected && canBeDirected && new Vector2(relativeMouseX, relativeMouseY).magnitude != 0)
            {
                waitTime = minimumShotTime;
                isTakingShot = true;
                CmdTakeShot(new Vector2(relativeMouseX, relativeMouseY));
            }

            isBeingDirected = false;
        }

        if (isTakingShot && GetComponent<Rigidbody>().velocity.magnitude <= velocityStopPoint && waitTime <= 0)
        {
            isTakingShot = false;
            CmdEndShot();
        }
    }

    [Command]
    public void CmdTakeShot(Vector2 shotVector)
    {
        RpcTakeShot(shotVector, gameObject.transform.position);
    }
    [ClientRpc]
    public void RpcTakeShot(Vector2 shotVector, Vector3 ballPos)
    {
        gameObject.transform.position = ballPos;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Collider>().enabled = true;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, shotVector.x, transform.localEulerAngles.z);
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shotVector.y);
    }

    [Command]
    public void CmdStopMovement()
    {
        RpcStopMovement(gameObject.transform.position);
    }
    [ClientRpc]
    public void RpcStopMovement(Vector3 ballPos)
    {
        gameObject.transform.position = ballPos;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        isTakingShot = false;
    }

    [Command]
    public void CmdEndShot()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider>().enabled = false;
        CmdStopMovement();
    }

    [Command]
    public void CmdSetBallColor(Color ballColor)
    {
        RpcSetBallColor(ballColor);
    }
    [ClientRpc]
    public void RpcSetBallColor(Color ballColor)
    {
        gameObject.GetComponent<Renderer>().material.color = ballColor;
    }
}