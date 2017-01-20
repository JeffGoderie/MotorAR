using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;
using Leap;
using System.Linq;
using UnityEngine.UI.Extensions;

public class FeedbackController : MonoBehaviour {

   public GameObject m_HandR;
    public GameObject m_HandL;
    
    public GameObject[] rightElements;
    public GameObject[] leftElements;

    float offset = 25.0f;

	float height;
	float width;
	float right_hScale;
	float right_wScale;
	float left_hScale;
	float left_wScale;

    //float pinch;
    public List<positionData> rightList = new List<positionData>();
    public List<positionData> leftList = new List<positionData>();

    private List<float> rightTimestamps = new List<float>();
    private List<float> leftTimestamps = new List<float>();
	private List<float> rightPinchDistance = new List<float>();
    private List<float> leftPinchDistance = new List<float>();

    public UILineRenderer r_UILineRenderer;
    public UILineRenderer r_indicator;
    public UILineRenderer l_UILineRenderer;
    public UILineRenderer l_indicator;
    public Slider r_Slider;
    public Slider l_Slider;

    public int index ;

    void Update(){
    	if (Input.GetKeyDown("l")){
            foreach(GameObject left in leftElements){
            	left.SetActive(true);
            }
            foreach(GameObject right in rightElements){
            	right.SetActive(false);
            }
        }
        if (Input.GetKeyDown("r")){
            foreach(GameObject left in leftElements){
            	left.SetActive(false);
            }
            foreach(GameObject right in rightElements){
            	right.SetActive(true);
            }
        }
    }
    //public GameObject m_HandL;
	// Use this for initialization
	public void loadData (string path) {
		if(path == ""){
			return;
		}
        //hand = m_HandR.GetComponent<HandModel>();
        Debug.LogError(path);
        rightList = new List<positionData>();
        leftList = new List<positionData>();
        rightTimestamps = new List<float>();
    	leftTimestamps = new List<float>();
		rightPinchDistance = new List<float>();
    	leftPinchDistance = new List<float>();

        var file = File.Open(path, FileMode.Open);
        List<string> txt = new List<string>();

        // read all the data in txt line by line into a list of string
        using (var stream = new StreamReader(file))
        {
            while (!stream.EndOfStream)
            {
                txt.Add(stream.ReadLine());
            }
        }
        
        // split the string by ";" and assign them into positionData
        Debug.Log(txt.Count);
        for (int i = 0; i < txt.Count; i++)
        {

            string t = txt[i];

            List<string> Data1 = new List<string>(t.Split(';'));


            positionData newdata = new positionData();

            newdata.timeStamp = float.Parse(Data1[0]);
            newdata.hand = Data1[1];
            newdata.pinchDsitance = float.Parse(Data1[2]);

            newdata.palmLeapPosX = float.Parse(Data1[3]);
            newdata.palmLeapPosY = float.Parse(Data1[4]);
            newdata.palmLeapPosZ = float.Parse(Data1[5]);
            newdata.palmNormalPosX = float.Parse(Data1[6]);
            newdata.palmNormalPosY = float.Parse(Data1[7]);
            newdata.palmNormalPosZ = float.Parse(Data1[8]);

            newdata.palmWorldPosX = float.Parse(Data1[9]);
            newdata.palmWorldPosY = float.Parse(Data1[10]);
            newdata.palmWorldPosZ = float.Parse(Data1[11]);
            newdata.palmWorldRotW = float.Parse(Data1[12]);
            newdata.palmWorldRotX = float.Parse(Data1[13]);
            newdata.palmWorldRotY = float.Parse(Data1[14]);
            newdata.palmWorldRotZ = float.Parse(Data1[15]);

            newdata.thumbBone0W = float.Parse(Data1[16]);
            newdata.thumbBone0X = float.Parse(Data1[17]);
            newdata.thumbBone0Y = float.Parse(Data1[18]);
            newdata.thumbBone0Z = float.Parse(Data1[19]);
            newdata.thumbBone1W = float.Parse(Data1[20]);
            newdata.thumbBone1X = float.Parse(Data1[21]);
            newdata.thumbBone1Y = float.Parse(Data1[22]);
            newdata.thumbBone1Z = float.Parse(Data1[23]);
            newdata.thumbBone2W = float.Parse(Data1[24]);
            newdata.thumbBone2X = float.Parse(Data1[25]);
            newdata.thumbBone2Y = float.Parse(Data1[26]);
            newdata.thumbBone2Z = float.Parse(Data1[27]);

            newdata.indexBone0W = float.Parse(Data1[28]);
            newdata.indexBone0X = float.Parse(Data1[29]);
            newdata.indexBone0Y = float.Parse(Data1[30]);
            newdata.indexBone0Z = float.Parse(Data1[31]);
            newdata.indexBone1W = float.Parse(Data1[32]);
            newdata.indexBone1X = float.Parse(Data1[33]);
            newdata.indexBone1Y = float.Parse(Data1[34]);
            newdata.indexBone1Z = float.Parse(Data1[35]);
            newdata.indexBone2W = float.Parse(Data1[36]);
            newdata.indexBone2X = float.Parse(Data1[37]);
            newdata.indexBone2Y = float.Parse(Data1[38]);
            newdata.indexBone2Z = float.Parse(Data1[39]);

            newdata.middleBone0W = float.Parse(Data1[40]);
            newdata.middleBone0X = float.Parse(Data1[41]);
            newdata.middleBone0Y = float.Parse(Data1[42]);
            newdata.middleBone0Z = float.Parse(Data1[43]);
            newdata.middleBone1W = float.Parse(Data1[44]);
            newdata.middleBone1X = float.Parse(Data1[45]);
            newdata.middleBone1Y = float.Parse(Data1[46]);
            newdata.middleBone1Z = float.Parse(Data1[47]);
            newdata.middleBone2W = float.Parse(Data1[48]);
            newdata.middleBone2X = float.Parse(Data1[49]);
            newdata.middleBone2Y = float.Parse(Data1[50]);
            newdata.middleBone2Z = float.Parse(Data1[51]);

            newdata.ringBone0W = float.Parse(Data1[52]);
            newdata.ringBone0X = float.Parse(Data1[53]);
            newdata.ringBone0Y = float.Parse(Data1[54]);
            newdata.ringBone0Z = float.Parse(Data1[55]);
            newdata.ringBone1W = float.Parse(Data1[56]);
            newdata.ringBone1X = float.Parse(Data1[57]);
            newdata.ringBone1Y = float.Parse(Data1[58]);
            newdata.ringBone1Z = float.Parse(Data1[59]);
            newdata.ringBone2W = float.Parse(Data1[60]);
            newdata.ringBone2X = float.Parse(Data1[61]);
            newdata.ringBone2Y = float.Parse(Data1[62]);
            newdata.ringBone2Z = float.Parse(Data1[63]);

            newdata.pinkyBone0W = float.Parse(Data1[64]);
            newdata.pinkyBone0X = float.Parse(Data1[65]);
            newdata.pinkyBone0Y = float.Parse(Data1[66]);
            newdata.pinkyBone0Z = float.Parse(Data1[67]);
            newdata.pinkyBone1W = float.Parse(Data1[68]);
            newdata.pinkyBone1X = float.Parse(Data1[69]);
            newdata.pinkyBone1Y = float.Parse(Data1[70]);
            newdata.pinkyBone1Z = float.Parse(Data1[71]);
            newdata.pinkyBone2W = float.Parse(Data1[72]);
            newdata.pinkyBone2X = float.Parse(Data1[73]);
            newdata.pinkyBone2Y = float.Parse(Data1[74]);
            newdata.pinkyBone2Z = float.Parse(Data1[75]);

            if(Data1[1] == "right"){
            	rightList.Add(newdata);
            	rightTimestamps.Add(newdata.timeStamp);
            	rightPinchDistance.Add(newdata.pinchDsitance);
            }
            else{
            	leftList.Add(newdata);
            	leftTimestamps.Add(newdata.timeStamp);
            	leftPinchDistance.Add(newdata.pinchDsitance);
            }

        }



        file.Close();
        
		height = r_UILineRenderer.transform.parent.GetComponent<RectTransform>().rect.height - 2*offset;
		width = r_UILineRenderer.transform.parent.GetComponent<RectTransform>().rect.width - 2*offset;
		

		if(rightList.Count > 0){
			right_hScale = height/rightPinchDistance.Max();
			right_wScale = width/rightTimestamps.Max();
			Vector2[] points = new Vector2[rightTimestamps.Count];
	
	        for (int i = 0; i < rightPinchDistance.Count; i++) {
				points[i] = new Vector2(rightTimestamps[i]*right_wScale - width/2, rightPinchDistance[i]*right_hScale - height/2);
			}
	
			r_UILineRenderer.Points = points;
			r_Slider.maxValue = rightTimestamps.Count;
			r_Slider.value = 0.0f;
		}

		if(leftList.Count > 0){
			left_hScale = height/leftPinchDistance.Max();
			left_wScale = width/leftTimestamps.Max();
	        
			Vector2[] points = new Vector2[leftTimestamps.Count];

	        for (int i = 0; i < leftPinchDistance.Count; i++) {
				points[i] = new Vector2(leftTimestamps[i]*left_wScale - width/2, leftPinchDistance[i]*left_hScale - height/2);
			}

			l_UILineRenderer.Points = points;
			l_Slider.maxValue = leftTimestamps.Count;
			l_Slider.value = 0.0f;
		}
	}
    
