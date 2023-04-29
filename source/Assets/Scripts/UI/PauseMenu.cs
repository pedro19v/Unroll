using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public LoadZone load;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject controlsUI;

    private Boy boy;

    private void Awake()
    {
        load = FindObjectOfType<LoadZone>();
        Cursor.visible = false;
        boy = FindObjectOfType<Boy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        controlsUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Boy.hasKey = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        boy.ForceReportInformation();
        Time.timeScale = 1f;
        GameIsPaused = false;
        load.SaveGame();
        SceneManager.LoadScene(0);
    }

    public void MainMenuLastScene()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(0);
    }
}
