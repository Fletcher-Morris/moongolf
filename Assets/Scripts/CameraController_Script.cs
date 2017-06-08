using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Script : MonoBehaviour{

    public Vector2 mouseAxis;

    public float aimingSensitivity = 0.2f;

    public GameObject mainCam;
    public GameObject holeCam;

    public GameObject axis1;
    public GameObject axis2;

    public BallController_Script myBallController;

    public float lerpSpeed = 1f;

    public Vector3 ballGravUp;

    float newY = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        if (myBallController)
        {
            if (!myBallController.isBeingDirected)
            {
                mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                ballGravUp = myBallController.gravityUpVector;

                transform.position = Vector3.Lerp(transform.position, myBallController.gameObject.transform.position, lerpSpeed);

                Vector3 targetDir = (myBallController.gameObject.transform.position - ballGravUp) - transform.position;
                float step = lerpSpeed * 10 * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                Debug.DrawRay(transform.position, newDir, Color.red);
                transform.rotation = Quaternion.LookRotation(newDir + new Vector3(0, 0, 0));

                axis1.transform.localEulerAngles += new Vector3(0, 0, -mouseAxis.x * aimingSensitivity);

                newY -= mouseAxis.y * aimingSensitivity;
                newY = Mathf.Clamp(newY, -89, -1);

                axis2.transform.localEulerAngles = new Vector3(newY, 0, 0);  
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}