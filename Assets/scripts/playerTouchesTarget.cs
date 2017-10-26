using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTouchesTarget : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
    void OnCollisionEnter(Collision collision) {

            if (collision.gameObject.name == "Human")
            {

                Destroy(collision.gameObject);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }

    }



