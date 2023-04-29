using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Lamp : MonoBehaviour
{
    public Ball ball;

    public Light spotLight;

    public GameObject plane;

    public ElementalColor reactionColor;
    // Start is called before the first frame update
    void Start()
    {
        spotLight.enabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && ball.color.Equals(reactionColor))
        {
            spotLight.enabled = true;
            plane.SetActive(true);
        }
    }
}
