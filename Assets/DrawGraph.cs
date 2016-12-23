using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System.IO;
using System.Linq;

using UnityEngine.UI.Extensions;

public class DrawGraph : MonoBehaviour {

/**
	[SerializeField]
	private GameObject lineRender; 
	[SerializeField]
	private Camera m_Camera;
**/
	private Vector3[] m_Positions;

	private GameObject[] m_LineRenderers;

	private float m_PointSpacingX = 10;

	private Vector2[] points;

	private int currentIndex;

	public UILineRenderer indicator;

	List<float> angles;

	float height;
	float width;
	float hScale;
	float wScale;

	// Use this for initialization
	void Start () {
		string fileName = ".\\points.txt";
		angles = new List<float>(); // gets all lines into separate strings
		//Debug.Log(angles);
		//Debug.Log ("blahblahblah " + angles.Length);
		float currentPosX = 0;

		Debug.Log(transform.parent.GetComponent<RectTransform>().rect.width);
		Debug.Log(transform.parent.GetComponent<RectTransform>().rect.height);

		string line; 
		StreamReader theReader = new StreamReader(fileName, Encoding.Default);
		using(theReader)
		{
			do {
				line = theReader.ReadLine();
				if(line != null)
				angles.Add(float.Parse(line));

			} while(line != null);
		}

		points = new Vector2[angles.Count];

		float offset = 25.0f;

		height = transform.parent.GetComponent<RectTransform>().rect.height - 2*offset;
		width = transform.parent.GetComponent<RectTransform>().rect.width - 2*offset;
		hScale = height/angles.Max();
		wScale = width/((angles.Count-1) * m_PointSpacingX);

		Debug.Log(hScale);

		for (int i = 0; i < angles.Count; i++) {
			Debug.Log(i*m_PointSpacingX*wScale - width/2);
			points[i] = new Vector2(i*m_PointSpacingX*wScale - width/2, angles[i]*hScale - height/2);
		}

		GetComponent<UILineRenderer>().Points = points;

		currentIndex = 0;

		setIndicatorPosition(currentIndex*m_PointSpacingX*wScale - width/2);


/**
		for (int i = 0; i < angles.Count - 1; i++) {
			m_LineRenderers = new GameObject[angles.Count];
			m_LineRenderers[i] = (GameObject) Instantiate (lineRender, Vector3.zero, Quaternion.identity);
			float angle = angles[i];
			//Debug.Log("Angle: " + angle);
			m_LineRenderers[i].GetComponent<LineRenderer>().SetPosition (0, new Vector3(currentPosX, angle, 0));
			currentPosX += m_PointSpacingX;
			angle = angles [i+1];
			m_LineRenderers[i].GetComponent<LineRenderer>().SetPosition (1, new Vector3(currentPosX, angle, 0));
		}

		// Not sure how to readjust camera so that it fits all the graph inside. other solution would be to resize the
		// the graph instead.... :)
		var desiredWidth = currentPosX;
		float distance = 50.0f;
		var height = desiredWidth * m_Camera.aspect;
		m_Camera.fieldOfView = 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
		//m_Camera.pixelWidth = desiredWidth;
	}
		
	// Update is called once per frame
	void Update () {
		**/
	}

	private void setIndicatorPosition(float position){
		Vector2[] points = new Vector2[2];

		points[0] = new Vector2(position,-1000f);
		points[1] = new Vector2(position,1000f);

		indicator.Points = points;
	}

	void Update (){

        if (Input.GetKeyDown("left")){
        	if(currentIndex > 0){
        		currentIndex -= 1;
				setIndicatorPosition(currentIndex*m_PointSpacingX*wScale - width/2);
        	}
        }
        
        if (Input.GetKeyDown("right")){
            if(currentIndex < angles.Count()-1){
        		currentIndex += 1;
				setIndicatorPosition(currentIndex*m_PointSpacingX*wScale - width/2);
        	}
    	}

	}

}
