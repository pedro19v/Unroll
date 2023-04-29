using UnityEngine;

public class HeldItemsUI : MonoBehaviour
{
    bool isEnabled = false;
    public GameObject heldItemUI;

    // Update is called once per frame
    void Update()
    {
        if (Boy.hasKey && !isEnabled)
        {
            isEnabled = true;
            heldItemUI.SetActive(true);
        }
        
        else if (!Boy.hasKey)
        {
            isEnabled = false;
            heldItemUI.SetActive(false);
        }
    }
}
