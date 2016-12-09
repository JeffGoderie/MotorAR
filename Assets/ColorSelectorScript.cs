using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelectorScript : MonoBehaviour {

	public GameObject[] colorBoxes;

	private Color[] availableColors = new Color[]{Color.red, Color.blue, Color.green, Color.yellow, Color.black, Color.cyan};

	private Color[] usedColors;

	// Use this for initialization
	void Start () {

		usedColors = new Color[colorBoxes.Length];

		for(int i = 0; i < colorBoxes.Length; i++){
			usedColors[i] = availableColors[i];
			colorBoxes[i].GetComponent<Renderer>().material.color = availableColors[i];
		}
	}
	
	public void changeColors(){
		Color main = usedColors[0];

		for(int i = 0; i < colorBoxes.Length - 1; i++){
			colorBoxes[i].GetComponent<Renderer>().material.color = usedColors[i+1];
			usedColors[i] = usedColors[i+1];
		}

		colorBoxes[colorBoxes.Length - 1].GetComponent<Renderer>().material.color = main;
		usedColors[colorBoxes.Length - 1] = main;
	}

	public Color getCurrentColor(){
		return usedColors[0];
	}
}
