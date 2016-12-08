using UnityEngine;
using System.Collections;
using Leap.Unity;
public class PalmInteraction : MonoBehaviour {

	[SerializeField]
	private ControllerScript colorController;

	private int numTaps;
	// Use this for initialization
	void Start () {
		numTaps = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "palm") {
			numTaps++;
			colorController.ColorIn();
			Debug.Log ("Numver of Taps: " + numTaps);
		}
	}
}
