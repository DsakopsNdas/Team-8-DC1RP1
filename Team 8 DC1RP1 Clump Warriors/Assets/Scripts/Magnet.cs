using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    //floats
    public float magnetStrength = 15f;

    //transforms
    public Transform attachPoint;

    //bools
    public bool beingMagnetized;
    public bool attached;

    public Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        attachPoint = GameObject.FindGameObjectWithTag("Sword").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (beingMagnetized && attachPoint != null && !attached)
        {
            if (Input.GetMouseButton(0))
            {
                transform.position = Vector3.MoveTowards(transform.position, attachPoint.position, magnetStrength * Time.deltaTime);
            }
           
        }

        if (Vector3.Distance(transform.position, attachPoint.position) < 0.8f)
        {
            AttachToSword();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            beingMagnetized = true;
            gameObject.tag = "Sword";
        }
    }
    private void AttachToSword()
    {
        beingMagnetized = false;
        attached = true;

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.constraints = RigidbodyConstraints.FreezeAll;

            transform.SetParent(attachPoint);
        }
    }
}
