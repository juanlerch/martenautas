using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsMostrarOcultarPorSegundos: MonoBehaviour
{
    public float seconds = 0.0f;

    public GameObject[] mostrar;
    public GameObject[] ocultar;


    IEnumerator esperar()/*   StartCoroutine(esperarYJugar());*/
    {

        yield return new WaitForSeconds(seconds);
        mostrarOcultar();
    }

    void Start () {
        StartCoroutine(esperar());
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
