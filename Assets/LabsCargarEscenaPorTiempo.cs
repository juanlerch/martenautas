using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsCargarEscenaPorTiempo : MonoBehaviour {
	public string escena;

	public int segundos;



	IEnumerator esperarYMostrarEscena ()/*   StartCoroutine(esperarYJugar());*/
	{

		yield return new WaitForSeconds(segundos);
		Principal p = new Principal ();
		p.cargarEscena (escena);
	}


	// Use this for initialization
	void Start () { 
		StartCoroutine(esperarYMostrarEscena ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
