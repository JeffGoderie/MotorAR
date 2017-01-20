using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

	private bool trackPalm;

	private GameObject palm = null;

	private float oldPosition = float.MinValue;

	public SliderController sc;

	public float scaling = 4.0f;

	public bool triggered = false;

	private float width = 0.0f;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		if(width == 0.0f){
			width = GetComponent<RectTransform>().rect.width;
		}
		Slider sl = GetComponent<Slider>();
		
		//if no new collision has been discovered for over 0.5f, drop known information
		if(palm && trackPalm && sl.interactable){
			
			float newPosition = palm.transform.position.x;
			if(oldPosition == float.MinValue){
				oldPosition = newPosition;
			}
			else{
				float difference = (newPosition - oldPosition) / (width*0.0005f*scaling);
				
				sl.value = sl.value + difference;
				oldPosition = newPosition;
			}
		}
	}

	void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "Palm")
        {
        	triggered = true;
        	trackPalm = true;
        	sc.colliderState = true;
        	palm = col.gameObject;
        	oldPosition = float.MinValue;
        }
    }

    void OnTriggerExit(Collider col){
    	if(col.gameObject.tag == "Palm")
        {
        	sc.colliderState = false;
        	triggered = false;
        	trackPalm = false;
        }
    }
}
