using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LabTaller03Terminos : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 startPosition;
    Vector3 posicionInicial;

    GameObject izquierda;
    GameObject derecha;
    
	GameObject contador;
    GameObject txtSolucion;
    GameObject textSolucion;

	string ubicacionTermino; //I: Izquierda, D: Derecha, A: Afuera

    void Start()
    {
		ubicacionTermino = "A";
		posicionInicial = GetComponent<RectTransform>().localPosition;
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

		izquierda = GameObject.Find("Panel-Izquierda");
		derecha   = GameObject.Find("Panel-Derecha");

		float altoPanelIzq = izquierda.GetComponent<RectTransform> ().rect.height;
		float anchoPanelIzq = izquierda.GetComponent<RectTransform> ().rect.width;
		float distanciaHorizontalIzquierda = (new Vector2 (izquierda.transform.position.x, 0f) - new Vector2 (transform.position.x, 0f)).magnitude;
		float distanciaVerticalIzquierda = (new Vector2 (0f, izquierda.transform.position.y) - new Vector2 (0f, transform.position.y)).magnitude;

		float altoPanelDer = derecha.GetComponent<RectTransform> ().rect.height;
		float anchoPanelDer = derecha.GetComponent<RectTransform> ().rect.width;
		float distanciaHorizontalDerecha = (new Vector2 (derecha.transform.position.x, 0f) - new Vector2 (transform.position.x, 0f)).magnitude;
		float distanciaVerticalDerecha = (new Vector2 (0f, derecha.transform.position.y) - new Vector2 (0f, transform.position.y)).magnitude;

		valor = transform.GetComponent<Text> ().text;

		if ((distanciaHorizontalIzquierda <= (anchoPanelIzq / 2)+(transform.GetComponent<RectTransform>().rect.width/2)) && (distanciaVerticalIzquierda <= (altoPanelIzq / 2)+(transform.GetComponent<RectTransform>().rect.height/2))) {
			if (ubicacionTermino != "I") {
				if (ubicacionTermino == "D") {
					GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionDerecha.Remove (this.gameObject);
				}
				ubicacionTermino = "I";
				GameObject.Find ("Scripts").GetComponent<LabTaller03> ().resolucionIzquierda.Add (this.gameObject);
			}

			GameObject.Find ("bt_click").GetComponent<AudioSource> ().Play ();

		} else if ((distanciaHorizontalDerecha <= (anchoPanelDer / 2)+(transform.GetComponent<RectTransform>().rect.width/2)) && (distanciaVerticalDerecha <= (altoPanelDer / 2)+(transform.GetComponent<RectTransform>().rect.height/2))) {
			
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
