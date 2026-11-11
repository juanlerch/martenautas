using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class neutron : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	Vector3 startPosition;
	public GameObject orbitas;
	public GameObject modeloNeutron;

	string ubicacionNeutron; // A: Afuera; N: En el núcleo
	Vector3 posicionNeutron;

	int radioNucleo = 50;

	// Use this for initialization
	void Start () {
		ubicacionNeutron = "A";
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void limpiar(){
		transform.localScale = new Vector3 (1f, 1f, 1f);
		ubicacionNeutron = "A";
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		startPosition = transform.position - Input.mousePosition;
		posicionNeutron = transform.position;
		/*
		if (ubicacionNeutron == "A") {
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		}
		*/
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition + startPosition;	

		float distancia = (new Vector2 (orbitas.transform.position.x, orbitas.transform.position.y) - new Vector2 (transform.position.x, transform.position.y)).magnitude;
		if (distancia <= radioNucleo) {
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		} else {
			transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{   
		
		float distancia = (new Vector2 (orbitas.transform.position.x, orbitas.transform.position.y) - new Vector2 (transform.position.x, transform.position.y)).magnitude;
		if (distancia <= radioNucleo) {
			if (ubicacionNeutron == "A") {
				Laboratorio2.neutronesUbicados++;
			//	transform.position = GameObject.Find ("nucleoNeutron" + Laboratorio2.neutronesUbicados).transform.position;
			} else {
				transform.position = posicionNeutron;
				return;

			}

			ubicacionNeutron = "N";

		} else {
			
			transform.position = modeloNeutron.transform.position;
			Laboratorio2.electronesEnOrbita2.Remove (this.gameObject);
			if (ubicacionNeutron != "A") {
				Laboratorio2.neutronesUbicados--;
				ubicacionNeutron = "A";
		//		transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
		}

	}
}
