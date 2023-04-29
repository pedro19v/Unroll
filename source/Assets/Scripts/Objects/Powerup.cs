using UnityEngine;

public class Powerup : MonoBehaviour
{
    private readonly float ROTATE_SPEED = 60;

    private Ball ball;

    public ElementalColor color;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up, ROTATE_SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
            ball.ChangeColor(color);
    }
}
