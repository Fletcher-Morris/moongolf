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
    public float maxShotForce = 20f;
    public float shotForceMultiplier = 0.1f;
    public float myShotForce = 0;

    public float jumpForce = 1f;

    public LayerMask groundLayers;
    float groundCheckDistance = 0.15f;
    public bool isGrounded;

    float startMouseX;
    public float relativeMouseX;
    float startMouseY;
    public float relativeMouseY;

    public Vector3 gravityUpVector;

    public float waitTime;

    public CameraController_Script myCamController;

    private void Start()
    {
        if (!isLocalPlayer)
            return;

        myCamController = GameObject.Find("Camera Rig").GetComponent<CameraController_Script>();
        myCamController.myBallController = this;
    }

    private void Update()
    {
        isGrounded = checkGrounded();

        if (!isLocalPlayer)
            return;

        waitTime -= Time.deltaTime;

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
            myShotForce += Input.GetAxis("Mouse Y") * shotForceMultiplier;
            myShotForce = Mathf.Clamp(myShotForce, 0, maxShotForce);
        }
        else
        {
            myShotForce = 0;
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (isBeingDirected && canBeDirected && !isTakingShot && myShotForce >= 0)
            {
                float shotForce = Mathf.Clamp(relativeMouseY, 0, maxShotForce);

                waitTime = minimumShotTime;
                isTakingShot = true;
                CmdUpdateBall();
                CmdTakeShot(myCamController.axis1.transform.up * myShotForce);
                myShotForce = 0;
            }

            isBeingDirected = false;
        }

        if (Input.GetButtonDown("Jump") && isTakingShot && checkGrounded())
        {
            CmdUpdateBall();
            CmdJumpBall();
        }

        if (isTakingShot && GetComponent<Rigidbody>().velocity.magnitude <= velocityStopPoint && waitTime <= 0 && checkGrounded())
        {
            isTakingShot = false;
            CmdEndShot();
        }
    }

    public bool checkGrounded()
    {
        if (Physics.Raycast(transform.position, -gravityUpVector, groundCheckDistance, groundLayers))
            return true;

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isLocalPlayer)
            return;

        CmdUpdateBall();
    }

    [Command]
    public void CmdTakeShot(Vector3 shotVector)
    {
        RpcTakeShot(shotVector);
    }
    [ClientRpc]
    public void RpcTakeShot(Vector3 shotVector)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(shotVector, ForceMode.Impulse);
    }

    [Command]
    public void CmdUpdateBall()
    {
        RpcUpdateBall(GetComponent<Rigidbody>().velocity, GetComponent<Rigidbody>().angularVelocity, transform.position, transform.rotation, GetComponent<Collider>().enabled);
    }
    [ClientRpc]
    public void RpcUpdateBall(Vector3 velocity, Vector3 angularVelocity, Vector3 position, Quaternion rotation, bool movementState)
    {
        GetComponent<Rigidbody>().velocity = velocity;
        GetComponent<Rigidbody>().angularVelocity = angularVelocity;
        transform.position = position;
        transform.rotation = rotation;
        GetComponent<Rigidbody>().isKinematic = !movementState;
        GetComponent<Collider>().enabled = movementState;
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
        GetComponent<Rigidbody>().AddForce(forceVector, ForceMode.Impulse);
        Debug.Log("Jump (" + forceVector.ToString() + ")");
    }
}