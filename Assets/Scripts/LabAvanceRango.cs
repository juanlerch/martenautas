using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabAvanceRango : MonoBehaviour {
	public static string anteriorRango=Principal.nombreRango1Geonauta;
	public static string nuevoRango=null;
	public static string nuevaInsignia=null;
	public static string nuevoSimulador=null;

	public static int count = 0;//para el slider de logros 

	public static string toEscene;

	private int sec = 1; 

	public GameObject ico_Geonauta;
	public GameObject ico_Aeronauta;
	public GameObject ico_Cosmonauta;
	public GameObject ico_Martenauta;

	public GameObject panel_insg_enteros_on;
	public GameObject panel_insg_primos_on;
	public GameObject panel_insg_cuestionarios_on;
	public GameObject panel_insg_ecuaciones_on;
	public GameObject panel_insg_laboratorios_on;

	public GameObject text_simulador;
	public GameObject text_insignia;
	public GameObject text_rango;

	public GameObject panelNuevoRango;
	public GameObject panelNuevaInsignia;
	public GameObject panelNuevoSimulador;

	IEnumerator esperarYMostrarNuevoSimulador()
	{
		yield return new WaitForSeconds(sec); 
		if (nuevoSimulador != null) {
			if(text_simulador!=null) text_simulador.GetComponent<Text> ().text = "["+ Principal.Pretty(nuevoSimulador) + "]";
			panelNuevoSimulador.SetActive (true);
			nuevoSimulador = null; 
		}
		if (nuevaInsignia != null)
			StartCoroutine (esperarYMostrarNuevoRango ());
		else {
			StartCoroutine (esperarYSalir());
		}
	}

	void MostrarNuevoSimulador()
	{
		if (nuevoSimulador != null) {
			if(text_simulador!=null) text_simulador.GetComponent<Text> ().text ="["+   Principal.Pretty(nuevoSimulador) + "]" ;
			panelNuevoSimulador.SetActive (true);
			nuevoSimulador = null; 
		}
	}

	IEnumerator esperarYSalir()
	{
		yield return new WaitForSeconds(sec);
		salir ();
	}
    

	void MostrarInsginia()
	{
		panelNuevoSimulador.SetActive (false);
		if (nuevaInsignia != null) {
			text_insignia.GetComponent<Text>().text = "["+Principal.Pretty(nuevaInsignia)+"]";
			panelNuevaInsignia.SetActive (true);
		}
		else {
			if (nuevoRango != null) {
				mostrarNuevoRango ();
			} else {
				salir ();
			}
		}

	}

	void MostrarRango()
	{   panelNuevaInsignia.SetActive (false);
		panelNuevoSimulador.SetActive (false);
		if (nuevoRango != null) {
			text_rango.GetComponent<Text> ().text = "[" + nuevoRango.ToUpper () + "]";
			panelNuevoRango.SetActive (true);
		}
		else
			salir ();

	}
	void MostrarScenaLogros(){
		Principal p = new Principal();
		p.cargarEscena ("Logros");
	}

	IEnumerator esperarYMostrarNuevoRango()
	{
		yield return new WaitForSeconds(sec);
		if (nuevoRango != null) {
			text_rango.GetComponent<Text> ().text = "[" + nuevoRango.ToUpper () + "]";
			panelNuevoRango.SetActive (true);
		}
		StartCoroutine (esperarYMostrarScenaLogros ());
	}

	IEnumerator esperarYMostrarScenaLogros()/*   StartCoroutine(esperar);*/
	{
		yield return new WaitForSeconds(sec);
		Principal p = new Principal();
		p.cargarEscena ("Logros");
	}

	void mostrarNuevoRango(){
		if (nuevoRango!=null) {
			text_rango.GetComponent<Text> ().text = "[" + nuevoRango.ToUpper () + "]";
			panelNuevoRango.SetActive (true);
		}
	}

	void desbloquearCandados (string name){
		GameObject o = GameObject.Find (name);
		GameObject l = o.transform.Find("locked").gameObject;
		Animator a = l.GetComponent<Animator> ();

		Button b = o.GetComponent<Button> ();
		if (b != null)
			b.interactable = true;

		if (Principal.avance.avanceJugadorActual ().esDesbloqueado(name)) {
			l.SetActive (false);
		} 
		else{
			a.enabled = true; 
			Principal.avance.avanceJugadorActual ().desbloquear(name);

			GameObject sonido_logro = GameObject.Find("sonido_logro");
			AudioSource audio = sonido_logro.GetComponent<AudioSource>();
			audio.Play();

		}
	}



	public void continuarButton(){
		if (panelNuevoSimulador.activeSelf) {
			MostrarInsginia ();
		} else if (panelNuevaInsignia.activeSelf) {
			if (nuevoRango != null) {
				MostrarRango ();
			} else{
				MostrarScenaLogros();	
			}
		} else if (panelNuevoRango.activeSelf) {
			MostrarScenaLogros();
		}
	}

			
    void Start() 
	{
		Principal.inicializar ();
		Principal p = new Principal();
//		Debug.Log (Principal.avance.avanceJugadorActual ().nivel);
		if (Principal.avance.avanceJugadorActual ().nivel == null) {
			Principal.avance.avanceJugadorActual ().nivel = Principal.nombreRango1Geonauta;
		}

		anteriorRango = Principal.avance.avanceJugadorActual ().nivel;

		if (p.getActiveSceneName () == "Msj_InsigniaNivel") {  
			if (nuevoSimulador != null) {
				panelNuevoSimulador.SetActive (true);
				//StartCoroutine (esperarYMostrarNuevoSimulador ());
				MostrarNuevoSimulador();
			} 
			if (nuevaInsignia != null) {
				text_insignia.GetComponent<Text>().text = "["+Principal.Pretty(nuevaInsignia)+"]";
				panelNuevaInsignia.SetActive (true);
				//StartCoroutine (esperarYMostrarNuevoRango ());
			} else if (nuevoRango!=null) {
				text_rango.GetComponent<Text> ().text = "[" + nuevoRango.ToUpper () + "]";
				panelNuevoRango.SetActive (true);
				//StartCoroutine (esperarYMostrarScenaLogros());
			}
		} 
		else if (p.getActiveSceneName() == "Entrenar") {
            StartCoroutine(esperarYMostrarLogroObtenido(sec));
            if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango2Aeronauta) {
				desbloquearCandados("d_04");		
				desbloquearCandados("d_05");		
				desbloquearCandados("d_06");		
			} else if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango3Cosmonauta) {
				desbloquearCandados("d_04");		
				desbloquearCandados("d_05");		
				desbloquearCandados("d_06");		
				desbloquearCandados("d_07");		
				desbloquearCandados("d_08");		
				desbloquearCandados("d_09");		
			}
			else if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango4Martenauta) {
				desbloquearCandados("d_04");		
				desbloquearCandados("d_05");		
				desbloquearCandados("d_06");		
				desbloquearCandados("d_07");		
				desbloquearCandados("d_08");		
				desbloquearCandados("d_09");		
				desbloquearCandados("d_10");		
			}
		} 
		else if (p.getActiveSceneName () == "Logros") {
			activarInsignias ();
			Text text = GameObject.Find("RANGO").GetComponent<Text>();


			if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango2Aeronauta) {
				ico_Geonauta.SetActive (false);
				ico_Aeronauta.SetActive (true);
				ico_Aeronauta.GetComponent<LabsFade> ().fade();
				text.text = Principal.nombreRango2Aeronauta;
				GameObject.Find("Slider").GetComponent<Slider>().value=50;
			} else if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango3Cosmonauta) {
				ico_Geonauta.SetActive (false);
				ico_Cosmonauta.SetActive (true);
				ico_Cosmonauta.GetComponent<LabsFade> ().fade();
				text.text = Principal.nombreRango3Cosmonauta;
				GameObject.Find("Slider").GetComponent<Slider>().value=75;
			} else if (Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango4Martenauta) {
				ico_Geonauta.SetActive (false);
				ico_Martenauta.SetActive (true);
				ico_Martenauta.GetComponent<LabsFade> ().fade();
				text.text = Principal.nombreRango4Martenauta;
				GameObject.Find("Slider").GetComponent<Slider>().value=100;
	     	};
			animarLogros ();

		} else {
			StartCoroutine (esperarYMostrarLogroObtenido (sec));
		}
        
    }

	void activarInsignias(){

		if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreInsigniaOperacionesEnteros) != null) {
			panel_insg_enteros_on.SetActive (true);
		} else {
			panel_insg_enteros_on.SetActive (false);
		}

		if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreInsigniaEcuaciones) != null) {
			panel_insg_ecuaciones_on.SetActive (true);
		} else {
			panel_insg_ecuaciones_on.SetActive (false);
		}

		if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreInsigniaLaboratorios) != null) {
			panel_insg_laboratorios_on.SetActive (true);
		} else {
			panel_insg_laboratorios_on.SetActive (false);
		}

		if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreInsigniaNumerosPrimos) != null) {
			panel_insg_primos_on.SetActive (true);
		} else {
			panel_insg_primos_on.SetActive (false);
		}

		if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreInsigniaCuestionarios) != null) {
			panel_insg_cuestionarios_on.SetActive (true);
		} else {
			panel_insg_cuestionarios_on.SetActive (false);
		}


	}

	void animarLogros(){
		if (nuevaInsignia != null) {
			StartCoroutine (esperarYMostrarInsginiaObtenido ());
		}
		if (nuevoRango!=null && Principal.avance.avanceJugadorActual ().nivel != nuevoRango.ToUpper()) {  
			count = 25;
			StartCoroutine(esperarYActualizarSlider());//gCompleta.GetComponent<Toggle>().gameObject.SetActive(true);
		}
	} 

