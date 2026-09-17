// Daniel Nguyen
// 000972335

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    //randomizer to grab spawner script from
    public GameObject randomizer;

    //spawner script so we can use variables from it
    public RandomObjectSpawner spawner;

    //find randomizer object and grab RandomObjectSpawner class from it
    private void Start()
    {
        randomizer = GameObject.FindWithTag("Randomizer");
        spawner = randomizer.GetComponent<RandomObjectSpawner>();
    }

    //on trigger
    private void OnTriggerEnter(Collider collision)
    {
        //with player tagged object
        if (collision.gameObject.tag == "Player")
        {
            //choose new random chest spot
            spawner.currentChestSpot = Random.Range(0, spawner.chestTransform.Length);

            //set chest's position to the chosen random spot's position
            spawner.chest.transform.position = new Vector3(spawner.chestTransform[spawner.currentChestSpot].position.x,
            spawner.chestTransform[spawner.currentChestSpot].position.y,
            spawner.chestTransform[spawner.currentChestSpot].position.z);

            //update the counter text
            spawner.UpdateCounter();

            //make a new chest
            Instantiate(spawner.chest);

            //then self destructs
            Destroy(gameObject);
        }
    }
}
