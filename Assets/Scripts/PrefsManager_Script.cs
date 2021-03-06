﻿using System.Collections;
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

        SetUIItems();
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

    public void SetUIItems()
    {
        GameObject.Find("Volume Slider").transform.GetChild(0).gameObject.GetComponent<Slider>().value = prefs.volume;
        GameObject.Find("Quality Slider").transform.GetChild(0).gameObject.GetComponent<Slider>().value = prefs.quality;
    }
}