using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class humanScript : MonoBehaviour {
    public GameObject CartoonSMG;
    public bool isParent = false;
    float humanSpeedNormal = 5f;
    int humanHealth = 10;
    NavMeshAgent agent;
    public Transform target;
    // Use this for initialization

    public RaycastHit hit;
    IEnumerator wait (int time) {
        yield return new WaitForSeconds (time);
    }
    
    void Start () {

        agent = GetComponent<NavMeshAgent> ();
        agent.Warp(gameObject.transform.position);
        target = GameObject.FindGameObjectWithTag("Finish").transform;
        
    }
    //Reference: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    public GameObject FindClosestEnemy () {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag ("Zombie");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    void FixedUpdate () {

        Vector3 fwd = transform.TransformDirection (Vector3.forward);
        Vector3 offset = new Vector3(0,0,1);
        float distance = Vector3.Distance (transform.position, FindClosestEnemy ().transform.position);
        if (Physics.Raycast (transform.position +  transform.up * 2.0f, fwd, out hit, 15) && hit.transform.tag == "gun") {
           // Debug.Log ("I GOT YOU");
            agent.SetDestination (hit.transform.position);
            //    agent.SetDestination (hit.transform.position -1);
        } else if (Physics.Raycast (transform.position + transform.up * 2.0f, fwd, out hit, 5) && hit.transform.tag == "Wall") {
            transform.position = Vector3.MoveTowards (transform.position, hit.transform.position, -1 * humanSpeedNormal * Time.deltaTime);
            //  agent.SetDestination (hit.transform.position -1);
        } else {
            agent.SetDestination(target.position);
        }
        if (Physics.Raycast(transform.position + transform.up * 2.0f, fwd, out hit))
        {
            if (hit.transform.tag == "Zombie"){
                Debug.Log("FIRE TWO");
                if (gameObject.transform.name == "armed_guy_1"){
                {
                    
                    
                    

                }
            } }
        }
    }
         
    
    void Update () {

        // FindClosestEnemy ().transform.position
        //    agent.SetDestination (hit.transform.position -1);
        /* float timeLeft = 15;
         if (humanHealth < 10) {
             if (humanHealth != 0) {
                 timeLeft -= Time.deltaTime;
                 if (timeLeft <= 15) {
                     Debug.Log (timeLeft + "Till Human turns");
                 }
             }
         }
         */
    }
    IEnumerator zombieInfection (float time) {

        yield return new WaitForSeconds (time);
                Destroy(gameObject);
                Vector3 currentpos = transform.position;
                GameObject dHuman = Instantiate(Resources.Load("zombie")) as GameObject;
                dHuman.transform.position = currentpos;

    }
    private void OnCollisionEnter (Collision collision) {
        // If the tag of the collided object matches ''...
        if (collision.gameObject.tag == "gun") {
			 Destroy(gameObject);
            Vector3 currentpos = transform.position;
            GameObject dHuman = Instantiate(Resources.Load("armed_guy_1")) as GameObject;
            dHuman.transform.position = currentpos;
        }
        if (collision.gameObject.tag == "Wall") {
           // Debug.Log ("Hit a Wall");
        }
        if (collision.gameObject.tag == "Finish") {
            Debug.Log ("Human got to Objective, destorying human");
              Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Zombie") {
            humanHealth--;
            Debug.Log ("Human got hurt, takes 1 HP damage. Current HP: " + humanHealth);
            if (humanHealth <= 0) {
                Destroy(gameObject);
                Vector3 currentpos = transform.position;

                GameObject dHuman = Instantiate(Resources.Load("Dead Human")) as GameObject;
                dHuman.transform.position = currentpos;

            }

            if (humanHealth < 10) {
                if (humanHealth != 0) {
                    StartCoroutine (zombieInfection (20f));
                }
            }
        }
    }
}