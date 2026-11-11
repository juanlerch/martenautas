using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaSolarRA : MonoBehaviour {
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
                obj.info.SetActive(false);
        }


    }
    IEnumerator esperar()/*   StartCoroutine(esperarYJugar());*/
    {
        if (!esperando) {
            esperando = true;
            yield return new WaitForSeconds(3);
            nextLevel();
            esperando = false;
        }
    }


    public void nextLevel()
    {
        descripcion.SetActive(false);

        if (level+1 >= objetivos.Length)
        {
            //objetivosText.GetComponent<Text>().text = "¡Muy Bien!";
            //descripcion.GetComponent<Text>().text = "Misión cumplida, has identificado todo los astros.";
            //avance.GetComponent<Text>().text = "" + (objetivos.Length ) + "/" + objetivos.Length + ":";
            gPanelMuyBien.SetActive(true);
            gPanelObjectivos.SetActive(false);
            hecho.SetActive(true);
            descripcion.SetActive(true);
            ipuntos += 100; 
            puntos.GetComponent<LabsCounter>().setearValor(ipuntos);
        }
        else {
            level++;
            Debug.Log("level" + level);
            descripcion.GetComponent<Text>().text = objetivos[level].texto;
            avance.GetComponent<Text>().text = "" + (level +1)+ "/"+ objetivos.Length + ":";
            hecho.SetActive(false);
            descripcion.SetActive(true);
            descripcion.GetComponent<LabsAminateText>().animar();
            if(level>0) ipuntos += 100;

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
        if (selected == objetivos[level].target)
        {
            hecho.SetActive(true);
            StartCoroutine(esperar());
        }

        foreach (RaObjetivos obj in objetivos) {
            if (selected == obj.target) 
            {
                obj.info.SetActive(true);
            }
            else{
                obj.info.SetActive(false);
                obj.info.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public void continuar() {
        gPanelJuego.SetActive(false);
        gPanelCompleta.SetActive(true);
        LabsPanelCompleta completa = gPanelCompleta.GetComponent<LabsPanelCompleta>();

        completa.completar(Principal.nombreActSimRaSolar, 1,
                            ipuntos,
                            400);
    }
    // Update is called once per frame
    void Update () {
        if (selected == null) {
            nada();
        }
    }
}
