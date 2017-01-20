using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using System.IO;

public class DataScript : MonoBehaviour {

	public SettingsScript leftSettings;
	public SettingsScript rightSettings;

	void Start(){
		string path = "./_default_.settings";
		if(File.Exists(path)){
			setSettings(path);
		}
	}

	public void applySettings(){
		File.WriteAllLines("./_temp_.settings",getSettings().ToArray());
	}

	public void saveSettings(string path){
		if(path.Length != 0){
			File.WriteAllLines(path,getSettings().ToArray());
		}
	}

	public void loadSettings(string path){
		//string path = EditorUtility.OpenFilePanel("Load settings", "", "settings");
		setSettings(path);		
	}

	public List<string> getSettings(){
		List<string> settings = new List<string>();
		settings.Add("Left:");
		settings.AddRange(leftSettings.getValues());
		settings.Add("Right:");
		settings.AddRange(rightSettings.getValues());

		foreach(string s in settings){
			Debug.Log(s);
		}

		return settings;
	}

	public void setSettings(string path){
		if (path.Length != 0) {
			string[] lines = File.ReadAllLines(path);

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

			leftSettings.setValues(leftValues);
			rightSettings.setValues(rightValues);
		}
	}
}


