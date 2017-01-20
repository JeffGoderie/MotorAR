using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class loadFileBrowser : MonoBehaviour {
	//skins and textures
	public GUISkin[] skins;
	public Texture2D file,folder,back,drive;
	public string extension = "*";
	private bool flag = false;
	
	[Serializable]
	public class LoadFunctionality : UnityEvent<string>{}

	public void enable(){
		flag = true;
	}

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
		fb.searchPattern = "*." + extension;
		//show the search bar
		fb.showSearch = false;
		//search recursively (setting recursive search may cause a long delay)
		fb.searchRecursively = false;
	}
	
	void OnGUI(){
		//draw and display output
		if(flag){
			if(fb.draw()){ //true is returned when a file has been selected
				//the output file is a member if the FileInfo class, if cancel was selected the value is null
				if(fb.outputFile!=null){
                    #if(UNITY_EDITOR)
                    loadFunctionality.Invoke(fb.outputFile.ToString());
                    #else
                    loadFunctionality.Invoke(fb.currentDirectory + "/" + fb.outputFile.ToString());
                    #endif
                }
				else{
					loadFunctionality.Invoke("");
				}
				flag = false;
			}
		}
	}

	public LoadFunctionality loadFunctionality = new LoadFunctionality();
}
