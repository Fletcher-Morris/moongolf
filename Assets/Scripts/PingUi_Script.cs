using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PingUi_Script : NetworkBehaviour{

    public int avPing = 0;
    public NetworkManager nm;

    private void Start()
    {
        nm = GameObject.Find("NM").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (!isClient)
            return;

        if (nm != null)
        {
            avPing = nm.client.GetRTT();

            if (GetComponent<Text>())
            {
                GetComponent<Text>().text = "Ping: " + avPing + "ms";
            } 
        }
        else
        {
            nm = GameObject.Find("NM").GetComponent<NetworkedGameManager_Script>();
        }
    }
}