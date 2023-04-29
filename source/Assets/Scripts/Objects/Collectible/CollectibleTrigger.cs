using UnityEngine;
class CollectibleTrigger : MonoBehaviour
{
    public Collectible collectible;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("Player"))
            collectible.PickUp();
    }
}

