using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject playerCanvas;
    public string mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePause();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            if (playerCanvas != null)
            {
                playerCanvas.SetActive(false);
            }
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            if (playerCanvas != null)
            {
                playerCanvas.SetActive(true);
            }
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}