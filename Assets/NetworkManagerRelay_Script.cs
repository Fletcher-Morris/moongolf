using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerRelay_Script : MonoBehaviour{

    GameObject networkManagerObject;

    private void Start()
    {
        networkManagerObject = GameObject.FindWithTag("NM");
    }

    public void HostGameRelay()
    {
        if (networkManagerObject)
        {
            NetworkedGameManager_Script nm = networkManagerObject.GetComponent<NetworkedGameManager_Script>();

            nm.SetPort();

            nm.StartHost();
        }
    }

    public void JoinGameRelay()
    {
        if (networkManagerObject)
        {
            NetworkedGameManager_Script nm = networkManagerObject.GetComponent<NetworkedGameManager_Script>();

            nm.SetIpAddress(GameObject.Find("IP Input Field").GetComponent<InputField>().text);
            nm.SetPort();

            nm.StartClient();
        }
    }
}