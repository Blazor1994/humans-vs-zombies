using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGun : MonoBehaviour {

    public GameObject bulletPrefab;
   
    public bool isChild = false;
	GameObject tempBullet;
	void Start () {

	}

	// Update is called once per frame
	public void Update () {
 
      
    }
    public void fire()
    {

        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            gameObject.transform.position,
            gameObject.transform.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2);
    }
        
	}
