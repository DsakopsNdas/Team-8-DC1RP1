using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform targettedPlayer = null;
    public float enemyMoveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.gameObject.GetComponent<GroupDetectionTarget>().target = targettedPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (targettedPlayer != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targettedPlayer.position, enemyMoveSpeed * Time.deltaTime);
        }
    }
}