/*-----------------------------------------------------------------------------------------------------------*/
		IEnumerator esperarYMostrarInsginiaObtenido()/*   StartCoroutine(esperar);*/
			{
				yield return new WaitForSeconds(sec);
		if (Principal.nombreInsigniaOperacionesEnteros == nuevaInsignia) {
			panel_insg_enteros_on.SetActive (true);
			Principal.avance.avanceJugadorActual ().agregarLogro (Principal.nombreInsigniaOperacionesEnteros, 1, 1);
			nuevaInsignia = null;
		} else if (Principal.nombreInsigniaCuestionarios== nuevaInsignia) {
			panel_insg_cuestionarios_on.SetActive (true);
			Principal.avance.avanceJugadorActual ().agregarLogro (Principal.nombreInsigniaCuestionarios, 1, 1);
			nuevaInsignia = null;
		} else if (Principal.nombreInsigniaEcuaciones== nuevaInsignia) {
			panel_insg_ecuaciones_on.SetActive (true);
			Principal.avance.avanceJugadorActual ().agregarLogro (Principal.nombreInsigniaEcuaciones, 1, 1);
			nuevaInsignia = null;
		} else if (Principal.nombreInsigniaLaboratorios== nuevaInsignia) {
			panel_insg_laboratorios_on.SetActive (true);
			Principal.avance.avanceJugadorActual ().agregarLogro (Principal.nombreInsigniaLaboratorios, 1, 1);
			nuevaInsignia = null;
		} else if (Principal.nombreInsigniaNumerosPrimos== nuevaInsignia) {
			panel_insg_primos_on.SetActive (true);
			Principal.avance.avanceJugadorActual ().agregarLogro (Principal.nombreInsigniaNumerosPrimos, 1, 1);
			nuevaInsignia = null;
		}


	}

