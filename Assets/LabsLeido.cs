using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsLeido : MonoBehaviour {

	public string nombre;

	public bool marcarComoLeidoAlHabilitar= false;

	// Use this for initialization
	void Start () {
		Debug.Log ("Start"); 
	} 



	public void marcarLeido(string nombre2){
		Principal.avance.avanceJugadorActual ().agregarLeido (nombre2);
	}
	
	// Update is called once per frame
	void Update () {
		if (marcarComoLeidoAlHabilitar) {
			if (marcarComoLeidoAlHabilitar) {
				if (nombre != null) {
					Principal.avance.avanceJugadorActual ().agregarLeido (nombre);
				}
			}
		}
		if (gameObject.GetComponent<Image> () != null) {
			if (nombre != null && Principal.avance != null) { 
					Color color = gameObject.GetComponent<Image> ().color;

					if (Principal.avance.avanceJugadorActual ().leido != null && Principal.avance.avanceJugadorActual ().leido.Contains (nombre)) { 
						color.a = 1.0f;
					} else {
						color.a = 0.1f;
					}
					gameObject.GetComponent<Image> ().color = color;
				}
			}
	}
}
