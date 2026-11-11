using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsMostrarOcultar : MonoBehaviour
{
    public GameObject[] mostrar;
    public GameObject[] ocultar;
     
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
