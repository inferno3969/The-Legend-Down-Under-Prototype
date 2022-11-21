using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThankYouMenu : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}