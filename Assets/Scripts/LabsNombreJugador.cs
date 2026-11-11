using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class LabsNombreJugador : MonoBehaviour {

  
       // public GameObject Gpuntos;
        // Use this for initialization
        void Start()
        {
        }

	void Update(){
	
		Principal p =new Principal();
		p.cargarModelo();
		Text text = GetComponent<Text>();

		if (Principal.user !=text.text)  text.text = "" + Principal.user;

	} 
  
}
