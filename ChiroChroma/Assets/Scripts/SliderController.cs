using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {

	public Slider[] sliders;

	public bool colliderState = false;
	private bool gestureState = false;

	private bool isVisible = false;

	// Use this for initialization
	void Start () {
		deactivateSliders();			
	}

	void Update(){
		bool newState = colliderState && gestureState;
		if(newState != isVisible){
			setSliderVisibility(newState);
			isVisible = newState;
		}
	}
	
	public void activateSliders() {
		setSlidersState(true);
	}

	public void deactivateSliders() {
		setSlidersState(false);
	}

	private void setSlidersState(bool isInteractable) {
		gestureState = isInteractable;
	}

	private void setSliderVisibility(bool value){
		foreach (Slider slider in sliders){
			slider.interactable = value;
			
			slider.transform.Find("EnableBackground").gameObject.GetComponent<RawImage>().enabled = value;
			slider.transform.Find("DisableBackground").gameObject.GetComponent<RawImage>().enabled = !value;
		}
	}
}