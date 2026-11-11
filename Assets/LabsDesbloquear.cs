using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsDesbloquear : MonoBehaviour {

	public string logro;
	// Use this for initialization
	void Start () {
		if (logro != null) {
			Principal.inicializar ();
			AvanceJugador aj = Principal.avance.avanceJugadorActual ();
			AvanceLogro alogro = aj.obtenerLogro (logro);
			if (alogro!=null && alogro.nombre == logro) {
				Button b = gameObject.GetComponent<Button> ();
				if (b != null)
					b.interactable = true;

				Transform locked = gameObject.transform.Find ("locked");
				Animator a = locked.gameObject.GetComponent<Animator> ();

				if (aj.esDesbloqueado (logro)) {
					locked.gameObject.SetActive (false);
				} else {
					if (a != null)
						a.enabled = true;
					aj.desbloquear (logro);				
					GameObject sonido_logro = GameObject.Find("sonido_logro");
					AudioSource audio = sonido_logro.GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}	
	}
	

}
