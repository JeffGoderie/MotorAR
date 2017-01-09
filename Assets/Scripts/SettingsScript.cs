using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

	public Toggle SliderControls;
	public Dropdown SliderThumb;
	public Dropdown SliderIndex;
	public Dropdown SliderMiddle;
	public Dropdown SliderRing;
	public Dropdown SliderPinky;

	public Toggle ButtonControls;
	public Dropdown ButtonThumb;
	public Dropdown ButtonIndex;
	public Dropdown ButtonMiddle;
	public Dropdown ButtonRing;
	public Dropdown ButtonPinky;
	public Toggle PalmControls;
	public Slider PalmAngle;

	public Slider PinchClose;
	public Slider PinchOpen;

	private Dictionary<Selectable,string> objToStr;
	private Dictionary<string,Selectable> strToObj;

	// Use this for initialization
	void Start () {
		objToStr = new Dictionary<Selectable,string>();
		strToObj = new Dictionary<string,Selectable>();

		objToStr.Add(SliderControls, "SliderControls");
		strToObj.Add("SliderControls", SliderControls);
		objToStr.Add(SliderThumb, "SliderThumb");
		strToObj.Add("SliderThumb", SliderThumb);
		objToStr.Add(SliderIndex, "SliderIndex");
		strToObj.Add("SliderIndex", SliderIndex);
		objToStr.Add(SliderMiddle, "SliderMiddle");
		strToObj.Add("SliderMiddle", SliderMiddle);
		objToStr.Add(SliderRing, "SliderRing");
		strToObj.Add("SliderRing", SliderRing);
		objToStr.Add(SliderPinky, "SliderPinky");
		strToObj.Add("SliderPinky", SliderPinky);

		objToStr.Add(ButtonControls, "ButtonControls");
		strToObj.Add("ButtonControls", ButtonControls);
		objToStr.Add(ButtonThumb, "ButtonThumb");
		strToObj.Add("ButtonThumb", ButtonThumb);
		objToStr.Add(ButtonIndex, "ButtonIndex");
		strToObj.Add("ButtonIndex", ButtonIndex);
		objToStr.Add(ButtonMiddle, "ButtonMiddle");
		strToObj.Add("ButtonMiddle", ButtonMiddle);
		objToStr.Add(ButtonRing, "ButtonRing");
		strToObj.Add("ButtonRing", ButtonRing);
		objToStr.Add(ButtonPinky, "ButtonPinky");
		strToObj.Add("ButtonPinky", ButtonPinky);
		objToStr.Add(PalmControls, "PalmControls");
		strToObj.Add("PalmControls", PalmControls);
		objToStr.Add(PalmAngle, "PalmAngle");
		strToObj.Add("PalmAngle", PalmAngle);

		objToStr.Add(PinchClose, "PinchClose");
		strToObj.Add("PinchClose", PinchClose);
		objToStr.Add(PinchOpen, "PinchOpen");
		strToObj.Add("PinchOpen", PinchOpen);
	}

	public void setValues(List<string> values){
		foreach(string value in values){
			string[] separators = {": "};
			string[] split = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			Selectable obj = null;
			if(split.Length == 0){
				continue;
			}
			if (strToObj.TryGetValue(split[0], out obj))
			{
				if(obj is Toggle){
					if(split[1] == "1"){
						((Toggle)obj).isOn = true;
					}
					else{
						((Toggle)obj).isOn = false;
					}
				}
				else if(obj is Slider){
					((Slider)obj).value = float.Parse(split[1]);
				}
			    else if(obj is Dropdown){
			    	((Dropdown)obj).value = int.Parse(split[1]);;
			    }
			    else{
			    	Debug.Log("Casting has failed" + split[0]);
			    }
			}
			else
			{
			    Debug.Log("Settings are corrupted");
			}
		}
		
	}

	public List<string> getValues(){
		List<string> values = new List<string>();
		foreach(Selectable obj in objToStr.Keys){
			string value = "";
			if (objToStr.TryGetValue(obj, out value))
			{
				if(obj is Toggle){
					if(((Toggle)obj).isOn){
						values.Add(value + ": 1");
					}
					else{
						values.Add(value + ": 0");
					}
				}
				else if(obj is Slider){
					values.Add(value + ": " + ((Slider)obj).value);
				}
			    else if(obj is Dropdown){
			    	values.Add(value + ": " + ((Dropdown)obj).value);
			    }
			    else{
			    	Debug.Log("Casting has failed");
			    }
			}
			else
			{
			    Debug.Log("Settings are corrupted");
			}
		}

		return values;
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
