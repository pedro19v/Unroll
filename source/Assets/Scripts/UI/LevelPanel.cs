using TMPro;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    public LoadZone load;

    // Start is called before the first frame update
    void Start()
    {
        int levelReached = load.GetMaxLevel();
        int i = 0;

        foreach (string levelName in Const.LEVEL_NAMES)
        {
            if (levelReached > i)
            {
                if (i <= 2)
                    ShowLevel(levelName, "FirstRow");
                else
                    ShowLevel(levelName, "SecondRow");
                i ++;
            }
            else
                break;
        }

    }

    private void ShowLevel(string id, string row) {
        Transform selectLevelTransform = transform.Find(row).Find(id);

        selectLevelTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = id;

        Transform container = selectLevelTransform.Find("Container");
        container.Find("?").gameObject.SetActive(false);
        container.Find("Image").gameObject.SetActive(true);

    }
}
