using UnityEngine;

public class BallLight : MonoBehaviour
{
    public Light ballLight;
    public Ball ball;
    public ElementalColor reactionColor;

    private void Start()
    {
        ballLight.enabled = false;
    }

    private void Update()
    {
        if (!ball.color.Equals(reactionColor))
        {
            ballLight.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
            ballLight.enabled = true;
    }
}
