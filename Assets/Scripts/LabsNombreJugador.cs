using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class LabsNombreJugador : MonoBehaviour {

  
       // public GameObject Gpuntos;
        // Use this for initialization
        void Start()
        {
            Principal p =new Principal();
            p.cargarModelo();

            Text text = GetComponent<Text>();
            text.text = "" + Principal.user;
        }

  
}
