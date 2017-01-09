using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {

	public Slider[] sliders;

	// Use this for initialization
	void Start () {
		deactivateSliders();			
	}
	
	public void activateSliders() {
		setSlidersState(true);
	}

	public void deactivateSliders() {
		setSlidersState(false);
	}

	private void setSlidersState(bool isInteractable) {
		foreach (Slider slider in sliders){
			slider.interactable = isInteractable;
		}
	}
}