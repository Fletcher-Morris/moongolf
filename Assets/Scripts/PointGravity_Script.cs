using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PointGravity_Script : MonoBehaviour{
    
    public bool applyGravity = true;
    public bool drawDebugRays = true;
    public float gravityStrength = 9.81f;
    public float gravityFieldRadius = 500;
    public bool freezeRotation = false;

    public AnimationCurve gravityStrengthCurve;

    public List<Rigidbody> connectedRigidbodies;

    private void FixedUpdate()
    {
        connectedRigidbodies = GetConnectedBodies();

        if (applyGravity)
        {
            foreach (Rigidbody body in connectedRigidbodies)
            {
                Vector3 vectorDelta = (gameObject.transform.position - body.gameObject.transform.position).normalized;

                float vectorDistance = (gameObject.transform.position - body.gameObject.transform.position).magnitude;

                body.AddForce(vectorDelta * ((gravityStrengthCurve.Evaluate(vectorDistance / gravityFieldRadius) + 1) * gravityStrength), ForceMode.Acceleration);

                if (body.gameObject.GetComponent<BallController_Script>())
                {
                    body.gameObject.GetComponent<BallController_Script>().gravityUpVector = (body.position - transform.position).normalized;
                }

                if (drawDebugRays)
                {
                    Color lineColor = new Color(vectorDistance / gravityFieldRadius, 1 - vectorDistance / gravityFieldRadius, 0);

                    Debug.DrawLine(transform.position, body.transform.position, lineColor);
                }
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
            if (obj != gameObject && obj.GetComponent<Rigidbody>())
            {
                if (Vector3.Distance(gameObject.transform.position, obj.transform.position) <= gravityFieldRadius)
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