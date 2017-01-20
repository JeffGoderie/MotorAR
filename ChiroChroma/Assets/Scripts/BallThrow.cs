using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Leap.Unity;
using Leap;

enum FieldValues{
	Ring1 = 10,
    Ring2 = 5,
    Ring3 = 4,
    Ring4 = 2,
    Ring5 = 1,
    Plane = 0
}

public class BallThrow : MonoBehaviour {
    [SerializeField]
    private GameObject m_Controller;
    [SerializeField]
    private GameObject m_HitPoint;
	private GameObject m_Hand = null;
	[SerializeField]
	private float m_SpeedScale = 1.0f;
    [SerializeField]
	private float minDispThrow = 0.03f;

    private Color[] availableColors = new Color[] { Color.green };

	// ======================= 
	private Hand m_HandModel;
	private Vector3 ballInitPos;
	private Vector3 initPos;
	private Vector3 endPos;
	private Vector3 initPalmPos;
	private bool isDragged = false;
	private bool isThrown = false;
	private bool collided = false;
	// Use this for initialization
    private float m_TimeToWait = 2.0f;
    private float m_TimeAfterCollision;
    private GameStateChange m_MiniGameController; 
	void Start () {
		//Fix
		
        m_MiniGameController = m_Controller.GetComponent<GameStateChange>();

	}
	
	// Update is called once per frame
	void Update () {
		if (!isThrown) {             //check whether it is the throw action or drag action
			if (isDragged) {
				CapsuleHand capsuleHand = m_Hand.GetComponentInChildren<CapsuleHand> ();
                if (capsuleHand)
                {
                    m_HandModel = capsuleHand.GetLeapHand();
                    transform.position += (m_HandModel.PalmPosition.ToVector3() - initPalmPos);
                    initPalmPos = m_HandModel.PalmPosition.ToVector3();
                }
			}
		}
        else
        {
            if (collided) {         //if the time to wait to reset the ball
                m_TimeAfterCollision += Time.deltaTime;
                if (m_TimeAfterCollision >= m_TimeToWait)
                {
                    m_MiniGameController.ResetBall();
                }
            }
            
        }
	}
    
	public void setAvailableColors(Color[] colors){
		availableColors = colors;
	}

    void OnCollisionEnter(Collision collision)
    {
    	if(!collided){
	        
	        Quaternion rot = Quaternion.FromToRotation(Vector3.up, Vector3.forward);

	        if(collision.gameObject.tag == "Target"){

	        	Vector3 hitLocation = new Vector3(collision.contacts[0].point.x,    //record where the ball hit after it has been thrown
	        									  collision.contacts[0].point.y,
	        									  2.0f);


				float randomNumber = UnityEngine.Random.Range(0.0f,availableColors.Length);
                Debug.Log(Mathf.FloorToInt(randomNumber));
                Debug.Log(availableColors.Length);
				m_HitPoint.GetComponent<ParticleSystem>().startColor = availableColors[Mathf.FloorToInt(randomNumber)];      //if the ball hit the target, start coloring the target
				m_HitPoint.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startColor = availableColors[Mathf.FloorToInt(randomNumber)];
				
				
                if (collision.gameObject.name != "Plane")   //hit the target, add score
                {
                    Instantiate(m_HitPoint, hitLocation, m_HitPoint.transform.localRotation);
                }
	        	collided = true;
	        	GetComponent<Collider>().enabled = false;
	        	
                int score = (int)Enum.Parse(typeof(FieldValues), collision.gameObject.name);
               
                m_MiniGameController.AddToScore(score);
               
                m_TimeAfterCollision = 0;

	        	
	        }
	    }
    }
	public void GrabBall(GameObject hand) {
		
		m_Hand = hand;
		if (!isThrown) {        //get the hand initial position if it is not the throw action
			CapsuleHand capsuleHand = m_Hand.GetComponentInChildren<CapsuleHand> ();
			m_HandModel = capsuleHand.GetLeapHand ();
			initPos = m_HandModel.PalmPosition.ToVector3 ();
			initPalmPos = m_HandModel.PalmPosition.ToVector3 ();
			isDragged = true;
		}
	}
	public void ReleaseBall(){
		
		if (!isThrown) {     //get the throw distance from the initial hand position and the position after grabbing
			CapsuleHand capsuleHand = m_Hand.GetComponentInChildren<CapsuleHand> ();
            if (capsuleHand) { 
			m_HandModel = capsuleHand.GetLeapHand ();
			endPos = m_HandModel.PalmPosition.ToVector3 ();
			Rigidbody rb = GetComponent<Rigidbody> ();
			Vector3 displacement = initPos - endPos;
			Vector3 throwDirection = displacement.normalized;
			
            //This is very finally the throw happenes
			if (displacement.sqrMagnitude > minDispThrow) {     // give the thrown ball falling speed with gravity
				isThrown = true;
				rb.velocity = throwDirection * displacement.magnitude * m_SpeedScale;
				rb.useGravity = true;
			} else {
				transform.position = ballInitPos; 
			}
            }
            isDragged = false;
		}
	}
	public void Reset (Vector3 pos) {         //reset everything
		GetComponent<Collider>().enabled = true;
		ballInitPos = pos;
		transform.position = ballInitPos;
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.useGravity = false;
		isThrown = false;
		collided = false;
        m_TimeAfterCollision = 0.0f;
	}
}
