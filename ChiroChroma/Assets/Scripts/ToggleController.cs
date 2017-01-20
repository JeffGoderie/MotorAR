using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour {

	public GameObject[] toggles;

	// Use this for initialization
	void Start () {
		deactivateToggles();			
	}
	
	public void activateToggles() {
		setTogglesState(true);
	}

	public void deactivateToggles() {
		setTogglesState(false);
	}

	private void setTogglesState(bool isInteractable) {
		foreach (GameObject toggle in toggles){
			toggle.GetComponent<Collider>().enabled = isInteractable;
			if(!isInteractable){
				toggle.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;
			}
			else{
				toggle.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;
			}
		}
	}
}
