using UnityEngine;
using TMPro;
using ECM.Walkthrough.OverShoulderCamera;
using ECM.Components;

public class Ball : MonoBehaviour
{
    public ElementalColor color;
    public TMP_Text ballColorTextBox;

    public float DISTANCE_TO_BOY;
    public float STICK_THRESHOLD;
    public float RELEASE_THRESHOLD;

    public Rigidbody colliderRigidbody;
    public Boy boy;

    private MeshRenderer myRenderer;
    private Rigidbody myRigidbody;
    private SphereCollider myCollider;

    private BlocksManager blocksManager;

    private Vector3 lastPosition;
    private float RADIUS;
    
    public Transform ecmTransform;

    public Rigidbody ballRigidbody;

    public Rigidbody ecmRigidbody;

    public CapsuleCollider ecmCollider;

    public SphereCollider ballCollider;

    public MyCharacterController myCharacterController;

    public CharacterMovement characterMovement;

    public Camera ballCam;

    void Awake()
    {
        myRenderer = GetComponent<MeshRenderer>();
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<SphereCollider>();
        blocksManager = FindObjectOfType<BlocksManager>();
    }

    private void Start()
    {
        lastPosition = transform.position;
        RADIUS = transform.localScale.y / 2;
    }

    private void Update()
    {
        if (ballColorTextBox != null)
        {
            ballColorTextBox.text = color.displayName;
            ballColorTextBox.color = color.color;
        }

        if (boy.HasBall())
        {
            UpdatePosition();
            UpdateRotation();
        }
    }

    private void UpdatePosition()
    {
        lastPosition = transform.position;

        transform.position = new Vector3(
            boy.transform.position.x + boy.transform.forward.x * DISTANCE_TO_BOY,
            colliderRigidbody.transform.position.y,
            boy.transform.position.z + boy.transform.forward.z * DISTANCE_TO_BOY
        );

        //CheckDistanceToFloor();
    }

    private void CheckDistanceToFloor()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit downHit, LayerMask.GetMask("Default")))
        {
            if (downHit.distance > RADIUS + RELEASE_THRESHOLD)
                boy.ReleaseBall();
            else if (downHit.distance >= RADIUS && downHit.distance <= RADIUS + STICK_THRESHOLD)
                StickToFloor(downHit);      
        }
    }

    private void StickToFloor(RaycastHit downHit)
    {
        transform.position += Vector3.down * (downHit.distance - RADIUS);
        if (Physics.Raycast(transform.position, -downHit.normal, out RaycastHit normalHit, RADIUS + STICK_THRESHOLD, LayerMask.GetMask("Default")))
            colliderRigidbody.velocity = downHit.normal * (normalHit.distance - RADIUS) / Time.deltaTime;
    }

    private void UpdateRotation()
    {
        Vector3 difference = transform.position - lastPosition;
        Vector3 cross = Vector3.Cross(Vector3.up, difference.normalized);

        transform.Rotate(cross, difference.magnitude * Mathf.Rad2Deg / RADIUS, Space.World);
    }

    public void ChangeColor(ElementalColor newColor)
    {
        blocksManager.ChangeBreakableBlocks(color, newColor);
        myRenderer.material = newColor.ballMaterial;
        color = newColor;
    }

    public void OnGrab()
    {
        myRigidbody.velocity = Vector3.zero; // So that on release the velocity is forgotten
        myRigidbody.isKinematic = true;
        myCollider.enabled = false;
        transform.position = colliderRigidbody.transform.position;
    }

    public void OnRelease()
    {
        myRigidbody.isKinematic = false;
        myCollider.enabled = true;
    }
    public void ActivateControl() {
        ballRigidbody.isKinematic = true;
        ecmRigidbody.isKinematic = false;
        ecmCollider.enabled = true;
        ballCollider.enabled = false;
        myCharacterController.enabled = true;
        characterMovement.enabled = true;
        Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1 , gameObject.transform.position.z);
        ecmTransform.position = newPos;
        ecmTransform.rotation = Quaternion.identity;
        ballCam.gameObject.SetActive(true);
        ballColorTextBox.gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.transform.localPosition = new Vector3(0, 1f, 0);
    }

    public void DeactivateControl() {
        ballRigidbody.isKinematic = false;
        ecmRigidbody.isKinematic = true;
        ecmCollider.enabled = false;
        ballCollider.enabled = true;
        myCharacterController.enabled = false;
        characterMovement.enabled = false;
        ballCam.gameObject.SetActive(false);
        ballColorTextBox.gameObject.transform.parent.gameObject.SetActive(false);

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Key"))
        {
            boy.GrabKey(other.gameObject);
        }
    }*/
}
