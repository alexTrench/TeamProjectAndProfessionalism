using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedscript : MonoBehaviour
{

    public GameObject pickupEffect;
    [SerializeField]
    private int duration = 10;
    private float multiplier = 2f;
 
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }


    IEnumerator Pickup(Collider player)
    {
        float speed = player.GetComponent<PlayerController>().GetMovementSpeed();
        player.GetComponent<PlayerController>().SetMovementSpeed(speed * multiplier);
        Instantiate(pickupEffect, transform.position, transform.rotation);


        //disable the power up object on the level so we cannot collide with it again till it destroys itself later
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        player.GetComponent<PlayerController>().SetMovementSpeed(speed);
        Destroy(gameObject);
    }

}
