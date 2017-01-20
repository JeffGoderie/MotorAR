using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChange : MonoBehaviour {

    [SerializeField]
	private GameObject m_Ball;
    [SerializeField]
    private GameObject m_Indicator;
	public float m_BallDisp = 1.0f;
	public float m_TargetDisp = 3.0f;

	public GameObject m_Target;

    [SerializeField]
    private int m_InitNumOfAttempts = 3;
    private int m_NumAttempts;
    private int m_UserScore;

    private bool m_GameStarted = false;

    private BallThrow m_BallThrowScript;

    private GameObject artoolkit;
    private GameObject markerScene;

    private GameObject cameraObject;
    private Vector3 cameraPosition;
    private Quaternion cameraRotation;

    private ControllerScript controller;
    private TextMesh scoreText;

    private List<GameObject> m_Indicators;
	// Use this for initialization
	void Start () {
        markerScene = GameObject.FindGameObjectWithTag("MarkerScene");
        m_BallThrowScript = m_Ball.GetComponent<BallThrow>();
        artoolkit = GameObject.FindGameObjectWithTag("ARToolKit");
        controller = gameObject.GetComponent<ControllerScript>();
        cameraObject = GameObject.FindGameObjectWithTag("Finish");
        cameraPosition = cameraObject.transform.position;
        cameraRotation = cameraObject.transform.rotation;
        m_UserScore = 0;
        m_NumAttempts = m_InitNumOfAttempts;
        scoreText = m_Target.transform.Find("Score").gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_GameStarted)
        {
           
        }
		if (Input.GetKeyDown ("space")) {   //press space key to start the game

            ResetMiniGame();
            //Get colors here...
		}
        if (m_NumAttempts <= 0)
        {
            GameOver();
        }
	}
    public void ResetMiniGame()
    {
        m_UserScore = 0;
        scoreText.text = "" + m_UserScore;
        m_NumAttempts = m_InitNumOfAttempts;
        m_BallThrowScript.setAvailableColors(controller.getColors());
        m_GameStarted = true;
        
        artoolkit.GetComponent<ARController>().enabled = false;       //setting the mini game object
      
        cameraObject.transform.position = cameraPosition;
        cameraObject.transform.rotation = cameraRotation;
        
        Physics.gravity = cameraObject.transform.up * -2.0f;
        m_Ball.SetActive(true);
        m_Ball.GetComponent<BallThrow>().Reset(cameraPosition + cameraObject.transform.forward.normalized * m_BallDisp);

        m_Target.SetActive(true);
        foreach (Transform area in m_Target.transform)
        {
            if (!area.gameObject.activeSelf) area.gameObject.SetActive(true);
        }
        m_Target.GetComponent<Target>().Reset(cameraPosition + cameraObject.transform.forward.normalized * m_TargetDisp);
        
        GameObject markerScene = GameObject.FindGameObjectWithTag("MarkerScene");     //find the marker
        if (markerScene)
            markerScene.SetActive(false);
        Pause pauseScript = GetComponent<Pause>();
        pauseScript.Reset();
        ResetAttemptsIndicator();
       
    }
    public void CloseMiniGame()
    {
        markerScene.SetActive(true);
        artoolkit.GetComponent<ARController>().enabled = true;
        m_Ball.SetActive(false);
        m_Target.SetActive(false);

    }
    public void ResetBall()      //reset ball to the position
    {
        if (m_NumAttempts > 0) {
            m_Ball.GetComponent<BallThrow>().Reset(cameraPosition + cameraObject.transform.forward.normalized * m_BallDisp);
        }
    }
    public void AddToScore(int score)    // display the score of the ballthrow game
    {
        m_UserScore += score;
        
        scoreText.text = "" + m_UserScore;
        --m_NumAttempts; 
        Destroy(m_Indicators[m_NumAttempts]);
        m_Indicators.RemoveAt(m_NumAttempts);
      

    }

    public int GetNumOfAttemts()
    {
        return m_NumAttempts;
    }
    private void GameOver()
    {
        //TODO:
    }
    private void ResetAttemptsIndicator()   //reset the indicator position
    {
        float zDisp = -0.7f, yDisp = 0, xDisp = -0.87f, xRelDisp = -0.25f;
        m_Indicators = new List<GameObject>();
        for (int i = 0; i < m_NumAttempts; i++)
        {
            m_Indicators.Add(Instantiate(m_Indicator, m_Target.transform.position, Quaternion.identity));
            m_Indicators[i].transform.parent = m_Target.transform;
            m_Indicators[i].transform.localPosition = new Vector3(xDisp + xRelDisp * i, yDisp, zDisp);
                

        }
            
    }
}   
