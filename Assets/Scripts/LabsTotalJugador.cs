using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabsTotalJugador : MonoBehaviour {

    public GameObject Gpuntos;
	// Use this for initialization
	void Start () {
        Principal p = gameObject.GetComponent<Principal>();
        p.cargarModelo();

        Text text = Gpuntos.GetComponent<Text>();
        text.text = "" + Principal.avance.avanceJugadorActual().puntajeTotal;
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
