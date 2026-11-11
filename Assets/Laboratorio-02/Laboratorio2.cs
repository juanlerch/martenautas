using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laboratorio2 : MonoBehaviour {
	public GameObject gCompleto;
	public GameObject gPuntos;
	public GameObject gMenu;
	public GameObject gJuego;

	public GameObject[] objetivos;
	public GameObject[] checks;

	int nroObjetivo = 0;
	int objetivoCumplido = 0;

	float delay;
	float delayObjetivo;

	public GameObject modeloProton;
	public GameObject modeloElectron;
	public GameObject modeloNeutron;

	public GameObject tituloElemento;
	public GameObject numeroMasico;
	public GameObject cargaNeta;


	public static int protonesUbicados   = 0;
	public static int electronesUbicados = 0;
	public static int electronesEn11 = 0;
	public static int electronesEn12 = 0;

	public static int neutronesUbicados  = 0;

	string[] elementosTablaPeriodica; 

	GameObject e;

	int cantidadProtones   = 10;
	int cantidadElectrones = 10;
	int cantidadNeutrones  = 10;

	List<GameObject> protones = new List<GameObject> ();
	List<GameObject> electrones = new List<GameObject> ();
	List<GameObject> neutrones = new List<GameObject> ();


	public static List<GameObject> electronesEnOrbita2 = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		Principal p = gameObject.GetComponent<Principal>();
		p.cargarModelo();
		Transform tpuntos = gMenu.transform.Find("MIDDLE Area/Contenedor MENU/Panel/Puntos");
		GameObject puntos = tpuntos.gameObject;
		Text text = puntos.GetComponent<Text>();
		text.text = "" + Principal.juego.laboratorio01Estados.puntajeMaximo ;

		gMenu.SetActive (true);

		// Inicializo el Array con los elementos de la tabla periódica
		elementosTablaPeriodica = new string[11];
		elementosTablaPeriodica [0] = "";
		elementosTablaPeriodica [1] = "HIDRÓGENO";
		elementosTablaPeriodica [2] = "HELIO";
		elementosTablaPeriodica [3] = "LITIO";
		elementosTablaPeriodica [4] = "BERILIO";
		elementosTablaPeriodica [5] = "BORO";
		elementosTablaPeriodica [6] = "CARBONO";
		elementosTablaPeriodica [7] = "NITRÓGENO";
		elementosTablaPeriodica [8] = "OXÍGENO";
		elementosTablaPeriodica [9] = "FLÚOR";
		elementosTablaPeriodica [10] = "NEÓN";

		//Ubico los Protones
		for (int i = 0; i < cantidadProtones; i++) { 
			e = Instantiate (modeloProton);
			e.SetActive (true); 
			e.transform.SetParent (modeloProton.transform.parent);
			e.transform.position = modeloProton.transform.position;

			protones.Add (e);
		}


		//Ubico los Electrones
		for (int i = 0; i < cantidadElectrones; i++) { 
			e = Instantiate (modeloElectron);
			e.SetActive (true); 
			e.transform.SetParent (modeloElectron.transform.parent);
			e.transform.position = modeloElectron.transform.position;

			electrones.Add (e);
		}

		//Ubico los Neutrones
		for (int i = 0; i < cantidadNeutrones; i++) { 
			e = Instantiate (modeloNeutron);
			e.SetActive (true); 
			e.transform.SetParent (modeloNeutron.transform.parent);
			e.transform.position = modeloNeutron.transform.position;

			neutrones.Add (e);
		}

	}

	public void iniciar(){
		gMenu.SetActive (false);
		gCompleto.SetActive (false);
		gJuego.SetActive (true);

		Principal.juego.Laboratorio02Atomo.puntajeJuego = 0;
		gPuntos.GetComponent<Text> ().text = "0 XP";

		foreach (GameObject chk in checks) {
			chk.SetActive (false);
		}

		objetivos [objetivos.Length - 1].SetActive (false);
		objetivos [0].SetActive (true);
		nroObjetivo = 0;
	}

	public void finalizar(){
		gCompleto.SetActive (true);
		gJuego.SetActive (false);
		LabsPanelCompleta completa= gCompleto.GetComponent<LabsPanelCompleta> ();

		completa.completar (Principal.nombreActLabEstrucutraAtomica, 1, Principal.juego.Laboratorio02Atomo.puntajeJuego,
			Principal.juego.Laboratorio02Atomo.puntajeMaximo);

	}

	public void limpiarPantalla(){
		// Limpiar Protones
		foreach (GameObject p in protones) {
			p.transform.position = modeloProton.transform.position;
			p.GetComponent<proton> ().limpiar ();
			protonesUbicados = 0;
		}
	
		// Limpiar Electrones
		foreach (GameObject e in electrones) {
			e.transform.position = modeloElectron.transform.position;
			e.GetComponent<electron> ().limpiar ();
			electronesUbicados = 0;
		}

		electronesEnOrbita2.Clear ();
		electronesEn11 = 0;
		electronesEn12 = 0;

		// Limpiar Neutrones
		foreach (GameObject n in neutrones) {
			n.transform.position = modeloNeutron.transform.position;
			n.GetComponent<neutron> ().limpiar ();
			neutronesUbicados = 0;
		}

	}

	
	// Update is called once per frame
	void Update () {
		tituloElemento.GetComponent<Text> ().text = "" + elementosTablaPeriodica[protonesUbicados];
		numeroMasico.GetComponent<Text> ().text = "" + (protonesUbicados+neutronesUbicados);

		int carga = (protonesUbicados - electronesUbicados);
		if (carga > 0) {
			cargaNeta.GetComponent<Text> ().text = "+";
		} else {
			cargaNeta.GetComponent<Text> ().text = "";
		}

		cargaNeta.GetComponent<Text> ().text += "" + carga;

		//-------------------------------------
		// Chequear si se cumple el objeivo
		//-------------------------------------
		if (objetivoCumplido == 0) {
			if (objetivos [nroObjetivo].transform.Find ("objetivo").GetComponent<objetivoLab2> ().cantidadProtones == protonesUbicados &&
				objetivos [nroObjetivo].transform.Find ("objetivo").GetComponent<objetivoLab2> ().cantidadNeutrones == neutronesUbicados &&
				objetivos [nroObjetivo].transform.Find ("objetivo").GetComponent<objetivoLab2> ().cantidadElectrones == electronesUbicados) {
				objetivoCumplido = 1;
				checks[nroObjetivo].SetActive (true);
				if (nroObjetivo < objetivos.Length) {
					nroObjetivo++;
				}

				delayObjetivo = 3;

				Principal.juego.Laboratorio02Atomo.puntajeJuego += Principal.juego.Laboratorio02Atomo.data.puntoOK;
				Text puntos = gPuntos.GetComponent<Text>();
				LabsCounter counter = puntos.GetComponent<LabsCounter>();
				counter.setearValor(Principal.juego.Laboratorio02Atomo.puntajeJuego);

			}
		} else if (objetivoCumplido == 1) {
			if (delayObjetivo <= 0) {
				objetivos [nroObjetivo-1].SetActive (false);
				objetivos [nroObjetivo].SetActive (true);
				objetivoCumplido = 0;


			} else {
				delayObjetivo -= Time.deltaTime;
			}
	}
}
}