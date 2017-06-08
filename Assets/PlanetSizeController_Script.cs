using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlanetSizeController_Script : NetworkBehaviour{

    public Vector3 currentScale;
    public Vector3 newScale;
    float scaleFloat;
    float sensitivity = 0.5f;

    private void Start()
    {
        currentScale = transform.localScale;
        scaleFloat = currentScale.x;
    }

    private void Update()
    {
        currentScale = transform.localScale;

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            scaleFloat += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            newScale = new Vector3(scaleFloat, scaleFloat, scaleFloat);
            CmdSetNewSize();
        }
    }

    [Command]
    private void CmdSetNewSize()
    {
        RpcSetNewSize(newScale);
    }
    [ClientRpc]
    private void RpcSetNewSize(Vector3 scale)
    {
        transform.localScale = scale;
    }
}