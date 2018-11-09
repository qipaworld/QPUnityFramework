using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBaseStatus {
    public List<string> pauseKey = new List<string>();
    public static GameBaseStatus instance = null;
    public DataBase gameBaseStatusData;

    static public GameBaseStatus Instance
    {
        get
        {
            if (instance == null)
            {
                Init();
            }
            return instance;
        }
        // set {instance = value; }
    }
    public static void Init()
    {
        instance = new GameBaseStatus();
        instance.gameBaseStatusData = DataManager.Instance.addData("GameBaseStatus");
        instance.gameBaseStatusData.SetNumberValue("pauseGame", 0);
        instance.gameBaseStatusData.SetStringValue("GameScreenName", "StartScene");

    }
    public void PauseGame(string key)
    {
        if (!pauseKey.Contains(key))
        {
            Time.timeScale = 0;
            pauseKey.Add(key);
            gameBaseStatusData.SetNumberValue("pauseGame", 1);
        }

    }
    public void ResumeGame(string key)
    {
        pauseKey.Remove(key);
        if (pauseKey.Count == 0)
        {
            Time.timeScale = 1;
            instance.gameBaseStatusData.SetNumberValue("pauseGame", 0);
        }
    }
    public bool IsPauseGame()
    {
        return instance.gameBaseStatusData.GetNumberValue("pauseGame") == 1;
    }
    public bool IsPauseGameKey(string key)
    {
        return pauseKey.Contains(key);
    }
    public string GetRunScreenName()
    {
        return gameBaseStatusData.GetStringValue("GameScreenName");
    }
    public void SetRunScreenName(string key)
    {
        gameBaseStatusData.SetStringValue("GameScreenName",key);
    }
}
