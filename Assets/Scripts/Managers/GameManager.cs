using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject pausePanel;

    //public FadeInOut fadeInOut;

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


    private void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;
    }
}
