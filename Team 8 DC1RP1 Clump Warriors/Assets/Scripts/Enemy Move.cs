using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform targetTransform = null;
    public Vector3 targetLookPos = Vector3.zero;
    public float enemyMoveSpeed = 5f;
    public float enemyTurnSpeed = 0.1f;

    public Sticky stickyScript = null;

    private void Start()
    {
        stickyScript = GetComponent<Sticky>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.tag == "Untagged")
        {
            if (transform.parent.gameObject.GetComponent<GroupDetectionTarget>().targeted == true)
            {
                targetTransform = transform.parent.gameObject.GetComponent<GroupDetectionTarget>().target;
            }
        }
        
        
        if (targetTransform != null && stickyScript != null && stickyScript.attached == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, enemyMoveSpeed * Time.deltaTime);
            targetLookPos = Vector3.Lerp(transform.position, targetTransform.position, enemyTurnSpeed * Time.deltaTime);
            transform.LookAt(targetLookPos);
        }
    }
}
