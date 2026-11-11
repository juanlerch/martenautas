using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class proton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	Vector3 startPosition;
	public GameObject orbitas;
	public GameObject modeloProton;
	int radioNucleo = 50;

	string ubicacionProton; // A: Afuera; N: En el núcleo
	Vector3 posicionProton;

	// Use this for initialization
	void Start () {
		ubicacionProton = "A";
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void limpiar(){
		transform.localScale = new Vector3 (1f, 1f, 1f);
		ubicacionProton = "A";
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		startPosition = transform.position - Input.mousePosition;
		posicionProton = transform.position;
		/*
		if (ubicacionProton == "A") {
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
			if (ubicacionProton == "A") {
				Laboratorio2.protonesUbicados++;
		//		transform.position = GameObject.Find ("nucleoProton" + Laboratorio2.protonesUbicados).transform.position;
			} else {
				transform.position = posicionProton;
				return;

			}

			ubicacionProton = "N";

		} else {
			
			transform.position = modeloProton.transform.position;
			Laboratorio2.electronesEnOrbita2.Remove (this.gameObject);
			if (ubicacionProton != "A") {
				Laboratorio2.protonesUbicados--;
				ubicacionProton = "A";
			//	transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
		}

	}
}
