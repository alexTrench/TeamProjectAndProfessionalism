using UnityEngine;

public class RPGexplosion : MonoBehaviour
{

    public float radius = 5f;
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
        if(gameObject != null && gameObject.activeInHierarchy) {
            Debug.Log("BOOM!");

            //show effect
            Instantiate(
                explosionEffect, 
                transform.position, 
                transform.rotation, 
                gameObject.transform
            );
            //get nearby objects
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null && gameObject.tag != "Projectille")
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectille" || collision.gameObject.tag == "Player")
        {

        }
        else
        {
            print("hit" + collision.gameObject.tag + "!");
            Explode();
        }
    }
}
