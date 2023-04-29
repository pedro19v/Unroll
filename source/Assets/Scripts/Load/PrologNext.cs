using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologNext : MonoBehaviour
{
    public void OnClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
