using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UI;
using System.IO;

public class SteamScript_Script : MonoBehaviour
{
    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

    public bool steamInitialised = false;
    public string steamName = "...";

    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath).Parent;
        if(!File.Exists(dir.FullName + "/steam_appid.txt"))
        {
            File.WriteAllText(dir + "/steam_appid.txt", "480");
        }

        if (SteamManager.Initialized)
        {
            steamName = SteamFriends.GetPersonaName();
            Debug.Log("Connected To Steam Account As " + steamName);
        }
    }

    private void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);

            steamInitialised = true;

            steamName = SteamFriends.GetPersonaName();
        }
    }

    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        if (pCallback.m_bActive != 0)
        {
            Debug.Log("Steam Overlay has been activated");
        }
        else
        {
            Debug.Log("Steam Overlay has been closed");
        }
    }
}