using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Script : MonoBehaviour{

    public float mouseX;

    public float aimingSensitivity = 0.2f;

    public bool aimingBall = false;

    private void Update()
    {

        mouseX = Input.mousePosition.x;

        if (aimingBall)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, mouseX * aimingSensitivity, 0);
        }
    }
}