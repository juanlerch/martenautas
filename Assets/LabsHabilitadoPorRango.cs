using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabsHabilitadoPorRango : MonoBehaviour {

	public bool geoNauta = false;
	public bool aeroNauta = false;
	public bool cosmoNauta = false; 
	public bool marteNauta = false;

	// Use this for initialization
	void Start () {
		if (Principal.avance == null)
			Principal.inicializar ();

		Button b = gameObject.GetComponent<Button> ();
		if (b != null) {	
			if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango1Geonauta)
				b.interactable = geoNauta;
			if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango2Aeronauta)
				b.interactable = aeroNauta;
			if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango3Cosmonauta)
				b.interactable = cosmoNauta;
			if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango4Martenauta)
				b.interactable = marteNauta;
			
			Debug.Log ("interactble:" + b.interactable);
			if (b.interactable) {
				Transform l = gameObject.transform.Find ("locked");
				if (l != null)
					l.gameObject.SetActive (false);
				
			}

		} else {
			Debug.Log ("Button is null");
		}
	}

	// Update is called once per frame
	void Update () {

	}
}

