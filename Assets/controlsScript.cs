using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlsScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;

    public void ControlsButton()
    {
        mainMenu.gameObject.SetActive(false);
        controlsMenu.gameObject.SetActive(true);   
    }
}
