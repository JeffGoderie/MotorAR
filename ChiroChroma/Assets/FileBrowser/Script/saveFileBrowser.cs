using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class saveFileBrowser : MonoBehaviour {
	//skins and textures
	public GUISkin[] skins;
	public Texture2D file,folder,back,drive;
	public string extension = "*";
	public bool flag = false;

	[Serializable]
	public class SaveFunctionality : UnityEvent<string>{}

	public void enable(){
		flag = true;
	}

	string[] layoutTypes = {"Type 0","Type 1"};
	//initialize file browser
	FileBrowser fb = new FileBrowser();
	string output = "no file";
	// Use this for initialization
	void Start () {
		//setup file browser style
		fb.guiSkin = skins[0]; //set the starting skin
		//set the various textures
		fb.fileTexture = file; 
		fb.directoryTexture = folder;
		fb.backTexture = back;
		fb.driveTexture = drive;
		//show the search bar
		fb.showSave = true;
		fb.searchPattern = "";
		//search recursively (setting recursive search may cause a long delay)
		fb.searchRecursively = false;
	}
	
	void OnGUI(){
		fb.setLayout(1);
		if(flag){
			if(fb.draw()){ //true is returned when a file has been selected
				//the output file is a member if the FileInfo class, if cancel was selected the value is null
				if(fb.currentDirectory!=null){
					saveFunctionality.Invoke(fb.currentDirectory.ToString() + "/" + fb.saveBarString + "." + extension);	
				}
				else{
					saveFunctionality.Invoke("");
				}
				flag = false;
			}
		}
	}

	public SaveFunctionality saveFunctionality = new SaveFunctionality();
}
