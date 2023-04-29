using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Collectible;

public class CollectiblesPanel : MonoBehaviour
{
    public LoadZone load;

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, bool> isCollected = load.GetCollectibles();

        foreach (string id in isCollected.Keys)
            if (isCollected[id])
                ShowCollectible(id);
    }

    private void ShowCollectible(string id)
    {
        Debug.Log("Showing " + id);
        string levelName = GetLevelName(id);
        string metalName = GetMetalName(id);
        Debug.Log(metalName);
        Transform collectibleTransform = transform.Find(levelName).Find(metalName);
        Debug.Log(collectibleTransform);

        collectibleTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = metalName + " " + Const.LEVEL_TO_SHAPE[levelName];
        
        Transform container = collectibleTransform.Find("Container");
        container.Find("?").gameObject.SetActive(false);
        container.Find("Image").gameObject.SetActive(true);
        collectibleTransform.Find("+").gameObject.SetActive(true);
    }
}
