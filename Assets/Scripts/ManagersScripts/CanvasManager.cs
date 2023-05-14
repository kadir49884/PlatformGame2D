using UnityEngine;
using TMPro;
using DG.Tweening.Core.Easing;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasManager : Singleton<CanvasManager>
{
    private GameManager gameManager;
    private GameDatas gameDatas;
    private int scoreCount;
    private int bestScore;
    private int heartCount;
    private static bool isGameRestart;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private TextMeshProUGUI scoreTextGame;
    [SerializeField] private TextMeshProUGUI bestScoreTextStartPanel;
    [SerializeField] private TextMeshProUGUI bestScoreTextRestartPanel;
    [SerializeField] private TextMeshProUGUI scoreTextRestartPanel;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private List<Image> heartList = new List<Image>();
    [SerializeField] private Color heartGrayColor;



    private void Start()
    {

        gameManager = GameManager.Instance;
        GetDatas();
        WriteDatas();
        AddActions();
        SetStartSettings();

    }

    private void GetDatas()
    {
        gameDatas = StartOperations.Instance.gameDatas;
        scoreCount = gameDatas.ScoreCount;
        bestScore = gameDatas.BestScore;
        heartCount = gameDatas.HeartCount;
    }
    private void WriteDatas()
    {
        levelText.text = "Level: " + gameDatas.LevelIndex.ToString();
        scoreTextGame.text = scoreCount.ToString();
    }

    private void AddActions()
    {
        gameManager.GameWin += GameWin;
        gameManager.GameFail += GameFail;
        gameManager.SaveData += SaveData;
    }
    private void SetStartSettings()
    {
        if (isGameRestart)
        {
            StartCoroutine(WaitForLoad());
        }
        else
        {
            startPanel.SetActive(true);
            bestScoreTextStartPanel.text = "Best Score: " + gameDatas.BestScore.ToString();
        }

        for (int i = heartCount; i < heartList.Count; i++)
        {
            heartList[i].color = heartGrayColor;
        }
    }

    public void UpdateScore(int getScoreValue)
    {
        scoreCount += getScoreValue;
        if (scoreCount > bestScore)
        {
            bestScore = scoreCount;
        }
        scoreTextGame.text = scoreCount.ToString();
    }

    public void SaveData()
    {

    }
    public void GameStart()
    {
        gameManager.Initialize();
        gameManager.GameStart();
        startPanel.SetActive(false);
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(0.1f);
        GameStart();
    }

    public void NewGame()
    {
        isGameRestart = true;
        gameDatas.HeartCount = 3;
        LevelManager.Instance.NewGame();
    }

    public void GameRestart()
    {
        isGameRestart = true;
        gameManager.RestartLevel();
    }
    public void GameWin()
    {
        gameDatas.ScoreCount = scoreCount;
        gameDatas.BestScore = bestScore;
        isGameRestart = true;
    }

    public void GameFail()
    {
        gameDatas.HeartCount--;
        if (gameDatas.HeartCount < 0)
        {
            gameDatas.HeartCount = 3;
            isGameRestart = false;
            restartPanel.SetActive(true);
            bestScoreTextRestartPanel.text = "Best Score: " + bestScore.ToString();
            scoreTextRestartPanel.text = "Score: " + scoreCount.ToString();
        }
        else
        {
            GameRestart();
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

