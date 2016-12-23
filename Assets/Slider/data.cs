using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


[ExecuteInEditMode]
public class data : MonoBehaviour {

	//public static data control;

	public Slider slider;
	public Text text;
	public string unit;
	public byte decimals = 2;

	/*void Awake(){
		if(control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control != this){
			Destroy(gameObject);
		}
	}*/

	//Sliders
	void OnEnable () {
		slider.onValueChanged.AddListener (ChangeValue);
		ChangeValue (slider.value);

	}


	void OnDisable () {
		slider.onValueChanged.RemoveAllListeners ();

	}

	void ChangeValue(float value){
		text.text = value.ToString ("n" + decimals) + " " + unit;
	}
}



