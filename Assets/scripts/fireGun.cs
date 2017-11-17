using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGun : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var bullet = (GameObject)Instantiate(
               bulletPrefab,
               bulletSpawn.position,
               bulletSpawn.rotation);
          // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);        
    }
}
