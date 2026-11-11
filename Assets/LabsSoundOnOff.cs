using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsSoundOnOff : MonoBehaviour {

	public AudioSource[] on;

	public AudioSource[] off;

	public AudioSource[] pause;



	void OnEnable(){
		doit ();
	} 

	void OnDisable(){

		if(pause!=null)foreach (AudioSource s in pause){
				if(s!=null) s.Play ();
		}	
 		
		if(on!=null)foreach (AudioSource s in on){
				if(s!=null) s.Stop();
		}	

	} 


	public void doit(){
	
		if(off!=null) foreach (AudioSource s in off){
				if(s!=null) s.Stop ();
		}	

		if(on!=null) foreach (AudioSource s in on){
				if(s!=null) s.Play();
		}	

		if(pause!=null) foreach (AudioSource s in pause){
				if(s!=null) s.Stop ();
		}	



	}

}
