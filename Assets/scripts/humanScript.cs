using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class humanScript : MonoBehaviour {
    float humanSpeedNormal = 4.5f;
    int humanHealth = 10;
    NavMeshAgent agent;
    public Transform target;
    // Use this for initialization
    IEnumerator wait (int time) {

        yield return new WaitForSeconds (time);

    }
    void Start () {

        agent = GetComponent<NavMeshAgent> ();
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
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection (Vector3.forward);
        float distance = Vector3.Distance (transform.position, FindClosestEnemy ().transform.position);
        if (Physics.Raycast (transform.position + transform.up * 2.0f, fwd, out hit, 15) && hit.transform.tag == "gun") {
            Debug.Log ("I GOT YOU");
            agent.SetDestination (hit.transform.position);
        } else {
            agent.SetDestination (target.position);
        }

        /*  if (Physics.Raycast (transform.position + transform.up * 2.0f, fwd, out hit, 10) && hit.transform.tag == "Zombie") {
              Debug.Log ("Enemy spotted and is too close, run!");
              //   transform.LookAt(2 * transform.position - FindClosestEnemy().transform.position);
              //For documentation on Time.deltaTime https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
              transform.position = Vector3.MoveTowards (transform.position, FindClosestEnemy ().transform.position, -1 * humanSpeedNormal * Time.deltaTime);
              agent.SetDestination (target.position);
          } else {
              agent.SetDestination (target.position);
          }
          */

    }
    void Update () {
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
        Vector3 humanPos = transform.position;
        Quaternion humanRot = transform.rotation;
        Destroy (this.gameObject);
        GameObject myRoadInstance = Instantiate (Resources.Load ("road"), new Vector3 (5, 5, 5), Quaternion.humanRot) as GameObject;
        Instantiate (Resources.Load ("zombie"), humanPos, humanRot);
    }
    private void OnCollisionEnter (Collision collision) {
        // If the tag of the collided object matches ''...
        if (collision.gameObject.tag == "gun") {
            Destroy (collision.gameObject);

        }
        if (collision.gameObject.tag == "Wall") {
            Debug.Log ("Hit a Wall");
        }
        if (collision.gameObject.tag == "Shop") {
            Debug.Log ("Human got to Objective, destorying human");
        }
        if (collision.gameObject.tag == "Zombie") {
            humanHealth--;
            Debug.Log ("Human got hurt, takes 1 HP damage. Current HP: " + humanHealth);
            if (humanHealth <= 0) {
                Destroy (this.gameObject);
            }
            if (humanHealth < 10) {
                if (humanHealth != 0) {
                    StartCoroutine (zombieInfection (15f));
                }
            }
        }
    }
}