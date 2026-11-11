using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsMostrarOcultarPorSegundos: MonoBehaviour
{
	public static bool inmediato = false;
    public float seconds = 0.0f;

    public GameObject[] mostrar;
    public GameObject[] ocultar;


    IEnumerator esperar()/*   StartCoroutine(esperarYJugar());*/
    {

        yield return new WaitForSeconds(seconds);
        mostrarOcultar();
    }

    void Start () {
		if (inmediato) {
			mostrarOcultar ();
			inmediato = false;		
		}
		else StartCoroutine(esperar());
    }

	void OnEnable() {
		if (inmediato) {
			mostrarOcultar ();
			inmediato = false;		
		}
		else StartCoroutine(esperar()); 
	}

    

	public void mostrarOcultar()
    {
        foreach (GameObject m in mostrar)
        {
            if (m!=null) m.SetActive(true);
        };
        foreach (GameObject o in ocultar)
        {
            if (o != null) o.SetActive(false);
        }

    }




}
