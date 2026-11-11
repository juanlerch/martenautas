using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsUsuarioLogueado : MonoBehaviour {

	public bool requiereToken=true;

	// Use this for initialization
	void Start () {
		if(Principal.avance==null) Principal.inicializar ();

		Toggle toogle = GetComponent<Toggle> ();
		if (toogle != null) {
			toogle.isOn = Principal.avance.mantenerLogin;
		}
		InputField text= GetComponent<InputField> ();
		if(text!=null){
			if (requiereToken) {
				if (Principal.avance.avanceJugadorActual ().token != null) {
					text.text = Principal.user;
				}
				;
			}
			else {
				text.text = Principal.user;
			}

		}
	}

	public void cambiarValor(){
		Toggle toogle = GetComponent<Toggle> ();
		Principal.avance.mantenerLogin=toogle.isOn ;
		Debug.Log ("cambiarValor "+ toogle.isOn);
	}


	// Update is called once per frame
	void Update () {
		
	}
}
