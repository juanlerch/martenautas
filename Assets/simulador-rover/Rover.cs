using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rover : MonoBehaviour {
	public GameObject gCompleto;
	public  bool actividadCompleta = false;
	public int nivel=1;

	public int cantidadARecolectar = 10;
	public int cantidadItemsRecolectados = 0;
	public int puntosXRecolectar = 100;

	void Start () {
		Principal p = gameObject.GetComponent<Principal>();
		p.cargarModelo();
		Principal.nivelActual = nivel;
	}
	bool unavez=false;
	// Update is called once per frame
	void Update () {
//		Debug.Log (unavez);
		if (actividadCompleta && !unavez) {
			unavez= true; 
			LabsPanelCompleta completa = gCompleto.GetComponent<LabsPanelCompleta> ();

			completa.completar (Principal.nombreActSimExplorador, nivel, Principal.juego.simuladorRover.puntajeJuego,
				puntosXRecolectar*cantidadARecolectar+500);
		}
	}
}
