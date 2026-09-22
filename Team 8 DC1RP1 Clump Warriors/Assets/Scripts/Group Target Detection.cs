using UnityEngine;

public class GroupDetectionTarget : MonoBehaviour
{
    public Transform target = null;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject.GetComponent<Transform>();
        }
    }
}
