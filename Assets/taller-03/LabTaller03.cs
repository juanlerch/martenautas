using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using B83.ExpressionParser;

public class LabTaller03 : MonoBehaviour {
	public GameObject gCompleto;
	public GameObject gMenu;
	public GameObject gJuego;
	public GameObject gPuntos;

	public GameObject planteoIzquierda;
	public GameObject planteoDerecha;
	public GameObject modeloTermino;

	public List<GameObject> resolucionIzquierda;
	public List<GameObject> resolucionDerecha;

	public GameObject panelResolucion;
	public GameObject btNuevaEcuacion;
	public GameObject titTerminos;


	public GameObject gNivelMenu;
	public GameObject gNivelJuego;
	public GameObject gNivelCompleta;

	public GameObject HayOtraSolucion;

	List<GameObject> opciones = new List<GameObject> ();
	List<GameObject> planteos = new List<GameObject> ();

	Taller03Ecuaciones ecuaciones;

	public static int nivel=0;

	int nroEcuacion;
	int nroPaso;
	int cantidadEcuaciones;
	int cantidadPasos;
	int posPlanteo;

	float altoPlanteo;
	float altoResolucion;
	Vector3 posicionInicialResolucion;

	string strResultado;

	void Start () {
		Principal p = gameObject.GetComponent<Principal>();
		Principal.nivelActual = nivel;
		p.cargarModelo();
		Transform tpuntos = gMenu.transform.Find("MIDDLE Area/Contenedor MENU/Panel/Puntos");

		gNivelMenu.GetComponent<Text>().text = "" + (nivel + 1);
		gNivelJuego.GetComponent<Text>().text = "" + (nivel + 1);
		gNivelCompleta.GetComponent<Text>().text = "" + (nivel + 1);

		gameObject.GetComponent<LabsInfoXNivel> ().setNivel(nivel + 1);

		GameObject puntos = tpuntos.gameObject;
		Text text = puntos.GetComponent<Text>();
		text.text = "" + Principal.juego.taller03Ecuaciones[nivel].puntajeMaximo ;

		ecuaciones = Principal.juego.taller03Ecuaciones[nivel];

		altoPlanteo = planteoIzquierda.GetComponent<RectTransform> ().rect.height;


		altoResolucion = panelResolucion.GetComponent<RectTransform> ().rect.height - 10f;

		posicionInicialResolucion = panelResolucion.GetComponent<RectTransform> ().position;
        

		nroEcuacion = 0;

		cantidadEcuaciones = ecuaciones.data.Length;

		gJuego.SetActive (false);
		gCompleto.SetActive (false);

	}


	//Cargar una ecuación
	public void cargarEcuacion(int i){


		titTerminos.SetActive (true);

		if (i < cantidadEcuaciones) {
			gMenu.SetActive (false);
			gCompleto.SetActive (false);
			gJuego.SetActive (true);

			if (i == 0) {
				Principal.juego.taller03Ecuaciones[nivel].puntajeJuego = 0;
				gPuntos.GetComponent<Text> ().text = "0 XP";
				nroEcuacion = 0;
			}

			//Debug.Log(ecuaciones.data[0]);

			nroPaso = 0;
			posPlanteo = 1;
			cantidadPasos = ecuaciones.data [i].validosIzquierda.Length;
			panelResolucion.SetActive (true);

			btNuevaEcuacion.SetActive (false);

			//limpiar los planteos anteriores
			foreach (GameObject anterior in planteos) {
				anterior.SetActive (false);
				Object.Destroy (anterior);
			}
			planteos.Clear ();

			panelResolucion.GetComponent<RectTransform> ().position = posicionInicialResolucion;

			cargarPaso (i, nroPaso, ecuaciones.data [i].planteoIzquierda, ecuaciones.data [i].planteoDerecha);
		
		} else {
			LabsPanelCompleta completa= gCompleto.GetComponent<LabsPanelCompleta> ();

			completa.completar (Principal.nombreActEcuaciones, nivel+1 ,Principal.juego.taller03Ecuaciones[nivel].puntajeJuego,
				Principal.juego.taller03Ecuaciones[nivel].puntajeMaximo);

		}

	}

