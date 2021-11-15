using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame ()
    {
        Debug.Log("Quitting out of Game");
        Application.Quit();
    }

    public void Guide()
    {
        SceneManager.LoadScene("UserGuide");
    }
}
