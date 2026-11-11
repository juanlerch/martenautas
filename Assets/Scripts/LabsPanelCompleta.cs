using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LabsPanelCompleta : MonoBehaviour {
	

	public GameObject gPuntajeJugador;
	public GameObject gPuntajeRecord;
	public GameObject gPuntajeSlider;

    GameObject sonido_logro;
	GameObject sonido_conteo;
	int sumarAlTotal=0;
    int resto=0;
	//int recordTemp=0;
	bool estrellaMostrada=false;

	float initialTimeLeft;
	public float timeLeft=0.1f;
	bool completa = false;

	// Use this for initialization
	void Start () {
		estrellaMostrada=false;
	//	completa = false;
		initialTimeLeft = timeLeft;
        sonido_logro = GameObject.Find("sonido_logro");
		sonido_conteo=GameObject.Find("sonido_conteo");;
    }
	 
	bool unaVez=false;
	// Update is called once per frame
	void Update () {

		/*mostrar la estrella*/
		if (!estrellaMostrada && resto <= 0 && completa && !unaVez)  {
			
			GameObject gCompleta = gameObject.transform.Find ("MIDDLE Area/Contenedor MENU/Panel PUNTOS/Grupo/Completa/Star").gameObject;
			gCompleta.GetComponent<Toggle> ().isOn = true;
			gCompleta.GetComponent<Toggle> ().gameObject.SetActive (false);

			gCompleta.GetComponentInChildren<Animator> ().Play ("star");
			gCompleta.GetComponent<Toggle> ().gameObject.SetActive (true);

			AudioSource audio = sonido_logro.GetComponent<AudioSource> ();
			audio.Play ();
			estrellaMostrada = true;
			Debug.Log ("mostrando estrella...");

		}

		if(resto<=0 && !unaVez){
			unaVez = true;
			gPuntajeJugador.GetComponent<LabsCounter> ().addValor (sumarAlTotal,1.0);
		}

		if (resto<=0 && sonido_conteo != null) {
			AudioSource audio = sonido_conteo.GetComponent<AudioSource> ();
			if (audio != null) {
				audio.Pause ();
				audio.Stop ();
			} 
		}


		if (resto > 0) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {

				if (sonido_conteo != null) {
					AudioSource audio = sonido_conteo.GetComponent<AudioSource> ();
					if (audio != null)
					    if (!audio.isPlaying)
						          audio.Play ();
				}

				int incremento = 1;

				if (resto > 10) {
					incremento = 5;
				}
				if (resto > 500) {
						incremento = 10; 
				}
				// Principal.juego.jugador.puntos+= incremento;
				//	recordTemp += incremento;

				//gPuntajeSlider.GetComponent<Slider> ().value += incremento;
				//  gPuntajeJugador.GetComponent<Text>().text = "" + Principal.avance.avanceJugadorActual().puntajeTotal;
				//gPuntajeRecord.GetComponent<Text> ().text =""+ recordTemp;
				//yield return new WaitForSeconds (1.0f);
				gPuntajeSlider.GetComponent<Slider> ().value += incremento;
				resto -= incremento;

				timeLeft = initialTimeLeft;
			}
                
		}



    }


    public void completar(string actividad, int nivel, int puntajeObtenido, int puntajeMaximo) {
		Principal.avance.avanceJugadorActual ().estadistica(actividad, nivel).Finalizar ();
        int record = Principal.avance.puntaje(Principal.user, actividad, nivel);

        completar2(nivel,puntajeObtenido, record, puntajeMaximo);

        if (puntajeObtenido > record)
        {
            Principal.avance.actualizar(Principal.user, actividad, nivel, puntajeObtenido,puntajeMaximo);
        }


    }

	private void completar2(int nivel, int puntajeObtenido,int record, int puntajeMaximo){
		estrellaMostrada = false;
        Principal p = new Principal();
        p.guardarAvance(); 

		gPuntajeJugador.GetComponent<Text> ().text =""+ (Principal.avance.avanceJugadorActual().puntajeTotal);

		if (gPuntajeJugador.GetComponent<LabsCounter> () == null) {
			
			LabsCounter counter =gPuntajeJugador.AddComponent<LabsCounter>() as LabsCounter;
			counter.sufijo = null; 
		}
		gPuntajeJugador.GetComponent<LabsCounter> ().inicializar();
		gPuntajeJugador.GetComponent<LabsCounter> ().setearValor(Principal.avance.avanceJugadorActual().puntajeTotal);

		if (puntajeObtenido > record) {
			sumarAlTotal = puntajeObtenido - record;
		} else {
			sumarAlTotal = 0;
		}
		gPuntajeSlider.GetComponent<Slider> ().minValue= 0;
		gPuntajeSlider.GetComponent<Slider> ().maxValue= puntajeMaximo;
		gPuntajeSlider.GetComponent<Slider>().value = 0;


		gameObject.SetActive(true);


		GameObject tuRecord = GameObject.Find ("MIDDLE Area/Contenedor MENU/Panel PUNTOS/Grupo/Record");
		if (tuRecord) {
			if (puntajeObtenido > record) tuRecord.GetComponent<Text> ().text = "¡Nuevo récord!";
			else tuRecord.GetComponent<Text> ().text= "Tu récord:" + record;
		}		


		if(gameObject.transform.Find("MIDDLE Area/Contenedor MENU/Panel PUNTOS/Grupo/Icono/Nivel")!=null){
			GameObject gNivel= gameObject.transform.Find("MIDDLE Area/Contenedor MENU/Panel PUNTOS/Grupo/Icono/Nivel").gameObject;
			gNivel.GetComponent<Text> ().text = "" + nivel;
		}
        if (puntajeObtenido >= puntajeMaximo) completa = true;
		//if (puntajeObtenido > record) {
			gPuntajeRecord.GetComponent<Text> ().text = "" + puntajeObtenido + " / " + puntajeMaximo;

			resto = puntajeObtenido ;
			//recordTemp = 0;
		/*} else {
			gPuntajeRecord.GetComponent<Text> ().text = "" + record + " / " + puntajeMaximo;
		
		}*/
	}
		

}
