using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PointGravity_Script : MonoBehaviour{
    
    public bool applyGravity = true;
    public float gravityStrength = 9.81f;
    public float gravityFieldRadius = 50;
    public bool freezeRotation = false;

    public List<Rigidbody> connectedRigidbodies;

    private void FixedUpdate()
    {
        connectedRigidbodies = GetConnectedBodies();

        if (applyGravity)
        {
            foreach (Rigidbody body in connectedRigidbodies)
            {
                Vector3 vectorDelta = (gameObject.transform.position - body.gameObject.transform.position).normalized;

                body.AddForce(vectorDelta * gravityStrength, ForceMode.Acceleration);
            }
        }

        gameObject.GetComponent<Rigidbody>().freezeRotation = freezeRotation;
    }

    List<Rigidbody> GetConnectedBodies()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<Rigidbody> newBodyList = new List<Rigidbody>();

        foreach (GameObject obj in allObjects)
        {
            if (Vector3.Distance(gameObject.transform.position, obj.transform.position) <= gravityFieldRadius)
            {
                if (obj.GetComponent<Rigidbody>() && obj.transform.tag != "Player")
                {
                    newBodyList.Add(obj.GetComponent<Rigidbody>());
                }
            }
        }

        return newBodyList;
    }

    public void AttractSpecific(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 localUp = body.up;

        body.gameObject.GetComponent<Rigidbody>().AddForce(-gravityUp * gravityStrength, ForceMode.Acceleration);
        body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
    }
}