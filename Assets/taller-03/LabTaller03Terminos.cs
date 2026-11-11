using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LabTaller03Terminos : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 startPosition;
    Vector3 posicionInicial;

	Vector3 auxPosition;

	//Límites en los que se puede mover el término
	GameObject limiteInferior;
	GameObject limiteSuperior;

	//Límites donde se puede arrastrar un término para resolver la ecuación
	GameObject izquierdaInferior;
	GameObject izquierdaSuperior;
	GameObject derechaInferior;
	GameObject derechaSuperior;

	GameObject contador;
    GameObject txtSolucion;
    GameObject textSolucion;

	string ubicacionTermino; //I: Izquierda, D: Derecha, A: Afuera

    void Start()
    {
		ubicacionTermino = "A";
		posicionInicial = GetComponent<RectTransform>().localPosition;

		//Límites
		limiteInferior = GameObject.Find("limite-inferior");
		limiteSuperior = GameObject.Find("limite-superior");

    } 

    public void OnBeginDrag(PointerEventData eventData)
    {
		
        startPosition = transform.position - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
		transform.position = Input.mousePosition + startPosition;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
		// Verifico que no se salga de lo márgenes
		auxPosition = transform.position;

		if (transform.position.x < limiteInferior.transform.position.x) {
			auxPosition.x = limiteInferior.transform.position.x;
		} else if (transform.position.x > limiteSuperior.transform.position.x) {
			auxPosition.x = limiteSuperior.transform.position.x;
		}

		if (transform.position.y < limiteInferior.transform.position.y) {
			auxPosition.y = limiteInferior.transform.position.y;
		} else if (transform.position.y > limiteSuperior.transform.position.y) {
			auxPosition.y = limiteSuperior.transform.position.y;
		}


		transform.position = auxPosition;
		//------------------------------------------

        float distanciaIzquierda;
		float distanciaDerecha;

        float distanciaError;
        string txtLetraNro;
        int aux;

		string valor;
		string resultado;

		float xPanelDerecha;
		float yPanelDerecha;

        txtLetraNro = GetComponent<Text>().text;

		izquierdaInferior = GameObject.Find("izquierda-inferior");
		izquierdaSuperior = GameObject.Find("izquierda-superior");
		derechaInferior   = GameObject.Find("derecha-inferior");
		derechaSuperior   = GameObject.Find("derecha-superior");

		valor = transform.GetComponent<Text> ().text;

		if ((transform.position.x >= izquierdaInferior.transform.position.x)
		    && (transform.position.x <= izquierdaSuperior.transform.position.x)
		    && (transform.position.y >= izquierdaInferior.transform.position.y)
		    && (transform.position.y <= izquierdaSuperior.transform.position.y)) {
			if (ubicacionTermino != "I") {
				if (ubicacionTermino == "D") {
					GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionDerecha.Remove (this.gameObject);
				}
				ubicacionTermino = "I";
				GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionIzquierda.Add (this.gameObject);
			}

			GameObject.Find ("bt_click").GetComponent<AudioSource> ().Play ();
		} else if ((transform.position.x >= derechaInferior.transform.position.x)
		         && (transform.position.x <= derechaSuperior.transform.position.x)
		         && (transform.position.y >= derechaInferior.transform.position.y)
		         && (transform.position.y <= derechaSuperior.transform.position.y)) {

			if (ubicacionTermino != "D") {
				if (ubicacionTermino == "I") {
					GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionIzquierda.Remove (this.gameObject);
				}
				ubicacionTermino = "D";
				GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionDerecha.Add (this.gameObject);
			}

			GameObject.Find ("bt_click").GetComponent<AudioSource> ().Play ();
		} else {
			if (ubicacionTermino == "I") {
				GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionIzquierda.Remove (this.gameObject);
			}
			else if (ubicacionTermino == "D") {
				GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionDerecha.Remove (this.gameObject);
			}
			ubicacionTermino = "A";

		}

		float espacio = transform.GetComponent<RectTransform>().rect.width;

		int i = 0;
		//Alineo los términos de la izquierda
		int cantidad = GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionIzquierda.Count - 1;
		Vector3 posIniI = GameObject.Find ("Text-align-izquierda").transform.position - new Vector3((cantidad*espacio), 0, 0);

		foreach (GameObject o in GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionIzquierda) {
			o.GetComponent<Transform> ().position = posIniI+ new Vector3(espacio*i,0,0);
			i++;
		}

		i = 0;
		//Alineo los términos de la derecha
		Vector3 posIniD = GameObject.Find ("Text-align-derecha").transform.position;
		foreach (GameObject o in GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionDerecha) {
			o.GetComponent<Transform> ().position = posIniD+ new Vector3(espacio*i,0,0);
			i++;
		}

		GameObject.Find("Scripts").GetComponent<LabTaller03>().corregir();

    }

	public string getUbicacion(){
		return ubicacionTermino;
	}
}
