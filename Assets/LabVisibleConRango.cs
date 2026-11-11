using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabVisibleConRango : MonoBehaviour {

	public bool geoNauta = false;
	public bool aeroNauta = false;
	public bool cosmoNauta = false;
	public bool marteNauta = false;
	 
	// Use this for initialization
	void Start () {
		if (Principal.avance == null)
			Principal.inicializar ();

		if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango1Geonauta)
			gameObject.SetActive (geoNauta);
		if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango2Aeronauta)
			gameObject.SetActive (aeroNauta);
		if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango3Cosmonauta)
			gameObject.SetActive (cosmoNauta);
		if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango4Martenauta)
			gameObject.SetActive (marteNauta);
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
