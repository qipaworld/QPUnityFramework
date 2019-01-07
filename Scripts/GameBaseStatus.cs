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
        instance.gameBaseStatusData.SetStringValue("GameSceneName", "StartScene");
        instance.gameBaseStatusData.SetStringValue("StartGameSceneName", "StartScene");
        instance.gameBaseStatusData.SetNumberValue("GameError", 0);
        instance.gameBaseStatusData.SetNumberValue("isUpdateGame", 0);

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
    public string GetRunSceneName()
    {
        return gameBaseStatusData.GetStringValue("GameSceneName");
    }
    public void SetRunSceneName(string key)
    {
        gameBaseStatusData.SetStringValue("GameSceneName",key);
    }
    public string GetStartSceneName()
    {
        return gameBaseStatusData.GetStringValue("StartGameSceneName");
    }
    public void SetStartSceneName(string key)
    {
        gameBaseStatusData.SetStringValue("StartGameSceneName", key);
    }
    public string GetGBMusicName()
    {
        return gameBaseStatusData.GetStringValue("GBMusicName");
    }
    public void SetGBMusicName(string key)
    {
        gameBaseStatusData.SetStringValue("GBMusicName", key);
    }
    public void SetGameError(int key)
    {
        instance.gameBaseStatusData.SetNumberValue("GameError", key);
    }
    public void BindError(ChangeDataDelegate change)
    {
        instance.gameBaseStatusData.Bind(change);
    }
    public void UnbindError(ChangeDataDelegate change)
    {
        instance.gameBaseStatusData.Unbind(change);
    }
    public void BindGameUpdate(ChangeDataDelegate change)
    {
        instance.gameBaseStatusData.Bind(change);
    }
    public void UnbindGameUpdate(ChangeDataDelegate change)
    {
        instance.gameBaseStatusData.Unbind(change);
    }
    public bool IsReadyUpdate()
    {
        return gameBaseStatusData.GetNumberValue("isUpdateGame") == 1;
    }
    public void SetReadyUpdate(bool ready)
    {
        if (ready)
        {
            gameBaseStatusData.SetNumberValue("isUpdateGame", 1);
        }
        else
        {
            gameBaseStatusData.SetNumberValue("isUpdateGame", 0);
        }
    }
}
