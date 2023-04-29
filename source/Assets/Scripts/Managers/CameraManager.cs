using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera boyCamera;
    public Camera staticCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            boyCamera.gameObject.SetActive(true);
            staticCamera.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            boyCamera.gameObject.SetActive(false);
            staticCamera.gameObject.SetActive(true);
        }
    }
}
