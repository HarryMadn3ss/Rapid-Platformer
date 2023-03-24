using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backButton : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject controlsMenu;

    public void BackButton()
    {
        mainMenu.gameObject.SetActive(true);
        controlsMenu.gameObject.SetActive(false);
    }
}
