using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LabTaller01 : MonoBehaviour {

	public GameObject gJuego;
	public GameObject gMenu;
	public GameObject gCompleto;

	public GameObject gNumero;

	public GameObject gOpciones;

	public GameObject sampleOptions;

	public GameObject gPuntos;

	public GameObject gEsPrimo;

	public GameObject gEsCompuesto;

	private int[] orden; //orden de los indexes


	private List<GameObject> opciones = new List<GameObject> ();


	private List<GameObject> validar = new List<GameObject> ();
	private Hashtable valorEsperado  = new Hashtable();

    IEnumerator esperarYJugar()/*   StartCoroutine(esperarYJugar());*/
    {
        
        yield return new WaitForSeconds(2);
        jugar();
    }

    public static int index=0;

	public void pausar(){
		gMenu.SetActive (true);
		gJuego.SetActive (false);
	}

	public void iniciar(){
		gMenu.SetActive (false);
        gCompleto.SetActive(false);
        gJuego.SetActive (true);
        Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego = 0;
       // gPuntos.GetComponent<Text>().text = "" + Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego + " XP";
        index = 0;
		jugar ();
	}
    int signo = 0;//la primera vez tomo la posicion del item de referencia

    public void jugar(){
		//limpiar los del anterior;
		gEsPrimo.GetComponent<Toggle>().isOn=false;
		gEsCompuesto.GetComponent<Toggle> ().isOn = false;
         
		validar.Clear ();
		valorEsperado.Clear ();

		foreach(GameObject anterior in opciones){
			anterior.SetActive (false);
			Object.Destroy (anterior);
		}
		opciones.Clear();

		int[] validos   = Principal.juego.taller01NumeroPrimoNivel1.data[index].validos;

		int[] invalidos = Principal.juego.taller01NumeroPrimoNivel1.data[index].invalidos;

		Text text = gNumero.GetComponent<Text> ();

		text.text =""+ Principal.juego.taller01NumeroPrimoNivel1.data [index].numero;


		//gPuntos.GetComponent<Text> ().text = "" + Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego + " XP";

		int count = 1;

		orden= new int[validos.Length + invalidos.Length];

		/*ordenar los inde*/
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


        int pardesf = 0;        
		foreach (int value in orden)
		{
			GameObject o = Instantiate (sampleOptions);
			opciones.Add (o);
			o.SetActive (true); 
			o.transform.SetParent (sampleOptions.transform.parent);
			 
           /* RectTransform rt = o.GetComponent<RectTransform>();
            RectTransform rt1 = sampleOptions.GetComponent<RectTransform>();

            if (orden.Length % 2 == 0) //es par 
            {
                pardesf = 20;
            }
            else {
                pardesf = 0;
            }

            if (signo <0)
            {
                rt.position = rt1.position + new Vector3(pardesf + 40 + count * (signo * 40), 0, 0);
            }
            else {
                rt.position = rt1.position + new Vector3(pardesf + count * (signo * 40), 0, 0);
            }

            if (signo == 0) signo = 1;
            else signo = signo * -1;
			*/


            if (value < validos.Length) {
				o.GetComponent<Toggle> ().GetComponentInChildren<Text> ().text = "" + validos [value];
				valorEsperado.Add (o, true);
			} 
			else {
				o.GetComponent<Toggle> ().GetComponentInChildren<Text> ().text = "" + invalidos [value - validos.Length];
				valorEsperado.Add (o, false);
			}
			validar.Add(o);
			count++;
		}

		valorEsperado.Add (gEsPrimo, Principal.juego.taller01NumeroPrimoNivel1.data [index].esPrimo);
		valorEsperado.Add (gEsCompuesto, !Principal.juego.taller01NumeroPrimoNivel1.data [index].esPrimo);
		validar.Add (gEsPrimo);
		validar.Add (gEsCompuesto);

		sampleOptions.SetActive (false);
	}



	public void dale(){
		bool esCorrecta = true;

		foreach (GameObject go in validar) {
			bool ve = (bool)valorEsperado[go];
			bool va = go.GetComponent<Toggle> ().isOn;
			if (ve != va)
				esCorrecta = false;
		}	

		/*si es correcta pasa al siguiente*/
		if (esCorrecta) {
            Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego += Principal.juego.taller01NumeroPrimoNivel1.data[index].puntoOK;

            if (index < (Principal.juego.taller01NumeroPrimoNivel1.data.Length-1)) { //siguiente nivel
				index++;
                //jugar();
                StartCoroutine(esperarYJugar());
                
                //gPuntos.GetComponent<Text>().text = "" + Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego + " XP";
                // Text puntos = gPuntos.GetComponent<Text>();
                // LabsCounter counter = puntos.GetComponent<LabsCounter>();
                //counter.setearValor(Principal.juego.taller01NumeroPrimoNivel1.puntajeMaximo);


            }
            else {//termino
				index = 0;
				LabsPanelCompleta completa= gCompleto.GetComponent<LabsPanelCompleta> ();
				completa.completar (Principal.nombreActNumerosPrimos, 1,
                                    Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego,
									Principal.juego.taller01NumeroPrimoNivel1.puntajeMaximo);
			}

		} 
		/*si no es correcta resta el puntaje y queda en el actual*/
		else {
			Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego  += Principal.juego.taller01NumeroPrimoNivel1.data [index].puntoERR;
            if (Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego < 0)
            {
                Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego = 0;
            }
            //gPuntos.GetComponent<Text> ().text = "" + Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego+" XP";
        }
        Text puntos = gPuntos.GetComponent<Text>();
        LabsCounter counter = puntos.GetComponent<LabsCounter>();
        counter.setearValor(Principal.juego.taller01NumeroPrimoNivel1.puntajeJuego);
    }

	// Use this for initialization
	void Start () {
        Principal p = gameObject.GetComponent<Principal>();
        p.cargarModelo();
        Transform tpuntos = gMenu.transform.Find("MIDDLE Area/Contenedor MENU/Panel/Puntos");
        GameObject puntos = tpuntos.gameObject;
        Text text = puntos.GetComponent<Text>();
        text.text = "" + Principal.juego.taller01NumeroPrimoNivel1.puntajeMaximo ;
    }
	
	// Update is called once per frame
	void Update () {
        
        

    }
}
