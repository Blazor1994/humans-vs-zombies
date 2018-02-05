using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuScript : MonoBehaviour {

	public float cellCount = 10.0f;
	public float carCount = 0.0f;
	public float humanCount = 0.0f; 
	public float zombieCount = 0.0f;

	public UnityEngine.UI.Slider cellSlider;
	public UnityEngine.UI.Slider carSlider;
	public UnityEngine.UI.Slider humanSlider;
	public UnityEngine.UI.Slider zombieSlider;

	public UnityEngine.UI.Text cellText;
	public UnityEngine.UI.Text carText;
	public UnityEngine.UI.Text humanText;
	public UnityEngine.UI.Text zombieText;

	public GameManager gameManager;

	public void sliderChange(string type)
	{
		switch(type)
		{
			case "cell": 
				cellCount = cellSlider.value;
				cellText.text = cellSlider.value.ToString();
				break;
			case "car": 
				carCount = carSlider.value;
				carText.text = carSlider.value.ToString();
				break;
			case "human": 
				humanCount = humanSlider.value;
				humanText.text = humanSlider.value.ToString();
				break;
			case "zombie": 
				zombieCount = zombieSlider.value;
				zombieText.text = zombieSlider.value.ToString();
				break;
			
		}
	}

	public void startButton()
	{
		gameManager.BeginGame(cellSlider.value, carSlider.value, humanSlider.value, zombieSlider.value);
	}

	public void randomButton()
	{
		cellSlider.value = Random.Range(cellSlider.minValue, cellSlider.maxValue);
		carSlider.value = Random.Range(carSlider.minValue, carSlider.maxValue);
		humanSlider.value = Random.Range(humanSlider.minValue, humanSlider.maxValue);
		zombieSlider.value = Random.Range(zombieSlider.minValue, zombieSlider.maxValue);
		
	}
	// Use this for initialization
	void Start () {
		//cellSlider = GameObject.FindGameObjectWithTag("cellSlider");
	}
	
	// Update is called once per frame
	void Update () {

		carSlider.maxValue = cellSlider.value/2f;
		zombieSlider.maxValue = cellSlider.value/2;
		humanSlider.maxValue = cellSlider.value/2;
		
	}
}
