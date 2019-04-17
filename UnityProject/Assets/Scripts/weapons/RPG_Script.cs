using UnityEngine;

public class RPG_Script : MonoBehaviour
{
    //the same id as what the shotgun is in the gamecontroller database of weapons
    int id = 2;
    public GameObject gameController;
    weaponDatabase database;
    public Transform bulletSpawnLocation;
    private float currentAmmo;
    //how long in between bullets
    private float nextFireTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<weaponDatabase>();
        currentAmmo = database.weapons[id].MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1) && Time.time >= nextFireTime)
        {
            Fire();
            //sets next fire time = to fire rate, making it fire at a rate of ever 0.2 seconds
            nextFireTime = Time.time + database.weapons[id].fireRate;
        }
        
    }

    public void Fire()
    {
        if(gameObject != null && gameObject.activeInHierarchy) {
            GameObject o = Instantiate(
                database.weapons[id].bulleType, 
                bulletSpawnLocation.position, 
                bulletSpawnLocation.rotation,
                gameObject.transform
            );
            //add the speed of the bullet to the rigid body
            o.GetComponent<Rigidbody>().AddForce(o.transform.forward * database.weapons[id].bulletSpeed);
            //makes it so the objects all under the layer "bullet" do not collide with each other
            Physics.IgnoreLayerCollision(8, 8);
        }
       
    }
    
}
