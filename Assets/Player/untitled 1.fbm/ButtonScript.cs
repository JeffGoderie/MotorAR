using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	bool collision;
	float y;
	GameObject button;

	void Start(){
		button = transform.GetChild(0).gameObject;
		collision = false;
		y = button.transform.position.y;
	}

	void Update(){
		if(collision){
			y -= 0.05f;
			if(y < 0.0f){
				y = 0.0f;
			}
		}
		else{
			y += 0.05f;
			if(y > 0.5f){
				y = 0.5f;
			}
		}
		button.transform.position = new Vector3(button.transform.position.x, y, button.transform.position.z);
	}

	 void OnCollisionEnter (Collision col)
    {
        collision = true;
    }

    void OnCollisionExit(Collision col){
    	collision = false;
    }
}
