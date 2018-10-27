using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Progress : MonoBehaviour {
    public Image m_kBackground = null;
    public Image m_kForeground = null;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetFillAmound(float progress){
        m_kForeground.fillAmount = progress;

        //Debug.LogErrorFormat("当前进度{0}", progress);
    }
}
