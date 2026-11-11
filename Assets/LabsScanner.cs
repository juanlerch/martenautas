using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabsScanner : MonoBehaviour {


	public GameObject panelNoEncotrado;
	public GameObject ScanAnimacion;
	public int angulo =30;
	public int distance=75;

	public int esperarSeg = 3;

	public GameObject[] objetos;
	public GameObject[] paneles;
	public GameObject gCantidadEscaneados;
	public GameObject gPuntos;

	public int[] escaneados;
	int cantidadEscaneados = 0;

	void Start(){
		gCantidadEscaneados.GetComponent<Text> ().text = "" + cantidadEscaneados + "/" + escaneados.Length;
	}

	IEnumerator esperar() /*   StartCoroutine(esperarYJugar());*/
	{
		yield return new WaitForSeconds(esperarSeg);
		scanner();
	}
	 
	public void esperarYscannear(){
		StartCoroutine(esperar());
	}


	void Update(){
		if (Input.GetKeyDown(KeyCode.E)) 
		{
			ScanAnimacion.SetActive (true);

			esperarYscannear ();
		}
	}

	public void scanner(){
		
		GameObject selectGO=null;
		GameObject selectPanel=null;
		int index = 0;
		float lastDistance = 0.0f;
		foreach (GameObject obj in objetos) {
			Vector3 directionToTarget = obj.transform.position - transform.position ;
			float angle = Vector3.Angle(transform.forward, directionToTarget);

			float distance = directionToTarget.magnitude;

			if (Mathf.Abs (angle) <= angulo && distance <= 50) {
				if (selectGO == null) {
					selectGO = obj;
					selectPanel = paneles [index];
					lastDistance = distance;
				} else {
					if (distance < lastDistance) {
						selectGO = obj;
						selectPanel = paneles [index];
					}
				}

				if (escaneados [index] == 0) {
					escaneados [index] = 1;

					cantidadEscaneados++;
					gCantidadEscaneados.GetComponent<Text> ().text = "" + cantidadEscaneados + "/" + escaneados.Length;

					Principal.juego.simuladorRover.puntajeJuego += Principal.juego.simuladorRover.data.puntoOK;
					if(gPuntos==null) gPuntos = GameObject.Find("TextPuntaje");
					Text puntos = gPuntos.GetComponent<Text>();
					LabsCounter counter = puntos.GetComponent<LabsCounter>();
					counter.addValor(Principal.juego.simuladorRover.data.puntoOK);
				}
			}
			else {
					Debug.Log (" No " + obj.name + " angle:" + angle + "distance:" + distance );
				}	
			index++;
		}

		if (selectPanel != null)
			selectPanel.SetActive (true);
		else
			panelNoEncotrado.SetActive (true);
	}


}
