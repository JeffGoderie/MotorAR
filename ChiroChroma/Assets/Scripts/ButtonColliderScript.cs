using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColliderScript : MonoBehaviour {

	private float delta;

    public ControllerScript controller;
    public bool next;

    private float timer = 0.3f;
    private bool countdown = false;

    public bool triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(countdown){
            timer -= Time.deltaTime;
        }
	}

	void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "Palm"){
            triggered = true;

            if(timer <= 0.0f){
                if(next){
                    controller.selectNextArea();
                }
                else{
                    controller.selectPreviousArea();
                }
                countdown = false;
                timer = 0.3f;
            }
        }
    }

    void OnTriggerExit (Collider col)
    {
        if(col.gameObject.tag == "Palm")
        {
            triggered = false;
            countdown = true;
        }
    }

}
