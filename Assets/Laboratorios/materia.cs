using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class materia : MonoBehaviour {
	GameObject m;
	int cambioTemperatura; //1, -1 o 0
	int aceleracionTemperatura = 1;

	float temperatura;
	float delay;
	float delayObjetivo;

	float temperaturaMinima;
	float temperaturaMaxima;

	public GameObject gCompleto;
	public GameObject gPuntos;
	public GameObject gMenu;
	public GameObject gJuego;

	public GameObject[] tiposMoleculas;
	public GameObject[] objetivos;
	public GameObject[] checks;

	int nroObjetivo = 0;
	int objetivoCumplido = 0;

	List<GameObject> moleculas = new List<GameObject> ();

	string nombreMolecula;
	int moleculaElegida;
	float puntoFusion;
	float puntoEbullicion;
	string estado; //solido, liquido, gaseoso

	int moleculasRestantes;
	float delayCargarMolecula;

	GameObject slt;

	void Start () {
		Principal p = gameObject.GetComponent<Principal>();
		p.cargarModelo();
		Transform tpuntos = gMenu.transform.Find("MIDDLE Area/Contenedor MENU/Panel/Puntos");
		GameObject puntos = tpuntos.gameObject;
		Text text = puntos.GetComponent<Text>();
		text.text = "" + Principal.juego.laboratorio01Estados.puntajeMaximo ;

		slt = GameObject.Find ("Dropdown-moleculas");

		delay = 0;
		moleculasRestantes = 0;
		delayCargarMolecula = 0f;

		temperaturaMinima = GameObject.Find ("Slider").GetComponent<Slider> ().minValue;
		temperaturaMaxima = GameObject.Find ("Slider").GetComponent<Slider> ().maxValue;

		GameObject.Find ("Dropdown-moleculas").GetComponent<Dropdown> ().value = -1;
		GameObject.Find ("Label").GetComponent<Text> ().text  = "SELECCIONá";


	}


	public void iniciar(){
		gMenu.SetActive (false);
		gCompleto.SetActive (false);
		gJuego.SetActive (true);

		Principal.juego.laboratorio01Estados.puntajeJuego = 0;
		gPuntos.GetComponent<Text> ().text = "0 XP";

		foreach(GameObject t in tiposMoleculas){
			t.SetActive (false);
		}

		foreach (GameObject chk in checks) {
			chk.SetActive (false);
		}


		objetivos [objetivos.Length - 1].SetActive (false);
		objetivos [0].SetActive (true);
		nroObjetivo = 0;

	}


	public void elegirMolecula(){
		
		foreach (GameObject anterior in moleculas) {
			anterior.SetActive (false);
			Object.Destroy (anterior);
		}

		moleculas.Clear ();



		if(slt.GetComponent<Dropdown> ().value >= 0 &&
			slt.GetComponent<Dropdown> ().value < slt.GetComponent<Dropdown> ().options.Count ){
			moleculaElegida = slt.GetComponent<Dropdown> ().value;
			puntoFusion =     tiposMoleculas[moleculaElegida].transform.Find ("temperaturas").GetComponent<temperaturas> ().puntoFusion;
			puntoEbullicion = tiposMoleculas[moleculaElegida].transform.Find ("temperaturas").GetComponent<temperaturas> ().puntoEbullicion;
		
			moleculasRestantes = 75;
		}


	}


	public float getTemperatura(){
		return temperatura;
	}

	public void setCambioTemperatura(int cambio){
		if (slt.GetComponent<Dropdown> ().value >= 0) {
			cambioTemperatura = cambio;
			aceleracionTemperatura = 1;
		}
	}

	public void finalizar(){
		gCompleto.SetActive (true);
		gJuego.SetActive (false);
		LabsPanelCompleta completa= gCompleto.GetComponent<LabsPanelCompleta> ();

		completa.completar (Principal.nombreActLabEstadosMateria, 1, Principal.juego.laboratorio01Estados.puntajeJuego,
			Principal.juego.laboratorio01Estados.puntajeMaximo);

	}

	void Update () {
		//-------------------------------------
		// Subir y bajar temperatura
		//-------------------------------------
		if (cambioTemperatura != 0) {
			if (delay <= 0) {
				temperatura += cambioTemperatura*aceleracionTemperatura;
				if (aceleracionTemperatura < 9) {
					aceleracionTemperatura++;
				}

				if (temperatura < temperaturaMinima) {
					temperatura = temperaturaMinima;
				}
				else if(temperatura > temperaturaMaxima){
					temperatura = temperaturaMaxima;
				}

				delay = 0.1f;
			} else {
				delay -= Time.deltaTime;
			}
			GameObject.Find ("temperatura").GetComponent<Text> ().text = ""+temperatura;
			GameObject.Find ("Slider").GetComponent<Slider> ().value = temperatura;
		}
		//-------------------------------------
		// Cargar moléculas
		//-------------------------------------
		if (moleculasRestantes > 0) {
			if (delayCargarMolecula <= 0) {
				m = tiposMoleculas[moleculaElegida];

				GameObject p;

				for (int i = 0; i < 5; i++) {
					p = Instantiate (m);
					p.GetComponent<Transform> ().position = new Vector3(Random.Range(-6.0f, 6.0f),Random.Range(-6.0f, 0f),0);
					p.SetActive (true);

					moleculas.Add (p);
					moleculasRestantes--;				
				}

				delayCargarMolecula = 0.001f;


			} else {
				delayCargarMolecula -= Time.deltaTime;
			}
		}
		//-------------------------------------
		// Marcar el Estado de la Materia
		//-------------------------------------
		if(gJuego.activeSelf == true){
			if (temperatura <= puntoFusion) {
				estado = "solido";
				GameObject.Find ("Panel bt ESTADO/solido").GetComponent<Toggle> ().isOn = true;	
				GameObject.Find ("Panel bt ESTADO/liquido").GetComponent<Toggle> ().isOn = false;
				GameObject.Find ("Panel bt ESTADO/gaseoso").GetComponent<Toggle> ().isOn = false;

			} else if (temperatura > puntoFusion && temperatura < puntoEbullicion) {
				estado = "liquido";
				GameObject.Find ("Panel bt ESTADO/solido").GetComponent<Toggle> ().isOn = false;	
				GameObject.Find ("Panel bt ESTADO/liquido").GetComponent<Toggle> ().isOn = true;
				GameObject.Find ("Panel bt ESTADO/gaseoso").GetComponent<Toggle> ().isOn = false;

			} else {
				estado = "gaseoso";
				GameObject.Find ("Panel bt ESTADO/solido").GetComponent<Toggle> ().isOn = false;	
				GameObject.Find ("Panel bt ESTADO/liquido").GetComponent<Toggle> ().isOn = false;
				GameObject.Find ("Panel bt ESTADO/gaseoso").GetComponent<Toggle> ().isOn = true;
			}
		}

		//-------------------------------------
		// Chequear si se cumple el objeivo
		//-------------------------------------
		if (objetivoCumplido == 0) {
			if (objetivos [nroObjetivo].transform.Find ("objetivo").GetComponent<objetivo> ().estado == estado &&
			    objetivos [nroObjetivo].transform.Find ("objetivo").GetComponent<objetivo> ().nroMolecula == moleculaElegida) {
				objetivoCumplido = 1;
				checks[nroObjetivo].SetActive (true);
				if (nroObjetivo < objetivos.Length) {
					nroObjetivo++;
				}

				delayObjetivo = 3;

				Principal.juego.laboratorio01Estados.puntajeJuego += Principal.juego.laboratorio01Estados.data.puntoOK;
				Text puntos = gPuntos.GetComponent<Text>();
				LabsCounter counter = puntos.GetComponent<LabsCounter>();
				counter.setearValor(Principal.juego.laboratorio01Estados.puntajeJuego);
				cambioTemperatura = 0;

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
