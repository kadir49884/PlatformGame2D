using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{

    private GameDatas gameDatas;
    private GameManager gameManager;
    private int levelIndex;
    private int maxLevelCount;


    private void Start()
    {
        gameManager = GameManager.Instance;
        gameDatas = StartOperations.Instance.gameDatas;
        levelIndex = gameDatas.LevelIndex;
        maxLevelCount = gameDatas.MaxLevelCount;

        if (levelIndex > maxLevelCount)
        {
            levelIndex = Random.Range(1, maxLevelCount + 1);
        }

        GameObject levelObject = Resources.Load<GameObject>("Levels/Level" + levelIndex);

        Instantiate(levelObject);
    }


    public void NewGame()
    {
        gameDatas.LevelIndex = 1;
        gameManager.RestartLevel();
    }

    public void NextLevel()
    {

        gameDatas.LevelIndex++;
        gameManager.NextLevel();
    }

}
