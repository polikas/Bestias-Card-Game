using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    public void loadGameplay()
    { 
            SceneManager.LoadScene("MainGame");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadGameOverScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void loadVictoryScene()
    {
        SceneManager.LoadScene("WinScene");
    }
    public void loadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
