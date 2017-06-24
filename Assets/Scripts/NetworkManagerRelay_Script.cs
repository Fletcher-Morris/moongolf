using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerRelay_Script : MonoBehaviour{

    GameObject networkManagerObject;
    public GameObject startJoinPanelObject;
    public GameObject ipFieldObject;
    public GameObject cancelConnectionButtonObject;
    public GameObject connectedPlayersCanvas;

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

            startJoinPanelObject.SetActive(false);
            ipFieldObject.GetComponent<InputField>().text = Network.player.ipAddress.ToString();
            ipFieldObject.GetComponent<InputField>().interactable = false;
            cancelConnectionButtonObject.SetActive(true);
            connectedPlayersCanvas.SetActive(true);
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

            startJoinPanelObject.SetActive(false);
            ipFieldObject.GetComponent<InputField>().text = nm.networkAddress.ToString();
            ipFieldObject.GetComponent<InputField>().interactable = false;
            cancelConnectionButtonObject.SetActive(true);
            connectedPlayersCanvas.SetActive(true);
        }
    }

    public void CancelConnection()
    {
        if (networkManagerObject)
        {
            NetworkedGameManager_Script nm = networkManagerObject.GetComponent<NetworkedGameManager_Script>();

            nm.StopHost();

            startJoinPanelObject.SetActive(true);
            ipFieldObject.GetComponent<InputField>().text = "";
            ipFieldObject.GetComponent<InputField>().interactable = true;
            cancelConnectionButtonObject.SetActive(false);
            connectedPlayersCanvas.SetActive(false);
        }
    }
}