using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nave : MonoBehaviour {
	float gravedad;
	float empuje;
	public static int cambioEmpuje=0;
	public static int cambioSentido=0;
	public int nivel=1;
	float fuerza;
	float velocidad;
	float velocidadX; //Velocidad horizontal
	float altura;
	float desplazamiento;

	bool objetivoCumplido;

	int frenar=0;

	float velocidadImpacto = 0;
	float distanciaBase;

	Vector3 direccion;
	Vector3 direccionDescenso;
	float distanciaCamara;

	ParticleSystem.MainModule fuego;
	ParticleSystem.MainModule polvo;

	public GameObject gCompleto;
	public GameObject gMenu;
	public GameObject gJuego;
	public GameObject gPuntos;

	public GameObject gTips; 

	public GameObject TextAltura;
	public GameObject TextVelocidad;
	public GameObject TextImpacto;
	public GameObject TextObjetivosCumplidos;

	public GameObject alertaAltura;
	public GameObject alertaVelocidad;
	public GameObject alertaImpacto;

	public GameObject TextDistanciaBase;

	public GameObject sliderCombustible;
	public GameObject sliderEscudos;

	public GameObject vectorFuerzaEmpuje;
	public GameObject vectorFuerzaEmpujeXDerecha;
	public GameObject vectorFuerzaEmpujeXIzquierda;
	public GameObject vectorFuerzaEmpujeY;
	public GameObject vectorFuerzaGravedad;

	public GameObject[] plataformas;
	public GameObject[] flechas;
	public GameObject[] flechasNext;

	public GameObject gSonidoAterrizaje;
	public GameObject gSonidoDespegue;


	bool reproducirSonidoAterrizaje = true;
	bool reproducirSonidoDespegue = false;
	bool descendiendo = false;
	bool ascendiendo = false;
	float alturaAnterior = 0;

	int plataformasAlcanzadas = 0;

	public GameObject msj1;
	public GameObject msj2;
	public GameObject msjOK;
	public GameObject msjFinalizar;
	public GameObject msjFallo;

	float multiplicadorVectorFuerza;

	float cantidadCombustible=2000f;
	float cantidadEscudos=10f;

	int puntosDescuento;

	Vector3 posicionInicial;
	Vector3 posicionInicialRB;
	// Use this for initialization

	void Start () {
		Principal.nivelActual = nivel;
		ParticleSystem ps = GameObject.Find ("propulsion").GetComponent<ParticleSystem> ();
		fuego = ps.main;
		fuego.startLifetime = 0f;

		ps = GameObject.Find ("polvo").GetComponent<ParticleSystem> ();
		polvo = ps.main;
		polvo.startLifetime = 0f;

		sliderCombustible.GetComponent<Slider> ().maxValue = cantidadCombustible;
		sliderEscudos.GetComponent<Slider> ().maxValue = cantidadEscudos;

		foreach (GameObject f in flechas) {
			f.SetActive (false);
		}

		foreach (GameObject fn in flechasNext) {
			fn.SetActive (false);
		}

		alertaAltura.SetActive (false);
		alertaVelocidad.SetActive (false);
		alertaImpacto.SetActive (false);

	}

	void OnCollisionEnter(Collision col){
		string tipoColision;
		GameObject plataforma;

		reproducirSonidoDespegue = true;

		if (velocidadImpacto == 0) {
			velocidadImpacto = Mathf.Abs (velocidad);
			TextImpacto.GetComponent<Text> ().text = "" + velocidadImpacto.ToString ("F1") + " m/S";
		}

		if (velocidadImpacto > 5.0f) {
			cantidadEscudos -= Mathf.Ceil (velocidadImpacto - 5.0f);

			puntosDescuento = Mathf.FloorToInt(Mathf.Ceil (velocidadImpacto - 5.0f)*10);

			if (cantidadEscudos < 0) {
				cantidadEscudos = 0;
			}

			alertaImpacto.SetActive (true);
		}
		else{
			puntosDescuento = 0;
		}

		tipoColision = col.collider.tag;
		plataforma = col.collider.gameObject;



		// Si cae en la plataforma que corresponde, marco el objetivo como clumplido
		if (plataforma == plataformas [plataformasAlcanzadas]) {
			objetivoCumplido = true;

			Principal.juego.simuladorAterrizaje.puntajeJuego += Principal.juego.simuladorAterrizaje.data.puntoOK-puntosDescuento;
			Text puntos = gPuntos.GetComponent<Text>();
			LabsCounter counter = puntos.GetComponent<LabsCounter>();
			counter.setearValor(Principal.juego.simuladorAterrizaje.puntajeJuego);

			flechasNext [plataformasAlcanzadas].SetActive (true);

		}

		empuje = 0f;
		direccion = new Vector3 (0, 1, 0);

		/*
		if (tipoColision == "suelo") {
			Debug.Log ("OK");

		} else if (tipoColision == "obstaculo") {
			Debug.Log ("ERROR");
		
		}
		*/
	}

	public void iniciar(){
		//--------------------------------------
		// Start
		//--------------------------------------
		gravedad = 0f;
		objetivoCumplido = false;

		empuje = 0f;
		fuerza = 0f;
		distanciaCamara = 60.0f;

		//posicionInicial = gameObject.GetComponent<Transform> ().position;
		posicionInicial = transform.position;
		posicionInicialRB = GetComponent<Rigidbody> ().position;

		velocidad = 0f;
		altura = gameObject.GetComponent<Transform> ().position.y;
		desplazamiento = gameObject.GetComponent<Transform> ().position.x;

		multiplicadorVectorFuerza = 20.0f;

		ParticleSystem ps = GameObject.Find ("propulsion").GetComponent<ParticleSystem> ();
		fuego = ps.main;
		fuego.startLifetime = 0f;

		ps = GameObject.Find ("polvo").GetComponent<ParticleSystem> ();
		polvo = ps.main;
		polvo.startLifetime = 0f;

		sliderCombustible.GetComponent<Slider> ().maxValue = cantidadCombustible;
		sliderEscudos.GetComponent<Slider> ().maxValue = cantidadEscudos;



		//--------------------------------------

	//	gameObject.GetComponent<Transform> ().position = posicionInicial;
		transform.position = posicionInicial;
		GetComponent<Rigidbody> ().position = posicionInicialRB;

		//GetComponent<Rigidbody> ().AddForce (Vector3.zero, ForceMode.Force);	
		GetComponent<Rigidbody> ().AddRelativeForce (Vector3.zero, ForceMode.Force);	

		direccion = new Vector3 (0, 1, 0);
		cantidadCombustible = 2000f;
		cantidadEscudos = 10f;
		plataformasAlcanzadas = 0;
		empuje = 0.0f;

		GetComponent<Rigidbody> ().velocity = new Vector3(0, 0, 0);
		GetComponent<Rigidbody> ().angularVelocity = new Vector3(0, 0, 0);
		transform.rotation = new Quaternion (0f, 0f, 0f, 0f);


		gameObject.GetComponent<Transform> ().position = new Vector3 (0, 40, 0);
		Camera.main.orthographicSize  = 25f;
		//gravedad = 1.62f; //Gravedad en La Luna
		gravedad = 3.711f; //Gravedad en Marte
	}

	public void setCambioEmpuje(int cambio){
		cambioEmpuje = 3*cambio;
	
	}

	public void setCambioSentido(int sentido){
		cambioSentido = sentido;
	}

	void Update(){
		TextObjetivosCumplidos.GetComponent<Text> ().text = "" + plataformasAlcanzadas + "/" + plataformas.Length;
		empuje += cambioEmpuje * Time.deltaTime;
		//----------------------------------------------------------------------------
		// Regular el empuje
		//----------------------------------------------------------------------------
		if (empuje > 5) {
			empuje = 5;
		} else if (empuje < 0) {
			empuje = 0;
		}

		//----------------------------------------------------------------------------
		//Controlar los movimentos izquierda/derecha
		//----------------------------------------------------------------------------
		if(cambioSentido == -1 && transform.rotation.z < 0.2f){
			direccion.x += cambioSentido * 0.2f * Time.deltaTime;
			transform.Rotate (0, 0, (-1 * cambioSentido * 10.0f * Time.deltaTime));
		}

		else if(cambioSentido == 1 && transform.rotation.z > -0.2f){
			direccion.x += cambioSentido * 0.2f * Time.deltaTime;
			transform.Rotate (0, 0, (-1 * cambioSentido * 10.0f * Time.deltaTime));
		}
	
		//----------------------------------------------------------------------------
		// Izquierda / Derecha
		//----------------------------------------------------------------------------
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			setCambioSentido (-1);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			setCambioSentido (1);
		}

		if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)){
			setCambioSentido (0);
		}

		//----------------------------------------------------------------------------
		// Arriba / Abajo
		//----------------------------------------------------------------------------
		if (Input.GetKeyDown(KeyCode.UpArrow)){
			setCambioEmpuje (1);
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			setCambioEmpuje (-1);
		}

		if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)){
			setCambioEmpuje (0);
		}

	}

	void FixedUpdate () {
		float aux;
		float inclinacion;

		//----------------------------------------------------------------------------
		// Resto el combustible
		//----------------------------------------------------------------------------
		cantidadCombustible -= Time.deltaTime * empuje;
		sliderCombustible.GetComponent<Slider> ().value = cantidadCombustible;
		sliderEscudos.GetComponent<Slider> ().value = cantidadEscudos;

		//-----------------------------------------------------------------------------------------
		//Si se termina el combustible detener la nave
		//-----------------------------------------------------------------------------------------
		if(cantidadCombustible <= 0){
			empuje = 0f;
		}


		//----------------------------------------------------------------------------
		// 	Si no hay más escudos o no queda más combustible o si el ángulo del
		// módulo supera lo permitido, entonces muestro mensaje de Fallo
		//----------------------------------------------------------------------------
		if (cantidadEscudos == 0 || cantidadCombustible == 0 || Mathf.Abs(transform.rotation.z) > 0.5f) {
			msjFallo.SetActive (true);
			objetivoCumplido = false;

		//	completarPuntaje ();
		}

		if (objetivoCumplido == true) {
			plataformasAlcanzadas++;

			if (plataformasAlcanzadas == 1) {
				msj2.SetActive (true);
			} else if (plataformasAlcanzadas == 2) {
				msjOK.SetActive (true);
			}
			else if(plataformasAlcanzadas == plataformas.Length){
				msjFinalizar.SetActive (true);
			}

			objetivoCumplido = false;
		}



		//-----------------------------------------------------------------------------------------
		// Si hay empuje muestro el fuego
		//-----------------------------------------------------------------------------------------
		if (empuje > 0) {
			fuego.startLifetime = empuje * 1;
		} else {
			fuego.startLifetime = 0.00f;
		}

		//-----------------------------------------------------------------------------------------
		// Muestro a que altura está el módulo
		//-----------------------------------------------------------------------------------------
		TextAltura.GetComponent<Text> ().text = "" + Mathf.Floor(gameObject.GetComponent<Transform> ().position.y) + " m";

		if (gameObject.GetComponent<Transform> ().position.y > 30 && plataformasAlcanzadas > 0) {
			alertaAltura.SetActive (true);
		} else {
			alertaAltura.SetActive (false);
		}

		if (gameObject.GetComponent<Transform> ().position.y < alturaAnterior) {
			descendiendo = true;
			ascendiendo = false;
		} else if(gameObject.GetComponent<Transform> ().position.y > alturaAnterior){
			descendiendo = false;
			ascendiendo = true;
		}

		alturaAnterior = gameObject.GetComponent<Transform> ().position.y;

		//-----------------------------------------------------------------------------------------
		// Muestro el polvo cuando está a menos de tres metros
		//-----------------------------------------------------------------------------------------
		if (gameObject.GetComponent<Transform> ().position.y < 3f) {
			polvo.startLifetime = 2f*empuje;
			GameObject.Find ("polvo").GetComponent<Transform> ().position = new Vector3(gameObject.GetComponent<Transform> ().position.x, 0f, 0f);

		} else {
			polvo.startLifetime = 0f;
		}

		if (gameObject.GetComponent<Transform> ().position.y < 5f) {
			//-----------------------------------------------------------------------------------------
			//Activo el Sonido de Aterrizaje
			//-----------------------------------------------------------------------------------------

			if (!gSonidoAterrizaje.GetComponent<AudioSource> ().isPlaying && reproducirSonidoAterrizaje && descendiendo) {
				gSonidoAterrizaje.GetComponent<AudioSource> ().Play ();
				reproducirSonidoAterrizaje = false;
				reproducirSonidoDespegue = true;

			}

			if (!gSonidoDespegue.GetComponent<AudioSource> ().isPlaying && reproducirSonidoDespegue && ascendiendo && empuje > gravedad) {
				gSonidoDespegue.GetComponent<AudioSource> ().Play ();
				reproducirSonidoDespegue = false;
				reproducirSonidoAterrizaje = true;

			}

		}

		//-----------------------------------------------------------------------------------------
		// Si la altura supera a 50 le pongo una fuerza desde arriba
		//-----------------------------------------------------------------------------------------
		if (Mathf.Floor (gameObject.GetComponent<Transform> ().position.y) < 50f) {
			fuerza = empuje - gravedad;	
		} else {
			fuerza = empuje - 2*gravedad;	
		}


		//-----------------------------------------------------------------------------------------
		// Dirección de descenso
		//-----------------------------------------------------------------------------------------
		direccionDescenso = direccion;
		direccionDescenso.x = -1 * direccionDescenso.x;

		//-----------------------------------------------------------------------------------------
		// Si la nave está a más de dos metros de altura ocultar el texto del impacto
		//-----------------------------------------------------------------------------------------
		if (gameObject.GetComponent<Transform> ().position.y > 2) {
			TextImpacto.GetComponent<Text> ().text = "";
			velocidadImpacto = 0;
			alertaImpacto.SetActive (false);

		}

		//-----------------------------------------------------------------------------------------
		// Aplico la fuerza al módulo
		//-----------------------------------------------------------------------------------------
		if (fuerza > 0) {
			GetComponent<Rigidbody> ().AddRelativeForce (direccion * fuerza, ForceMode.Force);	
		} else {
			GetComponent<Rigidbody> ().AddRelativeForce (direccionDescenso * fuerza, ForceMode.Force);	

		}

		// Si está muy a la izquiera o muy a la dereca le aplico una fuerza en el sentido contrario
		if (transform.position.x < -10) {
			GetComponent<Rigidbody> ().AddRelativeForce (new Vector3(10,0,0), ForceMode.Force);
		}
		else if(transform.position.x > plataformas[plataformas.Length-1].transform.position.x + 20){
			GetComponent<Rigidbody> ().AddRelativeForce (new Vector3(-10,0,0), ForceMode.Force);
		}


		//-----------------------------------------------------------------------------------------
		// Frenar gradualmente el módulo cuando la orientación sea vertical
		//-----------------------------------------------------------------------------------------
		aux = gameObject.GetComponent<Transform> ().position.x;
		velocidadX = (desplazamiento - aux) / Time.deltaTime;
		desplazamiento = aux;


		if (Mathf.Abs (GetComponent<Transform> ().rotation.z * 100) <= 5f) {
			if (Mathf.Abs(velocidadX) > 0) {
				//GetComponent<Rigidbody> ().AddForce (new Vector3(velocidadX, 0, 0), ForceMode.Force);
				GetComponent<Rigidbody> ().AddRelativeForce (new Vector3(velocidadX, 0, 0), ForceMode.Force);
			}
		}


		//-----------------------------------------------------------------------------------------
		// Calcular la velocidad de descenso
		//-----------------------------------------------------------------------------------------
		aux = gameObject.GetComponent<Transform> ().position.y;
		velocidad = (altura - aux) / Time.deltaTime;
		altura = aux;

		TextVelocidad.GetComponent<Text> ().text = "" + Mathf.Abs(velocidad).ToString("F1") + " m/S";

		if (Mathf.Abs(velocidad) > 5) {
			alertaVelocidad.SetActive (true);

		} else {
			alertaVelocidad.SetActive (false);

		}

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

		//-----------------------------------------------------------------------------------------
		// Distancia a la Base
		//-----------------------------------------------------------------------------------------
		if (plataformasAlcanzadas < plataformas.Length) {
			distanciaBase = Mathf.Abs (gameObject.GetComponent<Transform> ().position.x - plataformas [plataformasAlcanzadas].GetComponent<Transform> ().position.x);
			TextDistanciaBase.GetComponent<Text> ().text = "" + distanciaBase.ToString ("F1") + " m";
		} else {
			TextDistanciaBase.GetComponent<Text> ().text = "";
		}

		//-----------------------------------------------------------------------------------------
		// Flecha que indica el Objetivo
		//-----------------------------------------------------------------------------------------
		if (plataformasAlcanzadas < plataformas.Length) {
			if (!flechas [plataformasAlcanzadas].activeSelf && (altura > 14 || distanciaBase > 3 )) {
				flechas [plataformasAlcanzadas].SetActive (true);
			
			}
			if(flechas [plataformasAlcanzadas].activeSelf && altura <= 14 && distanciaBase < 3){
				flechas [plataformasAlcanzadas].SetActive (false);
			}
		}

		//-----------------------------------------------------------------------------------------
		// Flecha que indica el Próximo Objetivo
		//-----------------------------------------------------------------------------------------
		if (plataformasAlcanzadas > 0) {
			if(flechasNext [plataformasAlcanzadas-1].activeSelf && altura > 15){
				flechasNext [plataformasAlcanzadas-1].SetActive (false);

			}
		}


	}

	public void finalizar(){
		gCompleto.SetActive (true);
		gJuego.SetActive (false);
		
		//Elimino la Nave
		Destroy(gameObject);
		
		completarPuntaje ();
	}

	void completarPuntaje(){
		LabsPanelCompleta completa= gCompleto.GetComponent<LabsPanelCompleta> ();

		if (nivel == 1) {
			completa.completar (Principal.nombreActSimAterrisaje, nivel, Principal.juego.simuladorAterrizaje.puntajeJuego, 300);
		}else{
			completa.completar (Principal.nombreActSimAterrisaje, nivel, Principal.juego.simuladorAterrizaje.puntajeJuego,Principal.juego.simuladorAterrizaje.puntajeMaximo);
		}
	}
}
