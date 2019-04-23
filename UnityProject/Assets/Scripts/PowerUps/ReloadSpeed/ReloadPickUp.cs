using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadPickUp : MonoBehaviour
{
    public GameObject pickupEffect;
    GameObject gameController;
    weaponDatabase database;
    public float duration = 4f;
    private int weaponDatabaseId;
    private float multiplier = 1.2f;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<weaponDatabase>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        Inventory playerInventory = player.GetComponent<Inventory>();
        for (int i = 0; i < database.weapons.Capacity; i++)
        {
            //adds the power up effect of more damage
            database.weapons[i].reloadTime = 0;
        }

        //disable the power up object on the level so we cannot collide with it again till it destroys itself later
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        for (int i = 0; i < database.weapons.Capacity; i++)
        {
            //removes the power up effect of more damage
            //after the duration has ended
            database.weapons[i].reloadTime = 3;
        }
        Destroy(gameObject);
    }
}
