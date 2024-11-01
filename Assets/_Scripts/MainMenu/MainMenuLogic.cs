using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject settingsPanel;
    private void Start()
    {
        settingsPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void startButton()
    {
        SceneManager.LoadScene(1);
    }

    public void settingsButton() 
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void exitButton()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
