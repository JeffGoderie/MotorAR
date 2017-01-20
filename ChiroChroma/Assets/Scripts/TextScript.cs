using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

	public void setText(float num){
		GetComponent<Text>().text = num.ToString(".####");
	}

	public void setText(int num){
		GetComponent<Text>().text = "" + num;
	}

	public void setText(string text){
		GetComponent<Text>().text = text;
	}
}
