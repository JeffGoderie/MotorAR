using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using Leap.Unity;

public class SettingsManager : MonoBehaviour {

	public GameObject leftHand;
	public GameObject leftPhysicsHand;
	public GameObject rightHand;
	public GameObject rightPhysicsHand;
	public GameObject handModels;

	private ExtendedFingerDetector leftSlider;
	private ExtendedFingerDetector rightSlider;
	private ExtendedFingerDetector leftButton;
	private ExtendedFingerDetector rightButton;
	private GestureDetection leftPinch;
	private GestureDetection rightPinch;

	private SliderController sliderController;
	private ToggleController toggleController;

	// Use this for initialization
	void Start () {
		sliderController = handModels.GetComponent<SliderController>();
		toggleController = handModels.GetComponent<ToggleController>();

		leftPinch = leftPhysicsHand.GetComponent<GestureDetection>();
		foreach(ExtendedFingerDetector detector in leftHand.GetComponents<ExtendedFingerDetector>()){
			if(detector.Focus == "Slider"){
				leftSlider = detector;
			}
			else if(detector.Focus == "Buttons"){
				leftButton = detector;
			}
		}

		rightPinch = rightPhysicsHand.GetComponent<GestureDetection>();
		foreach(ExtendedFingerDetector detector in rightHand.GetComponents<ExtendedFingerDetector>()){
			if(detector.Focus == "Slider"){
				rightSlider = detector;
			}
			else if(detector.Focus == "Buttons"){
				rightButton = detector;
			}
		}
	}
	
	public void applySettings(){
		string[] lines = File.ReadAllLines("./_temp_.settings");

		List<string> leftValues = new List<string>();
		List<string> rightValues = new List<string>();

		bool left = false;
		foreach(string line in lines){
			if(line == "Left:"){
				left = true;
			}
			else if(line == "Right:"){
				left = false;
			}
			else{
				if(left){
					leftValues.Add(line);
				}
				else{
					rightValues.Add(line);
				}
			}
		}

		applySettings(leftValues, "Left");
		applySettings(rightValues, "Right");
	}

	private void applySettings(List<string> settings, string side){
		string[] separators = {": "};

		foreach(string setting in settings){
			Debug.Log(setting);
			string[] split = setting.Split(separators, StringSplitOptions.RemoveEmptyEntries);
		
			if(split.Length == 0){
				continue;
			}
			else{
				if(split[0] == "SliderControls"){
					if(side == "Left"){
						if(split[1] == "0"){
							leftSlider.enabled = false;
						}
						else{
							leftSlider.enabled = true;
						}
					}
					else{
						if(split[1] == "0"){
							rightSlider.enabled = false;
						}
						else{
							rightSlider.enabled = true;
						}
					}
				}
				else if(split[0] == "SliderThumb"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftSlider.Thumb = state;
					}
					else{
						rightSlider.Thumb = state;
					}
				}
				else if(split[0] == "SliderIndex"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftSlider.Index = state;
					}
					else{
						rightSlider.Index = state;
					}
				}
				else if(split[0] == "SliderMiddle"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftSlider.Middle = state;
					}
					else{
						rightSlider.Middle = state;
					}
				}
				else if(split[0] == "SliderRing"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftSlider.Ring = state;
					}
					else{
						rightSlider.Ring = state;
					}
				}
				else if(split[0] == "SliderPinky"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftSlider.Pinky = state;
					}
					else{
						rightSlider.Pinky = state;
					}
				}
				else if(split[0] == "ButtonControls"){
					if(side == "Left"){
						if(split[1] == "0"){
							leftButton.enabled = false;
						}
						else{
							leftButton.enabled = true;
						}
					}
					else{
						if(split[1] == "0"){
							rightButton.enabled = false;
						}
						else{
							rightButton.enabled = true;
						}
					}
				}
				else if(split[0] == "ButtonThumb"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftButton.Thumb = state;
					}
					else{
						rightButton.Thumb = state;
					}
				}
				else if(split[0] == "ButtonIndex"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftButton.Index = state;
					}
					else{
						rightButton.Index = state;
					}
				}
				else if(split[0] == "ButtonMiddle"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftButton.Middle = state;
					}
					else{
						rightButton.Middle = state;
					}
				}
				else if(split[0] == "ButtonRing"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftButton.Ring = state;
					}
					else{
						rightButton.Ring = state;
					}
				}
				else if(split[0] == "ButtonPinky"){
					PointingState state = getPointingState(split[1]);
					if(side == "Left"){
						leftButton.Pinky = state;
					}
					else{
						rightButton.Pinky = state;
					}
				}
				else if(split[0] == "PinchClose"){
					if(side == "Left"){
						leftPinch.setPinchClose(float.Parse(split[1]));
					}
					else{
						rightPinch.setPinchClose(float.Parse(split[1]));
					}
				}
				else if(split[0] == "PinchOpen"){
					if(side == "Left"){
						leftPinch.setPinchOpen(float.Parse(split[1]));
					}
					else{
						rightPinch.setPinchOpen(float.Parse(split[1]));
					}
				}
				else{
					Debug.Log("Error");
				}
			}
		}

		if(side == "Left"){
			leftSlider.OnValidate();
			leftButton.OnValidate();
			repairSettings(leftSlider);
			repairSettings(leftButton);
		}
		else{
			rightSlider.OnValidate();
			rightButton.OnValidate();
			repairSettings(rightSlider);
			repairSettings(rightButton);
		}					
	}

	//FIXME: Temporary solution to compensate for broken settings
	private void repairSettings(ExtendedFingerDetector detector){
		if(!detector.enabled){
			detector.Thumb = PointingState.Either;
			detector.Index = PointingState.Either;
			detector.Middle = PointingState.Either;
			detector.Ring = PointingState.Either;
			detector.Pinky = PointingState.Either;
			detector.enabled = true;
		}
	}

	private PointingState getPointingState(string value){
		if(value == "0") return PointingState.Extended;
		else if(value == "1") return PointingState.NotExtended;
		else return PointingState.Either;
	}
}
