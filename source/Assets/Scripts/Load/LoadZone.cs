using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadZone : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;
    private int levelReached;

    private Dictionary<string, bool> isCollected;

    private void Awake()
    {
        LoadGame();
        levelToLoad = SceneManager.GetActiveScene().buildIndex;
    }

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        if (levelReached < levelToLoad)
        {
            levelReached = levelToLoad;
        }
        animator.SetTrigger("FadeOut");
    }

    public int GetLevelToLoad()
    {
        return levelToLoad;
    }

    public int GetMaxLevel()
    {
        return levelReached;
    }

    public void Collect(string id)
    {
        isCollected[id] = true;
        SaveGame();
    }

    public void OnFadeComplete()
    {
        SaveGame();

        SceneManager.LoadScene(levelToLoad);
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");

        SaveData saveData = new SaveData
        {
            currentLevel = levelToLoad,
            isCollected = isCollected
        };

        if (levelToLoad > levelReached)
        {
            saveData.levelReached = levelToLoad;
            levelReached = levelToLoad;
        }
        else
        {
            saveData.levelReached = levelReached;
        }

        bf.Serialize(file, saveData);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/MySaveData.dat");
            if (file.Length > 0)
            {
                SaveData saveData = (SaveData) bf.Deserialize(file);
                file.Close();
                levelToLoad = saveData.currentLevel;
                if (saveData.levelReached > levelReached)
                {
                    levelReached = saveData.levelReached;
                }
                isCollected = saveData.isCollected;
            }
            else
                ResetSaveData();
        }
        else
            ResetSaveData();
    }

    private void ResetSaveData()
    {
        levelToLoad = 1;
        levelReached = 0;

        isCollected = new Dictionary<string, bool>();
        foreach (string levelName in Const.LEVEL_NAMES)
            if (levelName.Contains("Intermediate") || levelName.Contains("Hard"))
                foreach (Collectible.Metal metalName in Const.COLLECTIBLE_METALS)
                    isCollected.Add(levelName + "-" + metalName, false);

        SaveGame();
    }

    public Dictionary<string, bool> GetCollectibles()
    {
        return isCollected;
    } 

    [Serializable]
    struct SaveData
    {
        public int currentLevel;
        public int levelReached;
        public Dictionary<string, bool> isCollected;
    }

}