/*----------------------------------------------------------------------------------------------------- */

	IEnumerator esperarYMostrarLogroObtenido(int sec) 
	{
		yield return new WaitForSeconds(sec);
		mostrarLogroObtenido();
	}

    void mostrarLogroObtenido () {
        Principal p = new Principal();
		//toEscene = p.escenaActualNombre ();
		if (Principal.avance.avanceJugadorActual ().obtenerLogro   (Principal.nombreActSimVuelo)==null && 
			Principal.avance.avanceJugadorActual ().estadoActividad(Principal.nombreActSimVuelo, 1) >= 1) {
			Principal.avance.avanceJugadorActual ().agregarLogro   (Principal.nombreActSimVuelo, 1, 1);
			nuevoSimulador = Principal.nombreActSimVuelo;
			toEscene = "Desafio_01";
		}

		if (Principal.avance.avanceJugadorActual ().obtenerLogro   (Principal.nombreActSimAterrisaje)==null && 
			Principal.avance.avanceJugadorActual ().estadoActividad(Principal.nombreActSimAterrisaje, 1) >= 1) {
			Principal.avance.avanceJugadorActual ().agregarLogro   (Principal.nombreActSimAterrisaje, 1, 1);
			nuevoSimulador = Principal.nombreActSimAterrisaje;
			toEscene = "Desafio_04";
		}

		if (Principal.avance.avanceJugadorActual ().obtenerLogro   (Principal.nombreActSimRaSolar)==null && 
			Principal.avance.avanceJugadorActual ().estadoActividad(Principal.nombreActSimRaSolar, 1) >= 1) {
			Principal.avance.avanceJugadorActual ().agregarLogro   (Principal.nombreActSimRaSolar, 1, 1);
			nuevoSimulador = Principal.nombreActSimRaSolar;
			toEscene = "Desafio_06";
		}
		if (Principal.avance.avanceJugadorActual ().obtenerLogro   (Principal.nombreActSimExplorador)==null && 
			Principal.avance.avanceJugadorActual ().estadoActividad(Principal.nombreActSimExplorador, 1) >= 1) {
			Principal.avance.avanceJugadorActual ().agregarLogro   (Principal.nombreActSimExplorador, 1, 1);
			nuevoSimulador = Principal.nombreActSimExplorador;
			toEscene = "Desafio_09";
		}


		//gano la insigunia
		if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreInsigniaOperacionesEnteros) == null &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActOperacionesEnteros, 1) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActOperacionesEnteros, 2) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActOperacionesEnteros, 3) == 2) {
			nuevaInsignia = Principal.nombreInsigniaOperacionesEnteros;
			LabsLogros.volverA = p.escenaActualNombre ();
			} 

		if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreInsigniaCuestionarios) == null &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 1) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 2) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 3) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 4) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 5) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 6) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 7) == 2) {
			nuevaInsignia = Principal.nombreInsigniaCuestionarios;
			LabsLogros.volverA = p.escenaActualNombre ();
		} 

		if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreInsigniaEcuaciones) == null &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActEcuaciones, 1) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActEcuaciones, 2) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActEcuaciones, 3) == 2) {
			nuevaInsignia = Principal.nombreInsigniaEcuaciones;
			LabsLogros.volverA = p.escenaActualNombre ();
		} 

		if (Principal.avance.avanceJugadorActual ().obtenerLogro (Principal.nombreInsigniaLaboratorios) == null &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActLabEstadosMateria, 1) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActLabEstrucutraAtomica, 1) == 2)   {
			nuevaInsignia = Principal.nombreInsigniaLaboratorios;
			LabsLogros.volverA = p.escenaActualNombre ();
		} 

		if (Principal.avance.avanceJugadorActual ().obtenerLogro    (Principal.nombreInsigniaNumerosPrimos) == null &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActNumerosPrimos, 1) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActNumerosPrimos, 2) == 2 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActNumerosPrimos, 3) == 2) {
			nuevaInsignia = Principal.nombreInsigniaNumerosPrimos;
			LabsLogros.volverA = p.escenaActualNombre ();
		} 

	
		/* termino los primeros tres desafios */

		if (
			//p.getActiveSceneName () == "Desafio_03" &&
			(Principal.avance.avanceJugadorActual ().nivel == null ||
			 Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango1Geonauta) &&
			 Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActOperacionesEnteros, 1) >= 1 &&
			 Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActNumerosPrimos, 1) >= 1 &&
			 Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActEcuaciones, 1) >= 1 &&
			 Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 1) >= 1 &&
			 Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 2) >= 1 &&
			 Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 3) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActSimVuelo, 1) >= 1 &&
			1 == 1) {
			anteriorRango = Principal.nombreRango1Geonauta;
			nuevoRango = Principal.nombreRango2Aeronauta;
		}
		else if (
			(Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango2Aeronauta) &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActOperacionesEnteros, 2) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActNumerosPrimos, 2) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActEcuaciones, 2) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 4) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 5) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActSimAterrisaje, 1) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActSimRaSolar, 1) >= 1 &&
			1 == 1) {
			anteriorRango = Principal.nombreRango2Aeronauta;
			nuevoRango = Principal.nombreRango3Cosmonauta;
		}
		else if (
			(Principal.avance.avanceJugadorActual ().nivel == Principal.nombreRango3Cosmonauta) &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActOperacionesEnteros, 3) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActNumerosPrimos, 3) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActEcuaciones, 3) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 6) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActCuestionarios, 7) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActLabEstrucutraAtomica, 1) >= 1 &&
			Principal.avance.avanceJugadorActual ().estadoActividad (Principal.nombreActSimExplorador, 1) >= 1 &&
			1 == 1) {
				anteriorRango = Principal.nombreRango3Cosmonauta;
				nuevoRango    = Principal.nombreRango4Martenauta;
		}





		// mostrar avances
		if (nuevoRango!=null || nuevaInsignia!=null || nuevoSimulador!=null) {
			p.cargarEscena ("Msj_InsigniaNivel");

		}

    } 

	public void reseterRango(){
		Principal.avance.avanceJugadorActual ().nivel = Principal.nombreRango1Geonauta;
		Principal.avance.avanceJugadorActual ().mostrado.Remove ("ico_"+Principal.nombreRango2Aeronauta);
	//	Principal.avance.avanceJugadorActual ().mostrado.Remove ("desafio1_enteros_star");
		Principal.avance.avanceJugadorActual ().mostrado.Remove (Principal.nombreInsigniaOperacionesEnteros);
	//	Principal.avance.avanceJugadorActual ().borrarLogro (Principal.nombreInsigniaOperacionesEnteros);
		Principal.avance.avanceJugadorActual ().borrarLogro (Principal.nombreActSimVuelo);
		Principal.avance.avanceJugadorActual ().borrarLogro (Principal.nombreActSimExplorador);

		ico_Geonauta.SetActive (true);
		GameObject.Find("RANGO").GetComponent<Text>().text=Principal.nombreRango1Geonauta.ToUpper();
		GameObject.Find("Slider").GetComponent<Slider>().value=25;

	//	Principal.avance.avanceJugadorActual ().actualizar (Principal.nombreActOperacionesEnteros, 1, 0, 600);  
		Principal.avance.avanceJugadorActual ().actualizar (Principal.nombreActSimVuelo, 1, 0, 400);  
		Principal.avance.avanceJugadorActual ().desbloqueo.Remove("Simulador Vuelo");
		Principal.avance.avanceJugadorActual ().desbloqueo.Remove("d_04");		
		Principal.avance.avanceJugadorActual ().desbloqueo.Remove("d_05");		
		Principal.avance.avanceJugadorActual ().desbloqueo.Remove("d_06");
		Principal.avance.avanceJugadorActual ().mostrado.Remove ("desafio1_vuelo_star");
		Principal.avance.avanceJugadorActual ().mostrado.Remove ("desafio1_vuelo_ok");


	}

	public void salir(){
		Principal p = new Principal ();
		if (toEscene != null) {
			string s = toEscene;
			toEscene = null;
			p.cargarEscena (s);

		} else {
			p.cargarEscena ("Main_Menu");
		}

	}

	IEnumerator esperarYActualizarSlider()/*   StartCoroutine(esperar);*/
	{
		Debug.Log ("esperarYActualizarSlider" + count);
		yield return new WaitForSeconds(0.02f);
		if (count > 0) {
			count--;
			GameObject.Find("Slider").GetComponent<Slider>().value++;
			if (count <= 0) {
				Text text = GameObject.Find("RANGO").GetComponent<Text>();

				text.text = nuevoRango.ToUpper();
				GameObject sonido_logro = GameObject.Find("sonido_logro");
				AudioSource audio = sonido_logro.GetComponent<AudioSource>();
				audio.Play();

				if(anteriorRango==Principal.nombreRango1Geonauta){
					ico_Geonauta.SetActive (false);// activeSelf = false;
					ico_Aeronauta.GetComponent<LabsFade>().fade();
					ico_Aeronauta.GetComponent<Animator>().Play("star");
					ico_Aeronauta.SetActive(true);
				}
				else if(anteriorRango==Principal.nombreRango2Aeronauta){
					ico_Aeronauta.SetActive (false);// activeSelf = false;
					ico_Cosmonauta.GetComponent<LabsFade>().fade();
					ico_Cosmonauta.GetComponent<Animator>().Play("star");
					ico_Cosmonauta.SetActive(true);
				}
				else if(anteriorRango==Principal.nombreRango3Cosmonauta){
					ico_Cosmonauta.SetActive (false);// activeSelf = false;
					ico_Martenauta.GetComponent<LabsFade>().fade();
					ico_Martenauta.GetComponent<Animator>().Play("star");
					ico_Martenauta.SetActive(true);
				} 
				Principal.avance.avanceJugadorActual ().nivel = nuevoRango;
				nuevoRango = null;
				LabsLogros.volverA = "Entrenar";

			}
			StartCoroutine(esperarYActualizarSlider());
		}

	}
    // Update is called once per frame
    void Update () {
       
	}
}
