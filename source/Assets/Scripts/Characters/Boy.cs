using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Boy : MonoBehaviour
{
    public float PUSH_FORCE;
    
    public static bool hasKey = false;

    // Text boxes
    public GameObject grabBallTextBox;
    public GameObject dropBallTextBox;
    public GameObject hardPushTextBox;

    public GameObject ballCollider;
    public Ball ball;

    private bool hasBall = false;
    private bool ballInRange;

    private InformationToSend information;
    private float sendTimer = 0;

    private int currentLevel;
    private LoadZone load;

    private void Start()
    {
        ReleaseBall();
        information = new InformationToSend();
        load = FindObjectOfType<LoadZone>();
        information.level = currentLevel = load.GetLevelToLoad();
    }

    private void Update()
    {
        if (hasBall)
        {
            if (Input.GetKeyDown(KeyCode.E) || !ballInRange)
            {
                ReleaseBall();
                information.pressE++;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ReleaseBall();
                ball.transform.GetComponent<Rigidbody>().AddForce(transform.forward * PUSH_FORCE);
            }
        }
        else // Not grabbing ball
        {
            dropBallTextBox.SetActive(false);
            hardPushTextBox.SetActive(false);

            if (Input.GetKeyDown(KeyCode.E) && ballInRange)
            {
                grabBallTextBox.SetActive(false);
                dropBallTextBox.SetActive(true);
                hardPushTextBox.SetActive(true);
                GrabBall();
                information.pressE++;
                information.ballGrabbed++;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Ball tempBall = FindObjectOfType<Ball>();
                if ((tempBall.transform.position - transform.position).magnitude < 3)
                {
                    information.pressE++;
                }
            }
        }

        if (sendTimer > 60)
        {
            ReportInformation(information);
            information = new InformationToSend
            {
                level = currentLevel
            };
            sendTimer = 0;
        }
        else
        {
            sendTimer += Time.deltaTime;
        }
    }

    private void GrabBall()
    {
        hasBall = true;
        ballCollider.SetActive(true);
        ballCollider.transform.position = 
            new Vector3(transform.position.x, ball.transform.position.y, transform.position.z) + 
                transform.forward * ball.DISTANCE_TO_BOY;
        ball.OnGrab();
    }

    public void ReleaseBall()
    {
        hasBall = false;
        ball.OnRelease();
        ballCollider.SetActive(false);
    }

    public bool HasBall()
    {
        return hasBall;
    }

    public void HasDied()
    {
        information.numberOfDeaths ++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBall && other.transform.CompareTag("Ball"))
        {
            grabBallTextBox.SetActive(true);
            ballInRange = true;
        }
    }

    public void GrabKey(GameObject key)
    {
        hasKey = true;
        key.SetActive(false);
        Debug.Log("Key picked up");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            grabBallTextBox.SetActive(false);
            ballInRange = false;
        }
    }

    public bool CheckIfHasKey()
    {
        return hasKey;
    }

    public void CollectiblePickedUp(Collectible.Metal metal)
    {
        switch(metal)
        {
            case Collectible.Metal.Copper:
                information.copperCollectible ++;
                break;
            case Collectible.Metal.Silver:
                information.silverCollectible ++;
                break;
            case Collectible.Metal.Gold:
                information.goldCollectible ++;
                break;
            default:
                Debug.LogWarning("No Corresponding Metal Found");
                break;
        }
    }

    public void ForceReportInformation()
    {
        ReportInformation(information);
    }

    private void ReportInformation(InformationToSend information)
    {
        AnalyticsEvent.Custom("periodic_info", new Dictionary<string, object>{
            { "random_number", Global.random },
            { "level", information.level },
            { "press_E", information.pressE },
            { "ball_grabbed", information.ballGrabbed },
            { "copper_colectibles", information.copperCollectible },
            { "silver_colectibles", information.silverCollectible },
            { "gold_colectibles", information.goldCollectible },
            { "number_of_deaths", information.numberOfDeaths }
        });
    }

    struct InformationToSend
    {
        public int level;
        public int pressE;
        public int ballGrabbed;
        public int copperCollectible;
        public int silverCollectible;
        public int goldCollectible;
        public int numberOfDeaths;
    }
}
