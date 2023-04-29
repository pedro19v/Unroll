using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class OpenDoors : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public Boy boy;
    public GameObject lock_;
    private bool canOpenDoor = false;

    public GameObject OpenDoorTextBox;

    public LoadZone load;

    private float startLevelTime;

    private void Start()
    {
        startLevelTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canOpenDoor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canOpenDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canOpenDoor || boy.HasBall())
        {
            canOpenDoor = false;
            OpenDoorTextBox.SetActive(false);
        }

        if (canOpenDoor && Boy.hasKey) {

            OpenDoorTextBox.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                leftDoor.Rotate(0f, -90f, 0f);
                rightDoor.Rotate(0f, 90f, 0f);
                canOpenDoor = false;
                lock_.SetActive(false);
                Boy.hasKey = false;
                OpenDoorTextBox.SetActive(false);
                ReportTimeToCompleteLevel(Time.time - startLevelTime, load.GetLevelToLoad());
                boy.ForceReportInformation();
                Debug.Log(Time.time - startLevelTime);
                load.FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void ReportTimeToCompleteLevel(float time, int level)
    {
        AnalyticsEvent.Custom("time_to_complete_level", new Dictionary<string, object>
        {
            { "random_number", Global.random },
            { "time", time },
            { "level", level }
        });
    }
}
