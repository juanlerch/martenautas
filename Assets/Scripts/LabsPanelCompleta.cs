using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabsPanelCompleta : MonoBehaviour {

	public GameObject gPuntajeJugador;
	public GameObject gPuntajeRecord;
	public GameObject gPuntajeSlider;

    GameObject sonido_logro;

    int resto=0;
	int recordTemp=0;

	// Use this for initialization
	void Start () {
		initialTimeLeft = timeLeft;
        sonido_logro = GameObject.Find("sonido_logro");

    }
	float initialTimeLeft;
	public float timeLeft=0.1f;
    bool completa = false;
	// Update is called once per frame
	void Update () {
        if (resto > 0) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                int incremento = 1;
                if (resto > 10){
                    incremento = 5 ; 
                }
               // Principal.juego.jugador.puntos+= incremento;
                recordTemp += incremento;

                gPuntajeSlider.GetComponent<Slider>().value+= incremento;
                gPuntajeJugador.GetComponent<Text>().text = "" + Principal.avance.avanceJugadorActual().puntajeTotal;
                //gPuntajeRecord.GetComponent<Text> ().text =""+ recordTemp;
                //yield return new WaitForSeconds (1.0f);

                resto -= incremento;
                if (resto == 0 && completa) {
                    GameObject gCompleta = gameObject.transform.Find("MIDDLE Area/Contenedor MENU/Panel PUNTOS/Grupo/Completa/Star").gameObject;
                    gCompleta.GetComponent<Toggle>().isOn = true;
                    gCompleta.GetComponent<Toggle>().gameObject.SetActive(false);

                    gCompleta.GetComponentInChildren<Animator>().Play("star");
                    gCompleta.GetComponent<Toggle>().gameObject.SetActive(true);

                    AudioSource audio = sonido_logro.GetComponent<AudioSource>();
                    audio.Play();

                }
                timeLeft = initialTimeLeft;
            }
                
        }
            //StartCoroutine (HandleIt ());
    }


    public void completar(string actividad, int nivel, int puntajeObtenido, int puntajeMaximo) {

        int record = Principal.avance.puntaje(Principal.user, actividad, nivel);

        completar(puntajeObtenido, record, puntajeMaximo);

        if (puntajeObtenido > record)
        {
            Principal.avance.actualizar(Principal.user, actividad, nivel, puntajeObtenido,puntajeMaximo);
        }


    }

    private void completar(int puntajeObtenido,int record, int puntajeMaximo){
        Principal p = new Principal();
        p.guardarAvance();

        gPuntajeJugador.GetComponent<Text> ().text =""+ Principal.avance.avanceJugadorActual().puntajeTotal;
		gPuntajeSlider.GetComponent<Slider> ().minValue= 0;
		gPuntajeSlider.GetComponent<Slider> ().maxValue= puntajeMaximo;
        Debug.Log("record" + record);
        gPuntajeSlider.GetComponent<Slider>().value = record;

        gameObject.SetActive(true);
        if (puntajeObtenido == puntajeMaximo) completa = true;
		if (puntajeObtenido > record) {
			gPuntajeRecord.GetComponent<Text> ().text = "" + puntajeObtenido + " / " + puntajeMaximo;

			resto = puntajeObtenido - record;
			recordTemp = record;
		} else {
			gPuntajeRecord.GetComponent<Text> ().text = "" + record + " / " + puntajeMaximo;
		
		}
	}
}