	public void setIndicatorPosition(string hand){
		Slider _Slider;
		UILineRenderer indicator;
		float wScale;
		List<positionData> list;
		
		if(hand == "left" ){
			_Slider = l_Slider;
			indicator = l_indicator;
			wScale = left_wScale;
			list = leftList;
		}
		else{
			_Slider = r_Slider;
			indicator = r_indicator;
			wScale = right_wScale;
			list = rightList;
		}
		int index = Mathf.FloorToInt(_Slider.value);
		float position = rightList[index].timeStamp*wScale-width/2;

		Vector2[] points = new Vector2[2];

		points[0] = new Vector2(position,-1000f);
		points[1] = new Vector2(position,1000f);

		indicator.Points = points;
	}

	// Update is called once per frame
	public void UpdateHand(string hand) {

		Slider _Slider;
		GameObject _hand;
		List<positionData> list;

		if(hand == "left"){
			_Slider = l_Slider;
			_hand = m_HandL;
			list = leftList;
		}
		else{
			_Slider = r_Slider;
			_hand = m_HandR;
			list = rightList;
		}
		
		int index = Mathf.FloorToInt(_Slider.value);
		Debug.Log(index);
    //    index = Mathf.RoundToInt(index);
       
        //if (rightList[index].hand == "right" )
       // {
            /*float xP = rightList[index].palmWorldPosX;
            float yP = rightList[index].palmWorldPosY;
            float zP = rightList[index].palmWorldPosZ;
            m_HandR.transform.position = new Vector3(xP, yP, zP);

            float xR = rightList[index].palmWorldRotX;
            float yR = rightList[index].palmWorldRotY;
            float zR = rightList[index].palmWorldRotZ;
            float wR = rightList[index].palmWorldRotW;
            m_HandR.transform.rotation = new Quaternion(xR, yR, zR, wR);*/

            for (int x = 0; x < 5; x++)
            {
                if (x == 0)
                {
                    Transform bone0 = _hand.transform.GetChild(x);
                    float xF0B0 = list[index].thumbBone0X;
                    float yF0B0 = list[index].thumbBone0Y;
                    float zF0B0 = list[index].thumbBone0Z;
                    float wF0B0 = list[index].thumbBone0W;
                    bone0.transform.localRotation = new Quaternion(xF0B0, yF0B0, zF0B0, wF0B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF0B1 = list[index].thumbBone1X;
                    float yF0B1 = list[index].thumbBone1Y;
                    float zF0B1 = list[index].thumbBone1Z;
                    float wF0B1 = list[index].thumbBone1W;
                    bone1.transform.localRotation = new Quaternion(xF0B1, yF0B1, zF0B1, wF0B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF0B2 = list[index].thumbBone1X;
                    float yF0B2 = list[index].thumbBone1Y;
                    float zF0B2 = list[index].thumbBone1Z;
                    float wF0B2 = list[index].thumbBone1W;
                    bone2.transform.localRotation = new Quaternion(xF0B2, yF0B2, zF0B2, wF0B2);

                }

                if (x == 1)
                {
                    Transform bone0 = _hand.transform.GetChild(x);
                    float xF1B0 = list[index].indexBone0X;
                    float yF1B0 = list[index].indexBone0Y;
                    float zF1B0 = list[index].indexBone0Z;
                    float wF1B0 = list[index].indexBone0W;
                    bone0.transform.localRotation = new Quaternion(xF1B0, yF1B0, zF1B0, wF1B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF1B1 = list[index].indexBone1X;
                    float yF1B1 = list[index].indexBone1Y;
                    float zF1B1 = list[index].indexBone1Z;
                    float wF1B1 = list[index].indexBone1W;
                    bone1.transform.localRotation = new Quaternion(xF1B1, yF1B1, zF1B1, wF1B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF1B2 = list[index].indexBone2X;
                    float yF1B2 = list[index].indexBone2Y;
                    float zF1B2 = list[index].indexBone2Z;
                    float wF1B2 = list[index].indexBone2W;
                    bone2.transform.localRotation = new Quaternion(xF1B2, yF1B2, zF1B2, wF1B2);

                }

                if (x == 2)
                {
                    Transform bone0 = _hand.transform.GetChild(x);
                    float xF2B0 = list[index].middleBone0X;
                    float yF2B0 = list[index].middleBone0Y;
                    float zF2B0 = list[index].middleBone0Z;
                    float wF2B0 = list[index].middleBone0W;
                    bone0.transform.localRotation = new Quaternion(xF2B0, yF2B0, zF2B0, wF2B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF2B1 = list[index].middleBone1X;
                    float yF2B1 = list[index].middleBone1Y;
                    float zF2B1 = list[index].middleBone1Z;
                    float wF2B1 = list[index].middleBone1W;
                    bone1.transform.localRotation = new Quaternion(xF2B1, yF2B1, zF2B1, wF2B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF2B2 = list[index].middleBone2X;
                    float yF2B2 = list[index].middleBone2Y;
                    float zF2B2 = list[index].middleBone2Z;
                    float wF2B2 = list[index].middleBone2W;
                    bone2.transform.localRotation = new Quaternion(xF2B2, yF2B2, zF2B2, wF2B2);

                }

                if (x == 3)
                {
                    Transform bone0 = _hand.transform.GetChild(x);
                    float xF3B0 = list[index].ringBone0X;
                    float yF3B0 = list[index].ringBone0Y;
                    float zF3B0 = list[index].ringBone0Z;
                    float wF3B0 = list[index].ringBone0W;
                    bone0.transform.localRotation = new Quaternion(xF3B0, yF3B0, zF3B0, wF3B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF3B1 = list[index].ringBone1X;
                    float yF3B1 = list[index].ringBone1Y;
                    float zF3B1 = list[index].ringBone1Z;
                    float wF3B1 = list[index].ringBone1W;
                    bone1.transform.localRotation = new Quaternion(xF3B1, yF3B1, zF3B1, wF3B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF3B2 = list[index].ringBone2X;
                    float yF3B2 = list[index].ringBone2Y;
                    float zF3B2 = list[index].ringBone2Y;
                    float wF3B2 = list[index].ringBone2W;
                    bone2.transform.localRotation = new Quaternion(xF3B2, yF3B2, zF3B2, wF3B2);

                }

                if (x == 4)
                {
                    Transform bone0 = _hand.transform.GetChild(x);
                    float xF4B0 = list[index].pinkyBone0X;
                    float yF4B0 = list[index].pinkyBone0Y;
                    float zF4B0 = list[index].pinkyBone0Z;
                    float wF4B0 = list[index].pinkyBone0W;
                    bone0.transform.localRotation = new Quaternion(xF4B0, yF4B0, zF4B0, wF4B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF4B1 = list[index].pinkyBone1X;
                    float yF4B1 = list[index].pinkyBone1Y;
                    float zF4B1 = list[index].pinkyBone1Z;
                    float wF4B1 = list[index].pinkyBone1W;
                    bone1.transform.localRotation = new Quaternion(xF4B1, yF4B1, zF4B1, wF4B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF4B2 = list[index].pinkyBone2X;
                    float yF4B2 = list[index].pinkyBone2Y;
                    float zF4B2 = list[index].pinkyBone2Z;
                    float wF4B2 = list[index].pinkyBone2W;
                    bone2.transform.localRotation = new Quaternion(xF4B2, yF4B2, zF4B2, wF4B2);

                }
            }
        
        //else if (dataList[index]._hand == "left" )
        //{
            /*float xP = dataList[index].palmWorldPosX;
            float yP = dataList[index].palmWorldPosY;
            float zP = dataList[index].palmWorldPosZ;
            m_HandL.transform.position = new Vector3(xP, yP, zP);

            float xR = dataList[index].palmWorldRotX;
            float yR = dataList[index].palmWorldRotY;
            float zR = dataList[index].palmWorldRotZ;
            float wR = dataList[index].palmWorldRotW;
            m_HandL.transform.rotation = new Quaternion(xR, yR, zR, wR);


            for (int x = 0; x < 5; x++)
            {
                if (x == 0)
                {
                    Transform bone0 = m_HandL.transform.GetChild(x);
                    float xF0B0 = dataList[index].thumbBone0X;
                    float yF0B0 = dataList[index].thumbBone0Y;
                    float zF0B0 = dataList[index].thumbBone0Z;
                    float wF0B0 = dataList[index].thumbBone0W;
                    bone0.transform.localRotation = new Quaternion(xF0B0, yF0B0, zF0B0, wF0B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF0B1 = dataList[index].thumbBone1X;
                    float yF0B1 = dataList[index].thumbBone1Y;
                    float zF0B1 = dataList[index].thumbBone1Z;
                    float wF0B1 = dataList[index].thumbBone1W;
                    bone1.transform.localRotation = new Quaternion(xF0B1, yF0B1, zF0B1, wF0B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF0B2 = dataList[index].thumbBone1X;
                    float yF0B2 = dataList[index].thumbBone1Y;
                    float zF0B2 = dataList[index].thumbBone1Z;
                    float wF0B2 = dataList[index].thumbBone1W;
                    bone2.transform.localRotation = new Quaternion(xF0B2, yF0B2, zF0B2, wF0B2);

                }

                if (x == 1)
                {
                    Transform bone0 = m_HandL.transform.GetChild(x);
                    float xF1B0 = dataList[index].indexBone0X;
                    float yF1B0 = dataList[index].indexBone0Y;
                    float zF1B0 = dataList[index].indexBone0Z;
                    float wF1B0 = dataList[index].indexBone0W;
                    bone0.transform.localRotation = new Quaternion(xF1B0, yF1B0, zF1B0, wF1B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF1B1 = dataList[index].indexBone1X;
                    float yF1B1 = dataList[index].indexBone1Y;
                    float zF1B1 = dataList[index].indexBone1Z;
                    float wF1B1 = dataList[index].indexBone1W;
                    bone1.transform.localRotation = new Quaternion(xF1B1, yF1B1, zF1B1, wF1B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF1B2 = dataList[index].indexBone2X;
                    float yF1B2 = dataList[index].indexBone2Y;
                    float zF1B2 = dataList[index].indexBone2Z;
                    float wF1B2 = dataList[index].indexBone2W;
                    bone2.transform.localRotation = new Quaternion(xF1B2, yF1B2, zF1B2, wF1B2);

                }

                if (x == 2)
                {
                    Transform bone0 = m_HandL.transform.GetChild(x);
                    float xF2B0 = dataList[index].middleBone0X;
                    float yF2B0 = dataList[index].middleBone0Y;
                    float zF2B0 = dataList[index].middleBone0Z;
                    float wF2B0 = dataList[index].middleBone0W;
                    bone0.transform.localRotation = new Quaternion(xF2B0, yF2B0, zF2B0, wF2B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF2B1 = dataList[index].middleBone1X;
                    float yF2B1 = dataList[index].middleBone1Y;
                    float zF2B1 = dataList[index].middleBone1Z;
                    float wF2B1 = dataList[index].middleBone1W;
                    bone1.transform.localRotation = new Quaternion(xF2B1, yF2B1, zF2B1, wF2B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF2B2 = dataList[index].middleBone2X;
                    float yF2B2 = dataList[index].middleBone2Y;
                    float zF2B2 = dataList[index].middleBone2Z;
                    float wF2B2 = dataList[index].middleBone2W;
                    bone2.transform.localRotation = new Quaternion(xF2B2, yF2B2, zF2B2, wF2B2);

                }

                if (x == 3)
                {
                    Transform bone0 = m_HandL.transform.GetChild(x);
                    float xF3B0 = dataList[index].ringBone0X;
                    float yF3B0 = dataList[index].ringBone0Y;
                    float zF3B0 = dataList[index].ringBone0Z;
                    float wF3B0 = dataList[index].ringBone0W;
                    bone0.transform.localRotation = new Quaternion(xF3B0, yF3B0, zF3B0, wF3B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF3B1 = dataList[index].ringBone1X;
                    float yF3B1 = dataList[index].ringBone1Y;
                    float zF3B1 = dataList[index].ringBone1Z;
                    float wF3B1 = dataList[index].ringBone1W;
                    bone1.transform.localRotation = new Quaternion(xF3B1, yF3B1, zF3B1, wF3B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF3B2 = dataList[index].ringBone2X;
                    float yF3B2 = dataList[index].ringBone2Y;
                    float zF3B2 = dataList[index].ringBone2Y;
                    float wF3B2 = dataList[index].ringBone2W;
                    bone2.transform.localRotation = new Quaternion(xF3B2, yF3B2, zF3B2, wF3B2);

                }

                if (x == 4)
                {
                    Transform bone0 = m_HandL.transform.GetChild(x);
                    float xF4B0 = dataList[index].pinkyBone0X;
                    float yF4B0 = dataList[index].pinkyBone0Y;
                    float zF4B0 = dataList[index].pinkyBone0Z;
                    float wF4B0 = dataList[index].pinkyBone0W;
                    bone0.transform.localRotation = new Quaternion(xF4B0, yF4B0, zF4B0, wF4B0);
                    Transform bone1 = bone0.transform.GetChild(0);
                    float xF4B1 = dataList[index].pinkyBone1X;
                    float yF4B1 = dataList[index].pinkyBone1Y;
                    float zF4B1 = dataList[index].pinkyBone1Z;
                    float wF4B1 = dataList[index].pinkyBone1W;
                    bone1.transform.localRotation = new Quaternion(xF4B1, yF4B1, zF4B1, wF4B1);
                    Transform bone2 = bone1.transform.GetChild(0);
                    float xF4B2 = dataList[index].pinkyBone2X;
                    float yF4B2 = dataList[index].pinkyBone2Y;
                    float zF4B2 = dataList[index].pinkyBone2Z;
                    float wF4B2 = dataList[index].pinkyBone2W;
                    bone2.transform.localRotation = new Quaternion(xF4B2, yF4B2, zF4B2, wF4B2);

                }
            }
        }
        
        index += 1;

		
	}

    public void SliderUpdate(float newIndex)
    {
        index = newIndex;
    }*/
	}
}

// the class to store position data for one time point
public class positionData
{

    public float timeStamp;
    public string hand;
    public float pinchDsitance;

    public float palmLeapPosX;
    public float palmLeapPosY;
    public float palmLeapPosZ;
    public float palmNormalPosX;
    public float palmNormalPosY;
    public float palmNormalPosZ;

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
