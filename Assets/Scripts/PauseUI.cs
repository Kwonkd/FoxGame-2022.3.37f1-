using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject pausePanel;
    void OnEnable()
    {
        Debug.Log("스크립트 시작");
        pausePanel.SetActive(false);
    }

    public void OnClickPauseButton()
    {
        Time.timeScale = 0;
        if (pausePanel != null)
        { 
            pausePanel.SetActive(true);
        }
    }
    public void OnClickContinueButton()
    {
        Time.timeScale = 1;
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }
    public void OnClickRetryButton()
    {
        Time.timeScale = 1;
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //현재 씬 재시작
        }
    }
    public void OnClickMainMenuButton()
    {
        Time.timeScale = 1;
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            SceneManager.LoadScene("MainScene");
        }
    }
}
