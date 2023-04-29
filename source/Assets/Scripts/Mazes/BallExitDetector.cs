using UnityEngine;

public class BallExitDetector : MonoBehaviour
{
    public Helmet helmet;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Equals("ECM_Ball") && helmet.activated)
        {
            helmet.deactivate();
            helmet.controlBall();
        }
    }
}
