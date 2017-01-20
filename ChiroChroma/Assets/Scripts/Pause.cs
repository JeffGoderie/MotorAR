using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    public GameObject m_Hand1;
    public GameObject m_Hand2;
    public Image m_PauseImage;

    private bool m_GamePaused = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("p"))   //pause the game or not
        {
            if (m_GamePaused)
                m_GamePaused = false;
            else
                m_GamePaused = true;
        }
        //vheck if there is at least one hand on the scene, if so, do not pause the game
        if (m_GamePaused || (!m_Hand1.activeSelf && !m_Hand2.activeSelf))
        {
            Time.timeScale = 0;
            m_PauseImage.enabled = true;

        }
        else
        {
            Time.timeScale = 1;
            m_PauseImage.enabled = false;
        }
    }
	public void Reset() {
		Time.timeScale = 1;
		m_PauseImage.enabled = false;
	}
}