	//Cargar el paso en el ejercicion de la ecuación
	void cargarPaso(int ecuacion, int paso, string planteoIzquierda, string planteoDerecha){
		GameObject planteo = GameObject.Find("Panel-planteo");
		GameObject p;

		resolucionIzquierda = new List<GameObject> ();
		resolucionDerecha   = new List<GameObject> ();

		//Si es el primer paso uso el Gameobject sinó lo copio
		if (paso == 0) {
			p = planteo;
		} else {
			p = Instantiate (planteo);
			p.SetActive (true); 
			p.transform.SetParent (modeloTermino.transform.parent);

			RectTransform pRt = p.GetComponent<RectTransform> ();
			RectTransform planteoRt = planteo.GetComponent<RectTransform> ();
			pRt.position = planteoRt.position + new Vector3(0,-paso*altoPlanteo,0);

			planteos.Add (p);
		}

		//Planteos
		p.transform.Find ("Text-planteo-izquierda").GetComponent<Text> ().text = planteoIzquierda;
		p.transform.Find ("Text-planteo-derecha").GetComponent<Text> ().text = planteoDerecha;

		GameObject.Find("Text-Resultado").GetComponent<Text> ().text = "";

		//Términos de la ecuación
		string[] terminos = new string[ecuaciones.data[ecuacion].validosIzquierda[paso].Length + ecuaciones.data[ecuacion].validosDerecha[paso].Length+ecuaciones.data[ecuacion].noValidos[paso].Length];
		int j = 0;

		for(int k = 0; k < ecuaciones.data[ecuacion].validosIzquierda[paso].Length; k++ ){
			terminos [j] = ecuaciones.data [ecuacion].validosIzquierda [paso] [k];
			j++;
		}

		for(int k = 0; k < ecuaciones.data[ecuacion].validosDerecha[paso].Length; k++ ){
			terminos [j] = ecuaciones.data [ecuacion].validosDerecha[paso] [k];
			j++;
		}

		for(int k = 0; k < ecuaciones.data[ecuacion].noValidos[paso].Length; k++ ){
			terminos [j] = ecuaciones.data [ecuacion].noValidos [paso] [k];
			j++;
		}

		//limpiar las opciones anteriores
		foreach(GameObject anterior in opciones){
			anterior.SetActive (false);
			Object.Destroy (anterior);
		}
		opciones.Clear();

		//Desordenar los términos
		for (int t = 0; t < terminos.Length; t++ ){
			string tmp = terminos[t];
			int r = Random.Range(t, terminos.Length);
			terminos[t] = terminos[r];
			terminos[r] = tmp;
		}

		// Para indicar la posición en la pantalla
		float anchoTermino = modeloTermino.GetComponent<RectTransform> ().rect.width /**1.5f*/;
		Vector3 posicionTermino = GameObject.Find("Panel-opciones").transform.position - new Vector3((((float)(terminos.Length-1)/2)*(anchoTermino)),0f,0f);


		// Crear los objetos para los términos de la ecuación
		foreach (string termino in terminos)
		{
			GameObject o = Instantiate (modeloTermino);
			opciones.Add (o);
			o.SetActive (true); 
			o.transform.SetParent (modeloTermino.transform.parent);

			o.GetComponent<RectTransform> ().position = posicionTermino;
			posicionTermino += new Vector3(anchoTermino, 0, 0);

			o.GetComponent<Text>().text = termino;
	
		}
		modeloTermino.SetActive (false);

	}

	//Fin de la Ecuacion
	void finEcuacion (string resultadoIzquierda, string resultadoDerecha){
		GameObject planteo = GameObject.Find("Panel-planteo");
		GameObject p;

		p = Instantiate (planteo);
		p.SetActive (true); 
		p.transform.SetParent (modeloTermino.transform.parent);

		RectTransform pRt = p.GetComponent<RectTransform> ();
		RectTransform planteoRt = planteo.GetComponent<RectTransform> ();
		pRt.position = planteoRt.position + new Vector3(0,-cantidadPasos*altoPlanteo,0);

		planteos.Add (p);

		//limpiar las opciones anteriores
		foreach(GameObject anterior in opciones){
			anterior.SetActive (false);
			Object.Destroy (anterior);
		}
		opciones.Clear();

		//Planteos
		p.transform.Find ("Text-planteo-izquierda").GetComponent<Text> ().text = resultadoIzquierda;
		p.transform.Find ("Text-planteo-derecha").GetComponent<Text> ().text = resultadoDerecha;
		panelResolucion.SetActive (false);

		GameObject.Find("Text-Resultado").GetComponent<Text> ().text = "¡MUY bIeN!";

		if (cantidadEcuaciones > nroEcuacion + 1) {
			btNuevaEcuacion.SetActive (true);
		} else {
			btNuevaEcuacion.SetActive (true);
			titTerminos.SetActive (false);

		}


		Principal.juego.taller03Ecuaciones[nivel].puntajeJuego += Principal.juego.taller03Ecuaciones[nivel].data[nroEcuacion].puntoOK;
		//gPuntos.GetComponent<Text> ().text = "" + Principal.juego.taller03Ecuaciones[nivel].puntajeJuego + " XP";
		Text puntos = gPuntos.GetComponent<Text>();
		LabsCounter counter = puntos.GetComponent<LabsCounter>();
		counter.setearValor(Principal.juego.taller03Ecuaciones[nivel].puntajeJuego);


	}

