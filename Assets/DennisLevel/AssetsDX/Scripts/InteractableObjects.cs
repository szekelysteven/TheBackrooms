using UnityEngine;

public class InteractableObjects : MonoBehaviour
{


    public float radius = 3f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
