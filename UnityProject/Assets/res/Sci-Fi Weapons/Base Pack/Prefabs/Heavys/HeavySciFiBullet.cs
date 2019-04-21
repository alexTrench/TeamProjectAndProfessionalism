using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySciFiBullet : MonoBehaviour
{

    //this is the same as the normal bullet behaviour, however since the sci fi guns use 
    //raycasting to damage enemys there is no need to make the bullets also damage enemys
    //so anything relating to the damaging of enemys removed from this script

    //dont need this right now as we dont need any of the information from the database 
    //[SerializeField] private int id = 0;
    //GameObject gameController;
    //weaponDatabase database;
    //how long the bullets stay till they despawn
    public float Life = 4f;


    private void Start()
    {
        //gameController = GameObject.FindGameObjectWithTag("GameController");
        //database = gameController.GetComponent<weaponDatabase>();
    }

    private void Update()
    {
        //call the destruction function with delay set to bullets lifespan
        StartCoroutine(DestroyBulletAfterTime(gameObject, Life));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectille")
        {

        }
        else if (other.gameObject.tag == "Enemy")
        {
         
            Destroy(gameObject);
        }
        else
        {

        }
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        //after a time destroy the bullet
        yield return new WaitForSeconds(delay);

        //destroys the gameobeject
        Destroy(bullet);

    }
}