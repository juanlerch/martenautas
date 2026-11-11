using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsCounter : MonoBehaviour {
    int value=0;

    public string sufijo = " XP";
    double initialDelay=0;
    public double delay=0.1;
    private bool  ison = false;
    GameObject sonido_ok;
    GameObject sonido_err;

	public int getValorActual(){
		string svalorActual = GetComponent<Text> ().text;
		if(sufijo!=null&&sufijo.Length>0) svalorActual = svalorActual.Replace (sufijo, "");
		int valorActual = int.Parse (svalorActual); 
		return valorActual;
	}
	public void addValor(int newValue,double delay) {
		this.delay = delay;
		addValor (newValue);
	}
	 
	public void addValor(int newValue) {
		ison = true;

		Principal.isInteractable = false;
		//value = newValue;

		string svalorActual = GetComponent<Text> ().text;
		if(sufijo!=null&&sufijo.Length>0) svalorActual = svalorActual.Replace (sufijo, "");
		int valorActual = int.Parse (svalorActual);
		if (newValue >= 0) {
			if (sonido_ok != null) {
			AudioSource audio = sonido_ok.GetComponent<AudioSource> ();
			audio.Play ();
				animacion (newValue);
			}
		} else {
			if (newValue < 0) {
				if (sonido_err != null) {
					AudioSource audio = sonido_err.GetComponent<AudioSource> ();
					audio.Play ();
					if(valorActual>0)animacion(newValue);
				}


			}
		}
		if (valorActual + newValue < 0)
			value = 0;
		else
			value= valorActual + newValue;
	}


    public void setearValor(int newValue) {
        ison = true;
		Principal.isInteractable = false; 
        value = newValue;

        string svalorActual = GetComponent<Text>().text;
		if (sufijo!=null&&sufijo.Length>0)svalorActual = svalorActual.Replace(sufijo, "");
        int valorActual = int.Parse(svalorActual);
        if (newValue >valorActual)
        {

            AudioSource audio = sonido_ok.GetComponent<AudioSource>();
            audio.Play();
			animacion (newValue-valorActual);
        }
        else {
            if(value < valorActual) {

                AudioSource audio = sonido_err.GetComponent<AudioSource>();
                audio.Play();
				animacion (newValue-valorActual);
            }
        }
    }

	void animacion(int newValue){
		string name = "Sumar";
		string namea = "xpsumar";
		string signo = "+"; 
		if (newValue < 0) {
			name = "Restar";
			namea = "xprestar";
			signo = ""; 
		} 

		Transform tanim = gameObject.transform.Find (name);

		if (tanim != null) {
			GameObject anim = tanim.gameObject;
			if (anim != null) {
				anim.GetComponent<Text>().text = signo + newValue;
				Animator a = anim.GetComponent<Animator> ();
				if (a != null) {
					a.enabled = true;	
					a.Play (namea,-1,0);
				}
			}
		}
	}

	public void inicializar(){
		initialDelay = delay;
		sonido_ok = GameObject.Find("sonido_ok");
		sonido_err = GameObject.Find("sonido_err");
		ison = false;
		Principal.isInteractable = true;
	}
    // Use this for initialization
	void Start () {
		ison = true;
		Principal.isInteractable = true;
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
				if (sufijo!=null&&sufijo.Length>0) svalorActual = svalorActual.Replace(sufijo, "");
               // Debug.Log("svalorActual  " + svalorActual);

                int valorActual = int.Parse(svalorActual);

               // Debug.Log("valorActual  " + valorActual);

                if (valorActual == value)
                {
                    ison = false;
					Principal.isInteractable = true;
					if (sufijo!=null&&sufijo.Length>0) GetComponent<Text>().text = "" + valorActual + sufijo;
					else GetComponent<Text>().text = "" + valorActual;
						
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
						if (sufijo!=null&&sufijo.Length>0) GetComponent<Text>().text = "" + valorActual + sufijo;
						else GetComponent<Text>().text = "" + valorActual ;
                    }
                    else {
						if (sufijo!=null&&sufijo.Length>0) GetComponent<Text>().text = "" + 0 + sufijo;
						else GetComponent<Text>().text = "" + 0 ;
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
						if (sufijo!=null&&sufijo.Length>0) GetComponent<Text>().text = "" + valorActual + sufijo;
						else GetComponent<Text>().text = "" + valorActual;
                    }
                    else {
						if (sufijo!=null&&sufijo.Length>0) GetComponent<Text>().text = "" + 0 + sufijo;
						else GetComponent<Text>().text = "" + 0;
                    }
                }
            }
            else {
                delay -= Time.deltaTime;
            };
        }

    }
}