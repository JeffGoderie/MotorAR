using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour {

	public GameObject drawingElement;
	
	private GameObject selectedArea;
	private Toggle pressedToggle;
	private Color selectedColor;

	public GameObject handModel;

	// Use this for initialization
	void Start () {
		selectedColor = Color.HSVToRGB(0.0f, 1.0f, 1.0f);
		pressedToggle = null;
		selectedArea = null;
	}
	
	public void setSelectedColor(float hue){
		selectedColor = Color.HSVToRGB(hue, 1.0f, 1.0f);
	}

	public void setPressedToggle(Toggle toggle){

		if(toggle.Equals(pressedToggle)){
			pressedToggle.isOn = false;
			pressedToggle = null;
		}
		else{
			if(pressedToggle != null){
				pressedToggle.isOn = false;
			}
			pressedToggle = toggle;
		}
	}

	public void setSelectedArea(GameObject area){
		if(area.Equals(selectedArea)){
			selectedArea.GetComponent<ColoringScript>().setDeselected();
			selectedArea = null;
		}
		else{
			if(selectedArea != null){
				selectedArea.GetComponent<ColoringScript>().setDeselected();
			}
			selectedArea = area;
			selectedArea.GetComponent<ColoringScript>().setSelected();
		}
	}

	public void colorSelectedArea(){
		Debug.Log("Pinch detected");
		if(selectedArea != null){
			selectedArea.GetComponent<ColoringScript>().colorIn(selectedColor);
		}
	}
}
