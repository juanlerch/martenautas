using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsStatActividades : MonoBehaviour {

	public bool actividades=false;
	public bool desafios=false;
	public bool simuladores=false;
	public bool archivos=false;
	void Start () {
		if (Principal.avance == null) {
			Principal.inicializar ();
		}

		int count = 0;
		if (actividades) {
			foreach(AvanceActividad a in Principal.avance.avanceJugadorActual().avanceActividad){
				count += a.puntajes.Count;
			}
		}

		if (desafios) {
			List<string> mostrado = Principal.avance.avanceJugadorActual ().mostrado;
			if(mostrado.Contains("desafio1_ok")|| mostrado.Contains("desafio1_star")) count++;
			if(mostrado.Contains("desafio2_ok")|| mostrado.Contains("desafio2_star")) count++;
			if(mostrado.Contains("desafio3_ok")|| mostrado.Contains("desafio3_star")) count++;
			if(mostrado.Contains("desafio4_ok")|| mostrado.Contains("desafio4_star")) count++;
			if(mostrado.Contains("desafio5_ok")|| mostrado.Contains("desafio5_star")) count++;
			if(mostrado.Contains("desafio6_ok")|| mostrado.Contains("desafio6_star")) count++;
			if(mostrado.Contains("desafio7_ok")|| mostrado.Contains("desafio7_star")) count++;
			if(mostrado.Contains("desafio8_ok")|| mostrado.Contains("desafio8_star")) count++;
			if(mostrado.Contains("desafio9_ok")|| mostrado.Contains("desafio9_star")) count++;
		}

		if (simuladores) {
			if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreActSimVuelo) != null)     count++;
			if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreActSimAterrisaje) != null)count++;
			if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreActSimExplorador) != null)count++;
			if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreActSimRaSolar) != null)   count++;
		}

		if (archivos) {
			if(Principal.avance.avanceJugadorActual ().leido!=null) count+=Principal.avance.avanceJugadorActual ().leido.Count ;
		}
		GetComponent<Text> ().text = "" + count;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
