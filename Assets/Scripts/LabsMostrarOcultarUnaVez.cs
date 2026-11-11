using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LabsMostrarOcultarUnaVez : MonoBehaviour {
   // public static List<int> scenes = new List<int>();
    
    public GameObject[] mostrar;
    public GameObject[] ocultar;
	 
     
    void Start()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        if (!Principal.avance.avanceJugadorActual().mostrado.Contains("escena"+y))
        {
            mostrarOcultar();
            Principal.avance.avanceJugadorActual().mostrado.Add("escena" + y);
        }
    }



    public void mostrarOcultar()
    {
        foreach (GameObject m in mostrar)
        {
            m.SetActive(true);
        };
        foreach (GameObject o in ocultar)
        {
            o.SetActive(false);
        }

    }




}
