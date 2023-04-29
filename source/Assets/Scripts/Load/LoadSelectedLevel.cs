using UnityEngine;
using TMPro;

public class LoadSelectedLevel : MonoBehaviour
{
    public int levelId;

    public LoadZone load;


    private void Start()
    {
        load = FindObjectOfType<LoadZone>();
    }

    public void LoadLevel()
    {
         load.FadeToLevel(levelId);
    }
}
