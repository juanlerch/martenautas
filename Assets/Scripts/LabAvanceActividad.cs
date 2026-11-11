using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabAvanceActividad : MonoBehaviour {

    public string actividad;
    public int nivel;
	// Use this for initialization
	void Start () {
		//if (nivel == 0) nivel = 1;
		if(Principal.avance==null) Principal.inicializar();
        int estado = Principal.avance.avanceJugadorActual().estadoActividad(actividad, nivel) ;

        if (estado== 0)
        {
            //gameObject.transform.Find("Nivel").gameObject.SetActive(true);
            gameObject.transform.Find("ok").gameObject.SetActive(false);
            gameObject.transform.Find("star").gameObject.SetActive(false);
        }
        else if (estado==1)
        {
            //  gameObject.transform.Find("Nivel").gameObject.SetActive(false);
            //gameObject.transform.Find("ok").gameObject.SetActive(true);
            gameObject.transform.Find("ok").GetComponent<LabsFade>().fade();
            gameObject.transform.Find("star").gameObject.SetActive(false);

        }
        else {
            // gameObject.transform.Find("Nivel").gameObject.SetActive(false);
            gameObject.transform.Find("ok").gameObject.SetActive(false);
            //gameObject.transform.Find("star").gameObject.SetActive(true);
            gameObject.transform.Find("star").GetComponent<LabsFade>().fade();

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
