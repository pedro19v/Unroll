using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public Transform anchor;
    public Transform plank;
    public Boy boy;
    bool canInteract = false;
    bool activated = false;
    public GameObject useLeverText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !boy.HasBall())
        {
            canInteract = true;
            useLeverText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = !boy.HasBall();
            useLeverText.SetActive(!boy.HasBall());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !boy.HasBall())
        {
            canInteract = false;
            useLeverText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInteract || boy.HasBall())
        {
            canInteract = false;
        }

        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            activated = !activated;
            plank.Rotate(0, 0, activated ? -40 : 40);
            anchor.Rotate(0, 0, activated ? 40 : -40);
        }
    }
}
