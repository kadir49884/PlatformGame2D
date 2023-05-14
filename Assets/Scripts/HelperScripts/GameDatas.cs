using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "GameDatas", menuName = "GameDataManager/GameDatas", order = 1)]
public class GameDatas : ScriptableObject
{
    public int LevelIndex = 1;
    public int MaxLevelCount = 10;
    public int ScoreCount = 0;
    public int BestScore = 0;
    public int HeartCount = 3;


    [Button]
    public void ResetGameData()
    {
        LevelIndex = 1;
        ScoreCount = 0;
        MaxLevelCount = 10;
        BestScore = 0;
        HeartCount = 3;
        Debug.Log("GameData Reset");

    }
}
