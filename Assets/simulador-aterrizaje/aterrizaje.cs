using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aterrizaje : MonoBehaviour {
	float gravedad;
	float empuje;
	float fuerza;
	float velocidad;
	float altura;

	Vector3 direccion;
	Vector3 direccionDescenso;
	float distanciaCamara;

	ParticleSystem.MainModule fuego;

	public GameObject textEmpuje;
	public GameObject TextDireccion;
	public GameObject TextAltura;
	public GameObject TextVelocidad;

	public GameObject vectorFuerzaEmpuje;
	public GameObject vectorFuerzaEmpujeXDerecha;
	public GameObject vectorFuerzaEmpujeXIzquierda;
	public GameObject vectorFuerzaEmpujeY;
	public GameObject vectorFuerzaGravedad;
	float multiplicadorVectorFuerza;


	// Use this for initialization
	void Start () {
		gravedad = 1.62f;
		empuje = 0f;
		fuerza = 0f;
		distanciaCamara = 60.0f;
		direccion = new Vector3 (0, 1, 0);
		velocidad = 0f;
		altura = gameObject.GetComponent<Transform> ().position.y;

		multiplicadorVectorFuerza = 40.0f;

		ParticleSystem ps = GameObject.Find ("propulsion").GetComponent<ParticleSystem> ();
		fuego = ps.main;
		fuego.startLifetime = 0f;

	}



	void OnCollisionEnter(Collision col){
		string tipoColision;
		float inclinacion;
		float velocidadImpacto;

		inclinacion = Mathf.Abs(transform.rotation.z);
		velocidadImpacto = Mathf.Abs(velocidad);
		Debug.Log ("Velocidad de impacto " + velocidadImpacto.ToString("F1") + " m/s");
		Debug.Log ("Inclinación:" + (inclinacion).ToString("F1") + "º");

		tipoColision = col.collider.tag;

		empuje = 0f;
		direccion = new Vector3 (0, 1, 0);


		if (tipoColision == "suelo") {
			Debug.Log ("OK");

		} else if (tipoColision == "obstaculo") {
			Debug.Log ("ERROR");
		
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		float aux;

		if (Input.GetKeyDown(KeyCode.UpArrow)){
			empuje+=0.2f;

		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			empuje-=0.2f;
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)){
			direccion.x -= 0.11f;
			transform.Rotate (0, 0,5);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)){
			direccion.x += 0.11f;
			transform.Rotate (0, 0, -5);
		}

		if (empuje > 0) {
			fuego.startLifetime = empuje * 5;
		} else {
			fuego.startLifetime = 0.00f;
		}
			

		fuerza = empuje - gravedad;
		textEmpuje.GetComponent<Text> ().text = "" + fuerza;
		TextDireccion.GetComponent<Text> ().text = "" + direccion;
		TextAltura.GetComponent<Text> ().text = "" + gameObject.GetComponent<Transform> ().position.y;

		direccionDescenso = direccion;
		direccionDescenso.x = -1 * direccionDescenso.x;
		/*
		if (gameObject.GetComponent<Transform> ().position.y > 5) {
			distanciaCamara = 120f;
		} else {
			distanciaCamara = 60f;
		}

		Camera.main.fieldOfView = distanciaCamara;
		*/
		if (gameObject.GetComponent<Transform> ().position.y > 5) {
	//		Camera.main.orthographicSize  = 10f;
	//		Camera.GetComponent<Camera>().transform.position.y = 5f;
		} else {
	//		Camera.main.orthographicSize  = 5f;
	//		Camera.GetComponent<Camera>().transform.position.y = 0f;
		}


		if (fuerza > 0) {
			GetComponent<Rigidbody> ().AddForce (direccion * fuerza, ForceMode.Force);	
		} else {
			//GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 1, 0) * fuerza, ForceMode.Force);	
			GetComponent<Rigidbody> ().AddForce (direccionDescenso * fuerza, ForceMode.Force);	
		}

		aux = gameObject.GetComponent<Transform> ().position.y;
		velocidad = (altura - aux) / Time.deltaTime;
		altura = aux;

		TextVelocidad.GetComponent<Text> ().text = "" + Mathf.Abs(velocidad).ToString("F1") + " m/s";

		//-----------------------------------------------------------------------------------------
		//	Vectores que representan las fuerzas
		//-----------------------------------------------------------------------------------------
		vectorFuerzaGravedad.GetComponent<RectTransform>().sizeDelta = new Vector2(20f,multiplicadorVectorFuerza*gravedad);
		vectorFuerzaEmpuje.GetComponent<RectTransform>().sizeDelta = new Vector2(20f,multiplicadorVectorFuerza*empuje);
		vectorFuerzaEmpuje.GetComponent<RectTransform> ().rotation = transform.rotation;
		float radianes = transform.rotation.eulerAngles.z * Mathf.PI/180.0f;
		float magnitudAux = -1 * multiplicadorVectorFuerza * (empuje * Mathf.Sin(radianes));

		if (magnitudAux > 0) {
			vectorFuerzaEmpujeXDerecha.GetComponent<RectTransform> ().sizeDelta = new Vector2 (20f, magnitudAux); 
			vectorFuerzaEmpujeXIzquierda.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0.0f, 0.0f); 

		} else {
			vectorFuerzaEmpujeXDerecha.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0.0f, 0.0f); 
			vectorFuerzaEmpujeXIzquierda.GetComponent<RectTransform> ().sizeDelta = new Vector2 (20f, -1*magnitudAux); 
		
		}

		vectorFuerzaEmpujeY.GetComponent<RectTransform>().sizeDelta = new Vector2(20f,multiplicadorVectorFuerza*(empuje*Mathf.Cos(radianes))); 
	}


}
