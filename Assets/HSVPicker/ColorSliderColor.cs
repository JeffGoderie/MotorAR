using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorSliderColor : MonoBehaviour {

	ColorBlock colorBlock;

	// Use this for initialization
	void Start () {
		colorBlock = GetComponent<Slider>().colors;
		setSliderColor(0.0f);
	}
	
	public void setSliderColor(float hue){
		Color normal = Color.HSVToRGB(hue, 1.0f, 1.0f);
		Color pressed = Color.HSVToRGB(hue, 1.0f, 0.8f);
		colorBlock.highlightedColor = normal;
		colorBlock.normalColor = normal;
		colorBlock.pressedColor = pressed;
		GetComponent<Slider>().colors = colorBlock;
	}
}
