using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Preferences
{
    public int volume;
    public Resolution resolution;
    public int quality;

    public Preferences()
    {
        volume = 5;
        resolution = new Resolution();
        quality = 3;
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(this);

        try
        {
            File.WriteAllText(Application.dataPath + "/Preferences.json", jsonString.ToString());
        }
        catch
        {
            Debug.LogWarning(System.DateTime.Now.ToString() + "   COULD NOT SAVE PREFERENCES FILE, TRYING AGAIN.");
            Directory.CreateDirectory(Application.dataPath + "/Data");
            Save();
        }
    }

    public void Load()
    {
        Preferences newPrefs = new Preferences();

        try
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Data");
            newPrefs = JsonUtility.FromJson<Preferences>(jsonString);

            this.volume = newPrefs.volume;
            this.resolution = newPrefs.resolution;
            this.quality = newPrefs.quality;
        }
        catch
        {
            Debug.LogWarning(System.DateTime.Now.ToString() + "   COULD NOT LOAD PREFERENCES FILE, MAKING A NEW ONE.");
            Save();
        }
    }
}