using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlayScript : MonoBehaviour {


	public UnityEngine.UI.Text humanText;
	public UnityEngine.UI.Text humanCount;
	public UnityEngine.UI.Text zombieText;
	public UnityEngine.UI.Text zombieCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		zombieCount.text =  GameObject.FindGameObjectsWithTag("Zombie").Length.ToString();
		humanCount.text =  GameObject.FindGameObjectsWithTag("Human").Length.ToString();
		
	}
}
