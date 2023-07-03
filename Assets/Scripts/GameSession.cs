using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using System;

public class GameSession : MonoBehaviour
{

    [SerializeField] float actionDelay = 1.0f;

    PlayerInput input;
    Canvas gameOverCanvas;

    readonly static string gameOverCanvasTag = "GameOverCanvas";

    private void Start()
    {
        input = FindObjectOfType<PlayerInput>();
        gameOverCanvas = GameObject.FindGameObjectWithTag(gameOverCanvasTag).GetComponent<Canvas>();
        gameOverCanvas.enabled = false;
        UnpauseGame();
    }

    public void EndGame()
    {
        gameOverCanvas.enabled = true;
        PauseGame();
    }

    public void Restart()
    {
        StartCoroutine(ReloadSceneWithDelay());
    }

    public void Quit()
    {
        StartCoroutine(QuitWithDelay());
    }

    IEnumerator ReloadSceneWithDelay()
    {
        yield return new WaitForSecondsRealtime(actionDelay);
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator QuitWithDelay()
    {
        yield return new WaitForSecondsRealtime(actionDelay);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void PauseGame()
    {
        input.enabled = false;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        input.enabled = true;

        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