	//Corregir la ecuación
	public void corregir(){
		string valor;

		int aux;
		string resultado = "OK";

		string txtPlanteoIzquierda = "";
		string txtPlanteoDerecha = "";

		string txtResolucionIzquierda = "";
		string txtResolucionDerecha = "";

		string ubicacion;


		foreach (GameObject o in opciones) {
			ubicacion = o.GetComponent<LabTaller03Terminos> ().getUbicacion ();
			valor = o.GetComponent<Text> ().text;

			if (ubicacion == "I") {
				aux = System.Array.IndexOf (ecuaciones.data [nroEcuacion].validosIzquierda [nroPaso], valor);
				if (aux == -1) {
					resultado = "ERROR";
				}
			}
			else if(ubicacion == "D"){
				aux = System.Array.IndexOf (ecuaciones.data [nroEcuacion].validosDerecha [nroPaso], valor);
				if (aux == -1) {
					resultado = "ERROR";
				}
			}
			else if(ubicacion == "A"){
				aux = System.Array.IndexOf (ecuaciones.data [nroEcuacion].noValidos [nroPaso], valor);
				if (aux == -1) {
					resultado = "ERROR";
				}
			}
		
		}

	


			foreach (GameObject ri in resolucionIzquierda) {
				txtPlanteoIzquierda += ri.GetComponent<Text> ().text;

				if (ri.GetComponent<Text> ().text.IndexOf ("x") != -1) {
					if (ecuaciones.data [nroEcuacion].resultado >= 0.00f) {
						strResultado = "" + ecuaciones.data [nroEcuacion].resultado;
					} else {
						strResultado = "(" + ecuaciones.data [nroEcuacion].resultado + ")";
					}
					//	if (ri.GetComponent<Text> ().text == "x") {
					if (ri.GetComponent<Text> ().text == "x" || ri.GetComponent<Text> ().text.Contains ("+") || ri.GetComponent<Text> ().text.Contains ("-")) {
						txtResolucionIzquierda += ri.GetComponent<Text> ().text.Replace ("x", " " + strResultado + "");
					} else {
						txtResolucionIzquierda += ri.GetComponent<Text> ().text.Replace ("x", " * " + strResultado + "");
					}
				} else {
					txtResolucionIzquierda += ri.GetComponent<Text> ().text;
				}


			}

			foreach (GameObject rd in resolucionDerecha) {
				txtPlanteoDerecha += rd.GetComponent<Text> ().text;

				if (rd.GetComponent<Text> ().text.IndexOf ("x") != -1) {
					if (ecuaciones.data [nroEcuacion].resultado >= 0.00f) {
						strResultado = "" + ecuaciones.data [nroEcuacion].resultado;
					} else {
						strResultado = "(" + ecuaciones.data [nroEcuacion].resultado + ")";
					}


					//if (rd.GetComponent<Text> ().text == "x") {
					if (rd.GetComponent<Text> ().text == "x" || rd.GetComponent<Text> ().text.Contains ("+") || rd.GetComponent<Text> ().text.Contains ("-")) {
						txtResolucionDerecha += rd.GetComponent<Text> ().text.Replace ("x", " " + strResultado);
					} else {
						txtResolucionDerecha += rd.GetComponent<Text> ().text.Replace ("x", " * " + strResultado);
					}
				} else {
					txtResolucionDerecha += rd.GetComponent<Text> ().text;
				}
			}



			txtResolucionIzquierda = txtResolucionIzquierda.Replace (".", "*");
			txtResolucionDerecha = txtResolucionDerecha.Replace (".", "*");

			txtResolucionIzquierda = txtResolucionIzquierda.Replace (":", "/");
			txtResolucionDerecha = txtResolucionDerecha.Replace (":", "/");

			//Debug.Log (txtPlanteoIzquierda + " = " + txtPlanteoDerecha);
			//Debug.Log (txtResolucionIzquierda + " = " + txtResolucionDerecha);

		if (resultado == "OK") {

			//-------------------------------------------------------------------
			//Parser
			//-------------------------------------------------------------------
			var parser = new ExpressionParser ();

			Expression expIzquierda = parser.EvaluateExpression (txtResolucionIzquierda);
			Expression expDerecha = parser.EvaluateExpression (txtResolucionDerecha);

			//Debug.Log (expIzquierda.Value + " = " + expDerecha.Value);

			if (expIzquierda.Value == expDerecha.Value) {
				panelResolucion.GetComponent<RectTransform> ().position = panelResolucion.GetComponent<RectTransform> ().position + new Vector3 (0, -1 * altoResolucion, 0);
				nroPaso++;

				if (nroPaso < cantidadPasos) {
					cargarPaso (nroEcuacion, nroPaso, txtPlanteoIzquierda, txtPlanteoDerecha);

				} else {
					resultado = "FIN!";
					titTerminos.SetActive (false);
					finEcuacion (txtPlanteoIzquierda, txtPlanteoDerecha);
				}
			
			}

		} else {
			//-------------------------------------------------------------------
			//Parser
			//-------------------------------------------------------------------
			var parser2 = new ExpressionParser ();
			//Debug.Log (txtResolucionIzquierda + " = " + txtResolucionDerecha);
			Expression expIzquierda = parser2.EvaluateExpression (txtResolucionIzquierda);
			Expression expDerecha = parser2.EvaluateExpression (txtResolucionDerecha);

			//Debug.Log (expIzquierda.Value + " = " + expDerecha.Value);

			if (expIzquierda.Value == expDerecha.Value) {
				HayOtraSolucion.SetActive (true);
				//Debug.Log ("hay otra solución");
			}
		}
	}

	public void nuevaEcuacion(){
		nroEcuacion++;
		cargarEcuacion (nroEcuacion);

	}

	// Update is called once per frame
	void Update () {
		
	}
}
