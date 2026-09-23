using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    //floats
    public float magnetStrength;

    //transforms
    public Transform sword;
    public Transform attachPoint;

    //bools
    public bool beingMagnetized;
    private bool attached;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beingMagnetized && sword != null && !attached)
        {
            transform.position = Vector3.MoveTowards(transform.position, attachPoint.position, magnetStrength * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, sword.position) < 0.8f)
        {
            AttachToSword();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            sword = other.transform;
            beingMagnetized = true;

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

            transform.SetParent(sword);
        }

    }
}
