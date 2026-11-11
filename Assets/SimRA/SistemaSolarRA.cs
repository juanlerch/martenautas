using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaSolarRA : MonoBehaviour {
	public bool modo_simulador = false;
    public static GameObject selected;
    public static int level = -1;
    public int ipuntos = 0;
    public GameObject puntos;
    public GameObject objetivosText;
    public GameObject descripcion;
    public GameObject avance;
    public GameObject gPanelObjectivos;
    public GameObject gPanelCompleta;
    public GameObject gPanelJuego;
    public GameObject gPanelMuyBien;
    public GameObject hecho;
    public GameObject slider;
    public RaObjetivos[] objetivos;
	public GameObject sonido_ok;
	public int puntosOK=100;
	public int puntosER=-10;

	List<RaObjetivos> subObjetivosAcertados= new List<RaObjetivos>();
	//RaObjetivos objectivoActual;
    // Use this for initialization
    void Start () {
         level = -1;
        objetivosText.GetComponent<Text>().text = "OBJETIVO";
        nextLevel();
    }
    public void nada() {
        slider.SetActive(false);
        slider.GetComponent<LabsMostrarOcultarPorTiempo>().done = false;
        foreach (RaObjetivos obj in objetivos)
        {
			if(obj.info!=null) obj.info.SetActive(false);
        }


    }
    IEnumerator esperar()/*   StartCoroutine(esperarYJugar());*/
    {
        if (!esperando) {
            esperando = true;
            yield return new WaitForSeconds(3);

			esperando = false;
			nextLevel();
	
        }
    }

	public void error(){
		if (ipuntos + puntosER < 0)
			ipuntos = 0;
		else { 
			ipuntos += puntosER;
			puntos.GetComponent<LabsCounter> ().addValor (puntosER);

		}

	}

	IEnumerator esperarMultiple()/*   StartCoroutine(esperarYJugar());*/
	{
		if (!esperando) {
			esperando = true;
			yield return new WaitForSeconds(3);
			esperando = false;
			nextSubLevel ();

		}
	}

	public void nextSubLevel()
	{
		sonido_ok.GetComponent<AudioSource> ().Play ();
		descripcion.GetComponent<Text>().text = objetivos[level].texto + " (" + (subObjetivosAcertados.Count)+ "/"+ objetivos[level].multiples_min + ")";
		avance.GetComponent<Text>().text = "" + (level +1)+ "/"+ objetivos.Length + ":";
		descripcion.SetActive(true);

		if (subObjetivosAcertados.Count >= objetivos[level].multiples_min) //si era el ultimo sub objetivo 
		{   hecho.SetActive(true);
			descripcion.SetActive(true);
			subObjetivosAcertados.Clear ();
			StartCoroutine(esperar()); 
		}
	}



    public void nextLevel()
    {
        descripcion.SetActive(false);

        if (level+1 >= objetivos.Length) //si era el ultimo objetivo 
        {    gPanelMuyBien.SetActive(true);
            gPanelObjectivos.SetActive(false);
            hecho.SetActive(true);
            descripcion.SetActive(true);
			ipuntos += puntosOK; 
            puntos.GetComponent<LabsCounter>().setearValor(ipuntos);
        }
        else { 
            level++;
			if(Principal.avance.avanceJugadorActual().puntaje(Principal.nombreActSimRaSolar,1) <=0){
				//fix para que te tome como que la actividad esta realizada aunque solo ingrese 
				Principal.avance.actualizar(Principal.user, Principal.nombreActSimRaSolar, 1, 1,puntosOK*objetivos.Length); 
			}
            descripcion.GetComponent<Text>().text = objetivos[level].texto;
			if(objetivos[level].multiples_min>0) descripcion.GetComponent<Text>().text = objetivos[level].texto + " (" + (subObjetivosAcertados.Count)+ "/"+ objetivos[level].multiples_min + ")";
			else{
				descripcion.GetComponent<Text>().text = objetivos[level].texto;
			}
            avance.GetComponent<Text>().text = "" + (level +1)+ "/"+ objetivos.Length + ":";
            hecho.SetActive(false);
            descripcion.SetActive(true);
            descripcion.GetComponent<LabsAminateText>().animar();
			if(level>0) ipuntos += puntosOK;

            puntos.GetComponent<LabsCounter>().setearValor(ipuntos);
        }
    }

    bool esperando=false;


    public void scanear() {
        nada();
        slider.GetComponent<Slider>().value = 0;
        slider.SetActive(true);
        Debug.Log("level" + level);
        Debug.Log("selected " + selected);
		bool encontrado = false;
	//	bool visible = false;
		if (objetivos [level].multiples_min > 0) { //si es un objetivo multiple
			foreach(RaObjetivos sub  in objetivos[level].multiples){
				if (selected == sub.target){
	//				visible = true;
					//hecho.SetActive(true);
					if (!subObjetivosAcertados.Contains (sub)) {
						subObjetivosAcertados.Add (sub);
					}
					encontrado = true;
					StartCoroutine(esperarMultiple());
				} 
			}
		}
		else //si es un objetivo simple
		{	if (selected == objetivos[level].target)
	        {
	            hecho.SetActive(true);
				encontrado=true;
	//			visible = true;
	            StartCoroutine(esperar());
	        }

		} 

		if(!encontrado && selected!=null) error();
		/*dejar visible solo el identificado*/
		dejarVisibleSoloElIdentificado(objetivos);

	}

	public void dejarVisibleSoloElIdentificado(RaObjetivos[] objetivos1){

		if (objetivos == null || objetivos.Length == 0) return;

		foreach (RaObjetivos obj in objetivos1) {
			if (selected == obj.target) {
				if (obj.info!=null) obj.info.SetActive(true);
			}
			else{
				if (obj.info != null) {
					obj.info.SetActive(false);
					obj.info.transform.parent.gameObject.SetActive(false);
				}			
			}
			if (obj.multiples_min > 0) {
				dejarVisibleSoloElIdentificado (obj.multiples);
			}
		}
	}

    public void continuar() {
        gPanelJuego.SetActive(false);
        gPanelCompleta.SetActive(true);
        LabsPanelCompleta completa = gPanelCompleta.GetComponent<LabsPanelCompleta>();

        completa.completar(Principal.nombreActSimRaSolar, 1,
                            ipuntos,
			puntosOK*objetivos.Length); 
    }


    // Update is called once per frame
    void Update () {
        if (selected == null) {
            nada();
        }
    }
}
