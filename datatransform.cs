using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class datatransform : MonoBehaviour {


	public GameObject gameHand;

	float pinch;

	List<positionData> dataList = new List<positionData>();
	int j = 0;

	// Use this for initialization

	void Start () {
		//hand_model = GetComponent ();
		gameHand = GameObject.Find("RiggedPepperCutHands");

		var file = File.Open ("test.txt", FileMode.Open);
		List<string> txt = new List<string> ();

		// read all the data in txt line by line into a list of string
		using (var stream = new StreamReader (file)) {
			while (!stream.EndOfStream) {				
				txt.Add(stream.ReadLine());
			}
		}

		// split the string by ";" and assign them into positionData
		for (int i = 0; i < (txt.Count / 2); i++) {

			string t = txt[(i*2-1)];
			string t2 = txt[(i*2)];
			List<string> Data1 = t.Split (';');
			List<string> Data2 = t2.Split (';');

			positionData newdata = new positionData();

			newdata.timeStamp = float.Parse (Data1 [0]);
			newdata.hand = Data1 [1];
			newdata.pinchDsitance = float.Parse (Data2 [0]);
			newdata.palmWorldPosX = float.Parse (Data2 [1]);
			newdata.palmWorldPosY = float.Parse (Data2 [2]);
			newdata.palmWorldPosZ = float.Parse (Data2 [3]);
			newdata.palmWorldRotW = float.Parse (Data2 [4]);
			newdata.palmWorldRotX = float.Parse (Data2 [5]);
			newdata.palmWorldRotY = float.Parse (Data2 [6]);
			newdata.palmWorldRotZ = float.Parse (Data2 [7]);

			newdata.thumbBone0W = float.Parse (Data2 [8]);
			newdata.thumbBone0X = float.Parse (Data2 [9]);
			newdata.thumbBone0Y = float.Parse (Data2 [10]);
			newdata.thumbBone0Z = float.Parse (Data2 [11]);
			newdata.thumbBone1W = float.Parse (Data2 [12]);
			newdata.thumbBone1X = float.Parse (Data2 [13]);
			newdata.thumbBone1Y = float.Parse (Data2 [14]);
			newdata.thumbBone1Z = float.Parse (Data2 [15]);
			newdata.thumbBone2W = float.Parse (Data2 [16]);
			newdata.thumbBone2X = float.Parse (Data2 [17]);
			newdata.thumbBone2Y = float.Parse (Data2 [18]);
			newdata.thumbBone2Z = float.Parse (Data2 [19]);

			newdata.indexBone0W = float.Parse (Data2 [20]);
			newdata.indexBone0X = float.Parse (Data2 [21]);
			newdata.indexBone0Y = float.Parse (Data2 [22]);
			newdata.indexBone0Z = float.Parse (Data2 [23]);
			newdata.indexBone1W = float.Parse (Data2 [24]);
			newdata.indexBone1X = float.Parse (Data2 [25]);
			newdata.indexBone1Y = float.Parse (Data2 [26]);
			newdata.indexBone1Z = float.Parse (Data2 [27]);
			newdata.indexBone2W = float.Parse (Data2 [28]);
			newdata.indexBone2X = float.Parse (Data2 [29]);
			newdata.indexBone2Y = float.Parse (Data2 [30]);
			newdata.indexBone2Z = float.Parse (Data2 [31]);

			newdata.middleBone0W = float.Parse (Data2 [32]);
			newdata.middleBone0X = float.Parse (Data2 [33]);
			newdata.middleBone0Y = float.Parse (Data2 [34]);
			newdata.middleBone0Z = float.Parse (Data2 [35]);
			newdata.middleBone1W = float.Parse (Data2 [36]);
			newdata.middleBone1X = float.Parse (Data2 [37]);
			newdata.middleBone1Y = float.Parse (Data2 [38]);
			newdata.middleBone1Z = float.Parse (Data2 [39]);
			newdata.middleBone2W = float.Parse (Data2 [40]);
			newdata.middleBone2X = float.Parse (Data2 [41]);
			newdata.middleBone2Y = float.Parse (Data2 [42]);
			newdata.middleBone2Z = float.Parse (Data2 [43]);

			newdata.ringBone0W = float.Parse (Data2 [44]);
			newdata.ringBone0X = float.Parse (Data2 [45]);
			newdata.ringBone0Y = float.Parse (Data2 [46]);
			newdata.ringBone0Z = float.Parse (Data2 [47]);
			newdata.ringBone1W = float.Parse (Data2 [48]);
			newdata.ringBone1X = float.Parse (Data2 [49]);
			newdata.ringBone1Y = float.Parse (Data2 [50]);
			newdata.ringBone1Z = float.Parse (Data2 [51]);
			newdata.ringBone2W = float.Parse (Data2 [52]);
			newdata.ringBone2X = float.Parse (Data2 [53]);
			newdata.ringBone2Y = float.Parse (Data2 [54]);
			newdata.ringBone2Z = float.Parse (Data2 [55]);

			newdata.pinkyBone0W = float.Parse (Data2 [56]);
			newdata.pinkyBone0X = float.Parse (Data2 [57]);
			newdata.pinkyBone0Y = float.Parse (Data2 [58]);
			newdata.pinkyBone0Z = float.Parse (Data2 [59]);
			newdata.pinkyBone1W = float.Parse (Data2 [60]);
			newdata.pinkyBone1X = float.Parse (Data2 [61]);
			newdata.pinkyBone1Y = float.Parse (Data2 [62]);
			newdata.pinkyBone1Z = float.Parse (Data2 [63]);
			newdata.pinkyBone2W = float.Parse (Data2 [64]);
			newdata.pinkyBone2X = float.Parse (Data2 [65]);
			newdata.pinkyBone2Y = float.Parse (Data2 [66]);
			newdata.pinkyBone2Z = float.Parse (Data2 [67]);

			dataList.Add (newdata);

		}

		file.Close ();

		
	} 



	
	// Update is called once per frame
	void Update () {
		if (time == dataList [j].timeStamp && j != dataList.Count) {

			if (dataList [j].hand == "right") {
				Transform hand = gameHand.transform.GetChild (0);
			} else {
				Transform hand = gameHand.transform.GetChild (1);
			}

			pinch = hand.PinchPosition;
			pinch = dataList [j].pinchDsitance;
			hand.PinchPosition = pinch;

			float xP = dataList [j].palmWorldPosX;
			float yP = dataList [j].palmWorldPosY;
			float zP = dataList [j].palmWorldPosZ;
			hand.transform.position = new Vector3 (xP, yP, zP);
			/*handPos = hand.transform.position;
		handPos.x = dataList[i].palmWorldPosX;
		handPos.y = dataList[i].palmWorldPosY;
		handPos.z = dataList[i].palmWorldPosZ;
		hand.transform.position = handPos;*/

			/*handRotation = hand.transform.localRotation;
		handRotation.x = dataList[i].palmWorldRotX;
		handRotation.y = dataList[i].palmWorldRotY;
		handRotation.z = dataList[i].palmWorldRotZ;
		handRotation.w = dataList[i].palmWorldRotW;
		hand.transform.localRotation = handRotation;*/
			float xR = dataList [j].palmWorldRotX;
			float yR = dataList [j].palmWorldRotY;
			float zR = dataList [j].palmWorldRotZ;
			float wR = dataList [j].palmWorldRotW;
			hand.transform.localRotation = new Quaternion (xR, yR, zR, wR);

			Transform finger0bone0 = hand.transform.GetChild (0);

			float xF0B0 = dataList [j].thumbBone0X;
			float yF0B0 = dataList [j].thumbBone0Y;
			float zF0B0 = dataList [j].thumbBone0Z;
			float wF0B0 = dataList [j].thumbBone0W;
			finger0bone0.transform.localRotation = new Quaternion (xF0B0, yF0B0, zF0B0, wF0B0);

			Transform finger0Bone1 = finger0bone0.transform.GetChild (0);

			float xF0B1 = dataList [j].thumbBone1X;
			float yF0B1 = dataList [j].thumbBone1Y;
			float zF0B1 = dataList [j].thumbBone1Z;
			float wF0B1 = dataList [j].thumbBone1W;
			finger0Bone1.transform.localRotation = new Quaternion (xF0B1, yF0B1, zF0B1, wF0B1);

			Transform finger0Bone2 = finger0Bone1.transform.GetChild (0);
			
			float xF0B2 = dataList [j].thumbBone1X;
			float yF0B2 = dataList [j].thumbBone1Y;
			float zF0B2 = dataList [j].thumbBone1Z;
			float wF0B2 = dataList [j].thumbBone1W;
			finger0Bone2.transform.localRotation = new Quaternion (xF0B2, yF0B2, zF0B2, wF0B2);

			Transform finger1bone0 = hand.transform.GetChild (1);

			float xF1B0 = dataList [j].indexBone0X;
			float yF1B0 = dataList [j].indexBone0Y;
			float zF1B0 = dataList [j].indexBone0Z;
			float wF1B0 = dataList [j].indexBone0W;
			finger1bone0.transform.localRotation = new Quaternion (xF1B0, yF1B0, zF1B0, wF1B0);

			Transform finger1Bone1 = finger1bone0.transform.GetChild (0);

			float xF1B1 = dataList [j].indexBone1X;
			float yF1B1 = dataList [j].indexBone1Y;
			float zF1B1 = dataList [j].indexBone1Z;
			float wF1B1 = dataList [j].indexBone1W;
			finger1Bone1.transform.localRotation = new Quaternion (xF1B1, yF1B1, zF1B1, wF1B1);

			Transform finger1Bone2 = finger1Bone1.transform.GetChild (0);

			float xF1B2 = dataList [j].indexBone2X;
			float yF1B2 = dataList [j].indexBone2Y;
			float zF1B2 = dataList [j].indexBone2Z;
			float wF1B2 = dataList [j].indexBone2W;
			finger1Bone2.transform.localRotation = new Quaternion (xF1B2, yF1B2, zF1B2, wF1B2);


			Transform finger2bone0 = hand.transform.GetChild (2);

			float xF2B0 = dataList [j].middleBone0X;
			float yF2B0 = dataList [j].middleBone0Y;
			float zF2B0 = dataList [j].middleBone0Z;
			float wF2B0 = dataList [j].middleBone0W;
			finger2bone0.transform.localRotation = new Quaternion (xF2B0, yF2B0, zF2B0, wF2B0);

			Transform finger2Bone1 = finger2bone0.transform.GetChild (0);

			float xF2B1 = dataList [j].middleBone1X;
			float yF2B1 = dataList [j].middleBone1Y;
			float zF2B1 = dataList [j].middleBone1Z;
			float wF2B1 = dataList [j].middleBone1W;
			finger2Bone1.transform.localRotation = new Quaternion (xF2B1, yF2B1, zF2B1, wF2B1);

			Transform finger2Bone2 = finger2Bone1.transform.GetChild (0);

			float xF2B2 = dataList [j].middleBone2X;
			float yF2B2 = dataList [j].middleBone2Y;
			float zF2B2 = dataList [j].middleBone2Z;
			float wF2B2 = dataList [j].middleBone2W;
			finger2Bone2.transform.localRotation = new Quaternion (xF2B2, yF2B2, zF2B2, wF2B2);

			Transform finger3bone0 = hand.transform.GetChild (3);

			float xF3B0 = dataList [j].ringBone0X;
			float yF3B0 = dataList [j].ringBone0Y;
			float zF3B0 = dataList [j].ringBone0Z;
			float wF3B0 = dataList [j].ringBone0W;
			finger3bone0.transform.localRotation = new Quaternion (xF3B0, yF3B0, zF3B0, wF3B0);

			Transform finger3Bone1 = finger3bone0.transform.GetChild (0);

			float xF3B1 = dataList [j].ringBone1X;
			float yF3B1 = dataList [j].ringBone1Y;
			float zF3B1 = dataList [j].ringBone1Z;
			float wF3B1 = dataList [j].ringBone1W;
			finger3Bone1.transform.localRotation = new Quaternion (xF3B1, yF3B1, zF3B1, wF3B1);

			Transform finger3Bone2 = finger3Bone1.transform.GetChild (0);

			float xF3B2 = dataList [j].ringBone2X;
			float yF3B2 = dataList [j].ringBone2Y;
			float zF3B2 = dataList [j].ringBone2Y;
			float wF3B2 = dataList [j].ringBone2W;
			finger3Bone2.transform.localRotation = new Quaternion (xF3B2, yF3B2, zF3B2, wF3B2);

			Transform finger4bone0 = hand.transform.GetChild (4);

			float xF4B0 = dataList [j].pinkyBone0X;
			float yF4B0 = dataList [j].pinkyBone0Y;
			float zF4B0 = dataList [j].pinkyBone0Z;
			float wF4B0 = dataList [j].pinkyBone0W;
			finger4bone0.transform.localRotation = new Quaternion (xF4B0, yF4B0, zF4B0, wF4B0);

			Transform finger4Bone1 = finger4bone0.transform.GetChild (0);

			float xF4B1 = dataList [j].pinkyBone1X;
			float yF4B1 = dataList [j].pinkyBone1Y;
			float zF4B1 = dataList [j].pinkyBone1Z;
			float wF4B1 = dataList [j].pinkyBone1W;
			finger4Bone1.transform.localRotation = new Quaternion (xF4B1, yF4B1, zF4B1, wF4B1);

			Transform finger4Bone2 = finger4Bone1.transform.GetChild (0);

			float xF4B2 = dataList [j].pinkyBone2X;
			float yF4B2 = dataList [j].pinkyBone2Y;
			float zF4B2 = dataList [j].pinkyBone2Z;
			float wF4B2 = dataList [j].pinkyBone2W;
			finger4Bone2.transform.localRotation = new Quaternion (xF4B2, yF4B2, zF4B2, wF4B2);




		} 
		j += 1;


	}
	public void SliderUpdate ( float newTime){
		time = newTime;
	}

}

