using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aterrizaje : MonoBehaviour {
	public GameObject nave;
	public GameObject gTips;
	public GameObject gMenu;
	public GameObject gPuntos;
	public GameObject gCamara;
	public int nivel=1; //1 y 2
	GameObject n;

	void Start () {
		Principal.nivelActual = nivel;
		gTips.SetActive (true);
		gMenu.SetActive (false);
	}

	// Use this for initialization
	public void iniciarJuego() {
		if(n != null){
			n.SetActive (false);
			Object.Destroy (n);
		}

		n = Instantiate (nave);
		nave.SetActive (false); 
		n.SetActive (true);
		n.transform.SetParent (nave.transform.parent);
		n.GetComponent<nave> ().iniciar ();

		gCamara.GetComponent<camara> ().nave = n;

		Principal.juego.simuladorAterrizaje.puntajeJuego = 0;
		gPuntos.GetComponent<Text>().text = "0 XP";
		//LabsCounter counter = puntos.GetComponent<LabsCounter>();
		//counter.setearValor(0);
	}

}
