using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;
using Leap;

public class ControllerScript : MonoBehaviour {

	public GameObject drawingElement;

	public GameObject leftHand;
	public GameObject rightHand;
	
	public SettingsManager manager;

	private GameObject selectedArea;
	private Toggle pressedToggle;
	private Color selectedColor = Color.red;
	private StreamWriter sw;

	public GameObject[] areas;
	private int index;
	private float delta_time;
	private float total_time;
	private bool animation_flag;
	private float animation_countdown;

	//Temporary
    private GameStateChange miniGame;

	// Use this for initialization
    private int m_GestureMode = 0; //0 - not known 1 - sector select 2 - color select
    private bool m_WritePinch = false;
	void Start () {                       //initialize the settings
		animation_flag = false;
		animation_countdown = 3.0f;
        miniGame = GetComponent<GameStateChange>();
		Directory.CreateDirectory("./Data");
		string filename = "Data" + String.Format("{0:s}", DateTime.Now);
		filename = filename.Replace(":",string.Empty);
		sw = File.AppendText("./Data/" + filename + ".txt");
		selectedColor = Color.HSVToRGB(0.0f, 1.0f, 1.0f);
		pressedToggle = null;
		selectedArea = null;
		delta_time = 0.0f;
		total_time = 0.0f;
	}

	public void triggerAnimationFlag(){
		animation_flag = true;
		
	}

	public void resetAnimationFlag(){
		animation_flag = false;
		animation_countdown = 3.0f;
	}

	void Update(){

		if(animation_flag && animation_countdown >= 0.0f){
			animation_countdown -= Time.deltaTime;
			if(animation_countdown < 0.0f){      //count the time to reset the MiniGame
				miniGame.ResetMiniGame();
			}
		}


		float dt = Time.deltaTime;
		delta_time += dt;
		total_time += dt;
		if(delta_time>=0.1f){      
			delta_time -= 0.1f;    //count down the time every 0.1s

			if(leftHand.active){        //if the leftHand is active, record the lefthand data
				writeHandData(leftHand);
			}
			if(rightHand.active){       //if the rightHand is active, record the righthand data
				writeHandData(rightHand);
			}

		}

		if (Input.GetKeyDown("l")){       //user input to control the game settings
            manager.applySettings();
        }
        if (Input.GetKeyDown("n")){
            restartGame();
            miniGame.CloseMiniGame();
        }
        if (Input.GetKeyDown("b")){
            miniGame.CloseMiniGame();
        }
	}

	private void writeHandData(GameObject handModel){
		string output = total_time +";";         //record the time

		Hand hand = handModel.GetComponent<HandModel>().GetLeapHand();
		if(hand.IsLeft){
			output += "left;";                //record the hand as left or right
		}
		else if(hand.IsRight){
			output += "right;";
		}
		
		output += hand.PinchDistance + ";"; //Plotting  pinch distance Value
        //TODO: add gesture set later
        /*
        output += m_GestureMode + ";";


        output += m_WritePinch + ";";
        if(m_WritePinch) m_WritePinch = false;
         * */

		Transform container = handModel.transform.Find("HandContainer");

		Vector position = hand.PalmPosition;         //get the palm world position
		output += position.x + ";";
		output += position.y + ";";
		output += position.z + ";";
		Vector normal = hand.PalmNormal;       //get leap motion palm normal position
		output += normal.x + ";";
		output += normal.y + ";";
		output += normal.z + ";";

		Vector3 containerWorldPosition = container.position;	//Plotting Value
		output += containerWorldPosition.x + ";";
		output += containerWorldPosition.y + ";";
		output += containerWorldPosition.z + ";";
		Quaternion containerWorldRotation = container.rotation;	//Plotting Value
		output += containerWorldRotation.w + ";";
		output += containerWorldRotation.x + ";";
		output += containerWorldRotation.y + ";";
		output += containerWorldRotation.z + ";";

		Transform _hand = container.GetChild(0);

		foreach(Transform bone0 in _hand){                //for each finger of the hand, record the bones rotation. Loop from thumb to pinky finger.
			Quaternion bone0LocalRotation = bone0.localRotation;	//the first bone of a finger, close to the palm
			output += bone0LocalRotation.w + ";";
			output += bone0LocalRotation.x + ";";
			output += bone0LocalRotation.y + ";";
			output += bone0LocalRotation.z + ";";
			Transform bone1 = bone0.GetChild(0);
			Quaternion bone1LocalRotation = bone1.localRotation;	//the second bone of a finger, connected directly to the first bone
			output += bone1LocalRotation.w + ";";
			output += bone1LocalRotation.x + ";";
			output += bone1LocalRotation.y + ";";
			output += bone1LocalRotation.z + ";";
			Transform bone2 = bone1.GetChild(0);
			Quaternion bone2LocalRotation = bone2.localRotation;	//the third bone 
			output += bone2LocalRotation.w + ";";
			output += bone2LocalRotation.x + ";";
			output += bone2LocalRotation.y + ";";
			output += bone2LocalRotation.z + ";";
		}
		sw.WriteLine(output);
	}

