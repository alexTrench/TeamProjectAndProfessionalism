using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            IDamagable damagable = GameObject.FindGameObjectWithTag("Player").GetComponent<IDamagable>();
            if (damagable != null)
                damagable.TakeDamage(1);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            IDamagable damagable = GameObject.FindGameObjectWithTag("Enemy").GetComponent<IDamagable>();
            if (damagable != null)
                damagable.TakeDamage(1);
        }
    }
}
