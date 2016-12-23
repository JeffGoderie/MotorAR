using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using LitJson;


public static class GameDataList{
/**	public static List<saveData> _GameList = new List<saveData>();
	public static GameDataList _ownGameList = null; 

	public static void saveGameData(saveData data){
		string filePath = Application.dataPath+ @"/Resources/Settings/JsonGameData.txt";
		if (!File.Exists (filePath)) {
			_ownGameList = new GameDataList ();
			_ownGameList._GameList.Add (data);
		} else {
			bool bFind = false;
			for(int i = 0; i < _ownGameList._GameList.Count; i++){
				saveData sd = _ownGameList._GameList [i];
				if (data.strID == sd.strID) {
					sd.heightThreshold = data.heightThreshold;
					sd.handTreshold = data.handTreshold;
					sd.minPinch = data.minPinch;
					sd.maxPinch = data.maxPinch;
					sd.amountColors = data.amountColors;
					bFind = true;
					break;
				}
				
			}
			if (!bFind)
				_ownGameList._GameList.Add (data);
		}

		FileInfo file = new FileInfo (filePath);
		StreamWriter sw = file.CreateText ();
		string json = JsonMapper.ToJson (_ownGameList);
		sw.WriteLine (json);
		sw.Close ();
		sw.Dispose ();


		AssetDatabase.Refresh ();

	}

	public static void LoadGameData(){
		TextAsset s = Resources.Load ("Setting/JsonGameData.txt") as TextAsset;
		if (!s)
			return;

		string strData = s.text;
		_ownGameList = JsonMapper.ToObject<GameDataList> (strData);
	}
**/
}

[System.Serializable]
class PlayerData{
	public float heightThreshold;
	public float handTreshold;
	public float minPinch;
	public float maxPinch;
	public float amountColors;
}