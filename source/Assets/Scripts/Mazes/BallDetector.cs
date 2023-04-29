using UnityEngine;

public class BallDetector : MonoBehaviour
{
    public Helmet helmet;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Equals("Ball")  && !helmet.activated)
        {
            helmet.activate();
        }
    }
}
