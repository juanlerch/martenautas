using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class electron : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	Vector3 startPosition;
	public GameObject orbitas;
	public GameObject orbita11;
	public GameObject orbita12;
	public GameObject orbita2;
	public GameObject modeloElectron;

	Vector3 posicionElectron;
	string ubicacionElectron; // A: Afuera; O1: Órbita 1; O2:Órbita 2
	bool isDragable;


	// Use this for initialization
	void Start () {
		ubicacionElectron = "A";
	}
	
	// Update is called once per frame
	void Update () {
		if ((ubicacionElectron == "O11" || ubicacionElectron == "O12") && Laboratorio2.electronesUbicados > 2) {
			isDragable = false;	
		} else {
			isDragable = true;
		}
	}

	public void limpiar(){
		transform.localScale = new Vector3 (1f, 1f, 1f);
		ubicacionElectron = "A";
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!isDragable) return;
		startPosition = transform.position - Input.mousePosition;
		posicionElectron = transform.position;
		/*
		if (ubicacionElectron == "A") {
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		}
		*/
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!isDragable) return;

		if(ubicacionElectron == "O2" || ((ubicacionElectron == "O11" || ubicacionElectron == "O12")  && Laboratorio2.electronesUbicados <= 2) || ubicacionElectron == "A"){
			transform.position = Input.mousePosition + startPosition;	
		}

		float distancia = (new Vector2 (orbitas.transform.position.x, orbitas.transform.position.x) - new Vector2 (transform.position.x, transform.position.x)).magnitude;
		if (distancia <= 250) {
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		} else {
			transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{   
		
		if (!isDragable) return;
		float distancia = (new Vector2 (orbitas.transform.position.x, orbitas.transform.position.x) - new Vector2 (transform.position.x, transform.position.x)).magnitude;
		if (distancia <= 250) {
			if (ubicacionElectron == "A") {
				Laboratorio2.electronesUbicados++;
			} else {
				transform.position = posicionElectron;
				return;

			}
			if (Laboratorio2.electronesUbicados == 1) {
				transform.position = orbita11.transform.position;
				ubicacionElectron = "O11";
				Laboratorio2.electronesEn11 = 1;
			} else if (Laboratorio2.electronesUbicados == 2) {
				if (Laboratorio2.electronesEn12 == 0) {
					Laboratorio2.electronesEn12 = 1;
					transform.position = orbita12.transform.position;
					ubicacionElectron = "O12";
				} else {
					Laboratorio2.electronesEn11 = 1;
					transform.position = orbita11.transform.position;
					ubicacionElectron = "O11";
				}
			} else {
				ubicacionElectron = "O2";
				Laboratorio2.electronesEnOrbita2.Add (this.gameObject);

			}




		} else {
			if (ubicacionElectron == "O11") {
				Laboratorio2.electronesEn11 = 0;
			}
			else if (ubicacionElectron == "O12") {
				Laboratorio2.electronesEn12 = 0;
			}
			transform.position = modeloElectron.transform.position;
			Laboratorio2.electronesEnOrbita2.Remove (this.gameObject);
			if (ubicacionElectron != "A") {
				Laboratorio2.electronesUbicados--;
				ubicacionElectron = "A";
			}
		}

		int cantidadElectonesEnOrbita2 = Laboratorio2.electronesEnOrbita2.Count;
		if (cantidadElectonesEnOrbita2 > 0) {
			float angulo = 360 / cantidadElectonesEnOrbita2;

			Quaternion aux = Quaternion.Euler (0f, 0f, 45f);
			orbita2.transform.rotation = aux;

			foreach (GameObject e in Laboratorio2.electronesEnOrbita2) {
				e.transform.position = orbita2.transform.Find ("Panel").transform.position;
				orbita2.transform.Rotate (new Vector3 (0f, 0f, angulo));	
			}
		}

	}
}
