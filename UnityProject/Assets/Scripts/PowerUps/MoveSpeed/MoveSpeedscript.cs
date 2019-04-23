using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedscript : MonoBehaviour
{

    public GameObject pickupEffect;
    [SerializeField]
    private int duration = 2;

 
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }


    IEnumerator Pickup(Collider player)
    {
        player.GetComponent<PlayerController>().SetMovementSpeed(20f);
        Instantiate(pickupEffect, transform.position, transform.rotation);


        //disable the power up object on the level so we cannot collide with it again till it destroys itself later
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        player.GetComponent<PlayerController>().SetMovementSpeed(12f);
        Destroy(gameObject);
    }

}
