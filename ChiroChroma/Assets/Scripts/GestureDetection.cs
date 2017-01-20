using UnityEngine;
using System.Collections;
using Leap.Unity;


public class GestureDetection : MonoBehaviour {

	[SerializeField]
	private float m_pinchMinDistance;
	[SerializeField]
	private float m_pinchMaxDistance;
	[SerializeField]
	private float m_pinchOpenStrength;
	[SerializeField]
	private float m_pinchCloseStrength;
    [SerializeField]
    private float m_PinchTime;

    private bool m_PinchStarted = false;
    private float m_PinchTimer = 0.0f;

   
    private HandModel m_HandModel;


    public ControllerScript controllerScript;


	// Use this for initialization
	void Start () {
		m_HandModel = this.GetComponent<HandModel> ();
       
    }

    public void setPinchClose(float percentage){
        Debug.Log("Percentage: " + percentage);
        m_pinchCloseStrength = percentage/100.0f;
    }

    public void setPinchOpen(float percentage){
        m_pinchOpenStrength = percentage/100.0f;
    }

    public float increaseOpen(){
        if(m_pinchOpenStrength + 0.01f < m_pinchCloseStrength){
            m_pinchOpenStrength += 0.01f;
        }

        return m_pinchOpenStrength;
    }

    public float decreaseOpen(){
        if(m_pinchOpenStrength - 0.01f > 0.0f){
            m_pinchOpenStrength -= 0.01f;
        }

        return m_pinchOpenStrength;
    }

    public float increaseClosed(){
        if(m_pinchCloseStrength + 0.01f <= 1.0f){
            m_pinchCloseStrength += 0.01f;
        }

        return m_pinchCloseStrength;
    }

    public float decreaseClosed(){
        if(m_pinchCloseStrength - 0.01f > m_pinchOpenStrength){
            m_pinchCloseStrength -= 0.01f;
        }

        return m_pinchCloseStrength;
    }

	// Update is called once per frame
	void Update () {
       
        if (m_HandModel) {
            Vector3 indexPosition = m_HandModel.fingers [1].GetBoneCenter (3);
            Vector3 thumbPosition = m_HandModel.fingers [0].GetBoneCenter (3);
            
            float distance = Vector3.Distance(indexPosition, thumbPosition);

            
            float normalizedPinchDistance = (distance - m_pinchMinDistance) / (m_pinchMaxDistance - m_pinchMinDistance);
            float pinchStrength = 1.0f - Mathf.Clamp01 (normalizedPinchDistance);
            
            m_PinchTimer -= Time.deltaTime;
            if(m_PinchTimer < 0 && !m_PinchStarted && pinchStrength >= m_pinchCloseStrength)
            {
                m_PinchStarted = true;
                m_PinchTimer = m_PinchTime;
               
                controllerScript.setPinchWrite();
                controllerScript.colorSelectedArea();
            }
          
            if( (m_PinchStarted && pinchStrength <= m_pinchOpenStrength))
            {
                m_PinchStarted = false;
                
            }

        }
    }
}