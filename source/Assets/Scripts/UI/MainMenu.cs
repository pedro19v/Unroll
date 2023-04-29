using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public LoadZone load;

    public TMP_InputField randomCode;

    private void Start()
    {
        InitRandomString();
        load.LoadGame();
    }

    public void PlayGame()
    {
        load.LoadGame();
        SceneManager.LoadScene(load.GetLevelToLoad());
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void InitRandomString()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/Random.dat"))
        {
            file = File.OpenRead(Application.persistentDataPath + "/Random.dat");
            Global.random = (string) bf.Deserialize(file);   
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/Random.dat");
            Global.random = GetRandomString();
            bf.Serialize(file, Global.random);
        }
        file.Close();

        randomCode.text = Global.random;
    }

    private string GetRandomString()
    {
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 10)
            .Select(s => s[Random.Range(0, s.Length)]).ToArray());
    }
}
