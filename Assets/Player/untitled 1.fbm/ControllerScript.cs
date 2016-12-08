using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour {

	private static ColoringScript currentSelected;

	public void setCurrentSelected(ColoringScript cs){
		if(currentSelected){
			currentSelected.setDeselected();
		}
		cs.setSelected();
		currentSelected = cs;
	}

	public void ColorIn(){
		if(currentSelected){
			currentSelected.color ();
		}

	}
}
