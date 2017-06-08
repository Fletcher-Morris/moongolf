using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor_Script : MonoBehaviour{

    public bool rotate = true;
    public Vector3 speed;

    private void FixedUpdate()
    {
        if (rotate)
        {
            transform.localEulerAngles += speed * Time.fixedDeltaTime; 
        }
    }
}