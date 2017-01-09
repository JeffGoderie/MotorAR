using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour {

	public Toggle[] toggles;

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
		foreach (Toggle toggle in toggles){
			toggle.interactable = isInteractable;
		}
	}
}
