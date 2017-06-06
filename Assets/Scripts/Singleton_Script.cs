using UnityEngine;
using System.Collections;

public class Singleton_Script : MonoBehaviour
{
    string objectTag;
    GameObject[] objectsOfTag;

    bool destroyOther = true;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        objectTag = gameObject.tag;
        objectsOfTag = GameObject.FindGameObjectsWithTag(objectTag);

        if (objectsOfTag.Length >= 2)
        {
            if (destroyOther == true)
            {
                Destroy(objectsOfTag[1]);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}