using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGun : MonoBehaviour {
   
	public GameObject bulletPrefab;
    public Transform bulletSpawn;
	void Start () {

	}

	// Update is called once per frame
	public void Update () {
 
      
    }
    
    public void fire()
    {

        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.rotation);
   
        // Add velocity to the bullet
         bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
       // bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
   
        // Destroy the bullet after 4 seconds
        Destroy(bullet, 1.3F);
    }
        
	}
