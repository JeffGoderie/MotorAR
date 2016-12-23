using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {
	private LineRenderer m_LineRenderer;
	private float dist;
	private float counter;

	[SerializeField]
	private Transform m_Origin;
	[SerializeField]
	private Transform m_Destination;
	// Use this for initialization
	[SerializeField]
	private float lineDrawSpeed = 5.0f;
	void Start () {
		m_LineRenderer = GetComponent<LineRenderer> ();
		m_LineRenderer.SetPosition (0, m_Origin.position);

		dist = Vector3.Distance (m_Origin.position, m_Destination.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (counter < dist) {
			counter += 1.0f / lineDrawSpeed;
			float x = Mathf.Lerp (0, dist, counter);

			Vector3 pointA = m_Origin.position;
			Vector3 pointB = m_Destination.position;

			Vector3 pointAlongLine = x * Vector3.Normalize (pointB - pointA) + pointA;

			m_LineRenderer.SetPosition (1, pointAlongLine);
		}
	}
}
