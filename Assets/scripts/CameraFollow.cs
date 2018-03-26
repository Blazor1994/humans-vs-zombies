
using UnityEngine;

public class CameraFollow : MonoBehaviour {
public float speed = 0.2f;
public Vector3 offset;
public GameObject target = null;

void LateUpdate(){

            
    if(target == null){
        	
    }
    if(FindClosestHumanShop.hum != null){
         transform.position = FindClosestHumanShop.hum.transform.position + offset;
            target = FindClosestHumanShop.hum;
             transform.LookAt(target.transform);
    }

   
	
}

}
