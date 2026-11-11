using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabAvanceDesafios : MonoBehaviour {

   // public int numero_desafio;

    public List<string> actividades;
    public List<int> niveles;
    // Use this for initialization 
    void Start()
    {
		Principal.inicializar();
        bool iniciada = true;
        bool completa = true;
        int count = 0;
        foreach (string actividad in actividades) {

            int nivel = niveles[count];
            int estado = Principal.avance.avanceJugadorActual().estadoActividad(actividad, nivel);
            if (estado == 0) iniciada = false;
            if (estado <= 1) completa = false;
            count++;
            Debug.Log(actividad + ":" + nivel +"->"+ estado) ;
        }

        /*-------------------------------------------------------------------------------------*/
        if (completa)
        {
            //gameObject.transform.Find("star").gameObject.SetActive(true);
			gameObject.transform.Find("star").GetComponent<LabsFade>().fade();
            gameObject.transform.Find("ok").gameObject.SetActive(false);

        }
        else if (iniciada)
        {
            gameObject.transform.Find("star").gameObject.SetActive(false);
			//gameObject.transform.Find("ok").gameObject.SetActive(true);
			gameObject.transform.Find("ok").GetComponent<LabsFade>().fade();

        }
        else {
            gameObject.transform.Find("star").gameObject.SetActive(false);
            gameObject.transform.Find("ok").gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
