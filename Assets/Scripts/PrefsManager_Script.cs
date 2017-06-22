using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PrefsManager_Script : MonoBehaviour
{
    [SerializeField]
    public Preferences prefs;

    private void Start()
    {
        prefs = new Preferences();
        prefs.Load();
        prefs.resolution = Screen.currentResolution;

        UpdateQuality(prefs.quality);
    }

    public void UpdateQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        prefs.quality = index;
        prefs.Save();
    }
    public void UpdateQuality(Slider mySlider)
    {
        UpdateQuality(Mathf.RoundToInt(mySlider.value));
        mySlider.gameObject.transform.parent.gameObject.GetComponent<Text>().text = "Quality : " + Mathf.RoundToInt(mySlider.value).ToString();
    }
}