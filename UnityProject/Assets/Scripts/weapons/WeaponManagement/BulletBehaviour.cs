using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private int id = 0;
    GameObject gameController;
    weaponDatabase database;
    public float Life = 4f;
    private float damage;
    [SerializeField]
    private ParticleSystem blood;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<weaponDatabase>();
        damage = database.weapons[id].damage;
    }

    private void Update()
    {
        //call the destruction function with delay set to bullets lifespan
        StartCoroutine(DestroyBulletAfterTime(gameObject, Life));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            other.GetComponent<Zombie>().GetComponentInParent<BaseCharacter>().TakeDamage((int)damage);
            //Debug.Log("damage = " + damage);
            Instantiate(blood, other.GetComponent<Zombie>().transform.position, Random.rotation);


        }
        else if(other.gameObject.tag == "Projectille")
        {

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