	void OnApplicationQuit(){
		sw.Close();
	}
	
	private void restartGame(){
		resetAnimationFlag();
		setSelectedArea(null);
		foreach(GameObject area in areas){
			area.GetComponent<ColoringScript>().resetColor();
		}
	}

	public void setSelectedColor(float hue){
		selectedColor = Color.HSVToRGB(hue, 1.0f, 1.0f);
	}
    /*
	public void setPressedToggle(Toggle toggle){         

		if(toggle.Equals(pressedToggle)){
			pressedToggle.isOn = false;
			pressedToggle = null;
		}
		else{
			if(pressedToggle != null){
				pressedToggle.isOn = false;
			}
			pressedToggle = toggle;
		}
	}*/

	public void selectNextArea(){     //tap the box to select the area on the ball
		if(selectedArea == null){
			index = 0;
		}
		else if(index == areas.Length - 1){
			index = 0;
		}
		else{
			index += 1;
		}
		setSelectedArea(areas[index]);
	}

	public void selectPreviousArea(){
		if(selectedArea == null){
			index = areas.Length - 1;
		}
		else if(index == 0){
			index = areas.Length - 1;
		}
		else{
			index -= 1;
		}
		setSelectedArea(areas[index]);
	}

	private void setSelectedArea(GameObject area){      //Active the selected area of the ball 
		if(area != null && area.Equals(selectedArea)){
			selectedArea.GetComponent<ColoringScript>().setDeselected();
			selectedArea.transform.GetChild(0).gameObject.SetActive(false);
			selectedArea = null;
		}
		else{
			if(selectedArea != null){
				selectedArea.GetComponent<ColoringScript>().setDeselected();
				selectedArea.transform.GetChild(0).gameObject.SetActive(false);
			}
			selectedArea = area;
			if(area != null){
				selectedArea.GetComponent<ColoringScript>().setSelected();
				selectedArea.transform.GetChild(0).gameObject.SetActive(true);
			}
		}
	}

	public void colorSelectedArea(){                 //color the selected area 
		GameObject[] colliders = GameObject.FindGameObjectsWithTag("HandCollider");
		GameObject[] colliders2 = GameObject.FindGameObjectsWithTag("HandCollider2");

		

		bool flag = true;

		foreach(GameObject collider in colliders){
			if(collider.GetComponent<ButtonColliderScript>().triggered){
				
				flag = false;
				break;
			}
		}
		if(flag){
			foreach(GameObject collider in colliders2){
				if(collider.GetComponent<SliderScript>().triggered){
					
					flag = false;
					break;
				}
			}
		}
		
		
		if(selectedArea != null){
			selectedArea.GetComponent<ColoringScript>().colorIn(selectedColor);
		}
	}
	public Color[] getColors(){
		Color[] colors = new Color[areas.Length];
		for(int i = 0; i<areas.Length; i++){
			colors[i] = areas[i].GetComponent<ColoringScript>().getColor();
		}
        Debug.Log(colors[1]);
		return colors;
	}
    public void setModeSector()
    {
        m_GestureMode = 1;
    }
    public void setModeColor()
    {
        m_GestureMode = 2;
    }
    public void setModeUnknown()
    {
        m_GestureMode = 0;
    }
    public void setPinchWrite()
    {
        m_WritePinch = true;
    }
    public void SetModePinch()
    {
        m_GestureMode = 3;
    }
}
