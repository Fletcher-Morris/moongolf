using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ColourSelector_Script : MonoBehaviour
{
    public GameObject rSlider;
    public GameObject gSlider;
    public GameObject bSlider;
    public GameObject resultColourImage;

    public Color resultColor;

    private void Update()
    {
        if (rSlider && bSlider && gSlider)
        {
            float r = rSlider.GetComponent<Slider>().value;
            float g = gSlider.GetComponent<Slider>().value;
            float b = bSlider.GetComponent<Slider>().value;

            resultColor = new Color(r,g,b);
        }

        if (resultColourImage.GetComponent<Image>())
        {
            resultColourImage.GetComponent<Image>().color = resultColor;
        }
    }
}