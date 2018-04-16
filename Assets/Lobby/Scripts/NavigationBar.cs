using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NavigationBar : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
