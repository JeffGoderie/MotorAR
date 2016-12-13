using UnityEngine;
using System.Collections;

public class AllignToARMarkers : MonoBehaviour {
	AROrigin arOrigin;
	ARMarker baseMarker;
	GameObject sceneRoot;

	// Use this for initialization
	void Start () {
		//sceneRoot = GameObject.FindGameObjectWithTag ("ScenePos");
		arOrigin = this.gameObject.GetComponentInParent<AROrigin>(); // Unity v4.5 and later.
	}

	void OnMarkerFound(ARMarker marker)
	{
		if (baseMarker == null) {
			baseMarker = arOrigin.GetBaseMarker();
		}
	}

	void OnMarkerLost(ARMarker marker){
		if(baseMarker.Equals(marker)){
			baseMarker = arOrigin.GetBaseMarker();
		}
	}

	void OnMarkerTracked(ARMarker marker){
		//Make sure that we have a baseMarker and another marker which is not the baseMarker
		if (baseMarker != null && !(baseMarker.Equals (marker))) {
			Vector3 positionStart = ARUtilityFunctions.PositionFromMatrix (arOrigin.transform.localToWorldMatrix);
			Vector3 positionTarget = ARUtilityFunctions.PositionFromMatrix (arOrigin.transform.localToWorldMatrix * baseMarker.TransformationMatrix.inverse * marker.TransformationMatrix);
			//sceneRoot.transform.position = positionStart; 
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
