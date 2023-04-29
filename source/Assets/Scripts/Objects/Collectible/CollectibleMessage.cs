using UnityEngine;

public class CollectibleMessage : MonoBehaviour
{
    public GameObject pauseMenu;
    public void HideMessage()
    {
        gameObject.SetActive(false);
        if (pauseMenu != null && !pauseMenu.gameObject.activeSelf)
            Cursor.visible = false;
    }
}
