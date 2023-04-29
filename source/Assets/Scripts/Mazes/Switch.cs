using UnityEngine;

public class Switch : MonoBehaviour
{
    public MeshCollider col;
    public MeshRenderer myRenderer;
    public Material material;
    public Transform mazeDoor;
    private readonly float translationConst = 1.42f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Ball")) {
            myRenderer.material = material;
            mazeDoor.position = new Vector3(mazeDoor.position.x - translationConst, mazeDoor.position.y, mazeDoor.position.z);
        }
    }
}
