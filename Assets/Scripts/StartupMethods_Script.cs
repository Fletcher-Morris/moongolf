using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class StartupMethods_Script : MonoBehaviour
{
    private void Start()
    {
        if (SteamManager.Initialized)
        {
            string steamName = SteamFriends.GetPersonaName();
        }
    }
}