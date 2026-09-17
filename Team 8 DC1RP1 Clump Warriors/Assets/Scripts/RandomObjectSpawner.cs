// Daniel Nguyen
// 000972335

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomObjectSpawner : MonoBehaviour
{
    //array for possible chest spots
    public Transform[] chestTransform = new Transform[10];

    //int for which spot the chest will go to
    public int currentChestSpot;

    //prefab for the chest that we'll spawn in randomized locations
    public GameObject chest;

    //tmp that we'll use to display the treasure count
    public TextMeshProUGUI treasureCountText;

    //the treasure count in question
    public int treasureCount = 0;

    //tmp that we'll use for the timer
    public TextMeshProUGUI timerText;

    //one minute timer
    public float timeLeft = 60;

    //game over scene to load to when game is completed
    public string targetScene;

    // Start is called before the first frame update
    void Start()
    {
        //set initial text
        treasureCountText.text = "Treasure Count: " + treasureCount;

        //randomly choose initial chest spot
        currentChestSpot = Random.Range(0, chestTransform.Length);

        //set chest's position to the chosen random spot's position
        chest.transform.position = new Vector3(chestTransform[currentChestSpot].position.x,
        chestTransform[currentChestSpot].position.y,
        chestTransform[currentChestSpot].position.z);

        //instantiate the actual chest
        Instantiate(chest);
    }

    //using fixed update for timer stuff since it seems more consistent
    private void FixedUpdate()
    {
        //count down time left
        timeLeft -= Time.fixedDeltaTime;

        //display time left on timer text
        timerText.text = "" + timeLeft;

        //if time runs out, go to target scene (set game over scene)
        if (timeLeft < 0)
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    //this function is called by treasure class when it collides with player
    public void UpdateCounter()
    {
        //add one to treasure count
        treasureCount++;

        //update treasure count text
        treasureCountText.text = "Treasure Count: " + treasureCount;

        //if the treasure count becomes ten, go to target scene (set game over scene)
        if(treasureCount >= 10)
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
