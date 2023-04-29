using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void OnClick()
    {
        SceneManager.LoadScene("Menu");
    }
}