// the class to store position data for one time point
public class positionData{

	public float timeStamp;
	public string hand;
	public float pinchDsitance;

	public float palmWorldPosX;
	public float palmWorldPosY;
	public float palmWorldPosZ;
	public float palmWorldRotW;
	public float palmWorldRotX;
	public float palmWorldRotY;
	public float palmWorldRotZ;

	public float thumbBone0W;
	public float thumbBone0X;
	public float thumbBone0Y;
	public float thumbBone0Z;
	public float thumbBone1W;
	public float thumbBone1X;
	public float thumbBone1Y;
	public float thumbBone1Z;
	public float thumbBone2W;
	public float thumbBone2X;
	public float thumbBone2Y;
	public float thumbBone2Z;

	public float indexBone0W;
	public float indexBone0X;
	public float indexBone0Y;
	public float indexBone0Z;
	public float indexBone1W;
	public float indexBone1X;
	public float indexBone1Y;
	public float indexBone1Z;
	public float indexBone2W;
	public float indexBone2X;
	public float indexBone2Y;
	public float indexBone2Z;

	public float middleBone0W;
	public float middleBone0X;
	public float middleBone0Y;
	public float middleBone0Z;
	public float middleBone1W;
	public float middleBone1X;
	public float middleBone1Y;
	public float middleBone1Z;
	public float middleBone2W;
	public float middleBone2X;
	public float middleBone2Y;
	public float middleBone2Z;

	public float ringBone0W;
	public float ringBone0X;
	public float ringBone0Y;
	public float ringBone0Z;
	public float ringBone1W;
	public float ringBone1X;
	public float ringBone1Y;
	public float ringBone1Z;
	public float ringBone2W;
	public float ringBone2X;
	public float ringBone2Y;
	public float ringBone2Z;

	public float pinkyBone0W;
	public float pinkyBone0X;
	public float pinkyBone0Y;
	public float pinkyBone0Z;
	public float pinkyBone1W;
	public float pinkyBone1X;
	public float pinkyBone1Y;
	public float pinkyBone1Z;
	public float pinkyBone2W;
	public float pinkyBone2X;
	public float pinkyBone2Y;
	public float pinkyBone2Z;
	
		
}

			



