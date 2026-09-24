using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    public Rigidbody rb;

    public Transform sword;

    private ProgressionManager progressionManager;

    public bool attached = false; 



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sword = GameObject.FindGameObjectWithTag("Sword").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(attached)
        {
            return;
        }

        if(other.CompareTag("Sword"))
        {
            if (this.tag == "Goblin")
            {
                progressionManager.goblin++;
                AttachTo(other.transform);
                return;
            }

            if (gameObject.tag == "Orc")
            {
                progressionManager.orc++;
               
                if (progressionManager.goblin == 10)
                {
                    AttachTo(other.transform);
                    return;
                }
                
            }

            if (gameObject.tag == "Monster")
            {
                progressionManager.monster++;
                if (progressionManager.orc == 5)
                {
                    AttachTo(other.transform);
                    return;
                }
                
            }
           
            if (gameObject.tag == "Dragon")
            {
                progressionManager.dragon++;
              if (progressionManager.monster == 3)
                {
                    AttachTo(other.transform);
                    return;
                }
                
            }


        }

        Sticky otherEnemy = other.GetComponentInParent<Sticky>();

        if (otherEnemy != null && otherEnemy.attached)
        {
            AttachTo(otherEnemy.transform);
        }

        
    }

   

    private void AttachTo(Transform parent)
    {
        EnemyMove movement = GetComponent<EnemyMove>();

        movement.enabled = false;
        attached = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.SetParent(parent, true);
    }
}
