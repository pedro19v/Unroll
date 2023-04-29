using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPortal : MonoBehaviour
{
    public ElementalColor voidPurple;

    public Ball ball;

    public Transform otherPortal;

    private bool entered = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && ball.color.Equals(voidPurple) && ball.transform.position.y > transform.position.y)
        {
            entered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (entered && other.CompareTag("Ball") && ball.color.Equals(voidPurple) && ball.transform.position.y <= transform.position.y)
        {
            entered = false;
            Vector3 ballPos = ball.transform.position;
            ball.transform.position = otherPortal.position - Vector3.down * 0.01f; 
        }
    }
}
