using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PingUi_Script : NetworkBehaviour{

    public int avPing = 0;

    private void Update()
    {
        avPing = NetworkManager.singleton.client.GetRTT();

        if (GetComponent<Text>())
        {
            GetComponent<Text>().text = "Ping: " + avPing + "ms"; 
        }
    }
}