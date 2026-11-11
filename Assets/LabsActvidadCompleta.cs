using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsActvidadCompleta : MonoBehaviour {
	public string nombreActividad;
	//public int nivel;
	// Use this for initialization
	void Start () {
		Text t = GetComponent<Text> ();
		if (Principal.avance == null)
			Principal.inicializar ();
		int nivel = Principal.nivelActual;
		if (nivel == 0) nivel = 1; //parche para el editor de unity
		int puntaje = Principal.avance.avanceJugadorActual ().puntaje (nombreActividad,nivel );
		if (puntaje > 0)
			t.text = "Tu récord: " + puntaje;
		Debug.Log ("Principal.nivelActual:" + Principal.nivelActual);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
