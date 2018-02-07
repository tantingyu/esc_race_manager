using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    int menuIndex = 0;
    int settingsIndex = 1;

    public void goToSettings()
    {
        SceneManager.LoadScene(settingsIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
