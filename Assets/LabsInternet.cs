using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabsInternet : MonoBehaviour {

	public GameObject conInternet;
	public GameObject sinInternet;

	public bool requiereToken=true;

	void Start () {
		if(Principal.avance==null) Principal.inicializar ();

		Toggle toogle = GetComponent<Toggle> ();
		if (toogle != null) {
			toogle.isOn = Principal.avance.internet;
		}
	}

	void Update(){
		actualizarUI (); 
	}

	public void actualizarUI(){
		if (Principal.avance.internet && (Principal.avance.avanceJugadorActual().token!=null || !requiereToken)) {
			if (conInternet != null) conInternet.SetActive (true);
			if (sinInternet != null) sinInternet.SetActive (false);
		} else {
			if (conInternet != null) conInternet.SetActive (false);
			if (sinInternet != null) sinInternet.SetActive (true);
		}
		Button b = gameObject.GetComponent<Button> ();
		if (b != null) {
			b.interactable = Principal.avance.internet;
		}
	}

	public void cambiarValor(){
		Toggle toogle = GetComponent<Toggle> ();
		Principal.avance.internet =toogle.isOn ;
		actualizarUI ();
//		Debug.Log ("cambiarValor "+ toogle.isOn);
	}

}
