
using UnityEngine;

public class CameraFollow : MonoBehaviour {
public float speed = 0.2f;
public Vector3 offset;
public GameObject target;

GameObject FindClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Human");
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
void LateUpdate(){

            
    if(target == null){
        	transform.position = FindClosestEnemy().transform.position + offset;
             transform.LookAt(target.transform);
    }
   
	
}

}
