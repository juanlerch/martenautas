using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsCounter : MonoBehaviour {
    int value=0;

    public string sufijo = " XP";
    double initialDelay=0;
    public double delay=0.1;
    bool  ison = false;
    GameObject sonido_ok;
    GameObject sonido_err;
    public void setearValor(int newValue) {
        ison = true;
        value = newValue;

        string svalorActual = GetComponent<Text>().text;
        svalorActual = svalorActual.Replace(sufijo, "");
        int valorActual = int.Parse(svalorActual);
        if (newValue >valorActual)
        {

            AudioSource audio = sonido_ok.GetComponent<AudioSource>();
            audio.Play();

        }
        else {
            if(value < valorActual) {

                AudioSource audio = sonido_err.GetComponent<AudioSource>();
                audio.Play();

            }
        }
    }

    // Use this for initialization
	void Start () {
        initialDelay = delay;
        sonido_ok = GameObject.Find("sonido_ok");
        sonido_err = GameObject.Find("sonido_err");
    }
	
	// Update is called once per frame
	void Update () {
        if (ison)
        {
           // Debug.Log("delay " + delay);
            //Debug.Log("value " + value);

            if (delay < 0)
            {
                delay = initialDelay;

                string svalorActual = GetComponent<Text>().text;
                svalorActual = svalorActual.Replace(sufijo, "");
               // Debug.Log("svalorActual  " + svalorActual);

                int valorActual = int.Parse(svalorActual);

               // Debug.Log("valorActual  " + valorActual);

                if (valorActual == value)
                {
                    ison = false;
                }
                else if (valorActual < value)
                {
                    if (value - valorActual > 10)
                    {
                        valorActual += (value - valorActual)/2;
                    }
                    else {
                        valorActual++;
                    }
                    if (valorActual >= 0)
                    {
                        GetComponent<Text>().text = "" + valorActual + sufijo;
                    }
                    else {
                        GetComponent<Text>().text = "" + 0 + sufijo;
                    }
                }
                else if (valorActual > value)
                {
                    if (valorActual - value > 10)
                    {
                        valorActual -= (valorActual-value ) / 2;
                    }
                    else {
                         valorActual--;//no le resto mas si llego a cero
                    }
                    if (valorActual >= 0)
                    {
                        GetComponent<Text>().text = "" + valorActual + sufijo;
                    }
                    else {
                        GetComponent<Text>().text = "" + 0 + sufijo;
                    }
                }
            }
            else {
                delay -= Time.deltaTime;
            };
        }

    }
}