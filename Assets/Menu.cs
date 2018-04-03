using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    int lobbyIndex = 0;

    public void QuitGame()
    {
        Application.Quit();
    }
}
