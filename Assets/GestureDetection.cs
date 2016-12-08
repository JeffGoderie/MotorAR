﻿using UnityEngine;
using System.Collections;
using Leap.Unity;


public class GestureDetection : MonoBehaviour {

	[SerializeField]
	private float m_pinchMinDistance;
	[SerializeField]
	private float m_pinchMaxDistance;
	[SerializeField]
	private float m_pinchOpenStrength;
	[SerializeField]
	private float m_pinchCloseStrength;
    [SerializeField]
    private float m_PinchTime;

    private bool m_PinchStarted = false;
    private float m_PinchTimer;

    [SerializeField]
    private float m_PalmRotationStartAngle;
    [SerializeField]
    private float m_PalmRotationEndAngle;
    [SerializeField]
    private float m_PalmRotationTime;

    private float m_PalmRotationTimer;
    private bool m_PalmRotationStarted = false;



    [SerializeField]
	private Transform referenceObject;


	[SerializeField]
	private bool DebugInfo = true;

   

    [SerializeField]
    private Color m_ColorIndex = new Color(1.0f, 0.0f, 0.0f);
    [SerializeField]
    private Color m_ColorThumb = new Color(0.0f, 0.0f, 1.0f);

    private Light m_PalmLight;
    private Light m_IndexLight; 
	private HandModel m_HandModel;


	public ColoringScript currentSelected;


	private void LeftMouseClick(){
		//if(player.hud.MouseInBounds()) {
		Debug.Log("1");
		GameObject hitObject = FindHitObject();
		if(hitObject){
			ColoringScript script = hitObject.GetComponent<ColoringScript>();
			if(script){
				if(currentSelected){
					currentSelected.setDeselected();
				}
				script.setSelected();
				currentSelected = script;
			}
		}
		//}
	}

	private void RightMouseClick(){
		Debug.Log("2");
		if(currentSelected){
			Debug.Log("3");
			currentSelected.color();
		}
	}

	private GameObject FindHitObject() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			return hit.collider.gameObject;
		}
		return null;
	}
    

	// Use this for initialization
	void Start () {
		m_HandModel = transform.GetComponent<HandModel> ();
        m_IndexLight = m_HandModel.GetComponentInChildren<Light>();
    
        m_PalmLight = m_HandModel.palm.GetComponentInChildren<Light>();
       

	}
	
	// Update is called once per frame
	void Update () {
        m_IndexLight.color = m_ColorIndex;
        m_PalmLight.color = m_ColorThumb;
        if (m_HandModel) {
				Vector3 indexPosition = m_HandModel.fingers [1].GetBoneCenter (3);
				Vector3 thumbPosition = m_HandModel.fingers [0].GetBoneCenter (3);
				float distance = (indexPosition - thumbPosition).magnitude;
				float normalizedPinchDistance = (distance - m_pinchMinDistance) / (m_pinchMaxDistance - m_pinchMinDistance);
				float pinchStrength = 1.0f - Mathf.Clamp01 (normalizedPinchDistance);
          if(!m_PinchStarted && pinchStrength <= m_pinchOpenStrength)
            {
                m_PinchStarted = true;
                m_PinchTimer = m_PinchTime;
                Debug.Log("Pinch opened");
            }
            m_PinchTimer -= Time.deltaTime;
            if(m_PinchTimer < 0)
            {
                m_PinchStarted = false;
            }
           else if(m_PinchStarted && pinchStrength >= m_pinchCloseStrength)
            {
                m_PinchStarted = false;
                Debug.Log("Color Switched!");
            }

                Vector3 palmNormal = m_HandModel.GetPalmNormal ();
				Vector3 elbowDirection = m_HandModel.GetArmDirection ();
				float palmAngle = Vector3.Angle (elbowDirection, palmNormal);

            float palmRotationAngle = m_HandModel.palm.transform.eulerAngles.z;
            if (!m_PalmRotationStarted && palmRotationAngle <= m_PalmRotationStartAngle)
            {
                m_PalmRotationStarted = true;
                m_PalmRotationTimer = m_PalmRotationTime;

            }
            m_PalmRotationTimer -= Time.deltaTime;
            if(m_PalmRotationTimer < 0)
            {
                m_PalmRotationStarted = false;
            }
            if ( m_PalmRotationStarted && palmRotationAngle >= m_PalmRotationEndAngle)
            {
                m_PalmRotationStarted = false;
                Debug.Log("Animation Activate!");
            }

                m_PalmLight.transform.forward = palmNormal;
               
				float distanceToReference = (referenceObject.position - m_HandModel.GetPalmPosition ()).magnitude;

            if (DebugInfo)
            { 
                Debug.Log("Pinch strength " + pinchStrength);
                Debug.Log("ANGLE: " + palmAngle);
            Debug.Log("ROtation Angle" + m_HandModel.palm.transform.eulerAngles.z);
                Debug.Log("Distance to Reference: " + distanceToReference);
            }

        } else {
				Debug.Log ("No hand model loaded");
		}
	}
}
