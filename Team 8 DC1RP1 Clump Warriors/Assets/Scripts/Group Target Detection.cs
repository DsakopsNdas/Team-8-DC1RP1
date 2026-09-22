using UnityEngine;

public class GroupDetectionTarget : MonoBehaviour
{
    public Transform target = null;
    public bool targeted = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject.GetComponent<Transform>();
            targeted = true;
        }
    }
}
