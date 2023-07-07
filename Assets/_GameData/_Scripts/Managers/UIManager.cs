using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas pregameCanvas;
    [SerializeField] private Canvas inGameCanvas;
    [SerializeField] private Canvas successCanvas;
    [SerializeField] private Canvas failCanvas;

    [SerializeField] private TMP_Text levelText;

    private void OnEnable()
    {
        EventManager.Instance.OnGameSuccessed += OnGameSuccessedHandler;
        EventManager.Instance.OnGameFailed += OnGameFailedHandler;
    }

    private void Start()
    {
        levelText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnGameFailedHandler()
    {
        inGameCanvas.enabled = false;
        failCanvas.enabled = true;
    }

    private void OnGameSuccessedHandler()
    {
        inGameCanvas.enabled = false;
        successCanvas.enabled = true;
    }

    public void StartGameTapAreaClicked()
    {
        EventManager.Instance.RaiseGameStarted();
        pregameCanvas.enabled = false;
        inGameCanvas.enabled = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ProceedLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0; // Loop the levels if levels are all finished.
        }
        SceneManager.LoadScene(nextLevel);
    }
}
