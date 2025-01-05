using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject gamePausePanel;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI inGameScoreText;


    public static Action ResetGame;
    public static Action GameOver;
    public static Action AddScore;

    private int score = 0;

    private void Start()
    {
        ShowGameStartPanel();
        Time.timeScale = 0;
        GameOver += ShowGameOverPanel;
        AddScore += UpdateScore;
    }

    public void UpdateScore()
    {
        score++;
        inGameScoreText.text = (score * 10).ToString();
        gameOverScoreText.text = (score * 10).ToString();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void ShowGameStartPanel()
    {
        gameStartPanel.SetActive(true);
    }

    public void startGame()
    {
        gameStartPanel.SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance.PlayButtonClickSFX();
        AudioManager.Instance.PlayGameBGM();
    }

    public void PauseGame()
    {
        gamePausePanel.SetActive(true);
        Time.timeScale = 0;
        AudioManager.Instance.PlayButtonClickSFX();
    }

    public void ResumeGame()
    {
        gamePausePanel.SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance.PlayButtonClickSFX();
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayButtonClickSFX();
        Application.Quit();
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        ResetGame?.Invoke();
        AudioManager.Instance.PlayButtonClickSFX();
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        gameOverPanel.SetActive(false);
        gamePausePanel.SetActive(false);
        inGameScoreText.text = "0";
        gameOverScoreText.text = "0";
        ResetGame?.Invoke();
        ShowGameStartPanel();
        AudioManager.Instance.PlayButtonClickSFX();
        AudioManager.Instance.PlayMenuBGM();
    }

    private void OnDestroy()
    {
        GameOver -= ShowGameOverPanel;
    }
}
