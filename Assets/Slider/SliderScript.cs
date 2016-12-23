using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

	public bool isFloatSlider;

	// Use this for initialization
	void Start () {
		if(isFloatSlider){
			transform.Find("Variable Value Text").GetComponent<Text>().text = GetComponent<Slider>().value.ToString(".##");
		}
		else{
			transform.Find("Variable Value Text").GetComponent<Text>().text = GetComponent<Slider>().value.ToString();
		}
		GetComponent<Slider>().onValueChanged.AddListener (delegate {ValueChanged();});
	}
	
	private void ValueChanged() {
		if(isFloatSlider){
			transform.Find("Variable Value Text").GetComponent<Text>().text = GetComponent<Slider>().value.ToString(".##");
		}
		else{
			transform.Find("Variable Value Text").GetComponent<Text>().text = GetComponent<Slider>().value.ToString();
		}
	}
}
