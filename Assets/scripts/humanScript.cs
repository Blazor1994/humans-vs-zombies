using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class humanScript : MonoBehaviour
{
    float humanSpeedNormal = 4.5f;
    NavMeshAgent agent;
    public Transform target;
    public RaycastHit hit;
    // Use this for initialization
    void Start()
    {
   
        agent = GetComponent<NavMeshAgent>();
    }
    //Reference: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    void Update()
    {

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        float distance = Vector3.Distance(transform.position, FindClosestEnemy().transform.position);

        if (Physics.Raycast(transform.position, fwd, out hit, 1) && hit.transform.tag == "Zombie")
        {

            Debug.Log("Enemy spotted and is too close, run!");
            //Get the zombie distance from the human
            var zombieDistance = Vector3.Distance(transform.position, FindClosestEnemy().transform.position);
            //If the zombie is less than 10 meters away

            //   transform.LookAt(2 * transform.position - FindClosestEnemy().transform.position);
            //For documentation on Time.deltaTime https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
            transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().transform.position, -1 * humanSpeedNormal * Time.deltaTime);


        }
        else if (Physics.Raycast(transform.position, fwd, out hit, 15) && hit.transform.tag == "gun")
        {

            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("gun").transform.position, 1 * humanSpeedNormal * Time.deltaTime);
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }
        private void OnCollisionEnter(Collision collision)
        {
            // If the tag of the collided object matches ''...
            if (collision.gameObject.tag == "gun")
            {
                Destroy(collision.gameObject);
           

        }
            if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("Hit a Wall");
            }
            //Debug.Log("Collided!");
        }

    }
    

