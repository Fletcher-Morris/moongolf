using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainMenuController_Script : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject prefsMenuCanvas;

    public Vector3 mainMenuCamPos;
    public Vector3 prefsMenuCamPos;

    public Color mainMenuCamColour;
    public Color prefsMenuCamColour;

    public float moveSpeed = 1;
    public float rotateSpeed = 1;

    public static bool movingToMainMenu = false;
    public static bool movingToPrefsMenu = false;

    public void GoToMainMenu()
    {
        movingToMainMenu = true;
        movingToPrefsMenu = false;
    }

    public void GoToPrefsMenu()
    {
        movingToMainMenu = false;
        movingToPrefsMenu = true;
    }

    private void Start()
    {
        GoToMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)){
            if (movingToMainMenu)
            {
                GoToPrefsMenu();
            }
            else if (movingToPrefsMenu)
            {
                GoToMainMenu();
            }
        }

        if (movingToMainMenu)
        {
            transform.position = Vector3.MoveTowards(transform.position, mainMenuCamPos, moveSpeed * Time.deltaTime);

            Vector3 targetDir = mainMenuCanvas.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotateSpeed * Time.deltaTime, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);

            GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, mainMenuCamColour, 1 * Time.deltaTime);
        }

        else if (movingToPrefsMenu)
        {
            transform.position = Vector3.MoveTowards(transform.position, prefsMenuCamPos, moveSpeed * Time.deltaTime);

            Vector3 targetDir = prefsMenuCanvas.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotateSpeed * Time.deltaTime, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);

            GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, prefsMenuCamColour, 1 * Time.deltaTime);
        }
    }
}