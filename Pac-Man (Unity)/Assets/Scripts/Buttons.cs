using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void OpenMainMenu ()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenCredits ()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
