//Cody Bonnett
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator swordAnim;
    public AudioSource swordSwing; // Audio source for sword swing sound
    // Start is called before the first frame update
    void Start()
    {
        swordAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //If user inputs left click, trigger "Slicing" animation perameter
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            swordAnim.SetTrigger("Slicing");
            swordSwing.Play(); // Play the sword swing sound
        }
    }
}
