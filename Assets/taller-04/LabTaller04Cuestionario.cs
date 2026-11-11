using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LabTaller04Cuestionario : MonoBehaviour {
	public GameObject gCompleto;
	public GameObject gPuntos;
	public GameObject gMenu;
	public GameObject gJuego;

	public GameObject gNivelMenu;
	public GameObject gNivelJuego;
	public GameObject gNivelCompleta;

	public static int nivel=0;

	int nroPregunta = 0;
	string opcionCorrecta;
	int debeCorregir; // Si es 0, no hay que corregir. Si es 1, hay que corregir. 

	float contadorSegundos;
	int siguientePregunta;


	public void cargarPregunta(int nroPreg){
		//Saco el foco de cualquiera de los botones
		EventSystem.current.SetSelectedGameObject(null);


		debeCorregir = 0;
		siguientePregunta = 0;
		contadorSegundos = 0;

		gMenu.SetActive (false);
		gJuego.SetActive (true);
		gCompleto.SetActive (false);

		if (nroPreg == 0) {
			Principal.juego.taller04Cuestionarios[nivel].puntajeJuego = 0;
			nroPregunta = 0;
		}

		//gPuntos.GetComponent<Text> ().text = "" + Principal.juego.taller04Cuestionarios[nivel].puntajeJuego + " XP";

		GameObject.Find("Tit Tema").GetComponent<Text> ().text = Principal.juego.taller04Cuestionarios[nivel].data.titulo;
		GameObject.Find("Pregunta").GetComponent<Text> ().text = Principal.juego.taller04Cuestionarios[nivel].data.preguntas[nroPreg].enunciado;

		List<Respuesta> opciones = Principal.juego.taller04Cuestionarios[nivel].data.preguntas [nroPreg].respuestas;

		//Desordenar las opciones
		for (int t = 0; t < opciones.Count; t++ ){
			Respuesta tmp = opciones[t];
			int r = Random.Range(t, opciones.Count);
			opciones[t] = opciones[r];
			opciones[r] = tmp;
		}
		//------------------------------------

		GameObject.Find ("Panel-opciones/A").GetComponent<Toggle> ().isOn = false;
		GameObject.Find("Panel-opciones/A/Label").GetComponent<Text> ().text = opciones[0].respuesta;
		if (opciones [0].correcta == 1) {
			opcionCorrecta = "A";
			GameObject.Find ("Panel-opciones/A/Background/Checkmark").GetComponent<Image> ().color = new Color (34f/255f, 152f/255f, 138f/255f,1f);

		} else {
			GameObject.Find ("Panel-opciones/A/Background/Checkmark").GetComponent<Image> ().color = new Color (229f/255f, 111f/255f, 111f/255f,1f);

		}

		GameObject.Find ("Panel-opciones/B").GetComponent<Toggle> ().isOn = false;
		GameObject.Find("Panel-opciones/B/Label").GetComponent<Text> ().text = opciones[1].respuesta;
		if (opciones [1].correcta == 1) {
			opcionCorrecta = "B";
			GameObject.Find ("Panel-opciones/B/Background/Checkmark").GetComponent<Image> ().color = new Color (34f/255f, 152f/255f, 138f/255f,1f);

		} else {
			GameObject.Find ("Panel-opciones/B/Background/Checkmark").GetComponent<Image> ().color = new Color (229f/255f, 111f/255f, 111f/255f,1f);

		}

		GameObject.Find ("Panel-opciones/C").GetComponent<Toggle> ().isOn = false;
		GameObject.Find("Panel-opciones/C/Label").GetComponent<Text> ().text = opciones[2].respuesta;
		if (opciones [2].correcta == 1) {
			opcionCorrecta = "C";
			GameObject.Find ("Panel-opciones/C/Background/Checkmark").GetComponent<Image> ().color = new Color (34f/255f, 152f/255f, 138f/255f,1f);

		} else {
			GameObject.Find ("Panel-opciones/C/Background/Checkmark").GetComponent<Image> ().color = new Color (229f/255f, 111f/255f, 111f/255f,1f);

		}

		GameObject.Find ("Panel-opciones/A").GetComponent<Toggle> ().enabled = true;
		GameObject.Find ("Panel-opciones/B").GetComponent<Toggle> ().enabled = true;
		GameObject.Find ("Panel-opciones/C").GetComponent<Toggle> ().enabled = true;

		debeCorregir = 1;

	}

	IEnumerator esperaryFinalizar()/*   StartCoroutine(esperaryFinalizar());*/
	{

		yield return new WaitForSeconds(2);
		LabsPanelCompleta completa= gCompleto.GetComponent<LabsPanelCompleta> ();

		completa.completar (Principal.nombreActCuestionarios, nivel+1, Principal.juego.taller04Cuestionarios[nivel].puntajeJuego,
			Principal.juego.taller04Cuestionarios[nivel].puntajeMaximo);

	}

	public void corregir(string opcion){
		if (debeCorregir == 1) {
			if (opcionCorrecta == opcion) {
				Principal.juego.taller04Cuestionarios[nivel].puntajeJuego += Principal.juego.taller04Cuestionarios[nivel].data.puntoOK;
				//gPuntos.GetComponent<Text> ().text = "" + Principal.juego.taller04Cuestionarios[nivel].puntajeJuego + " XP";
				Text puntos = gPuntos.GetComponent<Text>();
				LabsCounter counter = puntos.GetComponent<LabsCounter>();
				counter.setearValor(Principal.juego.taller04Cuestionarios[nivel].puntajeJuego);

				GameObject.Find ("Panel-opciones/A").GetComponent<Toggle> ().enabled = false;
				GameObject.Find ("Panel-opciones/B").GetComponent<Toggle> ().enabled = false;
				GameObject.Find ("Panel-opciones/C").GetComponent<Toggle> ().enabled = false;

				nroPregunta++;
				if (nroPregunta < Principal.juego.taller04Cuestionarios[nivel].data.preguntas.Count) {
					siguientePregunta = 1;

				} else {
					StartCoroutine (esperaryFinalizar ());


				}

			} else {
				GameObject.Find ("Panel-opciones/"+opcion).GetComponent<Toggle> ().enabled = false;
				Principal.juego.taller04Cuestionarios[nivel].puntajeJuego += Principal.juego.taller04Cuestionarios[nivel].data.puntoERR;

				Text puntos = gPuntos.GetComponent<Text>();
				LabsCounter counter = puntos.GetComponent<LabsCounter>();
				counter.setearValor(Principal.juego.taller04Cuestionarios[nivel].puntajeJuego);

			}
		}

	}

	// Use this for initialization
	void Start () {
		gNivelMenu.GetComponent<Text>().text = "" + (nivel + 1);
		gNivelJuego.GetComponent<Text>().text = "" + (nivel + 1);
		gNivelCompleta.GetComponent<Text>().text = "" + (nivel + 1);

		Principal p = gameObject.GetComponent<Principal>();
		p.cargarModelo();
		Transform tpuntos = gMenu.transform.Find("MIDDLE Area/Contenedor MENU/Panel/Puntos");
		GameObject puntos = tpuntos.gameObject;
		Text text = puntos.GetComponent<Text>();
		text.text = "" + Principal.juego.taller04Cuestionarios[nivel].puntajeMaximo ;

		gMenu.SetActive (true);
		gJuego.SetActive (false);

	}
	
	void Update () {
		if (siguientePregunta == 1) {
			if (contadorSegundos > 2) {
				cargarPregunta (nroPregunta);
			} else {
				contadorSegundos += Time.deltaTime;
			}
		}
	}
}
