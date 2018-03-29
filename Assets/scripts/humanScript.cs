using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class humanScript : MonoBehaviour
{
    public float fireRate = 2F;
    private float nextFire = 0.0F;
    //public bool isParent = false;
    float humanSpeedNormal = 10f;
    int humanHealth = 10;
    NavMeshAgent agent;
    private Transform target;
    // Use this for initialization
    public GameObject gunGuy;
    private fireGun gun;
    public RaycastHit hit;
    IEnumerator wait(int time)
    {
        yield return new WaitForSeconds(time);
    }

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        gun = GetComponent<fireGun>();
        agent.Warp(gameObject.transform.position);
        target = GameObject.FindGameObjectWithTag("Finish").transform;
        agent.SetDestination(target.position);

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
    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, FindClosestEnemy().transform.position);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 20))
        {
            if (hit.transform.tag == "gun")
            {
                if (gameObject.transform.name != "human2(Clone)")
                {
                    Debug.Log("gun");
                    agent.SetDestination(hit.transform.position);
                    if (hit.transform.tag != "gun")
                    {
                        agent.SetDestination(new Vector3(target.position.x, target.position.y, target.position.z));
                        Debug.Log("Setting destination fam");
                    }
                }
                else if (Vector3.Distance(gameObject.transform.position, hit.transform.position) <= 6)
                {

                    agent.SetDestination(hit.transform.InverseTransformDirection(Vector3.forward));
                    if (Vector3.Distance(gameObject.transform.position, hit.transform.position) > 5)
                    {
                        agent.SetDestination(new Vector3(target.position.x, target.position.y, target.position.z));
                        Debug.Log("Setting destination fam");
                    }
                }

                if (hit.transform.tag != "gun")
                {
                    agent.SetDestination(new Vector3(target.position.x, target.position.y, target.position.z));
                    Debug.Log("Setting destination fam");
                }
            }
            if (hit.transform.tag == "Wall")
            {
                Debug.Log("wall");
                agent.SetDestination(hit.transform.InverseTransformDirection(Vector3.forward));
                if (Vector3.Distance(gameObject.transform.position, hit.transform.position) < 10)
                {
                    agent.SetDestination(new Vector3(target.position.x, target.position.y, target.position.z));
                    Debug.Log("Setting destination fam");
                }
            }
            if (hit.transform.tag == "Zombie")
            {
                Debug.Log("Zombie fam, leg it bruv");
                agent.SetDestination(hit.transform.InverseTransformDirection(Vector3.forward));
                if (Vector3.Distance(gameObject.transform.position, hit.transform.position) < 7)
                {
                    agent.SetDestination(new Vector3(target.position.x, target.position.y, target.position.z));
                    Debug.Log("Setting destination fam");
                }
            }
        }
        if (gameObject.transform.name == "human2(Clone)")
        {
            if (distance < 2)
            {
                transform.LookAt(new Vector3(FindClosestEnemy().transform.position.x, FindClosestEnemy().transform.position.y, FindClosestEnemy().transform.position.z));
                //Fire rate based off https://answers.unity.com/questions/283377/how-to-delay-a-shot.html
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    Debug.Log("Firing Gun");
                    transform.LookAt(FindClosestEnemy().transform);
                    gun.fire();
                    if (Vector3.Distance(gameObject.transform.position, FindClosestEnemy().transform.position) > 2)
                    {
                        transform.LookAt(target);
                    }
                }
                if (Vector3.Distance(gameObject.transform.position, FindClosestEnemy().transform.position) > 2)
                {
                    transform.LookAt(target);
                }
            }
        }
    }



    void Update()
    {


    }
    IEnumerator zombieInfection(float time)
    {

        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        Vector3 currentpos = transform.position;
        currentpos.y += 0.01f;
        GameObject dHuman = Instantiate(Resources.Load("zombie")) as GameObject;
        dHuman.GetComponent<NavMeshAgent>().enabled = true;
        dHuman.GetComponent<ZombieNavMesh>().enabled = true;
        dHuman.GetComponent<RandomWalk>().enabled = true;
        dHuman.GetComponent<zombieScript>().enabled = true;
        dHuman.transform.position = currentpos;

    }
    IEnumerator fireTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // If the tag of the collided object matches ''...
        if (collision.gameObject.tag == "gun")
        {
            if (gameObject.transform.name != "human2(Clone)")
            {
                Debug.Log("Human got pickup");
                Destroy(gameObject);
                Vector3 currentpos = transform.position;
                GameObject dHuman = Instantiate(gunGuy) as GameObject;
                dHuman.transform.position = currentpos;

            }
        }
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Human got to Objective, destorying human");
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Zombie")
        {
            humanHealth--;
            Debug.Log("Human got hurt, takes 1 HP damage. Current HP: " + humanHealth);
            // if (humanHealth <= 0)
            // {
            //     Destroy(gameObject);
            //     Vector3 currentpos = transform.position;

            //     GameObject dHuman = Instantiate(Resources.Load("Dead Human")) as GameObject;
            //     dHuman.transform.position = currentpos;

            // }

            if (humanHealth < 10)
            {
                if (humanHealth != 0)
                {
                    StartCoroutine(zombieInfection(10f));
                }
            }
        }
    }
}