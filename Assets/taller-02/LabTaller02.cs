using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LabTaller02 : MonoBehaviour {
    public static int nivel = 2;
	public GameObject gJuego;
	public GameObject gMenu;
	public GameObject gCompleto;
    public GameObject gNivelMenu;
	public GameObject gNivelJuego;
	public GameObject gOperacion;
	public GameObject gPuntajeMenu;
	public GameObject gOpciones;

	public GameObject sampleOptions;

	public GameObject gPuntos;


	private int[] orden; //orden de los indexes


	private List<GameObject> opciones = new List<GameObject> ();


	private List<GameObject> validar = new List<GameObject> ();
	private Hashtable valorEsperado  = new Hashtable();
	private string ultimoError;

	public static int index=0;
    bool isDale = false;
    float daleTimeout = 1.0f;
    float daleTimeoutInit = 1.0f;
    public void pausar(){
		gMenu.SetActive (true);
		gJuego.SetActive (false);
	}

    IEnumerator esperarYJugar()/*   StartCoroutine(esperarYJugar());*/
    {

        yield return new WaitForSeconds(2);
        jugar();
    }

	IEnumerator esperarYTerminar()/*   StartCoroutine(esperarYJugar());*/
	{

		yield return new WaitForSeconds(2);
		LabsPanelCompleta completa= gCompleto.GetComponent<LabsPanelCompleta> ();
		completa.completar (Principal.nombreActOperacionesEnteros,nivel,
							Principal.juego.taller02NumerosNaturales[nivel - 1].puntajeJuego,
							Principal.juego.taller02NumerosNaturales[nivel - 1].puntajeMaximo);
	}


    public void iniciar(){
        gCompleto.SetActive(false);
        gMenu.SetActive (false);
		gJuego.SetActive (true);
      
        Principal.juego.taller02NumerosNaturales[nivel -1].puntajeJuego = 0;
		gPuntos.GetComponent<Text>().text="0"; 
        index = 0;
		Principal.avance.avanceJugadorActual ().estadistica (Principal.nombreActOperacionesEnteros, nivel).Inicializar ();
		jugar ();
	}
	public void jugar(){
		//limpiar los del anterior;
		

		validar.Clear ();
		valorEsperado.Clear ();

		foreach(GameObject anterior in opciones){
			anterior.SetActive (false);
			Object.Destroy (anterior);
		}
		opciones.Clear();

		string[] opcionesArray   = Principal.juego.taller02NumerosNaturales[nivel - 1].data[index].opciones.ToArray();

		Text text = gOperacion.GetComponent<Text> ();

		text.text = Principal.juego.taller02NumerosNaturales[nivel - 1].data [index].operacion;


        //gPuntos.GetComponent<Text> ().text = "" + Principal.juego.taller02NumerosNaturalesNivel1.puntajeJuego + " XP";
        
        int count = 1;

		orden= new int[opcionesArray.Length];

		/*ordenar los indeces*/
		for(int r=0;r<orden.Length;r++){
			orden[r] = r;
		}

		for (int t = 0; t < orden.Length; t++ )
			{
				int tmp = orden[t];
				int r = Random.Range(t, orden.Length);
				orden[t] = orden[r];
				orden[r] = tmp;
			}


        int signo = 0;//la primera vez tomo la posicion del item de referencia
		foreach (int value in orden)
		{
			GameObject o = Instantiate (sampleOptions);
			opciones.Add (o);
			o.SetActive (true); 
			o.transform.SetParent (sampleOptions.transform.parent);

			RectTransform rt = o.GetComponent<RectTransform> ();
			RectTransform rt1 = sampleOptions.GetComponent<RectTransform> ();
            if (signo < 0)
            {
                rt.position = rt1.position  + new Vector3(80+count * (signo * 80), 0, 0);
            }
            else {
                rt.position = rt1.position + new Vector3(count * (signo * 80), 0, 0);
            }
            if (Principal.juego.taller02NumerosNaturales[nivel - 1].data[index].resultados[0] == Principal.juego.taller02NumerosNaturales[nivel -1].data[index].opciones[value]) {
                Toggle toogle = o.GetComponent<Toggle>();
                toogle.GetComponentInChildren<Text> ().text = "" + opcionesArray [value];

                Image img = toogle.transform.Find("Background/Checkmark").GetComponent<Image>();
                img.color = Principal.colorOK;

                valorEsperado.Add(o, true);
                validar.Add(o);
                
			} 
			else {
                Toggle toogle = o.GetComponent<Toggle>();
                toogle.GetComponentInChildren<Text> ().text = "" + opcionesArray[value];
                Image img = toogle.transform.Find("Background/Checkmark").GetComponent<Image>();
                img.color = Principal.colorERR ;

                //valorEsperado.Add (o, false);
            }
			//validar.Add(o);
            count++;
            if (signo ==0) signo = 1;
            else signo = signo * -1;
		}

		
		sampleOptions.SetActive (false);
	}

 /*   public void dale1()
    {
        bool esCorrecta = true;
		ultimoError = "";
        foreach (GameObject go in validar)
        {
            bool ve = (bool)valorEsperado[go];
            bool va = go.GetComponent<Toggle>().isOn;
			if (ve != va) {
				esCorrecta = false;
				ultimoError += go.GetComponentInChildren<Text> ().text + " - ";

			}
        }
        if (esCorrecta)
        {
            isDale = true;
            daleTimeout = daleTimeoutInit;
        }
        else {
            dale1();
        }
    }*/
    public void dale(){
		if (!Principal.isInteractable)
			return;
        bool esCorrecta = true;

		foreach (GameObject go in validar)
        {
            bool ve = (bool)valorEsperado[go];
            bool va = go.GetComponent<Toggle>().isOn;
			if (ve != va) {
				esCorrecta = false;
			}
        }

		ultimoError = "_";
		foreach(GameObject go in opciones){
			bool va = go.GetComponent<Toggle>().isOn;
			if (va) {
				ultimoError += go.GetComponentInChildren<Text> ().text + " ; ";
			}
		}

        /*si es correcta pasa al siguiente*/
		if (esCorrecta) {
			Principal.avance.avanceJugadorActual ().estadistica ( Principal.nombreActOperacionesEnteros,   nivel).actualizarOK ();
            Principal.juego.taller02NumerosNaturales[nivel-1].puntajeJuego += Principal.juego.taller02NumerosNaturales[nivel - 1].data[index].puntoOK;

            if (index < (Principal.juego.taller02NumerosNaturales[nivel - 1].data.Length-1)) {
				index++;		
			} else {
				index = 0;

				StartCoroutine(esperarYTerminar()); //jugar ();


            }

            StartCoroutine(esperarYJugar()); //jugar ();

			//gPuntos.GetComponent<Text> ().text = "" + Principal.juego.taller02NumerosNaturalesNivel1.puntajeJuego +" XP";
		} 
		/*si no es correcta resta el puntaje y queda en el actual*/
		else {
			Principal.avance.avanceJugadorActual ().estadistica (Principal.nombreActOperacionesEnteros , nivel).actualizarERR("Index: " + index +" Numero:"+ gOperacion.GetComponent<Text> ().text + " respuesta: " + ultimoError );

			Principal.juego.taller02NumerosNaturales[nivel - 1].puntajeJuego  += Principal.juego.taller02NumerosNaturales[nivel - 1].data [index].puntoERR;
            if (Principal.juego.taller02NumerosNaturales[nivel - 1].puntajeJuego < 0) {
                Principal.juego.taller02NumerosNaturales[nivel - 1].puntajeJuego = 0;
            }
            //gPuntos.GetComponent<Text> ().text = "" + Principal.juego.taller02NumerosNaturalesNivel1.puntajeJuego+" XP";
        }

        Text puntos = gPuntos.GetComponent<Text>();
        LabsCounter counter = puntos.GetComponent<LabsCounter>();
        counter.setearValor(Principal.juego.taller02NumerosNaturales[nivel - 1].puntajeJuego);


    }

    // Use this for initialization
    void Start () {
		Principal.inicializar ();
        //Transform tpuntos = gMenu.transform.Find("MIDDLE Area/Contenedor MENU/Panel/Puntos");
        //GameObject puntos = tpuntos.gameObject;
        //Text text = puntos.GetComponent<Text>();
        //text.text = "" + Principal.juego.taller02NumerosNaturales[nivel - 1].puntajeMaximo;
		Debug.Log("nivel:" + nivel + "puntaje:" + Principal.juego.taller02NumerosNaturales[nivel - 1].puntajeMaximo);
		Principal.nivelActual = nivel;
		gPuntajeMenu.GetComponent<Text>().text="" + Principal.juego.taller02NumerosNaturales[nivel - 1].puntajeMaximo;
		gNivelMenu.GetComponent<Text>().text = "" + nivel;
		gNivelJuego.GetComponent<Text>().text = "" + nivel;
		gameObject.GetComponent<LabsInfoXNivel> ().setNivel (nivel);

	}

    public void continuar() {
        Principal p = new Principal();
        if (nivel == 1) p.cargarEscena("desafio_01");
        if (nivel == 2) p.cargarEscena("desafio_04");
        if (nivel == 3) p.cargarEscena("desafio_07");

    }

    // Update is called once per frame
    void Update () {
        if (isDale) { 
            if (daleTimeout < 0.0)
            {
                isDale = false;
             //   dale1();
            }
            else {
                daleTimeout = daleTimeout - Time.deltaTime;
            }
        }
    }
}
