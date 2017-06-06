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

    public float jumpForce = 1f;

    float startMouseX;
    public float relativeMouseX;
    float startMouseY;
    public float relativeMouseY;

    public Vector3 gravityUpVector;

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

            Camera.main.gameObject.GetComponent<CameraController_Script>().aimingBall = true;

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

            Camera.main.gameObject.GetComponent<CameraController_Script>().aimingBall = false;

            isBeingDirected = false;
        }

        if (Input.GetButtonDown("Jump") && isTakingShot)
        {
            CmdUpdateBall();
            CmdJumpBall();
        }

        if (isTakingShot && GetComponent<Rigidbody>().velocity.magnitude <= velocityStopPoint && waitTime <= 0)
        {
            isTakingShot = false;
            CmdEndShot();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isLocalPlayer)
            return;

        CmdUpdateBall();
    }

    [Command]
    public void CmdTakeShot(Vector2 shotVector)
    {
        RpcTakeShot(shotVector, gameObject.transform.position);
    }
    [ClientRpc]
    public void RpcTakeShot(Vector2 shotVector, Vector3 ballPos)
    {
        transform.position = ballPos;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, shotVector.x, transform.localEulerAngles.z);
        GetComponent<Rigidbody>().AddForce(Camera.main.gameObject.transform.forward * shotVector.y, ForceMode.Impulse);
    }

    [Command]
    public void CmdUpdateBall()
    {
        RpcUpdateBall(GetComponent<Rigidbody>().velocity, GetComponent<Rigidbody>().angularVelocity, transform.position, transform.rotation);
    }
    [ClientRpc]
    public void RpcUpdateBall(Vector3 velocity, Vector3 angularVelocity, Vector3 position, Quaternion rotation)
    {
        GetComponent<Rigidbody>().velocity = velocity;
        GetComponent<Rigidbody>().angularVelocity = angularVelocity;
        transform.position = position;
        transform.rotation = rotation;
    }

    [Command]
    public void CmdStopMovement()
    {
        RpcStopMovement(gameObject.transform.position);
    }
    [ClientRpc]
    public void RpcStopMovement(Vector3 ballPos)
    {
        transform.position = ballPos;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        isTakingShot = false;
    }

    [Command]
    public void CmdEndShot()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
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
        GetComponent<Renderer>().material.color = ballColor;
    }

    [Command]
    public void CmdJumpBall()
    {
        RpcJumpBall(gravityUpVector * jumpForce);
    }
    [ClientRpc]
    public void RpcJumpBall(Vector3 forceVector)
    {
        GetComponent<Rigidbody>().AddForce(forceVector);
        Debug.Log("Jump (" + forceVector.ToString() + ")");
    }
}