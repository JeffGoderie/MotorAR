using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System.IO;

public class DrawGraph : MonoBehaviour {


	[SerializeField]
	private GameObject lineRender; 
	[SerializeField]
	private Camera m_Camera;

	private Vector3[] m_Positions;

	private GameObject[] m_LineRenderers;

	private float m_PointSpacingX = 10;

	// Use this for initialization
	void Start () {
		string fileName = "D:\\\\workspace\\MotorAR\\points.txt";
		List<float> angles = new List<float>(); // gets all lines into separate strings
		//Debug.Log(angles);
		//Debug.Log ("blahblahblah " + angles.Length);
		float currentPosX = 0;

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
		
	}
		

}
