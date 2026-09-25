using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    public Rigidbody rb;

    public Transform sword;

    public ProgressionManager progressionManager;

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
        

        if (other.CompareTag("Sword"))
        {

            if (this.tag == "Goblin")
            {
                progressionManager.goblin += 1;
                //Debug.Log(progressionManager.goblin);
                AttachTo(other.transform);
                return;
            }
            if (this.tag == "Orc")
            {
                if (progressionManager.goblin == 10)
                {
                    progressionManager.orc += 1;

                    AttachTo(other.transform);
                    return;
                }
            }
            if (this.tag == "Monster")
            {

                if (progressionManager.orc == 5)
                {
                    progressionManager.monster += 1;

                    AttachTo(other.transform);
                    return;
                }

            }
            if (this.tag == "Dragon")
            {
                if (progressionManager.monster == 3)
                {
                    progressionManager.dragon += 1;

                    AttachTo(other.transform);
                    return;
                }

            }


        }

        Sticky otherEnemy = other.GetComponentInParent<Sticky>();

        if (otherEnemy != null && otherEnemy.attached)
        {

            if (this.tag == "Goblin")
            {
                progressionManager.goblin += 1;
                //Debug.Log(progressionManager.goblin);
                AttachTo(otherEnemy.transform);
                return;
            }
            if (this.tag == "Orc")
            {
                if (progressionManager.goblin == 10)
                {
                    progressionManager.orc += 1;
                    
                    AttachTo(otherEnemy.transform);
                    return;
                }
            }
            if (this.tag == "Monster")
            {
                
                if (progressionManager.orc == 5)
                {
                    progressionManager.monster += 1;
                   
                    AttachTo(otherEnemy.transform);
                    return;
                }
                
            }
            if (this.tag == "Dragon")
            {
               if (progressionManager.monster == 3)
                {
                    progressionManager.dragon += 1;

                    AttachTo(otherEnemy.transform);
                    return;
                }
                
            }

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
