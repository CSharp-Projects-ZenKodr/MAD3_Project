using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{

    [SerializeField]
    public int coinSpeed = 20; // Sets the coinSpeed to public 

    public GameManager gm;

    // Update is called once per frame
    void Update()
    {
        // Updates the transform rotation of the X-AXIS.
        transform.Rotate(0, coinSpeed * Time.deltaTime, 0);
    }

    /**
     * Checks if something collided with the coin and if its the player
     * Then destorys the coin and plays the coin sound effect
     * Add 1 coin to the existing counter of coins the player has collected
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().PlaySound("Coin");
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")+1);
        }
    }

}
