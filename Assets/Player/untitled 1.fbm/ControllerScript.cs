using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour {

	private static ColoringScript currentSelected;
	public static GameObject drawingElements;
	public static GameObject colorElements;
	private static bool played = false;

	public void setCurrentSelected(ColoringScript cs){
		if(currentSelected){
			currentSelected.setDeselected();
		}
		cs.setSelected();
		currentSelected = cs;
	}

	public void colorIn(){
		if(currentSelected){
			currentSelected.color(colorElements.GetComponent<ColorSelectorScript>().getCurrentColor());
		}

	}

	public void startAnimation(){
		if(currentSelected){
			currentSelected.setDeselected();
			currentSelected = null;
		}

		if(drawingElements && !played){
			played = true;
			drawingElements.GetComponent<Animation>().Play("2D-3DAnimation");
		}
	}

	public void switchColor(){
		colorElements.GetComponent<ColorSelectorScript>().changeColors();
	}

}
