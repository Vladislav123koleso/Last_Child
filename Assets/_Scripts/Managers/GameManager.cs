using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject pausePanel;
    
    public GameObject _fadeInOut;

    private bool isPaused = false;

    private void Start()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) /*&& !fadeInOut.IsFadeImageActive()*/)
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
            TogglePause();
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Time.timeScale == 1)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }

    public void settingsButton()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    public void resumeButton()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        TogglePause();
    }
    public void exitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }



    private void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;
    }
}
