using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameController gameController;
    public GameObject pickupEffect;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "SpeedBoost")
        {
            return;
        }

        if (other.tag == "Player")
        {
            Instantiate (pickupEffect, transform.position, transform.rotation);
        }
    }
}