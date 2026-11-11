using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsSound : MonoBehaviour {


	void Start(){
		gameObject.GetComponent<Toggle>().isOn = !AudioListener.pause ;
	}	

	public void mute(){ 
		AudioListener.pause = !gameObject.GetComponent<Toggle>().isOn;
	}

}
