using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsFade : MonoBehaviour {

    public string unaVezID=null;
    

   // static List<string> mostrado = new List<string>();
    Animator anim;


    public void fade() {

		if(unaVezID != null && unaVezID!=""){
			if (Principal.avance.avanceJugadorActual().mostrado.Contains (unaVezID)) { 
				gameObject.SetActive (true);
				anim = gameObject.GetComponent<Animator> ();
				anim.enabled = false;
			} else
			{
				doFade ();
			}	
		}else {
			doFade ();
		}	
    }

	void doFade(){
		gameObject.SetActive (true);
		GameObject sonido_logro = GameObject.Find("sonido_logro");
		AudioSource audio = sonido_logro.GetComponent<AudioSource>();
		audio.Play();
        Principal.avance.avanceJugadorActual().mostrado.Add(unaVezID);
	}

    // Use this for initialization
    void Start () {
            anim = GetComponent<Animator>();
			
    }

    void OnDisable()
    {
        anim.enabled=false;
    }

    // Update is called once per frame
    void Update () {

	}
  
}