using UnityEngine;

public class RPGexplosion : MonoBehaviour
{

    public float radius = 10f;
    //explosion particle effect
    public GameObject explosionEffect;
    //explosive force
    public float force = 700f;
   

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
      
            //show effect
            Instantiate(
                explosionEffect, 
                transform.position, 
                transform.rotation
            );
            //get nearby objects
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (gameObject.tag != "Projectille")
                {
                    Debug.Log("explosion force added!");
                    rb.AddExplosionForce(force, transform.position, radius);
                }
            }
            Debug.Log("bigger boom!");
            //add force to them
            //damage them
            //remove grenade
            Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Projectille" || collision.gameObject.tag == "Player")
        {

        }
        else
        {
            //print("hit" + collision.gameObject.tag + "!");
            Explode();
        }
    }
}
