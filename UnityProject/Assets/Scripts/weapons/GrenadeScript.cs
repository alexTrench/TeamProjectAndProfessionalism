using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{

    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    private float countdown;
    private bool hasExploded = false;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;


        if(countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Debug.Log("BOOM!");

        //show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);
        //get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb !=null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
       
             //add force to them
             //damage them
             //remove grenade
        Destroy(gameObject);
    }

}
